

**OrderManagementSystem**

 A distribuited, decoupled, and scalable order management system built using .NET 10.0. following the principles of microservices architecture. 
 The system is designed to handle high volumes of orders while ensuring reliability and maintainability.
 The system manages a product catalog and handles order processing, including order creation, updates, and notifications. It is built with a focus on modularity and separation of concerns, 
 allowing for easy maintenance and scalability.
The solution consists of the following projects:

- CatalogService: This project contains the core business logic for managing prodcuts. It includes services, repositories, and data models related to catalog management.
- OrderService : This project provides an API for interacting with the catalog service. It exposes endpoints for creating, updating, and retrieving orders.
- NotificationService: This project is responsible for sending notifications related to order events (order creation). 

 The microservices actuate in different ways, each service is responsible for a specific aspect of the order management process.
 CatalogService and OrderService operated completely isolated and operating with their own dedicated In-Memory Databases to ensure loose coupling and independent scalability. 
 The NotificationService is responsible for sending notifications related to order events, such as order creation.

## Technologies Used
