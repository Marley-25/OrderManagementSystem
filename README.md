# OrderManagementSystem
A simplified microservices based e-commerce system. This project consist of CatalogService, OrderService and NotificationService.

 A distribuited, decoupled, and scalable order management system built using .NET 10.0. following the principles of microservices architecture. 
 The system is designed to handle high volumes of orders while ensuring reliability and maintainability.
 The system manages a product catalog and handles order processing, including order creation, updates, and notifications. 
 It is built with a focus on modularity and separation of concerns, allowing for easy maintenance and scalability.

**Architectural Overview**

Theproject is split into 3 independent microservices: 

- CatalogService
- OrderService
- NotificationService

## Technologies Used

- Framework: .NET 10/ ASP.NET Core Web API
- Database: Entity Framework Core ( In-Memory Database Provider)
- HTTP Client: IHTTPClientFactory for inter-service communication 
_ Testing Tool: Bruno API Explorer 

In Details: 

- Catalog Service: (http://localhost:5171) : Manages the product inventory, availability and pricing. Uses In- Memory Dtabase.
- Order Service: (http://localhost:7202): Processes order placement requests ncontaining multiple items, orchestrates inventory 
				 checks with the CatalogService and broadcasts order confirmations. Uses an In Memory database.
_ Notification Service: (http://localhost:5060): Receives order updates asynchronously and logs events to both an in-memroy database and the sstem console.

The communication between the Order Service and the Notification Service implements a Fire and Forget patter, 
ensuring that order fulfillment is never blocked by notification processing delays.

**API References**

Each microservice handles specific CRUD (Create, Read, Update and Delete) responsabilities.
Because the services are decoupled, with operations in one service often trigger safe, downstream updates in another via HTTP.

Catalog Service
- POST (Create): Allows the addition of new products to the inventory, requiring: Name, Price and Quantity (initail stock level).
- GET (Read): Provides endepoints to fetch the full product list or retrieve details of a specific product with Guid Id.
- PUT (Update): Exposes a specialised operational enedpoitn to reduce stock levels when purchases are made: 
- DELETE (Delete): Useful to delete products from catalog. 

Order Service: 
_ POST: Receives a bulk request containing an array of products Guid and quantities.
		It reads products details from catalog Service
		It executes an Update request to decrease the catalog inventory stock
		It creates and persists the order local database.
		It triggers an asynchronous Create operation in the Notification Service using an Fire and Forget pattern

- GET: Allows users to read historical records of orders


Notification Service 
_POST: Receives the event sent by the Order Service with the creation of a new Order. 
		It saves the notification log in its local database repository and prints a formatted lo in console.


**Running All Microservices simultaneously**

To avoid opening separate terminal windows for each individual micorservice. 
You can leverage the .NET CLI multi-project run feature from the root folder of your solution

Ensure your solution file (".sln") is present in the rrot directory and taht all 3 micorservices are corrected referenced inside it.






