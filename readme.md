# Prerequisites
The following prerequisites apply for running the solution:

1. .NET Core 2.2
2. Azure Cosmos DB Emulator
3. Azure Function Core Tools

# Running the application
## Fetching
Fetching of TV shows is done by an Azure Function which runs every sunday (timer trigger). For development purposes, the timer value can be set to every 10 minutes (0 */10 * * * *).

## Processing
Only the first 1000 TV shows are processed for development purposes. When we want to make the Azure Function more durable and proces all the TV shows
we can create a queue message for each batch of 250 TV shows and let a separate Azure Function take care of the fetching of the cast and writing the information to the database.
This way we separate the concerns, avoid long running operations and make the fetching of information asynchronic.

## Running
When the TV show information is available within the database, the Web API project can be run in IIS Express.
By default the Swagger UI page opens and the endpoint for getting TV shows can be tested.
The page index (pagenumber) and page size (pagesize) parameters are mandatory for getting information from the API.

# Architecture
Each layer within the solution has it's own responsibility. Most of the business logic is captured within the TVmaze.Scraper.Application project. Most of the unit tests will and can therefore
be writen for this layer. The persistence and infrastructure layers can easily be mocked or faked since they are loosely coupled.

## Polly
Since the calls to TVmaze API are rate limited, Polly is used to catch any errors and retry if the status code equals too many requests. When a status code not found is returned, a default
empty value is returned so we can act on that from within our business layer.

## Azure Functions
Since dependency injection still isn't fully supported within Azure Functions (V2), a custom Startup class is implemented.
It creates the service collection with all dependencies and from within the Azure Function the correct service is resolved.

## Database
The database used is Azure Cosmos DB. On top of the database is an entity framework cosmos layer for read/write operations.

# Side note
Solution could be more generic, performance can be improved, etc. but I think the purpose of the assignment is showing how to setup a solution in a proper and clean way.