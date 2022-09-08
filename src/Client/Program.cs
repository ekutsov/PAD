using MudBlazor.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("PAD.APIGateway")
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5000/api/v1/"))
    .AddHttpMessageHandler(sp => sp.GetService<AuthorizationMessageHandler>()
        .ConfigureHandler((new[] { "https://localhost:5000" })));

builder.Services.AddScoped(provider =>
    provider.GetRequiredService<IHttpClientFactory>().CreateClient("PAD.APIGateway"));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://localhost:5001";
    options.ProviderOptions.ClientId = "web_client";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.ResponseMode = "query";
    options.AuthenticationPaths.RemoteRegisterPath = "https://localhost:5001/Identity/Account/Register";
    options.ProviderOptions.DefaultScopes.Add("roles");
    options.UserOptions.RoleClaim = "role";
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();