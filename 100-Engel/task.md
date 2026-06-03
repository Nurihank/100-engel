# 100 Engel — Proje Görev Listesi

> Bu dosya, projeyi sıfırdan tamamlanmış bir oyuna dönüştürmek için gereken tüm adımları içerir.
> Her görevin yanında nasıl yapılacağına dair kısa açıklamalar bulunmaktadır.

---

## 1. Proje Kurulumu ve Başlangıç Ayarları

- [x] **Unity Hub'dan yeni proje oluştur**
  - Unity Hub → New Project → **3D (URP)** şablonunu seç.
  - Proje adını `100-Engel` olarak yaz, kayıt konumunu belirle ve "Create project" tıkla.
  - URP (Universal Render Pipeline) seçmemizin sebebi: Hafif, performanslı ve mobil uyumlu.

- [x] **Proje klasör yapısını oluştur**
  - Project panelinde `Assets` klasörünün altına şu klasörleri oluştur (sağ tık → Create → Folder):
    ```
    Assets/
    ├── Scenes/          → Sahneler (MainMenu, MainScene)
    ├── Scripts/          → Tüm C# scriptleri
    │   ├── Player/       → Karakter ile ilgili scriptler
    │   ├── Managers/     → GameManager, TimerManager, CheckpointManager
    │   └── UI/           → Menü ve panel scriptleri
    ├── Prefabs/          → Tekrar kullanılacak hazır objeler (engeller, checkpoint vb.)
    ├── Materials/        → Renkler ve materyaller
    ├── UI/               → UI spriteları, fontlar
    └── Audio/            → Ses dosyaları (isteğe bağlı)
    ```

- [x] **Asset Store'dan ücretsiz paketleri indir ve import et**
  - Unity menüsünden `Window → Package Manager` aç.
  - Şu paketleri ara ve import et:
    - **Starter Assets – Third Person Character Controller** (Karakter kontrolü + Cinemachine kamera)
    - **TextMeshPro** (İlk UI elemanı oluşturduğunda Unity otomatik soracak, "Import TMP Essentials" de)
    - Parkur engelleri ve çevre modelleri için Asset Store'da "Obstacle Course", "Low Poly Environment" gibi ücretsiz paketleri ara.
  - Import ederken sadece ihtiyacın olan klasörleri seç, gereksiz demo sahnelerini import etme.

- [ ] **Build Settings ayarla**
  - `File → Build Settings` aç.
  - Platform olarak **PC, Mac & Linux Standalone** seçili olduğundan emin ol.
  - (İsteğe bağlı) Android desteği için: `Android` platformunu seç → `Switch Platform` tıkla. Bunun için Android SDK kurulu olmalı (Unity Hub → Installs → Add Modules → Android Build Support).

---

## 2. Karakter ve Hareket Mekanikleri

- [ ] **Karakter modelini sahneye ekle**
  - Starter Assets paketindeki `PlayerArmature` prefabını `MainScene` sahnesine sürükle-bırak.
  - Prefab içinde zaten `CharacterController`, `ThirdPersonController` scripti ve animasyonlar hazır gelecek.
  - Karakterin altına bir zemin (3D Object → Plane) koyarak düşmediğini test et.

- [ ] **Yürüme ve zıplama mekaniklerini test et ve ayarla**
  - Play moduna gir (Ctrl+P), WASD ile yürümeyi ve Space ile zıplamayı test et.
  - `ThirdPersonController` scriptindeki şu değerleri Inspector panelinden ayarla:
    - `MoveSpeed`: Yürüme hızı (örn. 4-6 arası)
    - `SprintSpeed`: Koşma hızı (örn. 8-10 arası)
    - `JumpHeight`: Zıplama yüksekliği (örn. 1.5-2.5 arası, engellere göre ayarla)
    - `Gravity`: Yerçekimi (varsayılan -15 genellikle iyi çalışır)

- [ ] **Tırmanma (Climb) mekaniğini ekle**
  - Yöntem 1 (Basit): Tırmanılacak duvarlara `Climbable` tag'ı ekle. Karakter bu objelere temas ettiğinde yerçekimini geçici olarak kaldırıp yukarı hareket ettiren bir `ClimbController.cs` scripti yaz:
    ```csharp
    // Basit tırmanma mantığı:
    // OnTriggerEnter ile Climbable tag'lı objeye temas algıla
    // isClimbing = true yap
    // Update içinde isClimbing true iken W tuşuyla yukarı hareket ettir
    // OnTriggerExit ile tırmanmadan çık
    ```
  - Yöntem 2 (Hazır): Asset Store'dan ücretsiz bir climbing sistemi ara ve entegre et.

- [ ] **Kamera ayarlarını yap (Cinemachine)**
  - Starter Assets ile gelen `PlayerFollowCamera` objesini seç.
  - Inspector'da Cinemachine ayarlarını kontrol et:
    - `Follow`: Karakterin transform'u atanmış olmalı.
    - `Body → Camera Distance`: Kameranın karaktere uzaklığı (örn. 5-8)
    - `Aim → Tracked Object Offset`: Kameranın baktığı yükseklik (Y eksenini biraz artırarak karakterin üzerinden baktır).

---

## 3. Parkur ve Harita Tasarımı

- [ ] **MainScene sahnesini oluştur**
  - `File → New Scene` → `Basic (Built-in)` seç, `Ctrl+S` ile `Assets/Scenes/MainScene` olarak kaydet.
  - Sahneye bir Directional Light ve Skybox zaten varsayılan olarak gelir.

- [ ] **Başlangıç platformunu tasarla**
  - Sahneye bir 3D Object → Cube ekle, Scale'ini genişlet (örn. X:5, Y:0.5, Z:5).
  - Pozisyonu (0, 0, 0) olarak ayarla. Bu, karakterin oyuna başlayacağı alan.
  - Bir materyal oluştur (`Assets/Materials` → Create → Material), yeşil veya mavi renk ver, küpün üstüne sürükle.
  - Bu platformun üstüne karakteri yerleştir (Y pozisyonunu platformun üst yüzeyine ayarla).

- [ ] **Engelleri "ObstacleCoursePack" paketinden seç ve ayarla**
  - Project panelinde `Assets/ObstacleCoursePack/Prefabs/` (veya benzeri bir klasör) içindeki hazır engelleri (MovableWall, RotationPlat, Pendulum vb.) kullan.
  - Eğer hazır objeler hareket etmiyorsa, daha önce yazdığımız `MovingPlatform.cs` veya `RotatingObstacle.cs` scriptlerini bu objelere ekleyerek ayarlarını yap.
  - Tırmanma duvarı olarak kullanacağın objelere (örneğin yüksek duvarlar) **`Climbable`** tag'ını eklemeyi unutma.

- [ ] **100 engeli parkura yerleştir**
  - Parkuru düz bir çizgi veya hafif kıvrımlı bir yol olarak tasarla.
  - Hazır prefabları sahneye sürükle ve **Ctrl+D** ile çoğaltarak diz.
  - İlk 30 engel: Sabit platformlar, boşluklar ve basit engeller.
  - 30-60 arası: `RotationPlat`, `MovableWall`, `Pendulum` gibi hareketli engeller.
  - 60-100 arası: Kombinasyonlar (dönen + hareketli + tırmanma).
  - **İpucu:** Engeller arası mesafeyi 3-5 birim tut, çok uzak olmasın. Her engele `Obstacle_01`, `Obstacle_02` şeklinde isim ver.

- [ ] **Bitiş çizgisini (Finish Line) ekle**
  - 100. engelin ardına büyük bir platform ve gösterişli bir hazır model ekle (örneğin bir kapı veya bayrak).
  - Bu objeye bir Box Collider (veya uygun bir collider) ekle ve `Is Trigger` işaretle.
  - Tag olarak `FinishLine` ver.
  - Objeye `FinishLineTrigger` scriptini ekle. Oyuncu buraya geldiğinde oyun bitecek.

---

## 4. Checkpoint (Kontrol Noktası) Sistemi

- [ ] **Checkpoint alanlarını yerleştir**
  - Her 10. engelden sonra (10, 20, 30 ... 100. engele kadar) bir checkpoint platformu koy.
  - Her checkpoint objesine bir Box Collider ekle ve `Is Trigger` işaretle.
  - Tag olarak `Checkpoint` ver.
  - Görsel fark yaratmak için checkpoint'lere farklı renk materyal ver (örn. altın sarısı).

- [ ] **CheckpointManager.cs scriptini yaz**
  - `Assets/Scripts/Managers/` altında `CheckpointManager.cs` oluştur:
    ```csharp
    // Singleton pattern kullan (sahne boyunca tek bir instance)
    // public Vector3 lastCheckpointPosition; → Son checkpoint pozisyonu
    // public void SetCheckpoint(Vector3 pos) → Yeni checkpoint kaydet
    // public Vector3 GetCheckpoint() → Son checkpoint'i döndür
    ```
  - Bu scripti sahnedeki boş bir GameObject'e (`_CheckpointManager`) ekle.

- [ ] **Checkpoint Trigger scriptini yaz**
  - `CheckpointTrigger.cs` adında bir script oluştur, her checkpoint objesine ekle:
    ```csharp
    // OnTriggerEnter(Collider other) → other.CompareTag("Player") ise
    //   CheckpointManager.Instance.SetCheckpoint(transform.position) çağır
    //   İsteğe bağlı: Görsel/sesli geri bildirim (renk değiştir, ses çal)
    ```

- [ ] **Death Zone (Ölüm Alanı) oluştur**
  - Haritanın altına (örn. Y = -20) büyük, görünmez bir Box Collider (Is Trigger) ekle.
  - Tag: `DeathZone`.
  - Alternatif: Karakter scriptinde her frame Y pozisyonunu kontrol et, belirli bir değerin altına düşerse respawn tetikle.

- [ ] **Respawn (Yeniden Doğma) mekanizmasını yaz**
  - Karakter objesine veya ayrı bir `PlayerRespawn.cs` scriptine:
    ```csharp
    // OnTriggerEnter → DeathZone tag'ını algıla
    // Karakterin pozisyonunu CheckpointManager.Instance.GetCheckpoint() konumuna taşı
    // Karakterin hızını sıfırla (velocity = Vector3.zero)
    // İsteğe bağlı: Kısa bir fade-to-black efekti ekle
    ```

---

## 5. Zaman Sayacı (Timer) ve Skor Sistemi

- [ ] **TimerManager.cs scriptini oluştur**
  - `Assets/Scripts/Managers/` altında oluştur:
    ```csharp
    // float elapsedTime = 0f; → Geçen süre
    // bool isRunning = false; → Sayaç çalışıyor mu?
    // void StartTimer() → isRunning = true
    // void StopTimer() → isRunning = false
    // void Update() → isRunning ise elapsedTime += Time.deltaTime
    // string GetFormattedTime() → TimeSpan ile "00:00.00" formatında döndür
    ```

- [ ] **Süreyi ekranda gösteren UI elemanını ekle**
  - Hierarchy → UI → Text - TextMeshPro ekle (İlk seferde TMP Essentials import et).
  - Canvas içinde sağ üst köşeye pozisyonla (Anchor: Top-Right).
  - `TimerDisplay.cs` scripti yaz:
    ```csharp
    // Update içinde:
    // timerText.text = TimerManager.Instance.GetFormattedTime();
    ```

- [ ] **Bitiş çizgisinde timer'ı durdur**
  - Oyuncu `FinishLine` trigger'ına girdiğinde `TimerManager.Instance.StopTimer()` çağır.
  - Bu çağrıyı `GameManager` üzerinden yap (merkezi kontrol).

- [ ] **En İyi Skor (Best Time) sistemini yaz**
  - `ScoreManager.cs` oluştur:
    ```csharp
    // void SaveBestTime(float time):
    //   float currentBest = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
    //   if (time < currentBest) PlayerPrefs.SetFloat("BestTime", time);
    //   PlayerPrefs.Save();
    //
    // float GetBestTime():
    //   return PlayerPrefs.GetFloat("BestTime", 0f);
    //
    // bool HasBestTime():
    //   return PlayerPrefs.HasKey("BestTime");
    ```

---

## 6. GameManager (Oyun Yöneticisi)

- [ ] **GameManager.cs scriptini oluştur**
  - Bu script oyunun genel akışını yönetir. `Assets/Scripts/Managers/` altında oluştur:
    ```csharp
    // Singleton pattern
    // public enum GameState { Menu, Playing, Paused, Finished }
    // GameState currentState;
    //
    // void StartGame():
    //   currentState = Playing
    //   TimerManager.Instance.StartTimer()
    //   Cursor.lockState = CursorLocked, Cursor.visible = false
    //
    // void FinishGame():
    //   currentState = Finished
    //   TimerManager.Instance.StopTimer()
    //   ScoreManager.Instance.SaveBestTime(elapsedTime)
    //   FinishUI panelini aç
    //   Cursor.lockState = None, Cursor.visible = true
    //   Time.timeScale = 0 (oyunu durdur) — dikkat: UI butonları çalışmaya devam etmeli
    //
    // void RestartGame():
    //   Time.timeScale = 1
    //   SceneManager.LoadScene(SceneManager.GetActiveScene().name)
    //
    // void GoToMainMenu():
    //   Time.timeScale = 1
    //   SceneManager.LoadScene("MainMenu")
    ```
  - Boş bir GameObject oluştur (`_GameManager`), bu scripti ekle.

---

## 7. Kullanıcı Arayüzü (UI) ve Menüler

### Ana Menü (Main Menu)

- [ ] **MainMenu sahnesini oluştur**
  - `File → New Scene` → `Assets/Scenes/MainMenu` olarak kaydet.
  - Sahneye sadece UI elemanları ve bir kamera olacak (3D obje yok).

- [ ] **Ana menü arayüzünü tasarla**
  - Hierarchy → UI → Canvas ekle. Canvas Scaler'ı `Scale With Screen Size` yap (Reference Resolution: 1920x1080).
  - Arkaplan: UI → Image ekle, tüm ekranı kaplayacak şekilde Stretch yap. Koyu renk veya gradient bir sprite kullan.
  - Oyun Başlığı: UI → Text (TMP) ekle, "100 ENGEL" yaz, büyük ve kalın font, ortaya hizala.

- [ ] **"Oyuna Başla" butonunu ekle**
  - UI → Button (TMP) ekle, üzerindeki text'e "OYUNA BAŞLA" yaz.
  - `MainMenuUI.cs` scripti oluştur ve Canvas'a ekle:
    ```csharp
    // using UnityEngine.SceneManagement;
    // public void OnPlayButtonClicked():
    //   SceneManager.LoadScene("MainScene");
    ```
  - Butonun `OnClick()` eventine bu fonksiyonu ata (Inspector'da + tıkla → scripti sürükle → fonksiyonu seç).

- [ ] **"En İyi Skor" gösterimini ekle**
  - UI → Text (TMP) ekle, altına bir text daha ekle.
  - `MainMenuUI.cs` içinde Start() metodunda:
    ```csharp
    // if (PlayerPrefs.HasKey("BestTime"))
    //   bestTimeText.text = "En İyi Süre: " + formattedTime;
    // else
    //   bestTimeText.text = "Henüz rekor yok!";
    ```

### Oyun İçi HUD (Heads-Up Display)

- [ ] **Oyun içi UI Canvas oluştur**
  - `MainScene`'de Hierarchy → UI → Canvas ekle.
  - Timer text'ini (sağ üst), checkpoint bilgisini (sol üst, isteğe bağlı) bu Canvas'a ekle.

### Oyun Sonu Ekranı (Finish Modal)

- [ ] **Sonuç panelini oluştur**
  - Aynı Canvas içinde UI → Panel ekle. Ekranın ortasında, yarı-saydam koyu arkaplan.
  - Panel içine: Başlık text ("PARKUR TAMAMLANDI!"), Süre text, "Tekrar Oyna" butonu, "Ana Menüye Dön" butonu ekle.
  - Paneli varsayılan olarak **kapalı** (SetActive false) yap.

- [ ] **FinishUI.cs scriptini yaz**
  - ```csharp
    // public GameObject finishPanel;
    // public TMP_Text timeText;
    //
    // public void ShowFinishScreen(float time):
    //   finishPanel.SetActive(true);
    //   timeText.text = formattedTime;
    //
    // public void OnRetryClicked():
    //   GameManager.Instance.RestartGame();
    //
    // public void OnMenuClicked():
    //   GameManager.Instance.GoToMainMenu();
    ```
  - Butonların OnClick eventlerine ilgili fonksiyonları ata.

- [ ] **Bitiş anında oyunu duraklat ve cursor'u göster**
  - `GameManager.FinishGame()` içinde `Time.timeScale = 0f` yap.
  - `Cursor.lockState = CursorLockMode.None; Cursor.visible = true;` ekle.
  - Restart ve menüye dönüş fonksiyonlarında `Time.timeScale = 1f` yapmayı unutma.

---

## 8. Sahne Yönetimi ve Oyun Döngüsü

- [ ] **Build Settings'e sahneleri ekle**
  - `File → Build Settings` aç.
  - `Assets/Scenes/MainMenu` sahnesini sürükle → index 0 olmalı (oyun bununla açılır).
  - `Assets/Scenes/MainScene` sahnesini sürükle → index 1 olmalı.

- [ ] **Tüm sahne geçişlerini test et**
  - Ana Menü → "Oyuna Başla" → MainScene yükleniyor mu?
  - Oyun Sonu → "Ana Menüye Dön" → MainMenu yükleniyor mu?
  - Oyun Sonu → "Tekrar Oyna" → MainScene yeniden yükleniyor mu?

- [ ] **Oyun döngüsünü uçtan uca test et**
  - Tam döngü: MainMenu → Oyuna Başla → Engelleri geç → Düş → Checkpoint'ten devam et → 100. engeli geç → Bitiş → Süreyi gör → Tekrar Oyna / Menüye Dön.
  - Her aşamada hata (bug) olup olmadığını kontrol et.

---

## 9. Polishing (Son Rötuşlar)

- [ ] **Ses efektleri ekle (isteğe bağlı)**
  - Zıplama sesi, checkpoint geçiş sesi, düşme sesi, bitiş sesi.
  - `AudioSource` component'i ile oynat.

- [ ] **Görsel efektler ekle (isteğe bağlı)**
  - Checkpoint'e ulaşınca parıltı (Particle System).
  - Düşme anında ekrana kısa siyah fade efekti.

- [ ] **Performans optimizasyonu**
  - Uzaktaki objeleri gizlemek için Occlusion Culling veya LOD kullan.
  - Gereksiz script/component'leri kaldır.

---

## 10. Build (Çıktı Alma) ve Teslim

- [ ] **Windows (.exe) build al**
  - `File → Build Settings → Build` tıkla.
  - Çıktı klasörünü seç (örn. `Builds/Windows/`).
  - Build tamamlandıktan sonra `.exe` dosyasını çalıştırarak test et.

- [ ] **Build'i test et**
  - Oyun düzgün açılıyor mu?
  - Tüm mekanikler çalışıyor mu?
  - Timer ve skor sistemi doğru çalışıyor mu?
  - Performans kabul edilebilir mi? (FPS)

- [ ] **(İsteğe bağlı) Android APK build al**
  - `File → Build Settings → Android → Switch Platform`.
  - Player Settings → Company Name, Product Name, Package Name ayarla.
  - `Build` tıkla → `.apk` dosyasını telefona yükle ve test et.
  - Dokunmatik kontrollerin çalışması için ek UI butonları (sanal joystick) gerekebilir (Starter Assets'te mobil kontroller mevcut olabilir).

---

> **💡 İpucu:** Her bölümü bitirdikten sonra ilgili görevin başındaki `[ ]` işaretini `[x]` olarak değiştir.
> Böylece ilerleme durumunu her zaman takip edebilirsin.
