using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SPCaemucals.Backend.Repositories;
using SPCaemucals.Backend.Services;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;
using SPCaemucals.Backend.Controllers;
using SPCaemucals.Excel;
using SPCaemucals.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Please insert JWT with Bearer into field"
    });

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
            new List<string>()
        } 
    });
});
builder.Services.AddAuthorization();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
    });


//builder.Services.AddSingleton<EmailSender, EmailSender>();

var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.CommandTimeout(60);
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }).EnableSensitiveDataLogging());


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false; // for special characters
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
    } )  // IdentityRole supplies the role functionality
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



builder.Services.AddScoped<CorrelationIdHelper>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ProductSeed>();



// Configure application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login"; // set your login path here
    options.SlidingExpiration = true;
});

// Before "var app = builder.Build();"
var key = builder.Configuration["JwtKey"];
builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        if (key != null)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }
        else
        {
            throw new Exception("key at AddJwtBearer is missing");
        }
            
    });

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();



var instrumentationKey = configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces) // Use the instrumentation key directly
    .CreateLogger();

builder.Host.UseSerilog(); // Add this line

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();


// Migrate the database here
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    
    //string filePath = Path.Combine(Directory.GetCurrentDirectory(),"sqlScript", "Provinces.sql");

    var seedData = new SeedProvince(dbContext);
    
   await seedData.ExecuteSqlFromFileAsync();
}

app.UseSerilogRequestLogging();

app.Use(async (context, next) =>
{
    var correlationId = context.Request.Headers["CorrelationId"].FirstOrDefault();
    if (string.IsNullOrEmpty(correlationId))
    {
        correlationId = Guid.NewGuid().ToString();
        context.Request.Headers.Add("CorrelationId", correlationId);
    }

    context.Response.OnStarting(state =>
    {
        var ctx = (HttpContext)state;
        ctx.Response.Headers.Add("CorrelationId", new StringValues(correlationId));
        return Task.CompletedTask;
    }, context);

    await next.Invoke();
});

app.Use(async (context, next) =>
{
    var url = context.Request?.Path.Value;
    var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

    // Enable buffering
    context.Request.EnableBuffering();

    // Read Request Body
    string body = string.Empty;
    var req = context.Request;
    req.Body.Position = 0; // rewind
    using (var reader = new StreamReader(req.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
    {
        body = await reader.ReadToEndAsync();
    }
    
    var correlationId = context.Request.Headers["CorrelationId"].FirstOrDefault();

    // Log Information
    Log.Information($"Incoming Request. CorrelationId: {correlationId}, Url: {url}, Authorization Header: {authorizationHeader}, Body: {body}");

    req.Body.Position = 0; // rewind it again so next middleware can read it

    await next.Invoke();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   await SeedData(app);
}

app.UseSwagger();
app.UseSwaggerUI();






app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 500; // you can set other status codes here
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            // Log the error details
            Log.Error($"Something went wrong in the app: {contextFeature.Error}", contextFeature.Error);

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."  // Modify this as per your need
            }.ToString());
        }
    });
});



app.MapControllers();
app.UseCors(x => x
    .WithOrigins("http://localhost:3000")  // allow specified origin
    .AllowAnyMethod()                      // allow all HTTP methods
    .AllowAnyHeader()                      // allow all headers
    .AllowCredentials());                  // allow credentials

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

Log.Information("Application Starting Up");

app.Run();

Log.Information("Application Shutting Down");

 static async Task SeedData(WebApplication app)
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();



        var seedObject = scope.ServiceProvider.GetRequiredService<ProductSeed>();

        // Check if any categories exist
        if (!context.Categories.Any() && !context.Products.Any())
        {
            await seedObject.SeedCategoryAndProductAsync();
        }


        await context.SaveChangesAsync();
    }

    
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

