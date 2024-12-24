# **API Documentation: Products Management API**

## **Base URL**
```
http://localhost:5038
```

---

## **Endpoints**

### **1. Get All Products**
Retrieve all products from the database.

- **Endpoint:**
  ```
  GET /api/v1/products/GetAllProducts
  ```

- **Response:**
  - **200 OK**
    ```json
    [
      { "id": 1, "name": "Product1", "Description": "Product1 Description", "price": 100 },
      { "id": 2, "name": "Product2", "Description": "Product2 Description", "price": 200 }
    ]
    ```
  - **404 Not Found**
    ```
    No products found.
    ```
  - **400 Bad Request**
    ```
    Error message
    ```

---

### **2. Get Product by ID**
Retrieve a specific product by its ID.

- **Endpoint:**
  ```
  GET /api/v1/products/GetProductsByid?id={id}
  ```

- **Query Parameters:**
  - `id` *(required)* - The unique identifier of the product.

- **Response:**
  - **200 OK**
    ```json
    { "id": 1, "name": "Product1", "Description": "Product1 Description", "price": 100 }
    ```
  - **404 Not Found**
    ```
    Product not found.
    ```
  - **400 Bad Request**
    ```
    ID is required.
    ```

---

### **3. Insert a Product**
Add a new product to the database.

- **Endpoint:**
  ```
  POST /api/v1/products/InsertProducts
  ```

- **Headers:**
  - `Content-Type: application/json`

- **Body:**
  ```json
  {
    "name": "Product1",
    "Description": "Product1 Description",
    "price": 100
  }
  ```

- **Response:**
  - **200 OK**
    ```
    Product1 was inserted successfully.
    ```
  - **400 Bad Request**
    ```
    Product data is invalid.
    ```

---

### **4. Update a Product**
Update the details of an existing product.

- **Endpoint:**
  ```
  PUT /api/v1/products/UpdateProducts?id={id}
  ```

- **Headers:**
  - `Content-Type: application/json`

- **Body:**
  ```json
  {
    "id": 1,
    "name": "UpdatedProduct",
    "Description": "Product1 Description updated",
    "price": 150
  }
  ```

- **Response:**
  - **200 OK**
    ```
    UpdatedProduct was updated successfully.
    ```
  - **400 Bad Request**
    ```
    No records is there to update.
    ```

---

### **5. Delete a Product**
Delete a product from the database by its ID.

- **Endpoint:**
  ```
  DELETE /api/v1/products/DeleteProducts?id={id}
  ```

- **Query Parameters:**
  - `id` *(required)* - The unique identifier of the product.

- **Response:**
  - **200 OK**
    ```
    Product was deleted successfully.
    ```
  - **400 Bad Request**
    ```
    Error occurred during deletion.
    ```

---

## **Error Codes**
- **400 Bad Request** - Invalid request data.
- **404 Not Found** - Resource not found.
- **500 Internal Server Error** - An unexpected error occurred.

---

## **Logging**
The API logs operations using ASP.NET Core's built-in logging infrastructure. Key operations, errors, and successes are recorded.

---
