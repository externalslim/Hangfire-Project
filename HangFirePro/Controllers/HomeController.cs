using System;
using System.Web.Helpers;
using System.Web.Mvc;
using Hangfire;
using HangFirePro.Helper;
using HangFirePro.Services;

namespace HangFirePro.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmailService _emailService = new EmailService();

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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult FireAndForget()
        {
            //Bir kez çalışır
            var jobId = BackgroundJob.Enqueue(() => _emailService.SendEmail("fire-and-forget"));

            return Json(string.IsNullOrWhiteSpace(jobId) ? "false" : "true");
        }

        [HttpPost]
        public JsonResult After5Min()
        {
            //Belirlenen süre sonunda bir kez çalışır.
            var jobId = BackgroundJob.Schedule(() => _emailService.SendEmail("1 Dakika sonra çalışacak!"), DateTime.Now.AddMinutes(1));

            return Json(string.IsNullOrWhiteSpace(jobId) ? "false" : "true");
        }

        [HttpPost]
        public JsonResult Schedule()
        {
            //Schedule çalışır
            RecurringJob.AddOrUpdate(() => _emailService.SendEmail("Her Cuma saat 16:30'de çalışacak"), CronConstants.EveryFridayAt1630, TimeZoneInfo.Local);

            return Json("true");
        }

        [HttpPost]
        public JsonResult ScheduleDaily()
        {
            //Schedule çalışır
            RecurringJob.AddOrUpdate(() => _emailService.Test(), CronConstants.EveryDayAt0830, TimeZoneInfo.Local);

            return Json("true");
        }

    }
}