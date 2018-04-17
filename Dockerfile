FROM microsoft/dotnet:2.0-sdk AS build-env
WORKDIR /app

COPY ./idefix.sln ./idefix.sln
COPY ./src/idefix/idefix.csproj ./src/idefix/idefix.csproj
COPY ./tests/idefix.Tests/idefix.Tests.csproj ./tests/idefix.Tests/idefix.Tests.csproj
RUN dotnet restore

COPY . ./
# run the tests
RUN dotnet test tests/idefix.Tests/idefix.Tests.csproj

# publish
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/dotnet:2.0-runtime 
WORKDIR /app

# copy only the app (not the tests)
COPY --from=build-env /app/src/idefix/out ./
ENTRYPOINT ["dotnet", "idefix.dll"]