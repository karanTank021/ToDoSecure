using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ToDoSecureMVC.Models;

public class AuthenticationController : Controller
{
    private readonly HttpClient client;

    public AuthenticationController()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7100/api/"); 
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    // ================= Register =================

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUser user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("Authentication?role=" + user.Role, content);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Registration Successful! Please login.";
            return RedirectToAction("Login");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Registration failed");
            return View(user);
        }
    }

    // ================= Login =================

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("Authentication/login", content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<TokenResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            HttpContext.Session.SetString("JWToken", tokenObj.Token);
            HttpContext.Session.SetString("UserName", tokenObj.UserName);
            if (tokenObj.Roles != null && tokenObj.Roles.Any())
            {
                HttpContext.Session.SetString("UserRole", tokenObj.Roles.First());
            }
            else
            {
                HttpContext.Session.SetString("UserRole", "");
            }

            TempData["SuccessMessage"] = "Login Successful!";
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return View(model);
        }
    }

}
