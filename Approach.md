# Number to Words Converter: Approach Documentation

## Chosen Approach

For this **Number to Words Converter**, I chose to implement a C# ASP.NET Core MVC application with the following components:

1. An `INumberToWordsConvertService` interface and `NumberToWordsConvertService` implementation for the conversion logic.
2. An MVC architecture with a `ConvertController` to handle requests.
3. A simple HTML interface with jQuery for AJAX requests.
4. Unit tests with NUnit following a TDD (Test-Driven Development) approach.

### Reasons for this approach:

1. **C# and ASP.NET Core:** 
   - Requested in the requirements. (preferable)
   - Provides a robust, scalable, and maintainable web application framework.

2. **Interface-based Design (INumberToWordsConvertService):**

   - Promotes loose coupling and adheres to the Dependency Inversion Principle.
   - Allows for easy mocking in unit tests.
   - Facilitates future extensions or alternative implementations.

3. **Service Implementation (NumberToWordsConvertService):**
    - Encapsulates the conversion logic, promoting separation of concerns.
    - Utilizes dependency injection for logging, enhancing testability and flexibility.

2. **MVC Architecture:**
   - Separates concerns (Model, View, Controller) for better organization and maintainability.
   - Allows for easy testing of individual components.

3. **jQuery for AJAX:**
   - Provides a simple way to make asynchronous requests without page reloads.
   - Widely supported and easy to implement.

4. **Unit Testing with NUnit and TDD:**
    - Ensures code quality and correctness through automated tests.
    - Follows TDD principles to guide the development process.
    - NUnit provides a robust testing framework for .NET applications.

## Alternative Approaches Considered

1. **Java with Spring Framework**
    - **Pros**: Well-established and widely-used framework.
    - **Cons**: Not the preferred technology stack for this project.
    - **Decision**: While I'm very comfortable with this stack, I chose to align with the project requirements.

2. **Web API with Separate Front-end (React + C# .NET Core)**:
   - **Pros**: Clear separation of front-end and back-end, potential for multiple clients.
   - **Cons**: More complex setup, potentially overkill for this simple application.
   - **Decision**: Rejected as it adds unnecessary complexity for this specific task.

3. **DDD (Domain-Driven Design):**
    - **Pros**: Handles complex business logic effectively.
    - **Cons**: May be too complex for simple applications.
    - **Decision**: For this relatively simple number conversion task, DDD was deemed unnecessary.

By choosing the ASP.NET Core MVC approach, we've created a solution that meets all requirements while providing a solid foundation for potential future enhancements.