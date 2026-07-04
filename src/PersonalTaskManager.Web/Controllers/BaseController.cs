using System.Web;
using System.Web.Mvc;
using PersonalTaskManager.Infrastructure.Repositories;

namespace PersonalTaskManager.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected int? CurrentUserId
        {
            get
            {
                var value = Session[SessionKeys.UserId];
                if (value is int id)
                {
                    return id;
                }

                if (value != null && int.TryParse(value.ToString(), out id))
                {
                    return id;
                }

                return null;
            }
        }

        protected string CurrentUsername => User?.Identity?.IsAuthenticated == true
            ? User.Identity.Name
            : Session[SessionKeys.Username] as string;

        protected int RequireUserId()
        {
            var userId = CurrentUserId;
            if (!userId.HasValue)
            {
                throw new HttpException(401, "User is not authenticated.");
            }

            return userId.Value;
        }

        /// <summary>Khôi phục Session UserId khi app pool recycle nhưng cookie còn.</summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User?.Identity?.IsAuthenticated == true && !CurrentUserId.HasValue)
            {
                var user = new UserRepository().GetByUsername(User.Identity.Name);
                if (user != null)
                {
                    Session[SessionKeys.UserId] = user.UserId;
                    Session[SessionKeys.Username] = user.Username;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
