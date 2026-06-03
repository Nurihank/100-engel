# Proje Başlığı: 100 Engel

## Proje Tanımı
Bu proje, Unity oyun motoru kullanılarak geliştirilecek olan, bilgisayar üzerinde oynanabilen ve isteğe bağlı olarak Android platformu için APK çıktısı alınabilen tek seviyeli bir 3D parkur oyunudur. 

Oyunun temel amacı, oyuncunun başlangıç noktasından hareket ederek parkur boyunca yer alan toplam 100 engeli aşması ve bitiş çizgisine ulaşmasıdır. Proje, final ödevi kapsamında kısa sürede geliştirilebilecek şekilde tasarlanmış olup temel oyun mekaniklerine odaklanacaktır.

## Oynanış ve Mekanikler
- **Temel Hareketler:** Oyuncu karakteri, parkur üzerinde bulunan platformlar ve engeller arasında ilerlemek için yürüme, zıplama (jump) ve tırmanma (climb) mekaniklerini kullanacaktır.
- **Checkpoint Sistemi:** Parkur tek bir haritadan oluşacak ve oyuncunun ilerleyişini kolaylaştırmak amacıyla **her 10 engelde bir checkpoint** (kontrol noktası) bulunacaktır. Oyuncu herhangi bir noktada düşerse veya başarısız olursa, oyuna en son geçtiği checkpoint noktasından devam edecektir. Böylece tüm parkuru baştan oynaması gerekmeyecek ve ilerleme durumu korunacaktır.
- **Zaman Sayacı (Timer):** Oyunun başlangıcında çalışan bir zaman sayacı bulunacaktır. Oyuncu bitiş çizgisine ulaştığında sayaç duracak ve elde edilen süre kaydedilecektir. 
- **En İyi Skor (Best Time):** Oyunda bir en iyi skor sistemi bulunacak ve oyuncunun bugüne kadar elde ettiği en iyi süre saklanacaktır.

## Kullanıcı Arayüzü (UI)
### 1. Ana Menü (Main Menu)
Proje içerisinde kullanıcı deneyimini artırmak amacıyla bir ana menü ekranı yer alacaktır. Ana menüde oyuncuya iki temel seçenek sunulacaktır:
- **Oyuna Başla:** Oyuncuyu doğrudan oyun sahnesine yönlendirir.
- **En İyi Skor:** Oyuncunun kaydedilmiş en iyi derecesini görüntülemesini sağlar.

### 2. Oyun Sonu Ekranı (Finish Modal)
Oyuncu parkuru tamamladığında ekranda bir sonuç penceresi açılacaktır. Bu pencerede oyuncunun bitirme süresi gösterilecek ve aşağıdaki seçenekler sunulacaktır:
- **Tekrar Oyna:** Oyunu yeniden başlatır.
- **Ana Menüye Dön:** Oyuncuyu ana menü ekranına yönlendirir.

## Geliştirme Süreci
Projenin geliştirme sürecinde zaman tasarrufu sağlamak amacıyla Unity'nin ücretsiz araçları ve Asset Store üzerindeki ücretsiz paketler kullanılacaktır. 
- Karakter kontrol sistemi
- Parkur elemanları
- Çevre modelleri
Yukarıdaki unsurlar hazır paketlerden alınacak, böylece geliştirme süresi azaltılırken görsel kalite korunacaktır.

## Proje Özellikleri Özeti
- [x] Unity ile geliştirilmiş 3D parkur oyunu
- [x] Tek harita üzerinde 100 adet engel
- [x] Zıplama ve tırmanma mekanikleri
- [x] Her 10 engelde bir checkpoint sistemi (Düşme durumunda son checkpoint'ten devam etme)
- [x] Oyun süresini ölçen timer sistemi
- [x] En iyi süreyi saklayan skor sistemi
- [x] Ana Menü ekranı (Oyuna Başla, En İyi Skor butonları)
- [x] Bitiş çizgisi (Finish Line) ve Oyun sonu sonuç ekranı (Tekrar Oyna, Ana Menüye Dön)
- [x] Ücretsiz Unity Asset Store paketleri kullanımı
- [x] Bilgisayar için `.exe` çıktısı ve isteğe bağlı Android APK desteği

## Beklenen Sonuç
Proje sonunda oyuncunun başlangıç noktasından bitiş çizgisine kadar ilerlediği, checkpoint sistemi sayesinde ilerlemesini kaybetmediği, süreye karşı yarıştığı ve kendi en iyi derecesini geliştirmeye çalıştığı, basit fakat tamamlanmış bir 3D parkur oyunu ortaya çıkarılmış olacaktır.