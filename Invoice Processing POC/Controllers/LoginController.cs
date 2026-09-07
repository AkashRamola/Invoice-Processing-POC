using Azure.Identity;
using DTO.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Login.Implementation;
using Services.Login.Interface;
using System.Text.Json.Nodes;

namespace Invoice_Processing_POC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginInterface _login;
        public LoginController(ILoginInterface Login)
        {
            _login=Login;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginUser(string username, string password)
        {
            var objUser = _login.GetUserLoginData(username, password);

            if (objUser != null)
            {
                var userInfo = new CustomerInformationDTO
                {
                    CustomerId= objUser.CustomerId,
                    Name=objUser.Name,
                    UserName= objUser.UserName,
                    RoleId= objUser.RoleId
                };
                var serializedObject = JsonConvert.SerializeObject(userInfo);
                HttpContext.Session.SetString("UserLoginTime", DateTime.UtcNow.ToString());
                HttpContext.Session.SetString("userInfo", serializedObject);
                return Json(new
                {
                    success = true,
                    message = "Login Successful",
                    data = objUser
                });
            }

            return Json(new
            {
                success = false,
                message = "Invalid Email or Password"
            });

        }
        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();

            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Json(new
            {
                success = true,
                message = "Logged out successfully."
            });
        }

    }
}
