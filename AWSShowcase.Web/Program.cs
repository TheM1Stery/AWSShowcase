using AWSShowcaseSite.Web.Pages;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var lambdaUrl = new Uri(Environment.GetEnvironmentVariable("LambdaUrl") ?? "http://localhost:5148");

builder.Services.AddRefitClient<ILambdaCalculatorClient>()
    .ConfigureHttpClient(c => c.BaseAddress = lambdaUrl);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();