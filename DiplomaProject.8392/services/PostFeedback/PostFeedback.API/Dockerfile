#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DiplomaProject.8392.sln", ""]
COPY ["docker-compose.dcproj", ""]

COPY ["Integration/RabbitMQEventBus/RabbitMQEventBus.csproj", "integration/RabbitMQEventBus/"]
COPY ["Integration/EventBus/EventBus.csproj", "integration/EventBus/"]
COPY ["BaseLibraries/Grpc.Base/Grpc.Base.csproj", "BaseLibraries/Grpc.Base/"]
COPY ["BaseLibraries/Domain.Logic.Base/Domain.Logic.Base.csproj", "BaseLibraries/Domain.Logic.Base/"]
COPY ["BaseLibraries/DAL.Base/DAL.Base.csproj", "BaseLibraries/DAL.Base/"]
COPY ["Gateway/APIGateway/APIGateway.csproj", "gateway/APIGateway/"]
COPY ["FrontEnd/FrontEndApp/FrontEndApp.csproj", "FrontEnd/FrontEndApp/"]
COPY ["Services/Account/Account.Utilities/Account.PasswordHandling/Account.PasswordHandling.csproj", "services/Account/Account.Utilities/Account.PasswordHandling/"]
COPY ["Services/Account/Account.Domain.Logic/Account.Domain.Logic.csproj", "services/Account/Account.Domain.Logic/"]
COPY ["Services/Account/Account.Domain/Account.Domain.csproj", "services/Account/Account.Domain/"]
COPY ["Services/Account/Account.DAL/Account.DAL.EF/Account.DAL.EF.csproj", "services/Account/Account.DAL/Account.DAL.EF/"]
COPY ["Services/Account/Account.API/Account.API.csproj", "services/Account/Account.API/"]
COPY ["Services/Booking/Booking.DAL/Booking.DAL.EF/Booking.DAL.EF.csproj", "services/Booking/Booking.DAL/Booking.DAL.EF/"]
COPY ["Services/Booking/Booking.Domain/Booking.Domain.csproj", "services/Booking/Booking.Domain/"]
COPY ["Services/Booking/Booking.Domain.Logic/Booking.Domain.Logic.csproj", "services/Booking/Booking.Domain.Logic/"]
COPY ["Services/Booking/Booking.API/Booking.API.csproj", "services/Booking/Booking.API/"]
COPY ["Services/PostFeedback/PostFeedback.DAL/Post.DAL.EF/PostFeedback.DAL.EF.csproj", "services/PostFeedback/PostFeedback.DAL/Post.DAL.EF/"]
COPY ["Services/PostFeedback/PostFeedback.Domain/PostFeedback.Domain.csproj", "services/PostFeedback/PostFeedback.Domain/"]
COPY ["Services/PostFeedback/PostFeedback.Domain.Logic/PostFeedback.Domain.Logic.csproj", "services/PostFeedback/PostFeedback.Domain.Logic/"]
COPY ["Services/PostFeedback/PostFeedback.API/PostFeedback.API.csproj", "services/PostFeedback/PostFeedback.API/"]

RUN dotnet restore "DiplomaProject.8392.sln"

COPY ["Services/Account/Account.API/.", "services/Account/Account.API/"]
COPY ["Services/Account/Account.Domain/.", "services/Account/Account.Domain/"]
COPY ["Services/Account/Account.Domain.Logic/.", "services/Account/Account.Domain.Logic/"]
COPY ["Services/Account/Account.DAL/Account.DAL.EF/.", "services/Account/Account.DAL/Account.DAL.EF/"]
COPY ["Services/Account/Account.Utilities/Account.PasswordHandling/.", "services/Account/Account.Utilities/Account.PasswordHandling/"]
COPY ["BaseLibraries/Grpc.Base/.", "BaseLibraries/Grpc.Base/"]
COPY ["BaseLibraries/Domain.Logic.Base/.", "BaseLibraries/Domain.Logic.Base/"]
COPY ["BaseLibraries/DAL.Base/.", "BaseLibraries/DAL.Base/"]
COPY ["Integration/RabbitMQEventBus/.", "integration/RabbitMQEventBus/"]
COPY ["Integration/EventBus/.", "integration/EventBus/"]
COPY ["Gateway/APIGateway/.", "gateway/APIGateway/"]
COPY ["FrontEnd/FrontEndApp/.", "FrontEnd/FrontEndApp/"]
COPY ["Services/Booking/Booking.DAL/Booking.DAL.EF/.", "services/Booking/Booking.DAL/Booking.DAL.EF/"]
COPY ["Services/Booking/Booking.Domain/.", "services/Booking/Booking.Domain/"]
COPY ["Services/Booking/Booking.Domain.Logic/.", "services/Booking/Booking.Domain.Logic/"]
COPY ["Services/Booking/Booking.API/.", "services/Booking/Booking.API/"]
COPY ["Services/PostFeedback/PostFeedback.DAL/Post.DAL.EF/.", "services/PostFeedback/PostFeedback.DAL/Post.DAL.EF/"]
COPY ["Services/PostFeedback/PostFeedback.Domain/.", "services/PostFeedback/PostFeedback.Domain/"]
COPY ["Services/PostFeedback/PostFeedback.Domain.Logic/.", "services/PostFeedback/PostFeedback.Domain.Logic/"]
COPY ["Services/PostFeedback/PostFeedback.API/.", "services/PostFeedback/PostFeedback.API/"]
WORKDIR "/src/services/PostFeedback/PostFeedback.API"
RUN dotnet build "PostFeedback.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostFeedback.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostFeedback.API.dll"]
