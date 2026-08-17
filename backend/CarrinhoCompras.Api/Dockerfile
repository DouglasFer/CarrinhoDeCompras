FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY CarrinhoCompras.Api/CarrinhoCompras.Api.csproj CarrinhoCompras.Api/
COPY CarrinhoCompras.Application/CarrinhoCompras.Application.csproj CarrinhoCompras.Application/
COPY CarrinhoCompras.Domain/CarrinhoCompras.Domain.csproj CarrinhoCompras.Domain/
COPY CarrinhoCompras.Infrastructure/CarrinhoCompras.Infrastructure.csproj CarrinhoCompras.Infrastructure/
RUN dotnet restore CarrinhoCompras.Api/CarrinhoCompras.Api.csproj

COPY . .
RUN dotnet publish CarrinhoCompras.Api/CarrinhoCompras.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CarrinhoCompras.Api.dll"]