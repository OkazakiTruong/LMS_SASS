using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS_SASS.Models;
using LMS_SASS.Databases;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Controllers;

public class AdminController : BaseController {

    private readonly DatabaseContext _context;

    public AdminController(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("/admin/accounts")]
    public IActionResult Accounts() {
        var userId = HttpContext.Session.GetInt32("UserId");
        ViewBag.UserId = userId.Value;
        var results = _context.Users.ToList();
        return View( results);
    }
    [HttpGet]
    [Route("/admin/accounts/add-an-account")]
    public IActionResult AddAnAccount()
    {
        // Truyền danh sách các vai trò cho view
        ViewBag.Roles = new List<string> { UserModel.ROLE_USER, UserModel.ROLE_TEACHER, UserModel.ROLE_ADMIN };
        return View();
    }
    [HttpPost]
    [Route("/admin/accounts/add-an-account")]
    public IActionResult AddAnAccount(UserModel user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = DB.Users
                .Where((u) => u.Username == user.Username)
                .FirstOrDefault();

            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username này đã tồn tại trong hệ thống!");
                return View("Register", user); // Trả về view "Register" với model để hiển thị lỗi
            }

            user.Created = DateTime.Now;

            DB.Users.Add(user);
            DB.SaveChanges();

       
            return RedirectToAction("Accounts"); // Đảm bảo phương thức Index tồn tại trong HomeController
        }

        return View("AddAnAccount", user); 
    }
    [HttpGet]
    [Route("/admin/accounts/edit-an-account/{id}")]
    public IActionResult EditAnAccount(int id)
    {
        var user = DB.Users.Where((u) => u.Id == id).FirstOrDefault();
        if (user != null)
        {

        // Truyền danh sách các vai trò cho view
        ViewBag.Roles = new List<string> { UserModel.ROLE_USER, UserModel.ROLE_TEACHER, UserModel.ROLE_ADMIN };
        return View(user);
        }
        return NotFound();
    }
    [HttpPost]
    [Route("/admin/accounts/edit-an-account/{id}")]
    public IActionResult EditAnAccount(UserModel user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = DB.Users
                .Where((u) => u.Username == user.Username)
                .FirstOrDefault();

            if (existingUser == null)
            {
                ModelState.AddModelError("Username", "Tài khoản không tồn tại");
                return View("Register", user); // Trả về view "Register" với model để hiển thị lỗi
            }

            // Cập nhật thông tin người dùng
            existingUser.Username = user.Username;
            existingUser.Name = user.Name;
            existingUser.DOB = user.DOB;
            existingUser.Phone = user.Phone;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.Role = user.Role;

            DB.SaveChanges();

            return RedirectToAction("Accounts"); // Đảm bảo phương thức Index tồn tại trong HomeController
        }

        return RedirectToAction("Index");
    }
    [HttpDelete("/admin/accounts/{id}")]
    public IActionResult Delete(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet("/admin/getdata")]
    public async Task<IActionResult> GetData()
    {
        var results = await _context.Users.ToListAsync();
        return new JsonResult(new { Data = results, TotalItems = results.Count });
    }
    [HttpPost("/admin/create")]
    public async Task<IActionResult> Create(UserModel model)
    {
        model.Created = DateTime.Now;
        _context.Users.Add(model);
        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Ok(new { success = false });
        }
    }
    [HttpGet("/admin/search")]
    public IActionResult Search(string query)
    {
        var users = _context.Users.Where(u =>
            u.Username.Contains(query) ||
            u.Name.Contains(query) ||
            u.Email.Contains(query) ||
            u.Address.Contains(query) ||
            u.Role.Contains(query)
        ).ToList();
        return PartialView("_UserTablePartial", users);
    }
    [HttpGet]
  
    [Route("/admin/courses")]
    public IActionResult Courses() {
        return View("Courses");
    }
}
