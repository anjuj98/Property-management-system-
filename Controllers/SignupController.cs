using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Property_rental_management_system.Models;
using Property_rental_management_system.Repository;
using System.IO;
using System.Web.Security;

namespace Property_rental_management_system.Controllers
{
    public class SignupController : Controller
    {
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

        /// <summary>
        /// Sign up
        /// </summary>
        /// <returns></returns>
        [HttpGet]   
        public ActionResult AddDetails()
        {
            return View();
        }

        /// <summary>
        ///  POST: Signup/Create
        /// </summary>
        /// <param name="signup"></param>
        /// <returns></returns>
        [HttpPost]
         public ActionResult AddDetails(Signup signup)
         {
             try
             {
                 // TODO: Add insert logic here
                 if (ModelState.IsValid)
                 {
                     homepageRepository signupRepo = new homepageRepository();
                     if (signupRepo.AddDetails(signup))
                     {
                        TempData["SignupSuccessMessage"] = "Signup  successfull.Now Sign in please....! ";
                    }
                    else
                    {
                        TempData["SignupErrorMessage"] = "Unable to save user details.";

                    }
                }

                 return RedirectToAction("Signin");
             }
            catch (SqlException ex)
            {
                TempData["SignupErrorMessage"] = ex.Message; // Store the SQL error message
                LogError(logFilePath, ex);

                return View();

            }
            catch (Exception ex)
            {
                TempData["SignupErrorMessage"] = "An error occurred: " + ex.Message;
                LogError(logFilePath, ex);

                return View();
            }
        }


        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }
      
        /// <summary>
        /// Sign in page
        /// </summary>
        /// <param name="signin"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Signin(Signin signin)
        {
            try
            {
                homepageRepository repository = new homepageRepository();

                bool isValidUser = repository.Signin(signin);
                bool isValidAdmin = repository.SigninAdmin(signin);


                if (isValidUser)
                {
                    Session["username"] = signin.Username.ToString();
                    Session["password"] = signin.Password.ToString();
                    FormsAuthentication.SetAuthCookie(signin.Username, false);
                    return RedirectToAction("UserIndex", "User");
                }
                else if (isValidAdmin)
                {
                    Session["username"] = signin.Username.ToString();
                    Session["password"] = signin.Password.ToString();
                    FormsAuthentication.SetAuthCookie(signin.Username, false);
                    return RedirectToAction("AdminIndex", "Admin");
                }
                else
                {
                    ViewData["Message"] = "Invalid username or password";
                }

                return View(signin);
            }
            catch (Exception ex)
            {
                LogError(logFilePath, ex);
                throw;
            }
        }


        
    }
}
