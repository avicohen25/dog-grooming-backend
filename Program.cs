using DogGrooming.Managers.Contracts;
using DogGrooming.Managers.Managers;
using DogGrooming.Providers.Contracts;
using DogGrooming.Providers.Providers;
using DogGrooming.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<JwtService>();


//Managers
builder.Services.AddTransient<ILoginManager, LoginManager>();
builder.Services.AddTransient<IAppointmentsManager, AppointmentsManager>();


//Providers
builder.Services.AddTransient<IGetUserProvider, GetUserProvider>();
builder.Services.AddTransient<IInsertUserProvider, InsertUserProvider>();
builder.Services.AddTransient<IGetUserByIdProvider, GetUserByIdProvider>();
builder.Services.AddTransient<IGetAppointmentsProvider, GetAppointmentsProvider>();
builder.Services.AddTransient<IGetHaircutTypesProvider, GetHaircutTypesProvider>();
builder.Services.AddTransient<IAddAppointmentProvider, AddAppointmentProvider>();
builder.Services.AddTransient<IUpdateAppointmentProvider, UpdateAppointmentProvider>();
builder.Services.AddTransient<IDeleteAppointmentProvider, DeleteAppointmentProvider>();


//-------------- Jwt --------------
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"])
            )
        };
    });
builder.Services.AddAuthorization();
//-------------- Jwt --------------


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllHeaders");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
