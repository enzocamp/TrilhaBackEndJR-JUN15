#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY *.csproj .
RUN dotnet restore

COPY . ./aspnetapp/
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Criando o diret�rio onde o banco de dados ser� criado
RUN mkdir -p /app/db

COPY --from=build /app ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "TaskWebMvc.dll"]

