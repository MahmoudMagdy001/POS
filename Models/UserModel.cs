using System;

namespace POS
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsAdmin => !string.IsNullOrEmpty(Role) && (string.Equals(Role, "Admin", StringComparison.OrdinalIgnoreCase) || Role == "مدير");
    }
}
