# Morgana's Trial Solution

## Overview
MorganasTrial is a .NET 9 solution that exposes a RESTful Web API for managing Umbraco CMS content and document types. It consists of three main components:
- **UmbracoBridge Web API**: Provides endpoints for content and document type management.
- **Umbraco CMS**: The content management system, extended with a custom "IsOK" endpoint under the Morgana group.
- **Aspire AppHost**: Orchestrates and connects the services, handling environment variables and service discovery.

## Prerequisites
- .NET 9 SDK
- Visual Studio 2022 or later

## Setup and Running Locally
1. Clone the repository:
   ```bash
   git clone https://github.com/cnabuin/MorganasTrial
   cd MorganasTrial
   ```
2. Set the startup project in Visual Studio:
   - It is recommended to set `MorganaTrial.AppHost` as the startup project for optimal operation. However, you can also run the projects individually without Aspire if needed.

3. (Optional) Configure environment variables for local runs (not necessary if running with Aspire):
   - `UMBRACO_BASE_ADDRESS`: This should match the Umbraco CMS address (e.g., `https://localhost:5001/`).
   - `UMBRACO_DELIVERY_API_KEY`: This is the key used for accessing the Umbraco Delivery API.

4. Build and run the solution:
   - In Visual Studio: Press F5.
   - Or via command line:
     ```bash
     dotnet run --project AppHost/AppHost.csproj
     ```
   - The Aspire dashboard will be available at `https://localhost:17024/` by default.

## Default URLs
- **Web API**: `https://localhost:7184/`
- **Umbraco CMS**: `https://localhost:5001/`
- **Umbraco Swagger UI**: `https://localhost:5001/umbraco/swagger/index.html?urls.primaryName=Umbraco+Management+API`

## Web API Endpoints & Examples

### 1. Health Check
- **GET** `/Healthcheck`
- **Calls**: `GET /umbraco/management/api/v1/health-check-group`
- **Example Response**:
  ```json
  {
    "total": 6,
    "items": [
      {"name": "Configuration"},
      {"name": "Data Integrity"},
      {"name": "Live Environment"},
      {"name": "Permissions"},
      {"name": "Security"},
      {"name": "Services"}
    ]
  }
  ```

### 2. Validate IsOk
- **GET** `/Validate?isOk={true|false}`
- **Example Response**:
  - **Status code 200**  
    ```json
    "It's Ok."
    ```
  - **Bad Request Response**
    ```json
    {
        "title": "It is not Ok.",
        "status": 400
    }
    ```

### 3. Create Document Type
- **POST** `/DocumentType`
- **Calls**: `POST /umbraco/management/api/v1/document-type`
- **Example Request Body**:
  ```json
  {
    "alias": "This is an alias",
    "name": "This is the Name",
    "description": "This is the description",
    "icon": "icon-notepad",
    "allowedAsRoot": true,
    "variesByCulture": false,
    "variesBySegment": false,
    "collection": null,
    "isElement": true
  }
  ```
- **Example Response**: 
  - **Success Response (Status Code: 200)**:
    ```json
    "e259b585-f64a-4930-a197-b6456fa18585"
    ```
  - **BadRequest Response**:
    ```json
    {
        "type": "ProblemDetails",
        "title": "Bad Request",
        "status": 400,
        "detail": "The request is invalid due to missing or incorrect parameters."
    }
    ```

### 4. Delete Document Type
- **DELETE** `/DocumentType/{id}`
- **Calls**: `DELETE /umbraco/management/api/v1/document-type/{id}`
- **Example**: 
  ```
  https://localhost:7184/DocumentType/c9a29642-3382-42b9-a0a3-ddc287a0bd4d
  ```
- **Example Response:**
  - **Success Response (Status Code: 200)**: 
    ```json
    {
        "status": 200
    }
    ```
  - **Not Found Response**:
    ```json
    {
        "type": "Error",
        "title": "Not Found",
        "status": 404,
        "detail": "The specified document type was not found",
        "operationStatus": "NotFound"
    }
    ```

### 5. ContentController Endpoints

#### Get All Content
- **GET** `/Content`
- **Calls**: `GET /umbraco/delivery/api/v2/content`
- **Example Request**: 
  ```
  https://localhost:7184/Content
  ```
- **Example Response**:
  ```json
  {
    "total": 2,
    "items": [
      {
        "id": "b1a1e1c2-1234-5678-9abc-def012345678",
        "name": "Home",
        "contentType": "home",
        "properties": {
          "title": "Welcome to the site",
          "bodyText": "This is the homepage."
        }
      },
      {
        "id": "c2b2f2d3-2345-6789-abcd-ef0123456789",
        "name": "About",
        "contentType": "about",
        "properties": {
          "title": "About Us",
          "bodyText": "Information about our company."
        }
      }
    ]
  }
  ```

#### Get Content by ID
- **GET** `/Content/{id}`
- **Calls**: `GET /umbraco/delivery/api/v2/content/item/{id}`
- **Example Request**: 
  ```
  https://localhost:7184/Content/b1a1e1c2-1234-5678-9abc-def012345678
  ```
- **Example Response**:
  ```json
  {
    "id": "b1a1e1c2-1234-5678-9abc-def012345678",
    "name": "Home",
    "contentType": "home",
    "properties": {
      "title": "Welcome to the site",
      "bodyText": "This is the homepage."
    }
  }
  ```
- **Not Found Response**:
  ```json
  {
    "type": "Error",
    "title": "Not Found",
    "status": 404,
    "detail": "The specified content item was not found"
  }
  ```

## Verifying the Setup
1. **Check Aspire Dashboard**: Ensure both applications are running and healthy at `https://localhost:17024/`.
2. **Test Endpoints**: Use a REST client (e.g., Bruno, Scalar) to call the endpoints above and confirm expected responses.
3. **Run Unit Tests**: Open Test Explorer in Visual Studio and run all tests in the MorganasTest project to verify controller and service logic.

## Notes
- The passwords and API keys are embedded in the code for simplification. In a real solution, they should be managed with secrets and not uploaded to a repository.
