# Ilgalaikio turto valdymo Sistema 

# Sistemos paskirtis

Registruoti ilgalaikio turto naudotojus, palengvinti darbuotojų, kurie atsakingi už ilgalaikį turtą darbą.

#	Funkciniai reikalavimai

 Taikomosios srities objektai susieti ryšiu: darbuotojas <- ilgalaikis turtas <- atsakingas už ilgalaikį turtą darbuotojas

  •		Įprastas darbuotojas
   1.	Peržiūrėti jam priskirtą ilgalaikį turtą.
   2.	Atmesti/patvirtinti jam priskirtą ilgalaikį turtą.
   3.	Patvirtinti turto grąžinimą.

 •		Atsakingas darbuotojas už ilgalaikį turtą
   1.	Peržiūrėti visą turtą už kurį yra atsakingas pagal jam priskirtas ilgalaikio turto kategorijas.
   2.	Priskirti naujus ilgalaikio turto naudotojus.
   3.	Ilgalaikį turtą sandėliuoti.
   4.	Inicijuoti turto grąžinimą.
   5.	Ištrinti parduotą ilgalaikį turtą.
   6.	Pridėti naują ilgalaikį turtą, keisti esamo informaciją.
 
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

