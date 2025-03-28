using GestionaleConcerti.Data;
using GestionaleConcerti.Models;
using GestionaleConcerti.Settings;
using GestionaleConcerti.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configura Settings da appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<IdentitySettings>(builder.Configuration.GetSection("Identity"));

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Identity
var identitySettings = builder.Configuration.GetSection("Identity").Get<IdentitySettings>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = identitySettings.SignInRequireConfirmedAccount;
    options.Password.RequiredLength = identitySettings.RequiredLength;
    options.Password.RequireDigit = identitySettings.RequireDigit;
    options.Password.RequireLowercase = identitySettings.RequireLowercase;
    options.Password.RequireNonAlphanumeric = identitySettings.RequireNonAlphanumeric;
    options.Password.RequireUppercase = identitySettings.RequireUppercase;
})
.AddRoles<ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Services
builder.Services.AddScoped<ArtistaService>();
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<BigliettoService>();
builder.Services.AddScoped<AuthService>();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
    };
});

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

    string adminEmail = "admin@concerti.com";

    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });

        await userManager.AddToRoleAsync(user, "Admin");
    }
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
