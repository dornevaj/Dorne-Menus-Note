using Evernote.BLL.Abstract;
using Evernote.Entities.Models;
using Evernote.Filters;
using Evernote.Helper;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Evernote.Controllers
{
    [EvernoteExceptionFilter]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                string md5Password = MD5.Calculate(user.Password);
                User isLoginUser = _userService.GetAllUser(x => (x.EmailAddress == user.EmailAddress) && (x.Password == md5Password)).FirstOrDefault();
                if (isLoginUser != null)
                {
                    Session["isLogin"] = isLoginUser;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Please Check Out Your Email and Password!");
                    return View(new User());
                }
            }
            return View(new User());
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                string userPassword = MD5.Calculate(user.Password);
                user.Password = userPassword;
                user.IsActive = true;
                user.UserRole = Entities.Models.User.UserRoles.Normal;
                _userService.AddUser(user);

                return RedirectToRoute("Login", new User());
            }

            return View();
        }
        public ActionResult LogOut()
        {

            Session.Clear();// is like removing books from the bookshelf
            Session.Abandon(); //is like throwing the bookshelf itself.
            FormsAuthentication.SignOut();

            return RedirectToRoute("Login", new User());
        }

        [HttpGet]
        public ActionResult RecoveryPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoveryPassword(string emailAddress)
        {
            User user = _userService.GetAllUser(x => x.EmailAddress == emailAddress).FirstOrDefault();
            if (user != null)
            {            
                using (MailMessage mail = new MailMessage())
                {
                    string newPassword = Membership.GeneratePassword(50, 0);
                    newPassword = Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => "");
                    newPassword = newPassword.Substring(0, 6);

                    mail.To.Add(emailAddress);
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress("info@capital.com.tr");
                    mail.Subject = "New Password";
                    mail.Body = "Your New Password:" + newPassword;

                    using (var client = new SmtpClient())
                    {

                        client.Host = "mail.capital.com.tr";
                        client.Port = 587;
                        client.EnableSsl = false;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new System.Net.NetworkCredential("info@capital.com.tr", "capital123");
                        client.Timeout = 100000;

                        try
                        {
                             client.Send(mail);//try send mail
                            user.Password = MD5.Calculate(newPassword);//if send mailupdate user password
                            _userService.UpdateUser(user);
                            ModelState.AddModelError("", "Check Out Your Email Address, For Login With New Password");//show message to user
                          
                        }
                        catch (Exception ex)
                        {
                            //show message to user
                            if (ex.InnerException !=null && ex.InnerException.Message !=null)
                            {
                                ModelState.AddModelError("", "Error:" + ex.Message + "Error Details :" + ex.InnerException.Message);
                            }
                            else
                            {
                                ModelState.AddModelError("", "Error:" + ex.Message);
                            }
                            
                        }
                    }//using client
                }//using mail            

                return View();

            }//if user not null
            else
            {
                ModelState.AddModelError("", "Check Out Your Email Address, This Email Address Is Not Exist");
                return View();
            }
        }
    }
}