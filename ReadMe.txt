Merhaba

MicroService lerin async iletiþimi üzerine olan proje,

.Net Core 5.0 da geliþtirilmiþtir.
PersonService ve ReportService olmak üzere 2 adet microservice bulunmaktadýr.
Database olarak servislerde Postgres kullanýlmýþtýr. Db le iletiþime geçerken ORM araçlarýndan EntityFramework Core kullanýlmýþtýr.
Proje development branchinde adým adým geliþtirilmiþ, commitlenmiþ, master branchine merge edilmiþtir.
Servisler HTTP üzerinden iletiþim kurmaktadýr.
Queue teknolojisinde MessageBroker olarak RabbitMQ kullanýlmýþtýr.
Proje NLayerArchitecture olarak geliþtirilip, SOLID e uygun kodlama yapýlmýþtýr.

Kiþi ile ilgili iþlemleri yapmak için person servis call edilmelidir.
Rapor talebi ve rapor getirme iþlemleri için report servis call edilmelidir.
Talep edilen raporlar rapor servis tarafýndan message broker a gönderilir.
Rapor taleplerinde bütün rapor gönderilmeyip id si gönderilerek network yükü azaltýlmýþtýr.
Person servis ayaða kalktýðýnda queue daki talepleri alarak rapor oluþturur ve report servise geri bildirimde bulunur.
Oluþturulan raporlar report servis wwwroot/files dizinine kaydedilir.

Servisler Kestrelde 5002 ve 5003 portundan ayaða kalkmaktadýr.Ports.txt dokümanýnda belirtilmiþtir.
Projenin migration yapýsý code first olarak geliþtirilmiþ, person ve  report  servisler ayaða kalktýðýnda db ler oluþacaktýr.
Db connection stringi servislerin appsettings.json dosyalarýndan ve DesignTimeDbContextFactory class ýndan okunmakta olup, ilgili bölümlerin iki servis için de tarafýnýzca düzenlenmesi gerekmektedir.
Ayrýca RabbitMQ cloud conn stringi yine appsettings.json dosyasýnda yer almaktadýr.
    

Projede bulunan endpointler aþaðýda verilmiþtir. Hata mekanizmasý için kod tekrarýný önlemek, temiz kod yazýmýný saðlamak adýna filterlar eklenilmesi gerektiðinin farkýnda olunup, süre kýsýtýndan dolayý eklenememiþtir.

Servislerin güvenliði açýsýndan bir token mekanýzmasý (JWT) süre nedeniyle eklenememiþtir.
Best practise ler not alýnýp, belirtilmiþtir.



http://localhost:5002/api/person/createperson HttpPost

http://localhost:5002/api/person/GetAllPerson HttpGet

http://localhost:5003/api/Report/CreateReport/2 HttpGet (id=kullaniciId)

http://localhost:5002/api/person/GetPerson/1 HttpGet (id=kullaniciId)

http://localhost:5002/api/person/removeperson?id=6 HttpDelete (id=kullaniciId)

http://localhost:5002/api/person/GetWithContactInfoByPersonId/1 HttpGet 

http://localhost:5003/api/Report/GetAllReports HttpGet

http://localhost:5003/api/Report/CreateReport/2 HttpGet 2 id li kullanýcýnýn rapor oluþturmasý.

http://localhost:5003/api/Report/GetReport/2 HttpGet 2 id li rapor bilgileri