# ResultService (.NET 8 - CQRS Pattern)

This project is a sample **.NET 8** Web API that follows the **CQRS (Command Query Responsibility Segregation)** pattern, created for testing purposes for **Sovos**.

The application is structured to cleanly separate read and write concerns, supporting maintainability, testability, and scalability.

---

## 🚀 Running the Application

1. Navigate to the API project:

    ```bash
    cd src/SovosTest.Api
    ```

2. Run the application:

    ```bash
    dotnet run
    ```

3. The API should be available at `https://localhost:5001` or `http://localhost:5000`.

---

## 🔐 Generating a JWT Token

To generate a basic JWT token with read/write access, run the following commands:

```bash
cd src/SovosTest.Api
dotnet user-jwts create --name test_user --scope "result-api.read" --scope "result-api.write" --audience result-api --claim "email=test@sovos.com"