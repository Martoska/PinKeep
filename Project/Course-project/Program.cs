using Course_project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Course_project.Interfaces;
using Course_project.Services;
using Course_project.Helpers;
using Westwind.AspNetCore.Markdown;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); ;
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMarkdown(config =>
{
    config.AddMarkdownProcessingFolder("/");
});
var app = builder.Build();

app.UseMarkdown();
app.MapHub<CommentHub>("/items/Details");
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
