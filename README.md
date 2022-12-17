# Ilgalaikio turto valdymo Sistema 

# Sistemos paskirtis

Registruoti ilgalaikio turto naudotojus, palengvinti darbuotojų, kurie atsakingi už ilgalaikį turtą darbą.

#	Funkciniai reikalavimai

 Taikomosios srities objektai susieti ryšiu: darbuotojas <- ilgalaikis turtas <- atsakingas už ilgalaikį turtą darbuotojas

  •		Įprastas darbuotojas
   1.	Peržiūrėti jam priskirtą ilgalaikį turtą.
   2.	Atmesti/patvirtinti jam priskirtą ilgalaikį turtą.

 •		Atsakingas darbuotojas už ilgalaikį turtą
   1.	Peržiūrėti visą turtą už kurį yra atsakingas pagal jam priskirtas ilgalaikio turto kategorijas.
   2.	Priskirti naujus ilgalaikio turto naudotojus.
   3.	Ilgalaikį turtą sandėliuoti.
   4.	Ištrinti parduotą ilgalaikį turtą.
   5.	Pridėti naują ilgalaikį turtą, keisti informaciją apie esamą.
 
 •		Sistemos administratorius 
  1.	Sukurti naujus darbuotojus.
  2.	Priskirti darbuotojams ilgalaikio turto kategorijas, už kurias jie bus atsakingi.
  3.	Šalinti/keisti priskirtas kategorijų priskyrimus, pašalinti darbuotojus, pridėti adminstratoriaus teises darbuotojams.


#	Sistemos architektūra
Kliento pusė realizuojama su Vue.js bei axios JavaScript.

Serverio pusė realizuojama su .NET 6.0, Entity framework, naudojama MySQL duomenų bazė.

![image](https://user-images.githubusercontent.com/79420546/193509884-d3dd292f-6883-4251-ac10-df653628d62b.png)

# Naudotojo sąsajos projektas

Prisijungimo langas

![image](https://user-images.githubusercontent.com/79420546/208240596-2819f00e-aa33-4274-938c-dd6a9a04498d.png)

![image](https://user-images.githubusercontent.com/79420546/208239920-9f192d81-674a-49ac-852b-c7b371bf9b70.png)

Darbuotojo pagrindinis langas

![image](https://user-images.githubusercontent.com/79420546/208240575-20381586-6c84-418d-b1b5-ba3a006759d8.png)

![image](https://user-images.githubusercontent.com/79420546/208239595-2c6cae86-447f-47a2-ad8f-88ea5fe0daa8.png)

Asmens atsakingo už ilgalaikį turtą pagrindinis langas

![image](https://user-images.githubusercontent.com/79420546/208241406-cf88139a-8d9f-4e2e-b499-00f119551652.png)

![image](https://user-images.githubusercontent.com/79420546/208239738-e3428425-7c1e-43fb-bc6e-bd99f0c73d7f.png)

Adminstratoriaus ilgalaikio turto langas

![image](https://user-images.githubusercontent.com/79420546/208241301-31c76911-dad0-4553-a929-774825ce9625.png)

![image](https://user-images.githubusercontent.com/79420546/208239762-58b4bb64-3883-42a3-8681-3badb0a5d0fc.png)

Darbuotojų valdymo langas

![image](https://user-images.githubusercontent.com/79420546/208241158-dcb5ce0a-4f06-4a37-a5f8-e032dd14d079.png)

![image](https://user-images.githubusercontent.com/79420546/208239788-93816e87-1dd7-4cb3-9644-5238be2007d8.png)

Asmenų atsakingų už ilgalaikį turtą valdymo langas

![image](https://user-images.githubusercontent.com/79420546/208241217-09bd681a-0583-443b-9ade-01e5903558bc.png)

![image](https://user-images.githubusercontent.com/79420546/208239808-59f64d07-b6c8-45cf-bef0-0ac078030bc9.png)

# API specifikacija

# POST auth/login

Autorizacija: nėra

Atsako kodai:
200, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/auth/login?username=employee&password=password

Atsakymas:
eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZW1wbG95ZWUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJFbXBsb3llZSIsImV4cCI6MTY3MTM3OTQyNH0.ENgIw2YyTY3kukb6jYKFwqFw3N9kdI0qesvriTZLnjLyrHG6XMjC6xoe1ZEshxxN-fHaAqhkMyS9eOxrSfg5TQ

# POST auth/register

Autorizacija: administratorius

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/auth/register

Request body:
```json
{
  "name": "employee2",
  "surname": "employee2",
  "username": "employee2",
  "password": "employee2",
  "isAdmin": false,
  "department": "department"
}
```

Atsakymas:
```json
{
    "name": "employee3",
    "surname": "employee3",
    "username": "employee3",
    "passwordHash": "Fgf93+x1dckRMd3hNHnMZxZNASly9J+IJMgXXIvJZ6N78IdvOIfTnCtawUoVBCJJKRzd9mWYjuOoYVcFz6kzpQ==",
    "passwordSalt": "YBrgNeQ55qyH4HnVRCc/sh2v9hYEDGnGHwWyRDxSqPCa7bADGwZXaUtn5l2vH8S/ibP5bzuZoWMGPeys3mHaVV9qkAKe5fa8yA8l9a4l+xMedNVZw9x//+nKQRFq0JBI6+XIrHwi+T7oZFHJkOKjk3G4RLB4ZECO265JASzj59k=",
    "department": "department",
    "isAdmin": false
}
```

# GET employees

Autorizacija: administratorius

Atsako kodai:
200, 401, 404

Panaudojimo pavyzdys:

Užklausa: GET https://fixedassetsapi.azurewebsites.net/employees

Atsakymas:
```json
[
    {
        "username": "admin",
        "name": "admin",
        "surname": "admin",
        "department": "IT",
        "isAdmin": true
    },
    {
        "username": "employee",
        "name": "Vardas",
        "surname": "Pavardė",
        "department": "HR",
        "isAdmin": false
    },
    {
        "username": "famanager",
        "name": "famanager",
        "surname": "famanager",
        "department": "department",
        "isAdmin": false
    }
]
```
# GET managers

Autorizacija: administratorius

Atsako kodai:
200, 401, 404

Panaudojimo pavyzdys:

Užklausa: GET https://fixedassetsapi.azurewebsites.net/managers

Atsakymas:

```json
[
    {
        "username": "famanager",
        "faCategory": "KOMPIUT"
    },
    {
        "username": "famanager",
        "faCategory": "MOB"
    }
]
```

# GET employee{username}

Autorizacija: administratorius

Atsako kodai:
200, 401, 400, 404

Panaudojimo pavyzdys:

Užklausa: GET https://fixedassetsapi.azurewebsites.net/employee/famanager

Atsakymas:

```json
[
    {
        "username": "famanager",
        "name": "famanager",
        "surname": "famanager",
        "department": "department",
        "isAdmin": false
    }
]
```

# GET fixed-asset/{code}

Autorizacija: administratorius, atsakingas už ilgalaikį turtą asmuo

Atsako kodai:
200, 401, 400, 404

Panaudojimo pavyzdys:

Užklausa: GET https://fixedassetsapi.azurewebsites.net/fixed-asset/008003

Atsakymas:

```json
[
    {
        "code": "008003",
        "description": "MacBook Pro13.3 DC i7 2.9GHz/G",
        "assignedBy": "famanager",
        "serialNumber": "C02HWVJDDTY3",
        "eventType": 2
    }
]
```

# GET manager/{username}

Autorizacija: administratorius

Atsako kodai:
200, 401, 400, 404

Panaudojimo pavyzdys:

Užklausa: GET https://fixedassetsapi.azurewebsites.net/manager/famanager

Atsakymas:

```json
[
    {
        "username": "famanager",
        "faCategory": "KOMPIUT"
    }
]
```

# GET assigned/{username}

Autorizacija: administratorius, darbuotojas

Atsako kodai:
200, 401, 400, 404

Panaudojimo pavyzdys:

Užklausa: GET https://fixedassetsapi.azurewebsites.net/assigned/employee

Atsakymas:

```json
[
    {
        "code": "008003",
        "description": "MacBook Pro13.3 DC i7 2.9GHz/G",
        "assignedBy": "famanager",
        "serialNumber": "C02HWVJDDTY3",
        "eventType": 2
    },
    {
        "code": "008015",
        "description": "Dell Kompiuteris",
        "assignedBy": "famanager",
        "serialNumber": "FA15634",
        "eventType": 1
    }
]
```

# GET managed/{username}

Autorizacija: atsakingas už ilgalaikį turtą asmuo

Atsako kodai:
200, 401, 400, 404

Panaudojimo pavyzdys:

Užklausa: GET https://fixedassetsapi.azurewebsites.net/managed/famanager

Atsakymas:

```json
[
    {
        "code": "008003",
        "description": "MacBook Pro13.3 DC i7 2.9GHz/G",
        "assignedTo": "employee",
        "serialNumber": "C02HWVJDDTY3",
        "eventType": 2
    },
    {
        "code": "008004",
        "description": "MacBook Pro13.3 DC i7 2.9GHz/G",
        "assignedTo": null,
        "serialNumber": "C02HWVJDDTY3",
        "eventType": 5
    },
    {
        "code": "008015",
        "description": "Dell Kompiuteris",
        "assignedTo": "employee",
        "serialNumber": "FA15634",
        "eventType": 1
    }
]
```
# POST fixed-asset

Autorizacija: atsakingas už ilgalaikį turtą asmuo, administratorius

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/fixed-asset

Request body:
```json
{
  "code": "test",
  "description": "test",
  "class": "test",
  "serialNumber": "test",
  "managedBy": "test"
}
```

Atsakymas: 200 OK

# POST manager

Autorizacija: administratorius

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/manager

Request body:
```json
{
  "username": "string",
  "faCategory": "string"
}
```

Atsakymas: 200 OK

# POST fixed-asset/accept

Autorizacija: administratorius, darbuotojas

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/fixed-asset/accept

Request body:
```json
{
  "code": "008015",
  "requestedBy": "employee"
}
```

Atsakymas: 200 OK

# POST fixed-asset/reject

Autorizacija: administratorius, darbuotojas

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/fixed-asset/reject

Request body:
```json
{
  "code": "008015",
  "requestedBy": "employee"
}
```

Atsakymas: 200 OK

# POST fixed-asset/assign

Autorizacija: asmuo atsakingas už ilgalaikį turtą

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/fixed-asset/assign

Request body:
```json
{
  "code": "008015",
  "assignTo": "employee",
  "assignedBy": "famanager"
}
```

Atsakymas: 200 OK

# POST fixed-asset/store

Autorizacija: asmuo atsakingas už ilgalaikį turtą

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: POST https://fixedassetsapi.azurewebsites.net/fixed-asset/store

Request body:
```json
{
  "code": "008015",
  "requestedBy": "famanager"
}
```

Atsakymas: 200 OK

# PUT employee

Autorizacija: administratorius

Atsako kodai:
200, 401, 400, 404

Panaudojimo pavyzdys:

Užklausa: PUT https://fixedassetsapi.azurewebsites.net/employee

Request body:
```json
{
  "username": "employee",
  "isAdmin": "false"
}
```

Atsakymas: 200 OK

# PUT fixed-asset

Autorizacija: administratorius

Atsako kodai:
200, 401, 400, 404

Panaudojimo pavyzdys:

Užklausa: PUT https://fixedassetsapi.azurewebsites.net/fixed-asset

Request body:
```json
{
  "code": "015649",
  "description": "description",
  "serialNumber": "AKFKSAOFPOIO-9",
  "class": "MOB"
}
```

Atsakymas: 200 OK

# PUT manager

Autorizacija: administratorius

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: PUT https://fixedassetsapi.azurewebsites.net/manager

Request body:
```json
{
  "username": "famanager",
  "currentCategory": "MOB",
  "newCategory": "KOMPIUT"
}
```

Atsakymas: 200 OK

# DELETE manager

Autorizacija: administratorius

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: DELETE https://fixedassetsapi.azurewebsites.net/manager

Request body:
```json
{
  "username": "famanager",
  "category": "MOB"
}
```

Atsakymas: 200 OK

# DELETE employee/{username}

Autorizacija: administratorius

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: DELETE https://fixedassetsapi.azurewebsites.net/employee/employee2

Atsakymas: 200 OK

# DELETE fixed-asset/{code}

Autorizacija: administratorius

Atsako kodai:
200, 401, 400

Panaudojimo pavyzdys:

Užklausa: DELETE https://fixedassetsapi.azurewebsites.net/fixed-asset/008015

Atsakymas: 200 OK

