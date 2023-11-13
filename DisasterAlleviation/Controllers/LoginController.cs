using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DisasterAlleviation.Controllers
{
    public class LoginController : Controller
    {
        DisplayRecords d = new DisplayRecords();
        // GET request for displaying the login view
        public IActionResult Login()
        {
            int donationCount = d.GetDonationCountForUser();
            ViewBag.DonationCount = donationCount; 

            decimal sumOfMonetaryDonations = d.GetSumOfMonetaryDonationsForUser();
            ViewBag.SumOfMonetaryDonations = sumOfMonetaryDonations;

            return View(d.AllocationInformation());
        }

        [HttpPost] // POST request for processing user login data
        public IActionResult Login(Models.DisplayRecords verify)
        {
            // Retrieve user input from the login form
            string user = Request.Form["username"].ToString();
            string password = Request.Form["password"].ToString();

            if (verify.verified(user, password)) // Check if the provided username and password are verified
            {
                return RedirectToAction("Index", "Home");  // Redirect to the Index action of the Home controller upon successful login
            }
            else
            {
               return View("Login"); // Return to the Login view in case of unsuccessful login attempt
            }
        }
    }

   
}
