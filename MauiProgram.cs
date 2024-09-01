using Dotmim.Sync;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using AttendanceManagement.Extensions;
using AttendanceManagement.Services;
using AttendanceManagement.Handlers;
using AttendanceManagement.Models;
using AttendanceManagement.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using AttendanceManagement.Components.Data.Services;
using AttendanceManagement.Data;

namespace AttendanceManagement
{
    public static class MauiProgram
    {
      
        public static MauiApp CreateMauiApp()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Combine(path, "test4.db");

            if (!File.Exists(dbPath))
            {
                using (var dbContext = new MyDbContext())
                {
                    dbContext.Database.ExecuteSqlRaw(AppConstants.initialQuery);
                }
            }


            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IAuthenticationStateService, AuthenticationStateService>();
            builder.Services.AddScoped<AuthenticationState>();
            builder.Services.AddSingleton<ISettingServices, SettingServices>();
            builder.Services.AddSingleton<SettingServices>();
            builder.Services.AddSingleton<OnlineAuthenticationStateProvider>();
            builder.Services.AddSingleton<ISyncServices, SyncServices>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s =>
              s.GetRequiredService<OnlineAuthenticationStateProvider>());
            builder.Services.AddSingleton<TokenHandler>();
            builder.Services.AddSingleton<MessageService>();
            builder.Services.AddSingleton<AnalysisService>();



            builder.Services.AddSingleton<SyncAgent>();

            builder.Services.AddSingleton<Sync>();

            builder.Services.AddDbContextFactory<MyDbContext>(options =>
          options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped<DbContext, MyDbContext>();
            builder.Services.AddSingleton<AuthenticationViewModel>();
            builder.Services.AddBlazorBootstrap();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF1cVGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUH1YcXJXQ2RVVUZyWg==");

            builder.Services.AddAuthorizationCore(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

            });
            // Register the custom policy and handler
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("AllowAnonymousPolicy", policy =>
                {
                    policy.Requirements.Add(new AllowAnonymousPolicyRequirement());
                });
            });
            builder.Services.AddScoped<IAuthorizationHandler, AllowAnonymousPolicyHandler>();
            builder.Services.AddScoped<ILogoutService, LogoutService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
