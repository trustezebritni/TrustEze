FROM mcr.microsoft.com/dotnet/sdk AS build-env
WORKDIR /app
COPY . .
RUN dotnet restore ./TrustEze/TrustEze.csproj
RUN dotnet publish -c Release -o out
RUN dotnet dev-certs https --export-path ./dev-cert.pfx

FROM mcr.microsoft.com/dotnet/aspnet:8.0

#ENV CLIENT_ID
#ENV CLIENT_SECRET
#ENV COGNITO_URI
#ENV CALLBACK_URI

WORKDIR /app
COPY --from=build-env /app/out ./

EXPOSE 8080
EXPOSE 80
EXPOSE 443


ENTRYPOINT ["dotnet", "./TrustEze.dll"]