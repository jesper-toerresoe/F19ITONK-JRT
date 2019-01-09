FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["F19ITONK.ASPNETCore.MicroService.RESTAPI/F19ITONK.ASPNETCore.MicroService.RESTAPI.csproj", "F19ITONK.ASPNETCore.MicroService.RESTAPI/"]
RUN dotnet restore "F19ITONK.ASPNETCore.MicroService.RESTAPI/F19ITONK.ASPNETCore.MicroService.RESTAPI.csproj"
COPY . .
WORKDIR "/src/F19ITONK.ASPNETCore.MicroService.RESTAPI"
RUN dotnet build "F19ITONK.ASPNETCore.MicroService.RESTAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "F19ITONK.ASPNETCore.MicroService.RESTAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "F19ITONK.ASPNETCore.MicroService.RESTAPI.dll"]