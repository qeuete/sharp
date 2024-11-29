using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Magaz.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestContext db;

        public AccountController(TestContext context)
        {
            db = context;
        }


        public IActionResult Autorization()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Autorization(string username, string password)
        {
            // Проверка на пустой логин и пароль
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Логин и пароль обязательны.");
                return View();
            }

            try
            {
                // Поиск пользователя в базе данных
                var user = db.Users.FirstOrDefault(u => u.LoginUs == username);

                // Проверка существования пользователя и правильности пароля
                if (user == null || !VerifyPassword(password, user.PasswordUs))
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                    return View();
                }

                var claims = new List<Claim>
                {
                  new Claim(ClaimTypes.Name, user.LoginUs),
                };
    
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Вход пользователя
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                // Перенаправление на главную страницу или каталог
                return RedirectToAction("Index", "Catalog");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Произошла ошибка при обработке запроса. Попробуйте снова.");
                return View();
            }
        }


        // GET: Reg
        public IActionResult Registr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registr(string login, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                ModelState.AddModelError("", "Все поля обязательны.");
                return View();
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Пароли не совпадают.");
                return View();
            }

            if (db.Users.Any(u => u.LoginUs == login))
            {
                ModelState.AddModelError("", "Логин уже занят.");
                return View();
            }

            var user = new User
            {
                LoginUs = login,
                PasswordUs = HashPassword(password)
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return RedirectToAction("Home");
        }


        // Метод для выхода
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Autorization");
        }

        // Пример хэширования пароля
        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Пример проверки пароля
        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return HashPassword(inputPassword) == storedPassword;
        }
    }
}
