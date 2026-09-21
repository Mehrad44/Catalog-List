# Catalog List

A simple **Product Catalog** web application built with **ASP.NET Core** and **Angular**.

This project demonstrates how a .NET backend can work together with an Angular frontend to provide a simple catalog management and browsing experience.

## 🛠️ Technologies

### Backend

* C#
* ASP.NET Core
* RESTful API
* Entity Framework Core

### Frontend

* Angular
* TypeScript
* HTML5
* CSS3

## 📁 Project Structure

```text
Catalog-List/
│
├── Catalog.Service/
│   └── ASP.NET Core backend and API
│
├── Catalog.Web/
│   └── Angular frontend
│
└── README.md
```

## ✨ Features

* Product catalog
* RESTful API
* Angular frontend
* ASP.NET Core backend
* Communication between Angular and .NET API
* Separation between frontend and backend applications

## 🚀 Getting Started

### Prerequisites

Make sure you have the following installed:

* [.NET SDK](https://dotnet.microsoft.com/download)
* [Node.js](https://nodejs.org/)
* Angular CLI

You can verify your installations with:

```bash
dotnet --version
node --version
ng version
```

### 1. Clone the repository

```bash
git clone https://github.com/Mehrad44/Catalog-List.git
cd Catalog-List
```

### 2. Run the Backend

Navigate to the backend project:

```bash
cd Catalog.Service
```

Restore the dependencies:

```bash
dotnet restore
```

Run the application:

```bash
dotnet run
```

The ASP.NET Core API will start locally.

### 3. Run the Frontend

Open another terminal and navigate to the Angular project:

```bash
cd Catalog.Web
```

Install the dependencies:

```bash
npm install
```

Start the Angular development server:

```bash
ng serve
```

Then open:

```text
http://localhost:4200
```

## 🔗 Architecture

The application follows a simple client-server architecture:

```text
┌─────────────────────┐
│                     │
│   Angular Frontend  │
│     Catalog.Web     │
│                     │
└──────────┬──────────┘
           │
           │ HTTP / REST API
           ▼
┌─────────────────────┐
│                     │
│   ASP.NET Core API  │
│   Catalog.Service   │
│                     │
└──────────┬──────────┘
           │
           ▼
      Data Storage
```

The Angular application communicates with the ASP.NET Core backend through HTTP requests and consumes the API endpoints provided by the service.

## 🎯 Purpose

The main purpose of this project is to practice building a full-stack application using the **.NET + Angular** stack.

It also provides practical experience with:

* Building REST APIs with ASP.NET Core
* Consuming APIs with Angular
* Structuring a backend and frontend application
* Working with Entity Framework Core
* Connecting frontend components to backend services

## 📚 Learning Goals

This project was created as a learning project while improving skills in:

* C# and .NET
* ASP.NET Core Web API
* Angular
* TypeScript
* Entity Framework Core
* RESTful API development
* Full-stack application architecture

## 👨‍💻 Author

**Mehrad Khavari**


---

⭐ If you find this project useful, feel free to star the repository.
