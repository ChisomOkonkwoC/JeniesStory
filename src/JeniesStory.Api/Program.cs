using JeniesStory.Api.Configuration;
using JeniesStory.Application.Mapper;
using JeniesStory.Domain.Entities;
using JeniesStory.Infrastructure.Data;
using JeniesStory.Infrastructure.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(UserMapper));
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
 opt.TokenLifespan = TimeSpan.FromMinutes(10));


    var app = builder.Build();

// Seed roles if they don't exist
using (var scope = app.Services.CreateScope())
{

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Seeders.Seed(roleManager, dbContext).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
