﻿using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
