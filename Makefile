up:
	docker-compose up -d
db/migrations/add:
	dotnet ef migrations add $(name) --project SmMarketScraper.Infrastructure -- "User ID=postgres;Password=Password123!;Host=localhost;Port=15432;Database=SmMarket;"
docker/build:
	docker build --tag jasontalon.com/smmarketscraper.console:latest --file ./SmMarketScraper.Console/Dockerfile .
docker/run:	 
	docker run --env-file .env --rm jasontalon.com/smmarketscraper.console:latest
gcloud/tag:
	docker tag jasontalon.com/smmarketscraper.console:latest asia.gcr.io/sm-market-scraper/jasontalon.com/smmarketscraper.console:latest
gcloud/push:
	docker push asia.gcr.io/sm-market-scraper/jasontalon.com/smmarketscraper.console:latest
publish/console:
	dotnet publish SmMarketScraper.Console --runtime linux-x64 --framework net7.0 --configuration Release -o ./dist
	tar -zcvf SmMarketScraper.Console.tar.gz dist
#https://linuxize.com/post/how-to-install-postgresql-on-debian-10/
install/postgres:
	sudo apt update
	sudo apt install postgresql