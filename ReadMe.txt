Merhaba

MicroService lerin async ileti�imi �zerine olan proje,

.Net Core 5.0 da geli�tirilmi�tir.
Database olarak Postgres kullan�lm��t�r. Db le ileti�ime ge�erken ORM ara�lar�ndan EntityFramework Core kullan�lm��t�r.
Proje talep �zerine development branchinde ad�m ad�m geli�tirilmi�,commitlenmi�, master branchine merge edilmi�tir.
Servisler HTTP �zerinden ileti�im kurmaktad�r.
Queue teknolojisinde MessageBroker olarak RabbitMQ kullan�lm��t�r.
Proje NLayerArchitecture olarak geli�tirilip, SOLID e uygun kodlama yap�lm��t�r.

Ki�i ile ilgili i�lemleri yapan ve rapor talebinde bulunan person servistir.
Kestrelde 5002 portundan aya�a kalkmaktad�r.Ports.txt dok�man�nda belirtilmi�tir.
Projenin migration yap�s� code first olarak geli�tirilmi�, person servis aya�a kalkt���nda db olu�acakt�r.
Db connection stringi Person servis appsettings.json dosyas�ndan okunmakta olup, buras�n�n taraf�n�zca d�zenlenmesi gerekmektedir.
Ayr�ca RabbitMQ cloud conn stringi yine appsettings.json dosyas�nda yer almaktad�r.

Person servis ki�i ile ilgili endpointlerinin yan� s�ra kullan�c� taraf�ndan rapor talebinde bulundu�unda,
rapor talebi kuyru�a gider ve i�lenmek �zere bekler.Raporun durumu creating dedir. 
FileCreateWorkerServis aya�a kalkt���nda -bu bir worker service- bu talepleri kuyruktan al�r, excel olu�turur ve
raporu proje alt�nda wwwroot klas�r�ne kaydeder. Ack bilgisini kuyru�a g�nderir.
Raporstate i tamamland�ya �eker.    

Projede bulunan endpointler a�a��da verilmi�tir. Hata mekanizmas�, kod tekrar�n� �nlemek temiz kod yaz�m�n� sa�lamak ad�na
filterlar eklenilmesi gerekti�inin fark�nda olunup, s�re k�s�t�ndan dolay� eklenememi�tir.

Servislerin g�venli�i a��s�ndan bir token mekan�zmas� (JWT) s�re nedeniyle eklenememi�tir.
Best practise ler not al�n�p, belirtilmi�tir.



http://localhost:5002/api/person/KisiOlustur HttpPost

http://localhost:5002/api/person/KisileriListele HttpGet

http://localhost:5002/api/person/RaporTalebi/1 HttpGet (id=kullaniciId)

http://localhost:5002/api/person/KisiyiGetir/1 HttpGet

http://localhost:5002/api/person/KisiKaldir/1 HttpDelete

http://localhost:5002/api/person/Kisiyi�letisimBilgisiIleGetir/1 HttpGet 

http://localhost:5002/api/person/RaporlariGetir HttpGet