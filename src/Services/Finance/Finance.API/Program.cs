WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddOptions();

builder.AddFinanceDbContext();

builder.AddOpenIdAuthentication();

builder.AddServices();

builder.AddAutomapper();

builder.AddControllersAndEndpoints();

builder.AddSwagger();

// Configure the HTTP request pipeline.

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseApplicationCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
