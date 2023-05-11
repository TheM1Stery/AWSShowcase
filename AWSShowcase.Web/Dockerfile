FROM node:20 as node
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY --from=node . .
WORKDIR /src
COPY ["AWSShowcase.Web.csproj", "."]
RUN dotnet restore "AWSShowcase.Web.csproj"
COPY . .
RUN dotnet build "AWSShowcase.Web.csproj" -c Release -o /app/build


FROM build as client
WORKDIR /src
COPY ["Client", "Client"]
WORKDIR /src/Client
RUN npm install -g pnpm
RUN pnpm install
RUN pnpm build


FROM client AS publish
WORKDIR /src
RUN dotnet publish "AWSShowcase.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AWSShowcase.Web.dll"]
