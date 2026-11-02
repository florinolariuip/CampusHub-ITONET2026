# CampusHub

A minimal ASP.NET Core Web API for managing campus events and participant registrations.

## Tech Stack

- **.NET 10** — ASP.NET Core Minimal APIs
- **Entity Framework Core 10** with SQLite
- **CQRS** pattern (commands/queries with auto-registration via reflection)

## Architecture

```
CampusHub/
├── Common/CQRS/          # ICommand, IQuery, ICommandHandler, IQueryHandler interfaces + DI registration
├── Domain/               # Aggregates, value objects, repository interfaces, Result<T>
│   ├── Events/           # Event aggregate, DateRange value object, EventType/EventStatus enums
│   ├── Participants/     # Participant entity
│   └── Registrations/   # Registration entity
├── Feature/Events/       # Vertical-slice feature handlers and endpoints
│   ├── CreateEvent/
│   └── GetEventById/
└── Infrastructure/
    └── Persistence/      # EF Core DbContext, entity configurations, repository implementations
```

## Endpoints

| Method | Route            | Description          |
|--------|------------------|----------------------|
| POST   | `/events`        | Create a new event   |
| GET    | `/events/{id}`   | Get event by ID      |

### POST /events — Request body

```json
{
  "title": "Hackathon 2027",
  "description": "Annual campus hackathon",
  "startDate": "2027-03-01T09:00:00Z",
  "endDate": "2027-03-02T18:00:00Z",
  "type": "Hackathon",
  "capacity": 200
}
```

`type` values: `Workshop`, `Hackathon`, `Lecture`

## Running Locally

```bash
dotnet run --project CampusHub
```

The SQLite database (`CampusHub.db`) is created automatically on first run.

OpenAPI docs are available at `/openapi/v1.json` in development.

## Configuration

Connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "CampusHubDB": "Data Source=CampusHub.db"
  }
}
```
