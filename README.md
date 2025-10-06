# ABookDB-v2

Projekty:
1) MVC pro UI, překlad dat do view modelů
  -pro zobrazování/editaci/přidávání pomocí view modelů (validace, security), při editaci/přidávání se to přeoží do db modelů a pošle na API pro zípis s bootstrapem, bude se starat o část authentikace/authorizace
2) API pro práci s daty + auth
  -konečně bych rád zprovoznil JWT bearer token, aby se kdyžtak MVC nemuselo ani ptát api na authentikaci/authorizaci, jinak basic CRUD API. 
3) Web scraper
  -htmlagilitypack pro získání dat, velká změna od toho co už jste viděli to nebude

Technologie: 
.NET core (MVC, API)
EF, migrations
MSSQL

