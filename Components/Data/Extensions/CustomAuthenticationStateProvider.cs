using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.AccessControl;
using System.Net.Http.Headers;
using System.Net.Http;

namespace AttendanceManagement.Extensions
{
    public interface IAuthenticationStateService
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task SetAuthenticationStateAsync(AuthenticationState authenticationState);
    }

    public class AuthenticationStateService : IAuthenticationStateService
    {
        private AuthenticationState authenticationState = new(new ClaimsPrincipal());

        public Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(authenticationState);
        }

        public Task SetAuthenticationStateAsync(AuthenticationState authenticationState)
        {
            this.authenticationState = authenticationState;
            return Task.CompletedTask;
        }
    }
    public class OnlineAuthenticationStateProvider : AuthenticationStateProvider
    {
     //   private readonly MyItems MyItems;
        private readonly IAuthenticationStateService authenticationStateService;
  
        public OnlineAuthenticationStateProvider(IAuthenticationStateService authStateService)
        {
            authenticationStateService = authStateService;
          //  MyItems = items;

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await authenticationStateService.GetAuthenticationStateAsync();
        }

        public async Task SetAuthenticationStateAsync(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Create claims for the authenticated user based on the JWT token
                var newClaim = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "Bearer"));
               
                var updatedIdentity = new ClaimsIdentity(newClaim.Identity);
                
                HttpClient http = new();
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
           
                updatedIdentity.AddClaim(new Claim(ClaimTypes.Name, "admin"));

                // Create a new claims principal with the updated claims identity
                var updatedUser = new ClaimsPrincipal(updatedIdentity);

                // Create a new authentication state with the updated claims principal
                var updatedAuthState = new AuthenticationState(updatedUser);

                await authenticationStateService.SetAuthenticationStateAsync(updatedAuthState);
                NotifyAuthenticationStateChanged(Task.FromResult(updatedAuthState));
            }
            else
            {
                var updatedIdentity = new ClaimsPrincipal();
                var updatedAuthState = new AuthenticationState(updatedIdentity);

                await authenticationStateService.SetAuthenticationStateAsync(updatedAuthState);
                NotifyAuthenticationStateChanged(Task.FromResult(updatedAuthState));

            }
        }
    }

}