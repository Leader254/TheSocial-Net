using Blazored.LocalStorage;
using Frontend;
using Frontend.Services;
using Frontend.Services.AuthProvider;
using Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//register localstorage
builder.Services.AddBlazoredLocalStorage();

//services
builder.Services.AddScoped<IAuthInterface, AuthService>();
builder.Services.AddScoped<IPostInterface, PostService>();

//configurations for authorization
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProviderService>();


await builder.Build().RunAsync();
