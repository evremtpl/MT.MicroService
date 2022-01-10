Merhaba

MicroService lerin async iletiþimi üzerine olan proje,

.Net Core 5.0 da geliþtirilmiþtir.
Database olarak Postgres kullanýlmýþtýr. Db le iletiþime geçerken ORM araçlarýndan EntityFramework Core kullanýlmýþtýr.
Proje talep üzerine development branchinde adým adým geliþtirilmiþ,commitlenmiþ, master branchine merge edilmiþtir.
Servisler HTTP üzerinden iletiþim kurmaktadýr.
Queue teknolojisinde MessageBroker olarak RabbitMQ kullanýlmýþtýr.
Proje NLayerArchitecture olarak geliþtirilip, SOLID e uygun kodlama yapýlmýþtýr.

Kiþi ile ilgili iþlemleri yapan ve rapor talebinde bulunan person servistir.
Kestrelde 5002 portundan ayaða kalkmaktadýr.Ports.txt dokümanýnda belirtilmiþtir.
Projenin migration yapýsý code first olarak geliþtirilmiþ, person servis ayaða kalktýðýnda db oluþacaktýr.
Db connection stringi Person servis appsettings.json dosyasýndan okunmakta olup, burasýnýn tarafýnýzca düzenlenmesi gerekmektedir.
Ayrýca RabbitMQ cloud conn stringi yine appsettings.json dosyasýnda yer almaktadýr.

Person servis kiþi ile ilgili endpointlerinin yaný sýra kullanýcý tarafýndan rapor talebinde bulunduðunda,
rapor talebi kuyruða gider ve iþlenmek üzere bekler.Raporun durumu creating dedir. 
FileCreateWorkerServis ayaða kalktýðýnda -bu bir worker service- bu talepleri kuyruktan alýr, excel oluþturur ve
raporu proje altýnda wwwroot klasörüne kaydeder. Ack bilgisini kuyruða gönderir.
Raporstate i tamamlandýya çeker.    

Projede bulunan endpointler aþaðýda verilmiþtir. Hata mekanizmasý, kod tekrarýný önlemek temiz kod yazýmýný saðlamak adýna
filterlar eklenilmesi gerektiðinin farkýnda olunup, süre kýsýtýndan dolayý eklenememiþtir.

Servislerin güvenliði açýsýndan bir token mekanýzmasý (JWT) süre nedeniyle eklenememiþtir.
Best practise ler not alýnýp, belirtilmiþtir.



http://localhost:5002/api/person/KisiOlustur HttpPost

http://localhost:5002/api/person/KisileriListele HttpGet

http://localhost:5002/api/person/RaporTalebi/1 HttpGet (id=kullaniciId)

http://localhost:5002/api/person/KisiyiGetir/1 HttpGet

http://localhost:5002/api/person/KisiKaldir/1 HttpDelete

http://localhost:5002/api/person/KisiyiÝletisimBilgisiIleGetir/1 HttpGet 

http://localhost:5002/api/person/RaporlariGetir HttpGet