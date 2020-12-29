## :eyeglasses: Project Introduction

**Residental Manager** is my defense project for **ASP.NET Core** course at [SoftUni](https://softuni.bg/ "SoftUni") (October-December 2020). The project is a system for managing residential properties.

## :pencil2: Overview

The **Residental Manger** allows you to manage all income, expenses, monthly fees of residential property.

The system has 2 user roles - **Administrator** and **User**. Administrator can create RealEstate Properties, to set monthly fees, to add and edit expences, to manage residents and pets for created properties. Administrator can generate monthly property taxes and to manage payments. Confirmation email can be sent for each paid tax. Administator can give access to a user's property through an administrative panel. Users, after approval by an administrator, can see taxes for their properties. They can see statistics and monthly costs for their residential property.


When writing the code I tried to follow the best practices for **Object Oriented design** and **high-quality code** for the Web application. I also tried to comply the **OOP principles** like data encapsulation, inheritance, abstraction and polymorphism and follow the principles of strong cohesion and loose coupling. I used **repository pattern** for better separation of responsibilities and easier testing of services.



## :hammer: Built With
- ASP.NET Core 5.0 (https://docs.microsoft.com/en-us/dotnet/core/dotnet-five "Core 5.0")
- Server-Side [Razor view engine](https://en.wikipedia.org/wiki/ASP.NET_Razor "Razor view engine")
- ASP.NET CORE view components
- ASP.NET Core areas
- MSSQL Server
- SendGrid
- Bootstrap
- [xUnit](https://xunit.net "xUnit") test library

## :wrench: DB Diagram
![alt text](https://i.ibb.co/kSg3ywC/Db-Diagram.jpg)

## :man_student: License
Copyright (c) 2020 Victor Stanoev
