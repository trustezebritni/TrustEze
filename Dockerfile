FROM mcr.microsoft.com/dotnet/aspnet:8.0


COPY . .
RUN dotnet restore TrustEze/TrustEze.csproj
RUN dotnet publish -c Release -o out
EXPOSE 80
ENTRYPOINT ["dotnet", "/app/out/TrustEze.dll"]
