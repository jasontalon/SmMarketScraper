
up:
	docker-compose up -d
db/migrations/add:
	dotnet ef migrations add $(name) --project SmMarketScraper.Infrastructure -- "User ID=postgres;Password=Password123!;Host=localhost;Port=15432;Database=SmMarket;"
docker/build:
	docker build --tag jasontalon.com/smmarketscraper.console --file ./SmMarketScraper.Console/Dockerfile .
docker/run:	 
	docker run --env-file .env --rm jasontalon.com/smmarketscraper.console 