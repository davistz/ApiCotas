# Etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.sln ./

COPY ApiCotas.csproj ./

RUN dotnet restore ApiCotas.csproj  

COPY . ./

RUN dotnet publish ApiCotas.csproj -c Release -o out  # Especifica o projeto a ser publicado

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "ApiCotas.dll"] 