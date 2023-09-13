using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviation.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registration(Models.DisplayRecords RegisterUser)
        {
            // Retrieve user input from the Registration form
            string name = Request.Form["FirstName"].ToString();
            string surname = Request.Form["surname"].ToString();
            string username = Request.Form["username"].ToString();
            string password = Request.Form["password"].ToString();

            //Checking if the user is registered in the database
            if (RegisterUser.Register(name, surname, username, password))
            {
                return RedirectToAction("Login", "Login"); 
            }
            else
            {
                return View("Register");  // Return to the Registration view in case of unsuccessful login attempt
            }

        }
    }
}
