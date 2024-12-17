namespace E_Commerce.Models
{
    public class Users
    {

        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RepeatPassword { get; set; }
  
    }
}
