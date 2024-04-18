Fiş Tarama Sistemi
----
Her fiş görseli için SaaS hizmetinden bir adet json response dönülmektedir. Json response içerisinde description ve ilgili description için koordinat bilgileri yer almaktadır. Amaç json’ın uygun şekilde parse edilerek fişe ait text’in görselde görünen haline en yakında halde
kaydedilmesidir. Buna uygun olarak gerekli C# kodunu yazınız.

Proje Detayları ve Derleme
----
Proje C# dilince yazılmış olup ve .Net 6.0 platformu kullanılarak, console uygulaması oluşturulmuştur. İşlemlerin tamamı Program.cs içerisinde olacak şekilde hazırlanmıştır. Projeyi indirdikten sonra Visual Studio aracılığıyla doğrudan çalıştırabilirsiniz.

Problemin Çözümü
----
NOT: Burada her bir kelimenin bir dikdörtgen olduğunu varsayalım. Gönderilen x - y değerlerinin işaret ettiği pozisyonlar için kullanacağımız indexler şu şekilde olacaktır.

vertices[0].x : yatay düzlemde sol üst köşe
vertices[3].x : yatay düzlemde sol alt köşe

vertices[1].x : yatay düzlemde sağ alt köşe
vertices[2].x : yatay düzlemde sağ alt köşe

vertices[0].y : dikey düzlemde sol üst köşe
vertices[3].y : dikey düzlemde sol alt köşe

vertices[1].y : dikey düzlemde sağ üst köşe
vertices[2].y : dikey düzlemde sağ alt köşe

1. İlk olarak, response.json içerisinden gelen veriyi okuyarak, gelen bilgileri JsonItems türünde bir listeye atıyoruz.
2. Sonrasında kelimelerin aynı düzlemde olup olmadığını anlamak için tepe ve dip noktalarını baz aldığımız bir eşik değer elde ettik. Bu değeri elde ederken baz aldığımız köşe noktası ise y ekseni değerlerini baz alacağız. Böylece kelimenin yüksekliğinden yola çıkarak boyunun ortalamasını hesapladık.
3. Bir sonraki adımda yapıyı her bir kelimeyi tek tek kontrol edecek şekilde oluşturuyoruz. Burada amacımız her bir kelimeyi sırasıyla gezerek aynı eşik değere sahip olanları bir liste içerisine almak oldu.
4. Bir sonraki adımda, bir önceki adımda oluşturduğumuz yeni listeyi kullanacağız. Referance tipli yeni bir class oluşturduk ve eşik değeri birbirini tutan kelimeleri bir string içerisinde birleştirerek listemizin son halini oluşturduk.
5. Son adımda oluşturduğumuz listeden gelen verileri "receiveResponse.txt" içerisine yazdırmak için gerekli işlemleri yaptık.
