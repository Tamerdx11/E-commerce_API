using E_commerceAPI.Controllers;
using E_commerceAPI.Data;
using E_commerceAPI.Models;
using E_commerceAPI.Repository;
using E_commerceAPI.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the contain

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCaching();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddDbContext<UserContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnection")));
builder.Services.AddDbContext<ProductContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProductsConnection")));
builder.Services.AddDbContext<OrderContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("OrdersConnection")));

builder.Services.AddTransient(typeof(IUser), typeof(UserRepository));
builder.Services.AddTransient(typeof(IProduct), typeof(ProductRepository));
builder.Services.AddTransient(typeof(IOrder), typeof(OrderRepository));

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:token").Value!))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();
