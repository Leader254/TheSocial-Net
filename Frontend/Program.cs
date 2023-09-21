using Blazored.LocalStorage;
using Frontend;
using Frontend.Services;
using Frontend.Services.AuthProvider;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SpinnerService>();
builder.Services.AddScoped<BlazorDisplaySpinnerAutomaticallyHttpMessageHandler>();

builder.Services.AddScoped(s =>
{
    var accessTokenHandler = s.GetRequiredService<BlazorDisplaySpinnerAutomaticallyHttpMessageHandler>();
    accessTokenHandler.InnerHandler = new HttpClientHandler();
    var uriHelper = s.GetRequiredService<NavigationManager>();
    return new HttpClient(accessTokenHandler)
    {
        BaseAddress = new Uri(uriHelper.BaseUri)
    };
});
//register localstorage
builder.Services.AddBlazoredLocalStorage();

//services
builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IPostInterface, PostService>();
builder.Services.AddScoped<ICommentInterface, CommentsService>();

//configurations for authorization
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProviderService>();


await builder.Build().RunAsync();
