using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebLab9.DataAbstractionLayer;
using WebLab9.Models;

namespace WebLab9.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly ILogger<ProfessorController> _logger;

        private readonly DAL dal;

        public ProfessorController(ILogger<ProfessorController> logger)
        {
            _logger = logger;

            dal = new DAL();
        }

        [HttpGet]
        public List<int> GetGroups()
        {
            string username = Request.Query["username"];

            return dal.GetGroups(username);
        }

        [HttpGet]
        public string GetStudentsByGroup()
        {
            int groupId = Int32.Parse(Request.Query["groupId"]);

            List<Student> students = dal.GetStudentsByGroup(groupId);

            int currentIndex = 0;

            string result = "<div id='page-1' class='page-content' style='display: block;'>";

            foreach (Student student in students)
            {
                if (currentIndex != 0 && currentIndex % 4 == 0) {
                    result += "</div>" +
                        "<div id='page-" + (currentIndex / 4 + 1) + "' class='page-content'>";
                }

                result += "<div id='student-" + student.Id + "' class='card'>" +
                    "<img class='avatar' src='assets/img_avatar.png' alt='Avatar'>" +
                    "<div class='container'>" +
                    "<h3><b>ID: " + student.Id + " - " + student.Username + "</b></h3>" +
                    "<p>Group: " + student.GroupId + "</p>" +
                    "<label for='grade'>Grade: </label>" +
                    "<input type='number' id='grade' value='" + student.Grade + "'>" +
                    "</div>" +
                    "</div>";

                currentIndex += 1;
            }

            return result;
        }

        [HttpPost]
        public string AssignGrade()
        {
            int studentId = Int32.Parse(Request.Form["studentId"]);
            float newGrade = float.Parse(Request.Form["newGrade"]);

            if (dal.AssignGrade(studentId, newGrade)) {
                return "Grade assigned successfully!";
            }

            return "There was an error while assigning the grade.";
        }
    }
}
