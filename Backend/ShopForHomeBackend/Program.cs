using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShopForHomeBackend.Data;
using ShopForHomeBackend.Services;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Database
// =======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =======================
//  Dependency Injection
// =======================
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IBulkUploadService, BulkUploadService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// =======================
// Controllers & JSON
// =======================
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// =======================
//  CORS
// =======================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        b => b.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});


// JWT Authentication

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// =======================
//Swagger
// =======================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ShopForHome API",
        Version = "v1",
        Description = "API documentation for ShopForHome"
    });

    // Prevent schema ID conflicts
    c.CustomSchemaIds(type => type.FullName);

    // Add JWT Authentication to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference 
            { 
                Type = ReferenceType.SecurityScheme, 
                Id = "Bearer" 
            }
        },
        Array.Empty<string>()
    }
});

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Apply migrations at startup
    Seeder.Seed(db); // Seed initial data
}   

// =======================
// Middleware Pipeline
// =======================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;

    });
    app.UseSwaggerUI(c =>
    {
        //  Match the document name "v1"
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopForHome API v1");

        // Optional: Serve Swagger UI at root (http://localhost:5263/)
        //c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
