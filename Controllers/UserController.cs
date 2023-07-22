using Property_rental_management_system.Models;
using Property_rental_management_system.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Property_rental_management_system.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        property_DAL _property_DAL = new property_DAL();

        private void LogError(string logFilePath, Exception ex)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine("Error Date: " + DateTime.Now.ToString());
                writer.WriteLine("Message: " + ex.Message);
                writer.WriteLine("Stack Trace: " + ex.StackTrace);
                writer.WriteLine("--------------------------------------------------");
            }
        }
        string logFilePath = "C:\\Users\\HP\\source\\repos\\Property rental management system\\ErrorLog.txt";

        private string GetSigninUsername()
        {
            string SigninUsername = Session["Username"] as string;
            return SigninUsername;
        }

        /// <summary>
        /// Used to get user home page
        /// </summary>
        /// <returns></returns>
        public ActionResult UserIndex()
        {
                if (Session["username"] == null || Session["password"] == null)
                {
                    return RedirectToAction("Signin", "Signup");
                }

                var propertyList = _property_DAL.GetAllProperties();
                if (propertyList.Count == 0)
                {
                    TempData["InfoMessage"] = "Currently property details not available in the database";
                }
                Session["propertyList"] = propertyList;
                return View(propertyList);
            
        }


        /// <summary>
        /// get user dettails by username
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Userprofile()
        {
            try
            {
                string username = Session["Username"] as string;

                if (string.IsNullOrEmpty(username))
                {
                    TempData["InfoMessage"] = "Username not found in the session.";
                    return RedirectToAction("UserIndex");
                }
                homepageRepository repository = new homepageRepository();

                var signupList = repository.GetDetailsByusername(username);

                if (signupList.Count == 0)
                {
                    TempData["InfoMessage"] = "Currently, user details are not available in the database.";
                }

                return View(signupList);
            }
            catch (Exception ex)
            {
                LogError(logFilePath, ex);
                throw;
            }
        }

        /// <summary>
        /// GET: Property details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public ActionResult PropertyDetails(int id)
        {
            try
            {
                var property = _property_DAL.GetPropertiesById(id).FirstOrDefault();
                if (property == null)
                {
                    TempData["InfoMessage"] = "Currently, property details are not available in the database.";
                    return RedirectToAction("UserIndex");

                }
                return View(property);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);
                return View();
            }
        }

        /// <summary>
        /// GET: Getting user details for updating
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult updateUserDetails(int Id)
        {
            homepageRepository repository = new homepageRepository();

            var userList = repository.GetDetailsById(Id).FirstOrDefault();
            if (userList == null)
            {
                TempData["InfoMessage"] = "User not available with this ID";
                return RedirectToAction("Userprofile");
            }
            return View(userList);
        }

        /// <summary>
        ///  POST: Updating user details
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>

        [HttpPost, ActionName("updateUserDetails")]
        public ActionResult updateDetails(Signup profile)
       {
            bool IsUpdated=false;
            homepageRepository repository = new homepageRepository();
            try
            {              
                    IsUpdated =repository.UpdateUserDetails(profile);

                    if (IsUpdated)
                    {
                        TempData["UpdateSuccessMessage"] = "Profile details updated successfully....!";

                    }
                    else
                    {
                        TempData["UpdateErrorMessage"] = "Unable to update profile details.";

                    }
                

                return RedirectToAction("Userprofile");
            }
            catch (SqlException ex)
            {
                TempData["UpdateErrorMessage"] = ex.Message; // Store the SQL error message
                LogError(logFilePath, ex);

                return View();

            }
            catch (Exception ex)
            {
                TempData["UpdateErrorMessage"] = "An error occurred: " + ex.Message;
                LogError(logFilePath, ex);

                return View();
            }
        }

        /// <summary>
        /// get delete user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserDelete(int id)
        {
            try
            {
                homepageRepository signupRepo = new homepageRepository();

                var user = signupRepo.GetDetailsById(id).FirstOrDefault();
                if (user == null)
                {
                    TempData["InfoMessage"] = "User not  available with this ID";
                    return RedirectToAction("Userprofile");

                }

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);
                return View();
            }
        }

        /// <summary>
        /// Delete user details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("UserDelete")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                homepageRepository signupRepo = new homepageRepository();

                string result = signupRepo.DeleteUser(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessUserMessage"] = result;

                }
                else
                {
                    TempData["ErrorUserMessage"] = result;

                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);
                return View();
            }
        }

        /// <summary>
        /// sign out
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// password changing
        /// </summary>
        /// <param name="passwordChange"></param>
        /// <returns></returns>

        [HttpPost]

        public ActionResult ChangePassword(passwordChange passwordChange)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    homepageRepository repository = new homepageRepository();

                    string enteredPassword = Session["Password"] as string;//retrieving entered password from session variable

                    string existingPassword = repository.GetExistingPassword(GetSigninUsername());

                    string oldPassword = passwordChange.OldPassword;
                    if (enteredPassword == oldPassword)
                    {
                        string newPassword = passwordChange.NewPassword;//generating new password

                        repository.UpdatePassword(GetSigninUsername(), newPassword);
                        TempData["PasswordChangeMessage"] = "Password changed successfully.Please sign in again..!";

                        return RedirectToAction("Signin", "Signup");
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Password incorrect");
                    }
                }

                return View(passwordChange);
            }
            catch (Exception ex)
            {
                LogError(logFilePath, ex);
                throw;
            }
        }


       

    }
}


