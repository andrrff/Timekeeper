```shell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Andrrff17112001" -p 1433:1433 --name sql1 --hostname sql1 -d   mcr.microsoft.com/mssql/server:2022-latest

docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Andrrff17112001" -Q "CREATE DATABASE TIMEKEEPERTESTS"
```