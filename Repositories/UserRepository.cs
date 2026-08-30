using System.Data;
using System.Threading.Tasks;

namespace POS.Repositories
{
    public interface IUserRepository
    {
        UserModel GetById(int userId);
        Task<UserModel> GetByIdAsync(int userId);
        (bool Success, string Message, UserModel User) Authenticate(string username, string password);
        Task<(bool Success, string Message, UserModel User)> AuthenticateAsync(string username, string password);
        (bool Success, string Message, int NewUserId) CreateUser(string username, string password, string fullName, string role, bool isActive = true);
        DataTable GetAllUsers(string searchTerm = "");
        Task<DataTable> GetAllUsersAsync(string searchTerm = "");
        (bool Success, string Message) UpdateUser(int userId, string fullName, string role, bool isActive, string newPassword = null);
        (bool Success, string Message) DeleteUser(int userId, int currentUserId);
        (bool Success, string Message) ToggleUserActive(int userId, int currentUserId, bool newStatus);
    }

    public class UserRepository : IUserRepository
    {
        public UserModel GetById(int userId) => DbHelper.GetUserById(userId);
        public Task<UserModel> GetByIdAsync(int userId) => DbHelper.GetUserByIdAsync(userId);
        public (bool Success, string Message, UserModel User) Authenticate(string username, string password) => DbHelper.Authenticate(username, password);
        public Task<(bool Success, string Message, UserModel User)> AuthenticateAsync(string username, string password) => DbHelper.AuthenticateAsync(username, password);
        public (bool Success, string Message, int NewUserId) CreateUser(string username, string password, string fullName, string role, bool isActive = true) => DbHelper.CreateUser(username, password, fullName, role, isActive);
        public DataTable GetAllUsers(string searchTerm = "") => DbHelper.GetAllUsers(searchTerm);
        public Task<DataTable> GetAllUsersAsync(string searchTerm = "") => DbHelper.GetAllUsersAsync(searchTerm);
        public (bool Success, string Message) UpdateUser(int userId, string fullName, string role, bool isActive, string newPassword = null) => DbHelper.UpdateUser(userId, fullName, role, isActive, newPassword);
        public (bool Success, string Message) DeleteUser(int userId, int currentUserId) => DbHelper.DeleteUser(userId, currentUserId);
        public (bool Success, string Message) ToggleUserActive(int userId, int currentUserId, bool newStatus) => DbHelper.ToggleUserActive(userId, currentUserId, newStatus);
    }
}
