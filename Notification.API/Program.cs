using Notification.Application.Implementation;
using Notification.Application.Interfaces;
using Notification.Application.Services;
using Notification.Infrastructure.Contract;
using Notification.Infrastructure.Logger;
using Notification.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<INotificationQueue, InMemoryNotificationQueue>();
builder.Services.AddSingleton<IPasswordResetService, InMemoryPasswordResetService>();

builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddSingleton<IAuditLogger, InMemoryAuditLogger>();
builder.Services.AddHostedService<NotificationWorker>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();


app.Run();
