using Microsoft.AspNetCore.Mvc;
using mission09_mh2323.Models;
using mission09_mh2323.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mission09_mh2323.Controllers
{
    public class HomeController : Controller
    {
        //Repo
        private IBookstoreRepository repo;
        public HomeController(IBookstoreRepository temp)
        {
            repo = temp;
        }
        //Index Action
        public IActionResult Index(int pageNum = 1)
        {
            //How many pages you want
            int pageSize = 10;
            var x = new BooksViewModel
            {
                Books = repo.Books
                .OrderBy(p => p.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumProjects = repo.Books.Count(),
                    ProjectsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            return View(x);
        }

    }
}
