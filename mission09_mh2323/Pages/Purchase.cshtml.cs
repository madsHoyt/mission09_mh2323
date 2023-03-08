using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mission09_mh2323.Infrastructure;
using mission09_mh2323.Models;

namespace mission09_mh2323.Pages
{
    public class PurchaseModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        // constructor requires an IBookstoreRepository
        public PurchaseModel(IBookstoreRepository temp)
        {
            repo = temp;
        }

        // make an instance of the Basket
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }
        // when clicking the add to cart button, it will actually hit this post before the above get
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            // get info for bookid passed in from the add cart buttonbutton
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // if session already exists use it else make a new basket
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();

            //Add item to the basket
            basket.AddItem(b, 1);

            // set the JSON file based on the new basket
            HttpContext.Session.SetJson("basket", basket);

            // this will redirect us to the cshtml page associated with this model
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
