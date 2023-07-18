using Property_rental_management_system.Models;
using Property_rental_management_system.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.IO;

namespace Property_rental_management_system.Controllers
{
    [Authorize]
    public class AdminController : Controller
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
        ///  Used to get admin home page
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminIndex()
        {

            if (Session["username"] == null || Session["password"] == null)
            {
                return RedirectToAction("Signin", "Signup");
            }
           
            return View();
        }

        /// <summary>
        /// Get all property details
        /// </summary>
        /// <returns></returns>
        public ActionResult PropertyDetails()
        {
            var propertyList = _property_DAL.GetAllProperties();
            if(propertyList.Count==0)
            {
                TempData["InfoMessage"] = "Currently property details not available in the database";
            }
            return View(propertyList);
        }

        /// <summary>
        /// Inserting property details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult insertProperty()
        {
            return View();
        }

        /// <summary>
        /// Inserting property details
        /// </summary>
        /// <param name="property"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult insertProperty(property property,HttpPostedFileBase file)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _property_DAL.InsertProperty(property,file);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Property details saved successfully....!";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save property details.";

                    }
                }
                return RedirectToAction("PropertyDetails");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);

                return View();
            }
        }

        /// <summary>
        /// GET: Getting user details
        /// </summary>
        /// <returns></returns>
        public ActionResult UserDetails()
        {
            homepageRepository signupRepo = new homepageRepository();
            ModelState.Clear();
            return View(signupRepo.GetDetails());
        }

        /// <summary>
        /// GET: Getting property details for updating
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult updatePropertyDetails(int id)
        {
            var property = _property_DAL.GetPropertiesById(id).FirstOrDefault();
            if(property == null)
            {
                TempData["InfoMessage"] = "Property not available with this ID";
                return RedirectToAction("PropertyDetails");
            }
            return View(property);
        }

        /// <summary>
        /// POST: Updating property details
        /// </summary>
        /// <param name="property"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost,ActionName("updatePropertyDetails")]
        public ActionResult updatePropertyDetails(property property, HttpPostedFileBase file)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bool IsUpdated = _property_DAL.UpdateProperty(property, file);

                    if(IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Property details updated successfully....!";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update property details.";

                    }
                }

                return RedirectToAction("PropertyDetails");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);

                return View();
            }
        }


        /// <summary>
        /// GET: Getting property details to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)

        {
            try
            {
                var property = _property_DAL.GetPropertiesById(id).FirstOrDefault();
                if (property == null)
                {
                    TempData["InfoMessage"] = "Property not available with this ID";
                    return RedirectToAction("PropertyDetails");

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
        /// POST: Deleting property details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _property_DAL.DeleteProperty(id);

                if(result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["ErrorMessage"] = result;

                }
                return RedirectToAction("PropertyDetails");
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
                    return RedirectToAction("UserDetails");

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
        /// post user delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost,ActionName("UserDelete")]

        public ActionResult DeleteUser(int id)
        {
            try
            {
                homepageRepository signupRepo = new homepageRepository();

                string result = signupRepo.DeleteUser(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["ErrorMessage"] = result;

                }
                return RedirectToAction("UserDetails", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);
                return View();
            }
        }

        /// <summary>
        /// Add new admin
        /// </summary>
        /// <returns></returns>

        [HttpGet]
       public ActionResult AddNewAdmin()
        {
            return View();
        }

        /// <summary>
        /// add admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>

        [HttpPost]

        public ActionResult AddNewAdmin(Admin admin)
        {
            
            homepageRepository signupRepo = new homepageRepository();

            try
            {
                if (ModelState.IsValid)
                {
                    if (signupRepo.AddAdmin(admin))
                    {
                        TempData["SuccessMessage"] = "Admin details saved  successfully....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save message details.";

                    }
                }
                return RedirectToAction("AdminIndex");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000 && ex.Class == 16) 
                {
                    TempData["ErrorMessage"] = ex.Message;
                    LogError(logFilePath, ex);
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while saving admin details.";
                    LogError(logFilePath, ex);

                }

                return View();
            }

        }

        /// <summary>
        /// get all messages
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageDetails()
        {
            homepageRepository repository = new homepageRepository();
            ModelState.Clear();

            return View(repository.GetMessages());
        }

        /// <summary>
        /// get delete message details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteMessages(int id)
        {
            try
            {
                homepageRepository repository = new homepageRepository();

                var messages = repository.GetMessagesById(id).FirstOrDefault();

                if (messages == null)
                {
                    TempData["InfoMessage"] = "Message not available with this ID";
                    return RedirectToAction("MessageDetails");

                }
                return View(messages);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);
                return View();
            }

        }

        /// <summary>
        /// post message delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost, ActionName("DeleteMessages")]

        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                homepageRepository repository = new homepageRepository();

                string result = repository.DeleteMessages(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["ErrorMessage"] = result;

                }
                return RedirectToAction("MessageDetails", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                LogError(logFilePath, ex);
                return View();
            }
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public ActionResult ChangeAdminPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeAdminPassword(passwordChange passwordChange)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    homepageRepository repository = new homepageRepository();

                    string enteredPassword = Session["Password"] as string;//retrieving entered password from session variable

                    string existingPassword = repository.GetExistingAdminPassword(GetSigninUsername());

                    string oldPassword = passwordChange.OldPassword;
                    if (enteredPassword == oldPassword)
                    {
                        string newPassword = passwordChange.NewPassword;//generating new password

                        repository.UpdateAdminPassword(GetSigninUsername(), newPassword);

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
