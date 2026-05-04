Solution:

My solution for this project was a small microservice weather API built using ASP.NET Core. The flow of operations in my solution was: a controller passes data to a handler, which passes it to an API integration service to retrieve data from external APIs. The service deserializes the data into DTOs, which the handler receives and maps for output before returning the data to the controller.

For exception handling, I implemented Exception Handling Middleware that catches exceptions occurring at runtime and returns appropriate errors (400 or 500 respectively). My solution prioritized scalability, allowing new endpoints to be easily implemented and different data fields from external APIs to be processed using provider-specific DTOs for geolocation or weather data from Open-Meteo.

I organized my project structure into the following components:
Controllers/ - contain the HTTP endpoints and handle requests
Handlers/Queries/ - contain CQRS query handlers
Handlers/Services/ - contain business logic and external API integration
Models/ - contain domain entities and data transfer objects
Middlewares/ - contains middleware for centralized exception handling
Mappings/ - AutoMapper profiles for transforming data between layers
The remaining files are configuration files that set up the application

Key Designs:

I chose CQRS with MediatR for scalability over simplicity. While it adds complexity, it allows multiple handlers for different operations without modifying existing code.

DTOs were used for separation of concerns and to expose only the necessary data in endpoint responses.

Exception Handling Middleware was used for centralized error handling of runtime exceptions.

The API Integration Service was used to abstract external APIs, process their responses, and extract only the data needed by the handler.

AutoMapper was used for transforming data between layers and mapping DTOs to models.

In WeatherModel, the status property uses an abstract class type to support different logic for different weather statuses if needed in the future.

The ApiConfiguration class was created to store and manage different API URLs from configuration settings rather than hardcoding them in the code, enabling scalability and flexibility.

The UriExtensions class was created to simplify building query parameters for external API URLs.

DependencyInjection.cs was created as a separate file to keep Startup.cs clean and maintainable, though it requires additional configuration upfront.

Unit testing:

For my solution to be unit tested it would need to implement mocks for the HttpClient and the API Integration Service. This way the tests could use the methods to get the values without routing to the external APIs, because the mocked HttpClient would intercept requests before they reach the external API. By using a mocking framework like Moq, a mock HttpMessageHandler could be created to intercept HTTP requests. When the API Integration Service calls the HttpClient to fetch geolocation data, the mock would return predefined geolocation data instead of calling the real Open-Meteo API. Similarly, weather API calls would be mocked to return predefined weather data.

Lambda Function URL

Public url: https://yjhlqi3vvj4hq2sqw265wuw5sq0vxjsy.lambda-url.eu-north-1.on.aws
Endpoint url: https://yjhlqi3vvj4hq2sqw265wuw5sq0vxjsy.lambda-url.eu-north-1.on.aws/api/weather/{name}

Path Parameter: name - the name of the city for which to retrieve weather data

Example Request 1: GET https://yjhlqi3vvj4hq2sqw265wuw5sq0vxjsy.lambda-url.eu-north-1.on.aws/api/weather/Krakow
Example Response 1: {"cityName":"Krakow","currentTemp":26,"currentTime":"2026-05-04T17:15:00","status":"Warm"}

Example Request 2: GET https://yjhlqi3vvj4hq2sqw265wuw5sq0vxjsy.lambda-url.eu-north-1.on.aws/api/weather/Rzeszow
Example Response 2: {"cityName":"Rzeszow","currentTemp":25.5,"currentTime":"2026-05-04T17:30:00","status":"Warm"}

New Weather Provider Added:

If a new weather provider were to be added to this project, it would simply require adding new API URLs to the appsettings files and adding a new provider configuration to the ApiConfiguration class. Additionally, the new provider would need its own API Integration Service and its own DTOs for deserializing the API responses. A new handler would also be needed, or the existing handler would need to be modified to accept a parameter that specifies which weather provider to use. New services could be easily added to the dependency injection container and tested using unit tests in the same way as the current provider.

If I had more time, I would improve the Exception Handling Middleware by creating separate classes for each exception type with more descriptive error messages. I would also spend more time refactoring the API Integration Service to separate the geolocation data retrieval logic from the weather data retrieval logic, making it easier to add different geolocation methods in the future. Additionally, I would improve the WeatherService to determine status display names using a cleaner approach instead of multiple if-else statements. Finally, I would add more endpoints to retrieve additional data from the external APIs to provide more comprehensive weather information beyond just temperature.