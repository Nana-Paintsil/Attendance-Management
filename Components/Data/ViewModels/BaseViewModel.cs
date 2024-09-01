using AttendanceManagement.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AttendanceManagement.ViewModels
{


    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        [ObservableProperty]
        string token;

        [ObservableProperty]
        string username;

        
        public AuthenticationViewModel AuthenticationViewModel { get; set;  }

        public Client Api;

        [ObservableProperty]
        HttpClient newClient;

        [ObservableProperty]
        bool isDashboardRegionVisible = true;

        [ObservableProperty]
        bool isTaxRegionVisible = false;
        [ObservableProperty]
        bool isSalesRegionVisible = false;
        [ObservableProperty]
        bool isInventoryRegionVisible = false;


        private AttendanceManagement.Services.HttpClientService httpClientService {get;set; }

        
       
        public BaseViewModel()
        {
            var displayInfo = DeviceDisplay.MainDisplayInfo;
          
            var uwpClientHandler = new CustomHandler();
              Api = new Client("https://localhost:7218/", AttendanceManagement.Services.HttpClientService.Instance.HttpClient);
          
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        }



        public bool IsNotBusy => !IsBusy;


        public class CustomHandler : DelegatingHandler
        {
            public CustomHandler(): base(new HttpClientHandler())
            {
            }
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                
                if(request.RequestUri.AbsoluteUri != "https://localhost:7218/Tokens/Authenticate")
                {
                    var token = await SecureStorage.GetAsync("Token");
                    request.Headers.Add("Authorization", "Bearer " + token);

                }
                else
                {

                }
                
                
                return await base.SendAsync(request, cancellationToken);
            }

        }


    }

}
