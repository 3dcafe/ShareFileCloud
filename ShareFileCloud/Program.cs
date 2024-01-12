using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Localization;
using ShareFileCloud.Extensions;
using ShareFileCloud.Middleware;
using System.Reflection;
using System.Text;
using System.Globalization;
using DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Builder

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
							corsPolicyBuilder =>
							{
								corsPolicyBuilder.AllowAnyHeader()
									   .AllowAnyMethod()
									   .SetIsOriginAllowed((_) => true)
									   .AllowCredentials();
							}));
builder.Services.Configure<FormOptions>(x =>
{
	x.ValueLengthLimit = int.MaxValue;
	x.MultipartBodyLengthLimit = int.MaxValue;
	x.BufferBodyLengthLimit = int.MaxValue;
	x.MultipartBoundaryLengthLimit = int.MaxValue;
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
    };
    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();

#if DEBUG
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "API",
		Description = "ASP.NET Core Web API",
		TermsOfService = new Uri("https://3dcafe.ru/"),
		Contact = new OpenApiContact
		{
			Name = "Nikolay Latin",
			Email = string.Empty,
			Url = new Uri("https://3dcafe.ru/"),
		},
		License = new OpenApiLicense
		{
			Name = "3D CAFE API",
			Url = new Uri("https://3dcafe.ru/"),
		}
	});
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
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
	c.SchemaFilter<AddSchemaDefaultValues>();
	c.DocumentFilter<SwaggerAddEnumDescriptions>();
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});
#endif

builder.Services.AddHttpContextAccessor();


var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
Console.WriteLine($"Connection {connectionString ?? "no env"}");

#if DEBUG
string path = @"C:\Source\test.txt";
if (File.Exists(path) && string.IsNullOrEmpty(connectionString))
{
    using (FileStream fstream = File.OpenRead(path))
    {
        byte[] buffer = new byte[fstream.Length];
        await fstream.ReadAsync(buffer, 0, buffer.Length);
        string textFromFile = Encoding.Default.GetString(buffer);
        Console.WriteLine($"Connection from file : {textFromFile}");
        connectionString = textFromFile;
    }
}
#endif

builder.Services.AddDbContext<ApplicationContext>
    (
        options =>
                options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .UseNpgsql(connectionString), ServiceLifetime.Transient
    );



builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services
	.AddSignalR(o =>
	{
		o.EnableDetailedErrors = true;
		o.MaximumReceiveMessageSize = null;
	});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateActor = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Issuer"],
		ValidAudience = builder.Configuration["Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"]))
	};
});

builder.Services.AddControllersWithViews();

#endregion


#region App
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

#endregion