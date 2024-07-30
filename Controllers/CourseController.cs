using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS_SASS.Models;
using LMS_SASS.Databases;
using System.Data;
using LMS_SASS.Interfaces;

namespace LMS_SASS.Controllers;

public class CourseController(DatabaseContext databaseContext) : BaseController(databaseContext) {

    [HttpGet]
    [Route("/course/{Id:int}")]
    public IActionResult ViewCourse(int Id) {
        var course = DB.Courses.Find(Id);

        if (course == null)
            return NotFound();

        return View("View", course);
    }

    [HttpGet]
    [Route("/course/create")]
    public IActionResult CreateCourseView() {
        ViewBag.Action = "create";
        return View("Create");
    }

    [HttpPost]
    [Route("/course/create")]
    public IActionResult CreateCourse(CourseModel course) {
        if (!ModelState.IsValid)
            return CreateCourseView();

        if (course.StartDate > course.EndDate) {
            ModelState.AddModelError("StartDate", "Ngày Bắt Đầu không thể sau Ngày Kết Thúc");
            ModelState.AddModelError("EndDate", "Ngày Bắt Đầu không thể sau Ngày Kết Thúc");
            return CreateCourseView();
        }

        var courseExist = DB.Courses.Any((c) => c.Code == course.Code);

        if (courseExist) {
            ModelState.AddModelError("Code", "Đã có khóa học với mã này rồi!");
            return CreateCourseView();
        }

        course.InviteCode = Utils.RandomString(7);
        DB.Courses.Add(course);
        DB.SaveChanges();

        return RedirectToAction("ViewCourse", new { Id = course.Id });
    }

    [HttpGet]
    [Route("/course/{Id:int}/edit")]
    public IActionResult EditCourseView(int Id) {
        var course = DB.Courses.Find(Id);

        if (course == null)
            return NotFound();

        ViewBag.Action = "edit";
        return View("Edit", course);
    }

    [HttpPost]
    [Route("/course/{Id:int}/edit")]
    public IActionResult EditCourse(CourseModel newCourse, int Id) {
        if (!ModelState.IsValid)
            return CreateCourseView();

        if (newCourse.StartDate > newCourse.EndDate) {
            ModelState.AddModelError("StartDate", "Ngày Bắt Đầu không thể sau Ngày Kết Thúc");
            ModelState.AddModelError("EndDate", "Ngày Bắt Đầu không thể sau Ngày Kết Thúc");
            return CreateCourseView();
        }

        DB.SaveChanges();

        return RedirectToAction("ViewCourse", new { Id = newCourse.Id });
    }

    [HttpGet]
    [Route("/course/{Id:int}/peoples")]
    public IActionResult Peoples(int Id) {
        var course = DB.Courses.Find(Id);

        if (course == null)
            return NotFound();

        var data = new CoursePeoplesModel {
            Course = course,
            CourseUsers = DB.CourseUsers
                .Where((cu) => cu.CourseId == course.Id)
                .ToList()
        };

        return View("Peoples", data);
    }

    [HttpGet]
    [Route("/course/{Id:int}/unenrolledUsers")]
    public IActionResult UnenrolledUsers(int Id) {
        var course = DB.Courses.Find(Id);

        if (course == null)
            return NotFound();

        var enrolledUserIds = DB.CourseUsers
            .Where(cu => cu.CourseId == course.Id)
            .Select(cu => cu.UserId)
            .ToList();

        var unenrolledUsers = DB.Users
            .Where(u => !enrolledUserIds.Contains(u.Id))
            .ToList();

        return Json(unenrolledUsers);
    }

    [HttpPost]
    [Route("/course/{Id:int}/enroll")]
    public IActionResult EnrollUser(int Id, EnrollUserModel enroll) {
        var course = DB.Courses.Find(Id);
        var user = DB.Users.Find(enroll.UserId);

        if (course == null || user == null)
            return NotFound();

        var item = new CourseUserModel {
            UserId = user.Id,
            CourseId = course.Id,
            Role = enroll.Role
        };

        DB.CourseUsers.Add(item);
        DB.SaveChanges();

        return Json(new { Message = "OK" });
    }

    [HttpGet]
    [Route("/course/selfEnroll")]
    public IActionResult SelfEnrollCourseView() {
        return View("SelfEnroll");
    }
}
