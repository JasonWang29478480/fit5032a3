using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace WebApplication1.Controllers
{
    public class SendMailController : Controller
    {
        // GET: SendMail
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Index(WebApplication1.Models.MailModel obMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {

                using (MailMessage mail = new MailMessage("3408265108@qq.com", obMail.To))
                {
                    mail.Subject = obMail.Subject;
                    mail.Body = obMail.Body;
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.qq.com";
                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential("3408265108@qq.com", "ayduacghkhtedaai");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 25;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    return View("Index", obMail);
                }
            }
            else
            {
                return View();
            }
        }
    }
}
