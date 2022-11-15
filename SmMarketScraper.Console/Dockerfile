FROM mcr.microsoft.com/dotnet/sdk:7.0 as builder

WORKDIR /build

COPY ./../ .

RUN dotnet publish SmMarketScraper.Console --framework net7.0 --configuration Release -o ./dist  
    
FROM mcr.microsoft.com/dotnet/runtime:7.0 as final

WORKDIR /app

COPY --from=builder /build/dist .

CMD ["./SmMarketScraper.Console"]