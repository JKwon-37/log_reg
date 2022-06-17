using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LogReg.Models;

public class UsersController : Controller
{
private LogRegContext _context;

public UsersController(LogRegContext context)
{
    _context = context;
}

[HttpGet("")]
public IActionResult LoginReg()
{
    return View("LoginReg");
}

[HttpGet("success")]
public IActionResult Success()
{
    if(HttpContext.Session.GetInt32("UUID") == null)
    {
        return LoginReg();
    }
    return View("Success");
}

[HttpPost("register")]
public IActionResult Register(User newUser)
{
    if(ModelState.IsValid)
    {
        if(_context.Users.Any(u => u.Email == newUser.Email))
        {
            ModelState.AddModelError("Email", " is taken");
        }
    }

    if(ModelState.IsValid == false)
    {
        return LoginReg();
    }

    PasswordHasher<User> Hasher = new PasswordHasher<User>();
    newUser.Pw = Hasher.HashPassword(newUser, newUser.Pw);
    _context.Users.Add(newUser);
    _context.SaveChanges();

    HttpContext.Session.SetInt32("UUID", newUser.UserId);

    return RedirectToAction("LoginReg");
}

[HttpPost("login")]
public IActionResult Login(LogUser loginUser)
{
    if (ModelState.IsValid == false)
    {
        return LoginReg();
    }

    User? dbUser = _context.Users.FirstOrDefault(u => u.Email == loginUser.LogEmail);

    if (dbUser == null)
    {
        ModelState.AddModelError("LogEmail", " and Password do not match!");
        return LoginReg();
    }

    PasswordHasher<LogUser> Hasher = new PasswordHasher<LogUser>();
    PasswordVerificationResult pwCompare = Hasher.VerifyHashedPassword(loginUser, dbUser.Pw, loginUser.LogPw);

    if(pwCompare == 0)
    {
        ModelState.AddModelError("LogEmail", " and Password do not match!");
        return LoginReg();
    }

    HttpContext.Session.SetInt32("UUID", dbUser.UserId);
    return RedirectToAction("Success", "Users");
}

[HttpPost("logout")]
public IActionResult Logout()
{
    HttpContext.Session.Clear();
    return RedirectToAction("LoginReg");
}
}