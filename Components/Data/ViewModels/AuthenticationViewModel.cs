using System.Windows.Input;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using AttendanceManagement.Extensions;
using AttendanceManagement.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AttendanceManagement.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AttendanceManagement.Handlers;

namespace AttendanceManagement.ViewModels
{



    public partial class AuthenticationViewModel : BaseViewModel
    {

        public ICommand AuthenticateCommand { get; set; }

        public OnlineAuthenticationStateProvider OnlineAuthenticationStateProvider { get; set; }
        public NavigationManager NavigationManager { get; set; }
        public MyDbContext MyDbContext { get; set; }

        public List<TypeOfStaff> TypesOfStaff = new();

        public List<Holiday> Holidays = new();
        public string Username;
        public string Password;

        HttpClient client = new();

        private SyncAgent syncAgent;

        public AuthenticationViewModel(NavigationManager navigation, OnlineAuthenticationStateProvider onlineAuthenticationState,MyDbContext dbContext)

        {
            OnlineAuthenticationStateProvider = onlineAuthenticationState;
            AuthenticateCommand = new Command(async () => await Authenticate());
            MyDbContext = dbContext;
        }

        
        public async Task Logout()
        {
            SecureStorage.Default.Remove("Token");
            await GoToLoginPage();
        }
        public async Task GoToLoginPage()
        {
            // var result = await NavigationService.NavigateAsync(nameof(MainContent));
        }
        public async Task NavigateToHomePage()
        {
            NavigationManager.NavigateTo("/");

        }

        private static readonly Random random = new Random();

        public static string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        public UserAuth? CreateNewEmployee(Employee newEmployee)
        {
            try
            {
                MyDbContext.Employees.Add(newEmployee);
                UserAuth newUserAuth = new ()
                {
                    Id = Guid.NewGuid(),
                    Email = newEmployee.Email,
                    PasswordHash = SecretHasher.Hash(GeneratePassword(8)),
                    Username = "BM" + MyDbContext.Employees.Count().ToString("D3"),
                TenantId = newEmployee.TenantId
                };

                MyDbContext.UserAuths.Add(newUserAuth);
                MyDbContext.SaveChanges();
                return newUserAuth;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

            public async Task Authenticate()
        {

            IsBusy = true;
            try
            {

                UserCred body = new()
                {
                    Email = Username,
                    Password = Password
                };
                var response = await LocalAuthentication(body);
                Preferences.Set("Token", response.Jwttoken);
                Preferences.Set("ExpiryTime", response.Expirytime.ToString());
                Preferences.Set("RefreshToken", response.Refreshtoken);
                Preferences.Set("Username", response.UserId);
                await OnlineAuthenticationStateProvider.SetAuthenticationStateAsync(response.Jwttoken);
            }

            catch (Exception ex)
            {
                /*
                Debug.WriteLine($"Unable to get Maverick_Shop_Test {ex.Message}");
                if (ex.Message.Contains("The HTTP status code of the response was not expected (401)."))
                {
                    await _dialogService.DisplayAlertAsync("Authentication Error!", "Invalid Username or Password", "OK");

                }
                else
                {

                    await _dialogService.DisplayAlertAsync("Authentication Error!", ex.Message, "OK");
                }
                */
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;

            }



        }
        public async Task<TokenResponse?> LocalAuthentication(UserCred userCred)
        {

            var userAuth = await MyDbContext.UserAuths.FirstOrDefaultAsync(item => item.Email == userCred.Email);

            var credentialsCorrect = SecretHasher.Verify(userCred.Password, userAuth.PasswordHash);
            if (!credentialsCorrect)
                return null;
            /// Generate Token
            var user = await MyDbContext.Employees.FirstOrDefaultAsync(item => item.Email == userCred.Email);
            byte[] tokenkey;
            using (var rng = new RNGCryptoServiceProvider())
            {
                tokenkey = new byte[32]; // 32 bytes = 256 bits
                rng.GetBytes(tokenkey);
            }
            var hmac = new HMACSHA512(Encoding.UTF8.GetBytes("thisisoursecurekeythisisoursecurekeythisisoursecurekeythisisoursecurekeythisisoursecurekey"));

            // Convert the byte array to Base64Url-encode
            var tokenhandler = new JwtSecurityTokenHandler();
            var expiry = DateTime.Now.AddMinutes(1000);
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Name, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("tenantId", user.TenantId.ToString()),
    }),
                Expires = expiry,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(hmac.Key), SecurityAlgorithms.HmacSha512
                )
            };
            var token = tokenhandler.CreateToken(tokendesc);
            string finaltoken = tokenhandler.WriteToken(token);

            var response = new TokenResponse() { Jwttoken = finaltoken, Refreshtoken = "", Expirytime = expiry, UserId = user.Id.ToString(), TenantId = user.TenantId };

            return response;
        }


    }
}
