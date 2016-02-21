using System;
using System.Web.Mvc;

using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;
using ReusableLibrary.Captcha;
using ReusableLibrary.Supplemental.System;
using ReusableLibrary.Web;
using ReusableLibrary.Web.Mvc;

using Public.WebMvc.Constants;
using Public.WebMvc.Models;
using Tickets.Interface.Services;

namespace Public.WebMvc.Controllers
{
    [OutputCache(CacheProfile = CacheProfileNames.None)]
    public sealed class AccountController : AbstractController
    {
        private readonly IFormsAuthentication formsAuthentication;
        private readonly IValidationService validationService;
        private readonly IMembershipService membershipService;
        private readonly ICaptchaValidator captchaValidator;

        public AccountController(IFormsAuthentication authService, 
            IValidationService validationService, 
            IMembershipService membershipService,
            IValidationState validationState)
        {
            this.formsAuthentication = authService;
            this.validationService = validationService;
            this.membershipService = membershipService;
            this.captchaValidator = new DefaultCaptchaValidator(validationState) 
            { 
                // This is disabled so the functional tests do not fail.
                // In real world application this option can be managed by
                // build process.
                Enabled = false
            };
        }

        [HttpGet]
        public ActionResult LogOn()
        {
            var result = Request.CheckReturnUrl(RouteData.Values);
            return result ?? AlternatePartialView(new LogonViewData());
        }

        [ActionName("LogOn"), HttpPost/*,
        ValidateAntiForgeryToken(Salt = Constants.AntiForgeryTokenSaltNames.Login)*/]
        public ActionResult Authenticate()
        {
            /* 
             * The salt passed to the method below can by dynamic, so you can include 
             * some extra information like id, etc... something that you usually add
             * into hidden input.
             */
            ValidateAntiForgeryToken(AntiForgeryTokenSaltNames.Login
                /*, Request.Form["id"], RouteData.Values["id"] as string */);

            if (User.Identity.IsAuthenticated)
            {
                return Ajax(RedirectToAction("index", "home"));
            }

            var viewData = new LogonViewData();
            if (!TryUpdateModel(viewData) 
                | !TryUpdateModel(viewData.Details) 
                | !validationService.Validate(viewData)
                || !captchaValidator.Validate()
                || !membershipService.ValidateUser(viewData.Details))
            {
                ModelState.ResetModelValue("TuringNumber");
                return AlternatePartialView(viewData);
            }

            formsAuthentication.SignIn(
                viewData.Details.UserName, 
                viewData.RememberMe,
                Guid.NewGuid().Shrink() 
                /* 
                 * You can read this value using formsAuthentication.UserData()
                 * at any place of the authenticated request
                 */);
            if (!String.IsNullOrEmpty(viewData.ReturnUrl))
            {
                return Redirect(viewData.ReturnUrl);
            }

            return Ajax(RedirectToAction("index", "home"));
        }

        [HttpGet, Authorize]
        public ActionResult LogOff()
        {
            formsAuthentication.SignOut();
            return Ajax(RedirectToAction("index", "home"));
        }

        [HttpGet, 
        OutputCache(CacheProfile = CacheProfileNames.PrivateContent)]
        public ActionResult Register()
        {
            var viewData = new RegisterViewData
            {
                Details =
                {
                    Question = membershipService.DefaultPasswordQuestion
                }
            };
            return View(AddViewDataLists(viewData));
        }

        [ActionName("Register"), HttpPost,
        ValidateAntiForgeryToken(Salt = AntiForgeryTokenSaltNames.Register)]
        public ActionResult CreateUser()
        {
            var viewData = new RegisterViewData();
            if (!TryUpdateModel(viewData)
                | !TryUpdateModel(viewData.Details)
                | !TryUpdateModel(viewData.Details.Credentials)
                | !TryUpdateModel(viewData.Details.Info)
                | !validationService.Validate(viewData))
            {
                return View(AddViewDataLists(viewData));
            }

            using (var unitOfWork = UnitOfWork.Begin(UnitOfWorkNames.Tickets))
            {
                if (!membershipService.CreateUser(viewData.Details))
                {
                    return View(AddViewDataLists(viewData));
                }

                unitOfWork.Commit();
            }

            if (membershipService.ValidateUser(viewData.Details.Credentials))
            {
                formsAuthentication.SignIn(viewData.Details.Credentials.UserName, false /* createPersistentCookie */);
            }

            return Ajax(RedirectToAction("index", "home"));
        }

        private RegisterViewData AddViewDataLists(RegisterViewData viewData)
        {
            if (membershipService.PasswordQuestionMap.ContainsKey(viewData.QuestionId))
            {
                viewData.Details.Question = membershipService.PasswordQuestionMap[viewData.QuestionId];
            }

            viewData.PasswordQuestionSelectList = membershipService.PasswordQuestions.ToSelectList(viewData.Details.Question.Key);
            return viewData;
        }
    }
}