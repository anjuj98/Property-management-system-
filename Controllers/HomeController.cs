using Property_rental_management_system.Models;
using Property_rental_management_system.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Property_rental_management_system.Controllers
{
    public class HomeController : Controller
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
        /// Main home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// About us page
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        /// <summary>
        /// Contact us page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// contact message insertion
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Contact(Messages messages)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    homepageRepository signupRepo = new homepageRepository();
                    if (signupRepo.ContactMessages(messages))
                    {
                        TempData["SuccessMessage"] = "Signup  successfull....!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save user details.";

                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogError(logFilePath, ex);
                return View();
            }
        }

        
    }
}