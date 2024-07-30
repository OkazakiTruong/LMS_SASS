using LMS_SASS.Models;

namespace LMS_SASS.Interfaces;

public interface ISessionService {
    UserModel? CurrentUser();
}
