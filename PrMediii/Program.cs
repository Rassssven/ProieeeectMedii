using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrMediii.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});


// Add services to the container.
builder.Services.AddRazorPages( options =>
{
    options.Conventions.AuthorizeFolder("/Modules/Index");
    options.Conventions.AuthorizeFolder("/Modules/Details");
    options.Conventions.AuthorizeFolder("/Trainers/Index");
    options.Conventions.AuthorizeFolder("/Trainers/Details");
    options.Conventions.AuthorizeFolder("/StudentGroupes/Index");
    options.Conventions.AuthorizeFolder("/StudentGroupes/Details");
    options.Conventions.AuthorizeFolder("/Students", "AdminPolicy");
}
    
);
builder.Services.AddDbContext<PrMediiiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrMediiiContext") ?? throw new InvalidOperationException("Connection string 'PrMediiiContext' not found.")));

builder.Services.AddDbContext<ModuleIdentityContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("PrMediiiContext") ?? throw new InvalidOperationException("Connection string 'PrMediiiContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<ModuleIdentityContext>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
