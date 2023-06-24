namespace NewsAPI.Models.AuthorizationModels
{
    [Index(nameof(UserName), nameof(Email), nameof(PhoneNumber), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Username is required.")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public override string Email { get; set; }
    }
}
