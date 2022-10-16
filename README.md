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
Kliento pusė realizuojama su Vue.js.

Serverio pusė realizuojama su .NET 6.0, Entity framework, naudojama MySQL duomenų bazė.

![image](https://user-images.githubusercontent.com/79420546/193509884-d3dd292f-6883-4251-ac10-df653628d62b.png)
