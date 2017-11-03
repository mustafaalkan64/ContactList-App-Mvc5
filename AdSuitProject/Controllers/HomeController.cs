using AdSuit.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdSuitProject.Controllers
{
    public class HomeController : Controller
    {
        ITagService _TagService;

        public HomeController(ITagService TagService)
        {
            _TagService = TagService;
        }
        public ActionResult Index()
        {
            var tags = _TagService.GetAll();
            return View(tags);
        }


    }
}
