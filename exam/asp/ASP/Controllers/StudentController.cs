﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.DataAbstractionLayer;

namespace ASP.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;

        private readonly DAL dal;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;

            dal = new DAL();
        }

        [HttpGet]
        public float GetGrade()
        {
            string username = Request.Query["username"];

            return dal.GetGrade(username);
        }
    }
}
