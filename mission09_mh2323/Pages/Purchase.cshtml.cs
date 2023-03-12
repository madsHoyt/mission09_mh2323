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
        public PurchaseModel(IBookstoreRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        // make an instance of the Basket
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        // when clicking the add to cart button, it will actually hit this post before the above get
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            // get info for bookid passed in from the add cart buttonbutton
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //Add item to the basket
            basket.AddItem(b, 1);

            // this will redirect us to the cshtml page associated with this model
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
