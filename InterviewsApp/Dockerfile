FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InterviewsApp.WebAPI/InterviewsApp.WebAPI.csproj", "InterviewsApp.WebAPI/"]
COPY ["InterviewsApp.Core/InterviewsApp.Core.csproj", "InterviewsApp.Core/"]
COPY ["InterviewsApp.Data/InterviewsApp.Data.csproj", "InterviewsApp.Data/"]
RUN dotnet restore "InterviewsApp.WebAPI/InterviewsApp.WebAPI.csproj"
COPY . .
WORKDIR "/src/InterviewsApp.WebAPI"
RUN dotnet build "InterviewsApp.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InterviewsApp.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterviewsApp.WebAPI.dll"]