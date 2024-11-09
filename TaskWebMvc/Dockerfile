#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY *.csproj .
RUN dotnet restore

COPY . ./app/

COPY ./Database/mydatabase.db /app/Database/mydatabase.db
COPY ./appsettings.json /app
COPY ./appsettings.Production.json /app

RUN dotnet publish -c release -o /app --no-restore

RUN apt-get update && apt-get install -y sqlite3 libsqlite3-dev

ENV ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}
ENV Jwt__Key=${Jwt__Key}
ENV Jwt__Audience=${Jwt__Audience}
ENV Jwt__Issuer=${Jwt__Issuer}

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

RUN chmod -R 777 Database

EXPOSE 8080
ENTRYPOINT ["dotnet", "TaskWebMvc.dll"]