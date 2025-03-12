using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
bool csrfEnabled = Convert.ToBoolean(builder.Configuration.GetSection("CsrfEnabled").Value);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor(); // Add this line to register IHttpContextAccessor

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    if (csrfEnabled)
    {
        options.Cookie.SameSite = SameSiteMode.Strict; //CSRF protected
    }
    else
    {
        options.Cookie.SameSite = SameSiteMode.None; //CSRF vulnerable
    }

});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(new string[] { "http://localhost:4200", "https://localhost:4200" }) // Replace with your allowed origins
                   .AllowAnyHeader()
                   .AllowAnyMethod().
                   AllowCredentials();
        });

});

// CSRF protection
if (csrfEnabled)
{
    builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
}

var app = builder.Build();

// Use CORS
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession(); // Add this line to enable session middleware

app.UseHttpsRedirection();

app.UseAuthorization();

// CSRF protection
if (csrfEnabled)
{
    /*
    app.UseAntiforgery();
    */
    var antiforgery = app.Services.GetRequiredService<IAntiforgery>();

    app.Use((context, next) =>
    {
        var requestPath = context.Request.Path.Value;

        if (string.Equals(requestPath, "/api/auth/login", StringComparison.OrdinalIgnoreCase))
        {
            var tokenSet = antiforgery.GetAndStoreTokens(context);
            context.Response.Cookies.Append("XSRF-TOKEN", tokenSet.RequestToken!,
                new CookieOptions { HttpOnly = false });
        }

        return next(context);
    });
}

app.MapControllers();

app.Run();
