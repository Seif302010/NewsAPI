namespace NewsAPI.Services.AuthorizationServices
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddToRoleAsync(AddRoleModel model);
    }
}
