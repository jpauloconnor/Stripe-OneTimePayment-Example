using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;


namespace ElevenNote.Web.Controllers
{
    public class OneTimePurchaseController : Controller
    {
        // GET: OneTimePurchase
        public ActionResult Index()
        {
            string stripePublishableKey = ConfigurationManager.AppSettings["stripePublishableKey"];
            var model = new OneTimePurchaseViewModel { StripePublishableKey = stripePublishableKey };
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Charge(ChargeViewModel chargeViewModel)
        {
            Debug.WriteLine(chargeViewModel.StripeEmail);
            Debug.WriteLine(chargeViewModel.StripeToken);

            var token = chargeViewModel.StripeToken; // Using ASP.NET MVC

            var charges = new StripeChargeService();
            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = 1000,
                Currency = "usd",
                Description = "Example charge",
                SourceTokenOrExistingSourceId = token
            });

            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            return View();
        }
    }
}