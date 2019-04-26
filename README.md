# React and ASP.NET Core Web API

This project was initially created with the "React" Visual Studio template.

This template provides the following:
* A basic react app in the `ClientApp` directory.
* A simple Web API controller, `SampleDataController`.
* Configuration in `Startup.cs` for serving static files and for serving `index.html` by default to allow for client-side routing.
* Dev and Production configuration for building the react app.

Things that ere added:
* React components to interact with the API.
* Entity Framework configuration in `Startup.cs`.
* An `ApplicationDbContext` that inherits from `IdentityDbContext`.
* Some model classes that are mapped to tables using Entity Framework.
* Migrations
* Identity Framework configuration in `Startup.cs`.
* A custom `ApplicationUser` model.
* JSON Web Token authentication configuration in `Startup.cs`. 
    > Further Reading: [jwt.io](https://jwt.io/), [JWT in ASP.NET Core](https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/), [JWT on Wikipedia](https://en.wikipedia.org/wiki/JSON_Web_Token)
* Client-side login, JWT token storage and use.
