FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["F19ITONK.ASPNETCore.MicroService.FrontEnd/F19ITONK.ASPNETCore.MicroService.FrontEnd.csproj", "F19ITONK.ASPNETCore.MicroService.FrontEnd/"]
RUN dotnet restore "F19ITONK.ASPNETCore.MicroService.FrontEnd/F19ITONK.ASPNETCore.MicroService.FrontEnd.csproj"
COPY . .
WORKDIR "/src/F19ITONK.ASPNETCore.MicroService.FrontEnd"
RUN dotnet build "F19ITONK.ASPNETCore.MicroService.FrontEnd.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "F19ITONK.ASPNETCore.MicroService.FrontEnd.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "F19ITONK.ASPNETCore.MicroService.FrontEnd.dll"]