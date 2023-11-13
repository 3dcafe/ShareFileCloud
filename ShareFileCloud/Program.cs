using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShareFileCloud.Extensions;
using ShareFileCloud.Middleware;
using System.Reflection;
using System.Text;

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
//builder.Services.AddInMemorySubscriptions();

bool IncludeExceptionDetails = false;
#if DEBUG
IncludeExceptionDetails = true;
#endif
/*
builder.Services
	 //.AddGraphQLServer()
	 //.AddAuthorization()
	 //.AddQueryType<Queries>()
	 //.AddMutationType<Mutation>()
	 //.AddProjections()
	 //.AddFiltering()
	 //.AddSorting()
	 //.ModifyRequestOptions(opt => opt.IncludeExceptionDetails = IncludeExceptionDetails);
	 */
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