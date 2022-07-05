using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectWithAspDotNetIdentity.Models;


//steps
//create db 
//add packages in given document
//use scaffold to automatically made db context model
//right click on project 
//click add scaffold item
//error occur try install the given package and try again scaffold item identity
//go to appsettings.json
// here make sure the server and database name change
//you will have areas in your project structure
//now got to areas data and copy the class name and inherits your dbcontext class    
//now remove the empty constructor and do something for override constructor
//now go to areas data
//public ProjectWithAspDotNetIdentityContext(DbContextOptions options)
//       : base(options)
//    {
//}
//add new constructor and remove <> just like above
//now write add-migration initial-context classNAME IN Console.


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

//Getting Connection string
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
//Getting Assembly Name
var migrationAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.
builder.Services.AddDbContext<UserDbContext>(options =>
           options.UseSqlServer(connString, sql => sql.MigrationsAssembly(migrationAssembly)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
