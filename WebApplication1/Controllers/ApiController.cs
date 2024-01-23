﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.EFModels;

namespace WebApplication1.Controllers
{
    public class ApiController : Controller
    {
        public readonly MyDBContext _dbContext;

        public ApiController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult Cities()
        {
            var cities = _dbContext.Addresses.Select(x => x.City).Distinct();
            return Json(cities);
        }
    }
}
