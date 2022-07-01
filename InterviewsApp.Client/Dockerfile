FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InterviewsApp.WebApp/InterviewsApp.WebApp.csproj", "InterviewsApp.WebApp/"]
RUN dotnet restore "InterviewsApp.WebApp/InterviewsApp.WebApp.csproj"
COPY . .
WORKDIR "/src/InterviewsApp.WebApp"
RUN dotnet build "InterviewsApp.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InterviewsApp.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterviewsApp.WebApp.dll"]