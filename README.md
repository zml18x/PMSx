# PMS - Property Management System API

Welcome to the **PMS** (Property Management System) API repository! This repository contains the backend implementation for managing hotels and properties using C# ASP.NET Core. The API interacts with a MS SQL Server database, which is containerized using Docker. The repository also includes a Dockerfile for setting up the database container.

## Table of Contents

- [Introduction](#introduction)
- [Controllers](#controllers)
- [Endpoints](#endpoints)
- [Database](#database)
- [Unit Tests](#unit-tests)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The Property Management System (PMS) API is designed to facilitate hotel and property management tasks. The API is built on C# ASP.NET Core framework and follows RESTful principles. It allows users to perform various operations related to user accounts and property management.

## Controllers

The PMS API consists of two main controllers:

1. **AccountController**: This controller handles user account-related operations.
   - `RegisterAsync`: Register a new user account.
   - `LoginAsync`: Authenticate user credentials and issue JWT tokens.
   - `GetAsync`: Retrieve user account information.
   - `GetProfileAsync`: Retrieve detailed user profile information.
   - `UpdateProfileAsync`: Update user profile details.

2. **PropertyController**: This controller manages property-related operations.
   - `AddNewPropertyAsync`: Create a new property (e.g., hotel).
   - `GetAllPropertiesAsync`: Get a list of all properties owned by a user.
   - `GetPropertyAsync`: Retrieve information about a specific property.
   - `GetPropertyAddressAsync`: Retrieve the address of a specific property.
   - `AddRoomsAsync` : Adds the indicated number of rooms to the property.
   - `GetAllRoomsAsync` : Returns a list of all rooms belonging to a given property.
   - `GetRoomAsync` : Returns the room with the given id with all data.
   - `AddAdditionalServicesAsync` : Adding additional services (e.g. restaurant) to the hotel or rooms.

## Endpoints

For detailed information about each endpoint and their request/response formats, please refer to the API documentation or code comments.

## Database

The API interacts with a MS SQL Server database, which is containerized using Docker. The Dockerfile for the database setup is provided in the repository.

## Unit Tests

The repository includes unit tests for the WebApi, Infrastructure, and Core layers. These tests ensure the correctness of the application's components.

## Getting Started

To run the PMS API locally, follow these steps:

1. Clone this repository to your local machine.
2. Install the necessary dependencies and tools (e.g., .NET Core, Docker).
3. Build the solution.
4. Run the database container using the provided Dockerfile.
5. Configure the database connection settings in the appsettings.json file.
6. Run the API using the .NET CLI or your preferred IDE.

## Usage

The PMS API provides powerful tools for managing hotels and properties. You can integrate the API with your frontend applications or use tools like Postman to test the endpoints.

---

Thank you for choosing the PMS API for your property management needs! If you have any questions or feedback, please don't hesitate to reach out.
