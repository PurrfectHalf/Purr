🐾 Purrfect Half
Purrfect Half, Unity motoru ile geliştirilmiş, "Diğer Yarım" temasını odağına alan 2D bir kedi sahiplendirme ve yönetim oyunudur. Oyuncular, barınağa gelen müşterilerin hikayelerini analiz ederek onlara en uygun kedi dostumuzu bulmaya çalışır.

🕹️ Oyun Akışı (Game Loop)
Oyunumuz birbirine bağlı dört ana aşamadan oluşmaktadır:

Barınak Sahnesi (Main Hub): Oyunun kalbi burasıdır. Barınağınızın genel görünümünü ve gelen müşterileri burada görürsünüz. Hazır olduğunuzda "Eşleştir" butonu ile süreci başlatırsınız.

Eşleştirme Sahnesi (Matching): Müşterinin hikayesi ile barınaktaki kedilerin özelliklerini karşılaştırdığınız karar merkezidir.

Mini Oyun Sahnesi: Eğer doğru eşleştirmeyi yaparsanız, kedi ve insan arasındaki bağı kurmak için rastgele veya belirlenmiş bir mini oyuna yönlendirilirsiniz.

Sonuç Ekranı:

Başarı: Mini oyun geçilirse sahiplendirme tamamlanır, ün puan 15 artar ve yeni müşteriye geçilir.

Hata/Kayıp: Yanlış eşleştirmeler veya mini oyun kayıpları yüzünden ün puanınız negatif olursa "Game Over" ekranına geçilir ve oyuncu barınağına geri dönmek için yeniden başlayabilir.

🚀 Teknik Özellikler
Motor: Unity 6000.4.6f1 

Dil: C#

Sahne Yönetimi: SceneManager kullanılarak sahneler arası dinamik veri aktarımı.

Puan Sistemi: Negatif değerlere düşmemesi gereken, oyuncunun prestijini temsil eden Ün Puanı mekaniği.

📂 Sahne Yapısı (Scene Hierarchy)
Proje dosyasındaki temel sahneler şunlardır:

GirisSahnesi: Oyunun giriş ve karşılama ekranı.

purrfect: Müşterilerin kabul edildiği ana barınak alanı.

MatchingScene: Hikaye okuma ve kedi seçme arayüzü.

MiniGame: Refleks veya yetenek gerektiren geçiş oyunları.

GameOverScene: Ün puanı bittiğinde veya yanlış seçimde gidilen ekran.

🛠️ Kurulum ve Çalıştırma
GitHub üzerinden projeyi indirin veya klonlayın.

Unity Hub üzerinden projeyi ekleyin ve uygun editör sürümüyle açın.

Assets/purrfect.unity dosyasını açarak Play butonuna basın.

👥 Geliştirici Ekibi
İrem Büşra Sürüm
Unvan: Takım Lideri & Çok Yönlü Geliştirici (Lead & Multidisciplinary Developer)

Yazılım: Oyun içi sistemlerin kodlanması,oyun mimarisi ve sahne yönetimi.

Tasarım: Piksel art karakter çizimleri ve çevre dizaynı.

Yönetim: Proje planlama ve ekip koordinasyonu.

Kayra Cesur
Unvan: Çok Yönlü Geliştirici (Multidisciplinary Developer)

Yazılım: Temel mekaniklerin kodlanması ve mekanik geliştirme.

Görsel: Piksel art varlıkların oluşturulması ve sahne tasarımı.

Dizayn: Oyun dengesi ve bölüm tasarımı.

Ümmü Habibe Yüce
Unvan: Çok Yönlü Geliştirici (Multidisciplinary Developer)

Yazılım: Kodlama ve sistem entegrasyonu.

Görsel: Piksel art çizimleri ve UI/UX tasarımı.

Dizayn: Hikaye akışı ve oyun içi etkileşim tasarımı.


🎨 Kullanılan Varlıklar (Assets & Credits)
Oyunun görsel ve işitsel dünyası, hazır varlıklar ile ekibimizin özgün tasarımlarının birleşiminden oluşmaktadır.

🖼️ Görsel Tasarım (Graphics & Pixel Art)
Karakterler ve Kediler: Ekibimiz tarafından pixilart sayfası kullanılarak online şekilde piksel art formatında özgün olarak çizilmiştir.
https://www.pixilart.com/draw?ref=home-page

Barınak ve Sahne Tasarımları: Ekibimiz tarafından projenin temasına uygun olarak tasarlanmıştır:
https://last-tick.itch.io/animated-pixel-kittens-cats-32x32
https://toffeecraft.itch.io/pixel-cat-animations (https://toffeecraft.itch.io/cat-pixel-mega-pack)
https://limezu.itch.io/moderninteriors

Font:
https://poppyworks.itch.io/silver?download


UI/Arayüz Elemanları ve Ekstralar: 
https://toffeecraft.itch.io/cat-user-interface?download
https://pooklea.itch.io/emote-speech-bubble-32p

🎵 Ses ve Müzik (Audio)
Arka Plan Müziği: 
https://purrplecat.itch.io/up-all-night


✒️ Yazı Tipleri (Fonts)
Oyun İçi Font: https://poppyworks.itch.io/silver?download
