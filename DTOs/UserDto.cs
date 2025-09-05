namespace eCommerce_backend.DTOs
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }

    public class LoggedUserDto
    {
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }

    public class LoginUserDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
