namespace NewsAPI.Models.AuthorizationModels
{
    public class TokenRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
