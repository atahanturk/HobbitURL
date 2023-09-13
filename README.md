# HobbitURL

HobbitURL is a url shortening service Web API.
To run the API, you need to first navigate to the path where the .csproj file is located in the terminal. First you should restore dependicies with:
- dotnet restore
Then you need to do database setup. You can see information about database on appsettings.json file. After initializing database, you should update migrations with:
- dotnet ef update database
And finally, you can run the application with:
- dotnet run

With this api, you can create short url adresses from long url adresses, store them in a database and query them whenever you need.
