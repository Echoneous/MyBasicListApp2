using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBasicListApp2.Models.DB;
using System.Threading.Tasks;
using MyBasicListApp2.Models.ViewModel;
using System.Net.Mail;
using System.Net;

namespace MyBasicListApp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyToDoList()
        {
            using (MyBasicListAppEntities db = new MyBasicListAppEntities())
            {
                return View(db.tblBasicLists.ToList());
            }
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(emailForm newUser)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} {1}</p><p>Message:</p><p>{2}</p>";
                var Message = new MailMessage();
                Message.To.Add(new MailAddress("todolistapptest@gmail.com"));
                Message.From = new MailAddress(newUser.emailEmail);
                Message.Subject = newUser.emailsubject;
                Message.Body = string.Format(body, newUser.emailfirstName, newUser.emaillastName, newUser.emailmessage);
                Message.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "todolistapptest@gmail.com",
                        Password = "1_t3st_em8t"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(Message);
                    return RedirectToAction("Index");

                }
            }
            return View();
        }

        public ActionResult Email()
        {
            return View();
        }
    }
}