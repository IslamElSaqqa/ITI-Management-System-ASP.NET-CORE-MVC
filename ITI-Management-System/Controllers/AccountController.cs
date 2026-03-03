using BCrypt.Net;
using ITI_Management_System.ViewModels;
using ITIEntities;
using ITIEntities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly ITIContext _context;

    public AccountController(ITIContext context)
    {
        _context = context;
    }

   

    [HttpGet]
public IActionResult Register()
{
    // Get departments from database
    var departments = _context.Departments
        .Select(d => new { d.DeptId, d.DeptName })
        .ToList();

    // Pass to view using ViewBag
    ViewBag.Departments = new SelectList(departments, "DeptId", "DeptName");

    return View(new RegisterVM());
}

[HttpPost]
    public IActionResult Register(RegisterVM model)
    {
        // Repopulate the dropdown
        var departments = _context.Departments
            .Select(d => new { d.DeptId, d.DeptName })
            .ToList();
        ViewBag.Departments = new SelectList(departments, "DeptId", "DeptName", model.DeptId);

        if (!ModelState.IsValid) 
            return View(model);

        
        if (_context.Users.Any(u =>
            u.Username.ToLower() == model.Username.ToLower()))
        {
            ModelState.AddModelError("Username",
                "Username already exists"); 
            return View(model);
        }

        var student = new Student
        {
            Name = model.Name,
            Age = model.Age,
            Deptno = model.DeptId
        };

        _context.Students.Add(student);
        _context.SaveChanges();

        var user = new User
        {
            Username = model.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = "Student",
            StudentId = student.ID
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }


    // Login on HTTP GET to show the login form
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _context.Users
            .FirstOrDefault(u =>
                u.Username.ToLower() == model.Username.ToLower());

        if (user == null || !BCrypt.Net.BCrypt
            .Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("UserId", user.ID.ToString()),
            new Claim("StudentId", user.StudentId?.ToString() ?? "")
        };

        // Add StudentId only if exists
        if (user.StudentId.HasValue)
        {
            claims.Add(new Claim("StudentId",
                user.StudentId.Value.ToString()));
        }

        var identity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return RedirectToAction("Index", "Home");
    }

   
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }
}