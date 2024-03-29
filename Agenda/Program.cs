using Agenda.Data;
using Agenda.Repositories;
using Agenda.Services;
using Agenda.Services.BackGroundServices;
using Telegram.Bot;
using Telegram.Bot.Polling;

var builder = WebApplication.CreateBuilder(args);

var token = builder.Configuration["token"];
builder.Services.AddSingleton(new TelegramBotClient(token));

builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.AddHostedService<ReminderBackgroundService>();

builder.Services.AddDbContext<AgendaDbContext>();

builder.Services.AddScoped<IChallengerRepository, ChallengerRepository>();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddSingleton<IUpdateHandler, UpdateHandlerService>();

var app = builder.Build();

app.Run();
