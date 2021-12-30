
namespace RadarLite.Interfaces.User;

public interface IUserConfigService {
    object GetAllUsersAsync();
    object GetUserByIdAsync(int id);
}

