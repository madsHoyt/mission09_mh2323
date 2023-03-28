using Microsoft.AspNetCore.Mvc;
using mission09_mh2323.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mission09_mh2323.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchaseRepository repo { get; set; }
        private Basket basket { get; set; }

        public PurchaseController(IPurchaseRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        //Checkout Get
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }

        //Checkout Post
        [HttpPost]
        public IActionResult Checkout(Purchase purchase)
        {
            // if basket is empty
            if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your basket is empty.");
                
            }


            if (ModelState.IsValid)
            {
                // from the purchase object passed in, go add lines from the items 
                purchase.Lines = basket.Items.ToArray();
                repo.SavePurchase(purchase);
                basket.ClearBasket();
                return RedirectToPage("/PurchaseCompleted");
            }
            else
            {
                return View();
            }
        }
    }
}
