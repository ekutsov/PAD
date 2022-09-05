// Add services to the container.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddOptions();

builder.AddDbContext();

builder.AddIdentity();

builder.AddQuartz();

builder.AddExternalAuthentication();

builder.AddOpenIdConnectAuthentication();

builder.AddServices();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddHostedService<Worker>();

// Configure the HTTP request pipeline.
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder => builder.WithOrigins("*")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();