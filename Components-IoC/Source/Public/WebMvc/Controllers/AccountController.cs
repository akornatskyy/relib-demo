using System;
using System.Web.Mvc;
using System.Web.Routing;
using Public.WebMvc.Models;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;
using ReusableLibrary.Captcha;
using ReusableLibrary.Supplemental.System;
using ReusableLibrary.Web;
using ReusableLibrary.Web.Mvc;
using Tickets.Interface.Services;

namespace Public.WebMvc.Controllers
{
    [OutputCache(CacheProfile = Constants.CacheProfileNames.None)]
    public sealed class AccountController : AbstractController
    {
        private readonly IFormsAuthentication m_formsAuthentication;
        private readonly IValidationService m_validationService;
        private readonly IMembershipService m_membershipService;
        private readonly ICaptchaValidator m_captchaValidator;

        public AccountController(IFormsAuthentication authService, 
            IValidationService validationService, 
            IMembershipService membershipService,
            IValidationState validationState)
        {
            m_formsAuthentication = authService;
            m_validationService = validationService;
            m_membershipService = membershipService;
            m_captchaValidator = new DefaultCaptchaValidator(validationState) 
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
            ValidateAntiForgeryToken(Constants.AntiForgeryTokenSaltNames.Login
                /*, Request.Form["id"], RouteData.Values["id"] as string */);

            if (User.Identity.IsAuthenticated)
            {
                return Ajax(RedirectToAction("index", "home"));
            }

            var viewData = new LogonViewData();
            if (!TryUpdateModel(viewData) 
                | !TryUpdateModel(viewData.Details) 
                | !m_validationService.Validate(viewData)
                || !m_captchaValidator.Validate()
                || !m_membershipService.ValidateUser(viewData.Details))
            {
                ModelState.ResetModelValue("TuringNumber");
                return AlternatePartialView(viewData);
            }

            m_formsAuthentication.SignIn(
                viewData.Details.UserName, 
                viewData.RememberMe,
                Guid.NewGuid().Shrink() 
                /* 
                 * You can read this value using m_formsAuthentication.UserData()
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
            m_formsAuthentication.SignOut();
            return Ajax(RedirectToAction("index", "home"));
        }

        [HttpGet, 
        OutputCache(CacheProfile = Constants.CacheProfileNames.PrivateContent)]
        public ActionResult Register()
        {
            var viewData = new RegisterViewData();
            viewData.Details.Question = m_membershipService.DefaultPasswordQuestion;
            return View(AddViewDataLists(viewData));
        }

        [ActionName("Register"), HttpPost,
        ValidateAntiForgeryToken(Salt = Constants.AntiForgeryTokenSaltNames.Register)]
        public ActionResult CreateUser()
        {
            var viewData = new RegisterViewData();
            if (!TryUpdateModel(viewData)
                | !TryUpdateModel(viewData.Details)
                | !TryUpdateModel(viewData.Details.Credentials)
                | !TryUpdateModel(viewData.Details.Info)
                | !m_validationService.Validate(viewData))
            {
                return View(AddViewDataLists(viewData));
            }

            using (var unitOfWork = UnitOfWork.Begin(Constants.UnitOfWorkNames.Tickets))
            {
                if (!m_membershipService.CreateUser(viewData.Details))
                {
                    return View(AddViewDataLists(viewData));
                }

                unitOfWork.Commit();
            }

            if (m_membershipService.ValidateUser(viewData.Details.Credentials))
            {
                m_formsAuthentication.SignIn(viewData.Details.Credentials.UserName, false /* createPersistentCookie */);
            }

            return Ajax(RedirectToAction("index", "home"));
        }

        private RegisterViewData AddViewDataLists(RegisterViewData viewData)
        {
            if (m_membershipService.PasswordQuestionMap.ContainsKey(viewData.QuestionId))
            {
                viewData.Details.Question = m_membershipService.PasswordQuestionMap[viewData.QuestionId];
            }

            viewData.PasswordQuestionSelectList = m_membershipService.PasswordQuestions.ToSelectList(viewData.Details.Question.Key);
            return viewData;
        }
    }
}
