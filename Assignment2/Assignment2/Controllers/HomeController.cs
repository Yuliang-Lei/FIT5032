using Assignment2.Models;
using FIT5032_Week08A.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Contact(SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();

                    var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
                    model.Upload = myUniqueFileName;
                    if (postedFile != null)
                    {
                        string serverPath = Server.MapPath("~/Uploads/");
                        string fileExtension = Path.GetExtension(postedFile.FileName);
                        string filePath = model.Upload + fileExtension;
                        model.Upload = filePath;
                        postedFile.SaveAs(serverPath + model.Upload);
                        es.SendWithAttachment(toEmail, subject, contents, model.Upload);
                    }
                    else
                    {
                        es.Send(toEmail, subject, contents);
                    }

                    ViewBag.Result = "Thanks for your enquiry, we'll send you an email for the reply time!";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}