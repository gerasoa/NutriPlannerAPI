using CCRS.Business.Interfaces;
using CCRS.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CCRS.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        public readonly IUser AppUser;

        protected Guid UserId { get; set; }
        protected bool UserAuthenticated { get; set; }

        protected MainController(INotifier notifier,
                                 IUser appUser)
        {
            _notifier = notifier;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UserId = appUser.GetUserId();
                UserAuthenticated = true;
            }
        }

        protected bool IsOperationValid()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errorr = _notifier.GetNotifications().Select(x => x.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyInvalidModelError(modelState);
            return CustomResponse();
        }

        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}


