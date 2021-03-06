﻿using Gentelella.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Gentelella.Interface;
using Gentelella.Models;
using Gentelella.Extensions;
using System.Net;
using Gentelella.Common.Exception;

namespace Gentelella.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly ICache _cache;

        public HomeController(IUserLogic userLogic, ICache cache)
        {
            this._userLogic = userLogic;
            this._cache = cache;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_cache.Get<PosCredential>(SessionConstants.PosCredential) == null)
            {
                return RedirectToAction("Login");
            }
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(CredentialModel credential)
        {
            var posCredential = credential.ToPosCredential();
            try
            {
                posCredential = _userLogic.Login(posCredential);
                _cache.Add<PosCredential>(SessionConstants.PosCredential, posCredential);
            }
            catch (CommonException ex)
            {
                ViewBag.ValidationMessage = ex.Message;
                return View("Login");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Register(CredentialModel credential)
        {
            var posCredential = credential.ToPosCredential();
            try
            {
                _userLogic.Register(posCredential);
            }
            catch (CommonException ex)
            {
                ViewBag.RegisterMessage = ex.Message;
                return View("Login");
            }

            return RedirectToAction("Index");
        }
    }
}