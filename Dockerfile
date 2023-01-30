FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
VOLUME [ "/backendwwwroot" ]
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DentOnline.WebAPI.dll"]