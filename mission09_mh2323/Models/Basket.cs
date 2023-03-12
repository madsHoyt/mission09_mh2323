using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mission09_mh2323.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();
        // to be able to add an item to the basket 
        public virtual void AddItem(Book book, int qty)
        {
            // go find the  associated with that ID passed in
            BasketLineItem line = Items.Where(bk => bk.Book.BookId == book.BookId)
                .FirstOrDefault();

            // if there's not one already associated with it,
            if (line == null)
            {
                // add a new item with the information we were passed 
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                // add the new quantity to the last total quantity
                line.Quantity += qty;
            }
        }
        public virtual void RemoveItem(Book book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }
        public virtual void ClearBasket()
        {
            Items.Clear();
        }
        public double CalculateTotal()
        {
            // Add the purchase up
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);
            return sum;
        }
    }
    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
