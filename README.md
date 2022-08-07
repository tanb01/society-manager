# Society Manager

## Introduction

A small project for my internship of 2021, the main objective was to get familiarized with technologies such as [ASP.NET Core](https://github.com/aspnet/Home), [Entity Framework Core 3.1](https://docs.microsoft.com/en-us/ef/core/) (server-side) and [Vue.js](https://vuejs.org/) (client-side).

The SPA (Single Page Application) uses basic CRUD operations for managing a society.
The API is built with `ASP.NET Core` and the client-side with `Vue.js`.

The current application uses an In-Memory Database but can be switched to a SQL Server Database if desired.

Made with:

- [ASP.NET Core](https://github.com/aspnet/Home)
- [Entity Framework Core 3.1](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)
- [Vue.js](https://vuejs.org/)
- [Vuex](https://vuex.vuejs.org/)
- [Vuetify](https://vuetifyjs.com/en/)
- [Auth0](https://auth0.com/)

Note: IIExpress is required to run the API in local. The My Societies page is currently inaccessible (it requires Auth0 authentication and permissions)

## Requirements

- [Visual Studio](https://visualstudio.microsoft.com/)
- [npm](https://www.npmjs.com/get-npm)
- [IISExpress](https://www.npmjs.com/get-npm)
- [Auth0](https://auth0.com/)

## Installation

Navigate to the the `society-manager-ui` folder and use npm to install the node modules with:

  ```bash
  npm install
  ```

## Getting Started

1. To start the application the `SocietyManager.sln` project must be opened with Visual Studio. Firstly, start by configuring the `launchSettings.json` file located under `Properties` in the `SocietyManager` folder. It should look something like this:

```js
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:3571/",
      "sslPort": 44364
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": false,
      "launchUrl": "api/societies",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "SocietyManager": {
      "commandName": "Project",
      "launchBrowser": false,
      "launchUrl": "api/societies",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    }
  }
}
```
2. Meanwhile under the `society-manager-ui` folder, add the `.env` which will contain the API's url, for example `https://localhost:44364/api` (the same SSL Port defined in `launchSettings.json`). Finally, add the `auth_config.json` by following the respective `.example` file, the information can be found on [Auth0](https://auth0.com/).

3. Now you can start up the server with `IIS Express`.

4. To start the vue application, use the following command from within the `society-manager-ui` folder:

    ```bash
    npm run serve
    ```

5. Then open the following link in your browser: <http://localhost:8080/>

To view the API's documentation start the server-side (1.) then open the following link in your browser: <https://localhost:44364/swagger/index.html>
