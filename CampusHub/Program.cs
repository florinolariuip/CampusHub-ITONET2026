using CampusHub.Common.CQRS;
using CampusHub.Domain.Common;
using CampusHub.Domain.Events;
using CampusHub.Domain.Participants;
using CampusHub.Feature.Events.CreateEvent;
using CampusHub.Feature.Events.GetEventById;
using CampusHub.Infrastructure.Persistence;
using CampusHub.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<CampusHubDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CampusHubDB")));

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CampusHubDbContext>());
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();

builder.Services.AddHandlers(typeof(Program).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CampusHubDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var events = app.MapGroup("/events");
CreateEventEndpoint.Map(events);
GetEventByIdEndpoint.Map(events);

app.Run();
