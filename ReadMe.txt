Merhaba

MicroService lerin async ileti�imi �zerine olan proje,

.Net Core 5.0 da geli�tirilmi�tir.
PersonService ve ReportService olmak �zere 2 adet microservice bulunmaktad�r.
Database olarak servislerde Postgres kullan�lm��t�r. Db le ileti�ime ge�erken ORM ara�lar�ndan EntityFramework Core kullan�lm��t�r.
Proje development branchinde ad�m ad�m geli�tirilmi�, commitlenmi�, master branchine merge edilmi�tir.
Servisler HTTP �zerinden ileti�im kurmaktad�r.
Queue teknolojisinde MessageBroker olarak RabbitMQ kullan�lm��t�r.
Proje NLayerArchitecture olarak geli�tirilip, SOLID e uygun kodlama yap�lm��t�r.

Ki�i ile ilgili i�lemleri yapmak i�in person servis call edilmelidir.
Rapor talebi ve rapor getirme i�lemleri i�in report servis call edilmelidir.
Talep edilen raporlar rapor servis taraf�ndan message broker a g�nderilir.
Rapor taleplerinde b�t�n rapor g�nderilmeyip id si g�nderilerek network y�k� azalt�lm��t�r.
Person servis aya�a kalkt���nda queue daki talepleri alarak rapor olu�turur ve report servise geri bildirimde bulunur.
Olu�turulan raporlar report servis wwwroot/files dizinine kaydedilir.

Servisler Kestrelde 5002 ve 5003 portundan aya�a kalkmaktad�r.Ports.txt dok�man�nda belirtilmi�tir.
Projenin migration yap�s� code first olarak geli�tirilmi�, person ve  report  servisler aya�a kalkt���nda db ler olu�acakt�r.
Db connection stringi servislerin appsettings.json dosyalar�ndan ve DesignTimeDbContextFactory class �ndan okunmakta olup, ilgili b�l�mlerin iki servis i�in de taraf�n�zca d�zenlenmesi gerekmektedir.
Ayr�ca RabbitMQ cloud conn stringi yine appsettings.json dosyas�nda yer almaktad�r.
    

Projede bulunan endpointler a�a��da verilmi�tir. Hata mekanizmas� i�in kod tekrar�n� �nlemek, temiz kod yaz�m�n� sa�lamak ad�na filterlar eklenilmesi gerekti�inin fark�nda olunup, s�re k�s�t�ndan dolay� eklenememi�tir.

Servislerin g�venli�i a��s�ndan bir token mekan�zmas� (JWT) s�re nedeniyle eklenememi�tir.
Best practise ler not al�n�p, belirtilmi�tir.



http://localhost:5002/api/person/createperson HttpPost

http://localhost:5002/api/person/GetAllPerson HttpGet

http://localhost:5003/api/Report/CreateReport/2 HttpGet (id=kullaniciId)

http://localhost:5002/api/person/GetPerson/1 HttpGet (id=kullaniciId)

http://localhost:5002/api/person/removeperson?id=6 HttpDelete (id=kullaniciId)

http://localhost:5002/api/person/GetWithContactInfoByPersonId/1 HttpGet 

http://localhost:5003/api/Report/GetAllReports HttpGet

http://localhost:5003/api/Report/CreateReport/2 HttpGet 2 id li kullan�c�n�n rapor olu�turmas�.

http://localhost:5003/api/Report/GetReport/2 HttpGet 2 id li rapor bilgileri