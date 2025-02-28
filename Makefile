add-migration:
	dotnet ef migrations add --project ./src/Infrastructure/Infrastructure.csproj --startup-project ./src/WebApi/WebApi.csproj --context Infrastructure.Persistence.AppDbContext --configuration Debug --verbose $(name) --output-dir Persistence/Migrations

apply-migrations:
	dotnet ef database update --project ./src/Infrastructure/Infrastructure.csproj --startup-project ./src/WebApi/WebApi.csproj --context Infrastructure.Persistence.AppDbContext --configuration Debug --verbose
