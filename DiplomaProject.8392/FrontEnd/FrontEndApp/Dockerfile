#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src
COPY ["FrontEnd/FrontEndApp/FrontEndApp.csproj", "FrontEnd/FrontEndApp/"]
RUN dotnet restore "FrontEnd/FrontEndApp/FrontEndApp.csproj"
COPY . .
WORKDIR "/src/FrontEnd/FrontEndApp"
RUN dotnet build "FrontEndApp.csproj" -c Release -o /app/build
FROM build-env AS publish
RUN dotnet publish "FrontEndApp.csproj" -c Release -o /app/publish
FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80