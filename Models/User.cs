using eCommerce_backend.Constants;

namespace eCommerce_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public Role Role { get; set; } = Role.User;

        // navigation property for related Orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
