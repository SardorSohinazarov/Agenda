using Agenda.BackGroundServices;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

var token = builder.Configuration["token"];
builder.Services.AddSingleton(new TelegramBotClient(token));
builder.Services.AddHostedService<BotBackgroundService>();

var app = builder.Build();


app.Run();
