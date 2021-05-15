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

Note: The My Societies page is currently inaccessible (it requires Auth0 authentication and permissions)

## Requirements

- [Visual Studio](https://visualstudio.microsoft.com/)
- [npm](https://www.npmjs.com/get-npm)

## Installation

Navigate to the the `society-manager-ui` folder and use npm to install the node modules with:

  ```bash
  npm install
  ```

## Getting Started

1. To start the application the `SocietyManager.sln` project must be opened with Visual Studio.
From there you can start up the server with `IIS Express`.

2. To start the vue application, use the following command from within the `society-manager-ui` folder:

    ```bash
    npm run serve
    ```

3. Then open following link in your browser: <http://localhost:8080/>

To view the API's documentation start the server-side (1.) then open the following link in your browser: <https://localhost:44364/swagger/index.html>
