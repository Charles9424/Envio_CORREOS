#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MT_ENVIO_CORREOS/MT_ENVIO_CORREOS.csproj", "MT_ENVIO_CORREOS/"]
RUN dotnet restore "MT_ENVIO_CORREOS.csproj"
COPY . .
WORKDIR "/src/MT_ENVIO_CORREOS"
RUN dotnet build "MT_ENVIO_CORREOS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MT_ENVIO_CORREOS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MT_ENVIO_CORREOS.dll"]
