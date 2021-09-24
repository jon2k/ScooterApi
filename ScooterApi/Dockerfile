FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ScooterApi/ScooterApi.csproj", "ScooterApi/"]
COPY ["ScooterApi.Data/ScooterApi.Data.csproj", "ScooterApi.Data/"]
COPY ["ScooterApi.Domain/ScooterApi.Domain.csproj", "ScooterApi.Domain/"]
COPY ["ScooterApi.Service/ScooterApi.Service.csproj", "ScooterApi.Service/"]
COPY ["ScooterApi.Messaging.Send/ScooterApi.Messaging.Send.csproj", "ScooterApi.Messaging.Send/"]
COPY ["Tests/ScooterApi.Test/ScooterApi.Test.csproj", "Tests/ScooterApi.Test/"]  
COPY ["Tests/ScooterApi.Service.Test/ScooterApi.Service.Test.csproj", "Tests/ScooterApi.Service.Test/"]  
COPY ["Tests/ScooterApi.Data.Test/ScooterApi.Data.Test.csproj", "Tests/ScooterApi.Data.Test/"] 
COPY ["ScooterApi/nuget.config", ""]
COPY ["*.props", "./"]

ARG PAT=localhost
RUN sed -i "s|</configuration>|<packageSourceCredentials><MicroserviceDemoNugets><add key=\"Username\" value=\"PAT\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /></MicroserviceDemoNugets></packageSourceCredentials></configuration>|" nuget.config

RUN dotnet restore "ScooterApi/ScooterApi.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/ScooterApi.Test/ScooterApi.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/ScooterApi.Service.Test/ScooterApi.Service.Test.csproj" --configfile "./nuget.config"
RUN dotnet restore "Tests/ScooterApi.Data.Test/ScooterApi.Data.Test.csproj" --configfile "./nuget.config"
COPY . .

RUN dotnet build "ScooterApi/ScooterApi.csproj" -c Release -o /app/build --no-restore
RUN dotnet build "Tests/ScooterApi.Test/ScooterApi.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/ScooterApi.Service.Test/ScooterApi.Service.Test.csproj" -c Release --no-restore
RUN dotnet build "Tests/ScooterApi.Data.Test/ScooterApi.Data.Test.csproj" -c Release --no-restore


FROM build AS test  
ARG BuildId=localhost
LABEL test=${BuildId}
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/ScooterApi.Test/ScooterApi.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/ScooterApi.Service.Test/ScooterApi.Service.Test.csproj  
RUN dotnet test --no-build -c Release --results-directory /testresults --logger "trx;LogFileName=test_results3.trx" /p:CollectCoverage=true /p:CoverletOutputFormat=json%2cCobertura /p:CoverletOutput=/testresults/coverage/ -p:MergeWith=/testresults/coverage/coverage.json  Tests/ScooterApi.Data.Test/ScooterApi.Data.Test.csproj

FROM build AS publish
RUN dotnet publish "ScooterApi/ScooterApi.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScooterApi.dll"]