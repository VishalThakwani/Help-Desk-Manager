# HelpDesk Ticket Management System

A web-based HelpDesk Ticket Management System developed using ASP.NET Core Web API, ASP.NET Core MVC, Entity Framework Core, SQL Server, xUnit, and GitHub.

## Project Overview

The HelpDesk Ticket Management System provides a centralized platform for creating, managing, tracking, and resolving technical support tickets.

The application uses an ASP.NET Core MVC frontend that communicates with an ASP.NET Core Web API. The API handles ticket operations and communicates with SQL Server through Entity Framework Core and the Repository Pattern.

## Technologies Used

- C#
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Bootstrap
- xUnit
- Swagger
- Git/GitHub
- Visual Studio 2022

## Project Structure

### HelpDesk.Api

ASP.NET Core Web API project responsible for providing REST API endpoints and handling backend ticket operations.

### HelpDesk.Mvc

ASP.NET Core MVC project responsible for the user interface and communicating with the Web API through the Ticket Service.

### HelpDesk.Tests

xUnit test project used for testing and validating application functionality.

## Main Features

- Dashboard
- View all tickets
- Create new tickets
- View ticket details
- Edit tickets
- Delete tickets
- Filter tickets by status
- REST API
- SQL Server database
- Repository Pattern
- Entity Framework Core

## Architecture

The application follows a layered architecture:

User → ASP.NET Core MVC → Ticket Service → ASP.NET Core Web API → Repository Pattern → Entity Framework Core → SQL Server

The response follows the reverse path back to the MVC application and is displayed to the user.

## Application Workflow

1. User opens the HelpDesk dashboard.
2. User creates or selects a ticket.
3. MVC sends the request to TicketService.
4. TicketService communicates with the Web API.
5. The API processes the request through the repository layer.
6. Entity Framework Core communicates with SQL Server.
7. The response is returned through the API and TicketService.
8. MVC displays the updated information to the user.

## Ticket Operations

The system supports the following ticket operations:

- Create Ticket
- View Ticket
- View Ticket Details
- Edit Ticket
- Delete Ticket
- Filter Tickets by Status

## Development Guidelines

- Use meaningful naming conventions.
- Follow the Repository Pattern.
- Use asynchronous methods where appropriate.
- Implement appropriate exception handling.
- Maintain clean and readable code.
- Use meaningful commit messages.

## Repository

This repository contains the complete HelpDesk Management solution, including the API, MVC application, test project, solution file, and project documentation.
