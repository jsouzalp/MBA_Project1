
Instalar o dotnet ef:
dotnet tool install --global dotnet-ef

Para executar o migration:
dotnet ef migrations add 0001_InitialMigration --project ..\Blog.Repositories --startup-project ..\Blog.Api --no-build

Para remover o migration:
dotnet ef migrations remove --project ..\Blog.Repositories --startup-project ..\Blog.Api --no-build

Para ver o script que será gerado:
dotnet ef migrations script --no-build

Para sincronizar com o BD
dotnet ef database update
