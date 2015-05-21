using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreCru.Web.Entity;
using TreCru.Web.Services.Interfaces;

namespace TreCru.Web.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IRepository<Character> _characterRepo;

        public CharacterController(IRepository<Character> characterRepo)
        {
            _characterRepo = characterRepo;
        }

        public ActionResult Index()
        {
            var allChars = _characterRepo.GetAll();
            ViewBag.Title = "Characters";
            return View(allChars);
        }
    }
}