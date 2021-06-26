using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GSExam.Models;
using Microsoft.Win32.SafeHandles;

namespace GSExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(FormModel form)
        {
            if(form == null)
            {
                form = new FormModel();
            }
            return View(form);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate([Bind]FormModel form)
        {
            //int? totalVil = 1;
            //for(int i = 0; i < form.year; i++)
            //{
            //    totalVil += i;
            //}
            var resYr1 = form.yearofdeath1 - form.ageofdeath1;
            var resYr2 = form.yearofdeath2 - form.ageofdeath2;

            int? totalVil1 = 1;
            if (resYr1 < 0)
            {
                form.resultyear1 = -1;
                form.totalkilled1 = -1;
            }
            else
            {
                form.resultyear1 = resYr1;
                for (int i = 0; i < resYr1; i++)
                {
                    totalVil1 += i;
                }
                form.totalkilled1 = totalVil1;
            }

            int? totalVil2 = 1;
            if (resYr2 < 0)
            {
                form.resultyear2 = -1;
                form.totalkilled2 = -1;
            }
            else
            {
                form.resultyear2 = resYr2;
                for (int i = 0; i < resYr2; i++)
                {
                    totalVil2 += i;
                }
                form.totalkilled2 = totalVil2;
            }
            if (resYr1 > 0 && resYr2 > 0)
            {
                decimal averagedt = (Convert.ToDecimal(totalVil1) + Convert.ToDecimal(totalVil2)) / 2;
                form.averagedata = averagedt;
            }
            else
            {
                form.averagedata = -1;
            }
            return View("Index", form);
        }
    }
}
