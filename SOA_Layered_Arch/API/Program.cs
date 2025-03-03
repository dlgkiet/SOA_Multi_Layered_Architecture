﻿using Microsoft.EntityFrameworkCore;
using SOA_Layered_Arch.DataAccessLayer;
using SOA_Layered_Arch.DataAccessLayer.Repositories;
using SOA_Layered_Arch.ServiceLayer;
using SOA_Layered_Arch.CoreLayer.Entities;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ✅ Bật hỗ trợ globalization
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

// ✅ Đăng ký DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Đăng ký Repository
builder.Services.AddScoped<IRepository<Movie>, MovieRepository>();
builder.Services.AddScoped<IRepository<MovieSeriesTag>, MovieSeriesTagRepository>();
builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Tag>, TagRepository>();

// ✅ Đăng ký Service (tránh lặp lại)
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<MovieSeriesTagService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TagService>();

// ✅ Cấu hình Controller và Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
