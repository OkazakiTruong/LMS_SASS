using LMS_SASS.Databases;
using LMS_SASS.Interfaces;
using LMS_SASS.Models;

namespace LMS_SASS.Services;

public class SessionService(DatabaseContext DB, IHttpContextAccessor HttpContextAccessor) : ISessionService {
    public UserModel? CurrentUser() {
		if (HttpContextAccessor.HttpContext == null)
			return null;

        var userId = HttpContextAccessor.HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return null;

        return DB.Users.Find(userId);
    }
}
