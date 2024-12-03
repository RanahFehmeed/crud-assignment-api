
# crud-assignment-api

This is the backend API for the crud-assignment-api, built with **.NET 8**.

## Installation

1. Clone the repository:
    ```bash
    git clone <repository-url>
    ```

2. Navigate to the backend directory:
    ```bash
    cd ProductManagementAPI
    ```

3. Restore the project dependencies:
    ```bash
    dotnet restore
    ```

4. Build the project:
    ```bash
    dotnet build
    ```

## Configuration

Ensure that the backend is configured to allow Cross-Origin Resource Sharing (CORS) from the frontend application.

### CORS Configuration Example:
In `Program.cs`, enable CORS like this:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173") // Frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});
