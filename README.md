# CurrencyConverter
Download the project and replace the variables "YOUR_DB_CONNECTION" with your own database connection and "YOUR_API_KEY" with the apiKey from fixer.io
It contains 2 projects.
CurrencyConverter is a console application created in .net core which is used to convert currency for a specific amount and the partucular date.
CurrencyLoader is a worker service which runs every 24 hours and loads all the live currencyand saves in database. The base currency is EUR as we are not passing any value to the api url and "EUR" is the default base currency.
d
