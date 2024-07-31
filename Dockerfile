FROM mcr.microsoft.com/dotnet/sdk AS build-env
WORKDIR /app
COPY . .
RUN dotnet restore ./TrustEze/TrustEze.csproj
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0



WORKDIR /app
COPY --from=build-env /app/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "./TrustEze.dll"]