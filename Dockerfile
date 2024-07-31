FROM mcr.microsoft.com/dotnet/sdk AS build-env
WORKDIR /app
COPY . .
RUN dotnet restore ./TrustEze/TrustEze.csproj
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0



WORKDIR /app
COPY --from=build-env /app/out ./

EXPOSE 8080
EXPOSE 80
EXPOSE 443
RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "./TrustEze.dll"]