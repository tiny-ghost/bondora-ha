## bondora-home assingment

### Requirements to run the project

- .NET 6 installed
- MSSQL installed
- Visual Studio 2022 (optional)

#### Running with Visual studio 2022

- Open solution and run the project. `Rental.API` is configured as a startup project.

- On the first run, the build process restores npm dependencies, which can take several minutes.

- Back-end and front-end will launch automatically after that

#### Running with .NET Core CLI

- Navigate to the solution folder
- Navigate to Rental.API sub folder and run command `dotnet run` or run `dotnet run --project Rental.API` from solution folder
- When project statred up in browser navigate to `http://localhost:5294`. This will trigger Angular front-end

### Running tests

#### Visual studio 2022

- Test -> Run All Tests `Ctrl+R,A`

#### .NET core CLI

- In solution folder run command `dotnet test`

---

##### Additional Info

- At each launch database will be recreated and seeded with rental equipment

###### API

Api can be tested with postman or any other tool.
Swagger is not included in the project.

###### Order

`POST` `/Order`

```
{
   "CustomerId": int,
   "RentalItems": [
       {
           "EquipmentId": int,
           "DaysOfRental": int
       }
    ]
}
```

`GET` `Order/{id}`

```
"customerId": int,
    "rentItems": [
        {
            "equipmentId": int,
            "equipment":
            {
                "name": string,
                "type": string,
                "id": int,
            },
            "daysOfRental": int,
            "orderId": int,
            "id": int,
        }

```

`GET` `/Order/Customer/{id}`

```
Return all orders for the customer
```

###### Invoice

`GET` `/Invoice/{OrderId}`

```
Invoice for order number: 1.

Customer Smith

Item Name                     Price
-----------------------------------
Bosch jackhammer           360,00 €
Volvo steamroller          340,00 €
KamAZ truck                820,00 €
-----------------------------------
Total:                   1 520,00 €


Royalty earned:                   3
```
