using AutoMapper;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;


namespace TeduShop.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }

        public ActionResult Index()
        {

            FeedbackViewModel FeedbackviewModel = new FeedbackViewModel();
            FeedbackviewModel.ContactDetail = GetContactDetail();
            return View(FeedbackviewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if(ModelState.IsValid)
            {
                //Feedback newFeedback = new Feedback();
                //newFeedback.UpdateFeedback(feedbackViewModel);
                var newFeedback = feedbackViewModel.ToModel();
                _feedbackService.Create(newFeedback);
                _feedbackService.Save();

                ViewData["successMsg"] = "Contact was sent successfully";
              
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/templates/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");

                MailHeper.SendMail(adminEmail, "Information from website", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Message = "";
                feedbackViewModel.Email = "";
            }
            feedbackViewModel.ContactDetail = GetContactDetail();
            return View("Index", feedbackViewModel);
            
        }

        private ContactDetailViewModel GetContactDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var viewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return viewModel;
        } 
    }
}