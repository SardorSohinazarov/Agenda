using Agenda.BackGroundServices;
using Agenda.Data;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

var token = builder.Configuration["token"];
builder.Services.AddSingleton(new TelegramBotClient(token));

builder.Services.AddHostedService<BotBackgroundService>();

builder.Services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();


app.Run();
