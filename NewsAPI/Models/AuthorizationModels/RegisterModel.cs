namespace NewsAPI.Models.AuthorizationModels
{
    public class RegisterModel
    {
        [Required, MaxLength(256)]
        public string UserName { get; set; }

        [Required, MaxLength(256)]
        public string Email { get; set; }

        [Required, MaxLength(50)]
        public string Password { get; set; }
    }
}
