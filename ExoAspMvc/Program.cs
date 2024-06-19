using ExoAspMvc.HubFolder;
using ExoAspMvc.Repository.AuthRepo;
using ExoAspMvc.Repository.ContactRepo;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(sp => new SqlConnection(
    builder.Configuration.GetConnectionString("MvcMovieContext")
    ));

builder.Services.AddScoped<IContactServices,ContactServices>();
builder.Services.AddScoped<IAuthServices,AuthServices>();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<MessageHub>("/signalr/message");

app.Run();
