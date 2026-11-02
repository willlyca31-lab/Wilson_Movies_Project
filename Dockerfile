# Stage 1: Build the ASP.NET Core MVC application.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies.
COPY ["Wilson_Movies_Project.csproj", "./"]
RUN dotnet restore "Wilson_Movies_Project.csproj"

# Copy source code and publish the application.
COPY . .
RUN dotnet publish "Wilson_Movies_Project.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run the published application.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Listen on Render's web service port.
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "Wilson_Movies_Project.dll"]
