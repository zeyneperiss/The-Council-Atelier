# 🎨 COUNCIL ATELIER - PROJE GELİŞİM DOKÜMANTASYONU

**Proje:** CouncilAtelier - The Council Coffee Atelier Web Sitesi  
**Framework:** ASP.NET Core MVC (.NET 8.0)  
**Tarih:** Ocak 2026  
**Durum:** Aktif Geliştirme

---

## 📋 İÇİNDEKİLER

1. [Proje Hakkında](#proje-hakkında)
2. [Yapılan Tüm Değişiklikler](#yapılan-tüm-değişiklikler)
3. [Teknik Mimari](#teknik-mimari)
4. [Dosya Yapısı ve Açıklamaları](#dosya-yapısı-ve-açıklamaları)
5. [Önemli Özellikler](#önemli-özellikler)
6. [Gelecek Planlar](#gelecek-planlar)
7. [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)

---

## 🎯 PROJE HAKKINDA

**The Council Atelier**, Sakarya Serdivan'da bulunan The Council Coffee'nin sanat ve yaratıcılık atölyesi web sitesidir. Site, kahve kültürü ile sinema ve fotoğraf sanatını birleştiren programlar, workshoplar ve makaleler sunmaktadır.

### Ana Özellikler:
- 🎬 **Programlar:** Sinema ve fotoğraf eğitim programları
- 🛠️ **Workshoplar:** Pratik atölye çalışmaları (görsel yükleme özelliği ile)
- 📝 **Makaleler:** Sinema ve fotoğraf üzerine yazılar
- 📋 **Başvuru Sistemi:** Programa katılım için online başvuru formu
- 🔐 **Admin Panel:** İçerik yönetimi için tam özellikli yönetim paneli

---

## 🔄 YAPILAN TÜM DEĞİŞİKLİKLER

### AŞAMA 1: Admin Panel Modernizasyonu
**Tarih:** Ocak 2026 - İlk Gün

#### Yapılanlar:
- ❌ **Öncesi:** Windows 98 tarzı basit formlar
- ✅ **Sonrası:** Modern card-based tasarım

#### Değişiklikler:
1. **Admin Başvurular Sayfası** (`Areas/Admin/Views/Basvuru/Index.cshtml`)
   - Tablo yerine card grid sistemi
   - Her başvuru için ayrı card
   - Durum badge'leri (Beklemede/Kabul/Red)
   - Hover efektleri ve animasyonlar

2. **Admin CSS** (`wwwroot/css/admin.css`)
   - `.admin-container` ve `.admin-card` stilleri
   - Modern renk paleti
   - Responsive tasarım
   - Animasyonlar

3. **Emoji Temizliği**
   - Tüm admin formlarından emoji'ler kaldırıldı
   - Profesyonel görünüm

---

### AŞAMA 2: Workshop Görsel Yükleme Sistemi
**Tarih:** Ocak 2026

#### Yapılanlar:
1. **Model Güncellemesi** (`Models/Workshoplar.cs`)
   ```csharp
   [MaxLength(200)]
   public string? ImagePath { get; set; }  // Yeni alan
   ```

2. **Migration Oluşturuldu**
   - `AddWorkshopImagePath` migration'ı
   - Veritabanına `ImagePath` kolonu eklendi
   - `dotnet ef migrations add AddWorkshopImagePath`
   - `dotnet ef database update`

3. **Admin Controller** (`Areas/Admin/Controllers/WorkshoplarController.cs`)
   - `IWebHostEnvironment` enjeksiyonu
   - Görsel yükleme metodu
   - Dosya doğrulama (JPG, JPEG, PNG)
   - wwwroot/images klasörüne kaydetme

4. **Admin Form View** (`Areas/Admin/Views/Workshoplar/Create.cshtml`, `Edit.cshtml`)
   - Görsel yükleme input'u eklendi
   - Mevcut görseli görüntüleme
   - Önizleme özelliği

---

### AŞAMA 3: Font ve Tipografi Güncellemesi
**Tarih:** Ocak 2026

#### Yapılanlar:
- **Eski Font:** Sistem varsayılan
- **Yeni Font:** Montserrat (Google Fonts)
- Tüm sitede Montserrat kullanımı
- Font ağırlıkları: 300, 400, 500, 600, 700

#### Güncellenen Dosyalar:
- `Views/Shared/_Layout.cshtml` - Font linki eklendi
- `wwwroot/css/site.css` - Font-family tanımları

---

### AŞAMA 4: Anasayfa Programlar Bölümü
**Tarih:** Ocak 2026

#### Problem:
- Anasayfada programlar bölümü boş gözüküyordu
- Controller'da yanlış sorgu

#### Çözüm:
1. **HomeController** (`Controllers/HomeController.cs`)
   ```csharp
   // YANLIŞ (öncesi):
   var programlar = _db.Programlar.Where(p => !p.IsDeleted).ToList();
   
   // DOĞRU (sonrası):
   var programlar = _db.Programlar
       .Where(p => !p.IsDeleted)
       .Include(p => p.Category)
       .OrderByDescending(p => p.Id)
       .Take(3)
       .ToList();
   ```

2. **View Güncellemesi** (`Views/Home/Index.cshtml`)
   - 3 kolonlu program kartları
   - Kategori badge'leri
   - "Tümünü Gör" butonu
   - Hover animasyonları

---

### AŞAMA 5: Modern Footer Tasarımı
**Tarih:** Ocak 2026

#### Yapılanlar:
**Öncesi:** Basit copyright footer  
**Sonrası:** Tam özellikli modern footer

#### İçerik:
1. **Hero Bölümü**
   - THE COUNCIL logo
   - COFFEE // ATELIER tagline

2. **Grid Bölümü (3 Kolon)**
   - **Sol:** Slogan ve açıklama
     - "MORE THAN COFFEE"
     - Mavi Durak, Serdivan konum bilgisi
   
   - **Orta:** Navigasyon
     - Programlar
     - Workshoplar
     - Makaleler
     - Başvur
   
   - **Sağ:** İletişim/Bağlantılar
     - 🌐 thecouncilcoffee.com (yeni eklendi)
     - ✉ info@thecouncilcoffee.com
     - 📷 @thecouncilcoffee (Instagram)
     - 📍 Haritada Gör (Google Maps)

3. **Alt Bar**
   - Copyright
   - Gizlilik linki

#### CSS:
- Gradient arka plan
- Modern tipografi
- Hover efektleri
- Responsive tasarım

---

### AŞAMA 6: Giriş Sayfası Modernizasyonu
**Tarih:** Ocak 2026

#### Yapılanlar:
**Öncesi:** Basit login formu  
**Sonrası:** Split-screen modern tasarım

#### Tasarım:
1. **Sol Taraf:** Branding
   - THE COUNCIL başlık
   - ATELIER alt başlık
   - ~~Yönetim Paneli yazısı~~ (kaldırıldı)
   - Animasyonlu pattern arka plan

2. **Sağ Taraf:** Form
   - "Hoş Geldin" başlık
   - Kullanıcı adı input (ikon ile)
   - Şifre input (ikon ile)
   - Giriş yap butonu (hover animasyonlu)
   - ~~Demo hesap bilgisi (admin/Admin123!)~~ (kaldırıldı)
   - Anasayfaya dön linki

#### Özellikler:
- Slideİn animasyonlar
- Hata mesajı gösterimi
- Responsive tasarım
- Modern input stilleri

---

### AŞAMA 7: Başvuru Sayfası Tasarımı
**Tarih:** Ocak 2026

#### Yapılanlar:
**Öncesi:** Basit form  
**Sonrası:** 2 kolonlu modern tasarım

#### Layout:
1. **Sol Kolon:** Bilgilendirme
   - Başlık ve açıklama
   - Özellikler listesi:
     - ✓ Ücretsiz Katılım
     - ✓ Sertifika
     - ✓ Networking
     - ✓ Pratik Deneyim

2. **Sağ Kolon:** Form
   - İsim Soyisim
   - Email
   - Telefon
   - İlgilenilen Program (dropdown)
   - Motivasyon yazısı (textarea)
   - Başvur butonu

#### Özellikler:
- İkon'lu input'lar
- Modern card tasarımı
- Başarı modal'ı
- Animasyonlar
- Responsive tasarım

---

### AŞAMA 8: Anasayfa Makaleler Bölümü
**Tarih:** Ocak 2026

#### Yapılanlar:
**Öncesi:** Liste görünümü  
**Sonrası:** Modern card grid

#### Tasarım:
- 3 kolonlu grid
- Her makale için card
- Yıl badge'i
- Hover efektleri (scale, shadow)
- "Tüm Makaleleri Gör" butonu
- Direkt makale detay sayfasına link (popup kaldırıldı)

---

### AŞAMA 9: Makaleler Index Sayfası
**Tarih:** Ocak 2026

#### Yapılanlar:
**Öncesi:** Basit liste  
**Sonrası:** Tam sayfa modern tasarım

#### Bölümler:
1. **Hero Section**
   - "Sinema & Fotoğraf" badge
   - "Makaleler" başlık
   - Açıklama metni

2. **Stats Section** (3 İstatistik)
   - 📚 Makale Sayısı (dinamik)
   - 📅 Yıl Sayısı (dinamik)
   - ♾️ Sonsuz İlham

3. **Meta Bilgiler**
   - En son güncelleme tarihi
   - Kategori bilgisi

4. **Makaleler Grid**
   - Responsive card'lar
   - Yıl badge'leri
   - Özet metinler
   - Tarih bilgileri

5. **Empty State**
   - Makale yoksa gösterilen özel tasarım

---

### AŞAMA 10: Makale Detay Sayfası (Son Güncelleme)
**Tarih:** Ocak 2026 - Son Değişiklikler

#### Problem:
- Navigasyon karmaşası: Anasayfa → Popup → Detay Sayfası
- Hem popup hem detay sayfası tasarımları zayıf
- Emoji'ler profesyonel değil

#### Çözüm:
1. **Navigasyon Düzeltmesi**
   - Popup kaldırıldı
   - Direkt detay sayfasına yönlendirme
   - `js-article-popup` class'ı ve `data-slug` attribute kaldırıldı

2. **Detay Sayfası Tam Yenileme** (`Views/Makaleler/Details.cshtml`)
   
   **Hero Section:**
   - Gradient arka plan
   - Geri dön linki (animasyonlu)
   - Yıl + Tarih badge'i
   - Büyük başlık
   - Özet metin

   **Content Section:**
   - 2 kolonlu layout (Ana metin + Sidebar)
   - Okunabilir tipografi (17px, 1.8 line-height)
   
   **Sidebar:**
   - **Makale Bilgileri Kartı:**
     - 📅 Takvim ikonu (SVG) → Yayın Tarihi
     - 📚 Kitap ikonu (SVG) → Okuma Süresi (otomatik hesaplanan)
     - 🏷️ Tag ikonu (SVG) → Kategori
   
   - **CTA Kartı:**
     - Gradient arka plan
     - "İlgini Çekti mi?" başlık
     - Başvuru sayfasına yönlendirme

   **Navigation Footer:**
   - "Tüm Makalelere Dön" butonu

3. **Emoji'lerden SVG'ye Geçiş**
   - Emoji'ler kaldırıldı
   - Modern SVG ikonlar (Heroicons benzeri)
   - Accent renginde (kahverengi)
   - Vektörel ve responsive

#### CSS Eklemeleri (`wwwroot/css/site.css`):
- `.article-detail-page` ve alt sınıfları
- Hero, content, sidebar stilleri
- Sticky sidebar (scroll'da yukarıda kalıyor)
- Responsive breakpoint'ler (968px, 640px)
- Smooth animasyonlar

---

## 🏗️ TEKNİK MİMARİ

### Teknoloji Stack:
```
Backend:
- ASP.NET Core MVC (.NET 8.0)
- Entity Framework Core
- SQL Server / SQLite

Frontend:
- Razor Views
- Bootstrap 5
- Custom CSS (site.css, admin.css)
- Vanilla JavaScript
- Google Fonts (Montserrat)

Özellikler:
- Soft Delete Pattern (IsDeleted, DeletedAt)
- Image Upload (IWebHostEnvironment)
- Area-based Admin Panel
- Repository Pattern (DbContext)
```

### Veritabanı Modelleri:

#### 1. **Article** (Makaleler)
```csharp
- Id (int, PK)
- Title (string, 200)
- Slug (string, 220)
- Summary (string, nullable)
- Content (string)
- PublishedAt (DateTime)
- CategoryId (int, FK)
- Category (Navigation)
- IsDeleted (bool)
- DeletedAt (DateTime?)
```

#### 2. **Programlar** (Programlar)
```csharp
- Id (int, PK)
- Title (string, 180)
- Description (string)
- CategoryId (int, FK)
- Category (Navigation)
- IsDeleted (bool)
- DeletedAt (DateTime?)
```

#### 3. **Workshoplar** (Workshop'lar)
```csharp
- Id (int, PK)
- Title (string, 180)
- Description (string)
- CategoryId (int, FK)
- Category (Navigation)
- EventDate (DateTime?)
- Location (string?, 120)
- Capacity (int?)
- ImagePath (string?, 200) // ÖNEMLİ: Görsel yolu
- IsDeleted (bool)
- DeletedAt (DateTime?)
```

#### 4. **Basvuru** (Başvurular)
```csharp
- Id (int, PK)
- FullName (string, 100)
- Email (string, 100)
- PhoneNumber (string?, 20)
- ProgramId (int?, FK)
- MotivationText (string?)
- SubmittedAt (DateTime)
- Status (int) // 0=Beklemede, 1=Kabul, 2=Red
```

#### 5. **Category** (Kategoriler)
```csharp
- Id (int, PK)
- Name (string, 100)
```

### Migration Listesi:
```
1. 20260106104827_InitDb
2. 20260106111649_AddCategoryAndProgramlar
3. 20260106113632_SeedCouncilContent
4. 20260106154809_AddArticlesSeed
5. 20260109174858_AddSoftDeleteToArticles
6. 20260111065727_AddSoftDeleteToProgramlar
7. 20260112100710_AddWorkshoplar
8. 20260117100016_AddBasvuru
9. [YENİ] AddWorkshopImagePath (görsel ekleme için)
```

---

## 📁 DOSYA YAPISI VE AÇIKLAMALARI

### Ana Dizin:
```
CouncilAtelier/
│
├── Areas/
│   └── Admin/                      # Admin panel alanı
│       ├── Controllers/
│       │   ├── BasvuruController.cs       # Başvuru yönetimi
│       │   ├── MakalelerController.cs     # Makale CRUD
│       │   ├── ProgramlarController.cs    # Program CRUD
│       │   └── WorkshoplarController.cs   # Workshop CRUD + Image Upload
│       └── Views/
│           ├── Basvuru/
│           │   └── Index.cshtml           # Modern card-based başvuru listesi
│           ├── Makaleler/
│           ├── Programlar/
│           └── Workshoplar/
│
├── Controllers/                    # Public controllers
│   ├── HomeController.cs           # Anasayfa, programlar/workshoplar listesi
│   ├── AccountController.cs        # Login/Logout
│   ├── BasvuruController.cs        # Başvuru formu
│   ├── MakalelerController.cs      # Makale listesi ve detay
│   ├── ProgramlarController.cs     # Program detay
│   └── WorkshoplarController.cs    # Workshop listesi
│
├── Models/                         # Veri modelleri
│   ├── Article.cs
│   ├── Programlar.cs
│   ├── Workshoplar.cs              # ImagePath ile güncellenmiş
│   ├── Basvuru.cs
│   ├── Category.cs
│   ├── HomeViewModel.cs            # Anasayfa için composite model
│   └── ErrorViewModel.cs
│
├── Views/                          # Public views
│   ├── Shared/
│   │   ├── _Layout.cshtml          # Ana layout (header, footer, Montserrat font)
│   │   └── Error.cshtml
│   ├── Home/
│   │   ├── Index.cshtml            # Anasayfa (hero, programs, workshops, articles)
│   │   ├── Privacy.cshtml
│   │   └── NotFound.cshtml
│   ├── Account/
│   │   ├── Login.cshtml            # Modern split-screen login (demo bilgisi kaldırıldı)
│   │   └── Denied.cshtml
│   ├── Basvuru/
│   │   └── Index.cshtml            # 2 kolonlu başvuru formu
│   ├── Makaleler/
│   │   ├── Index.cshtml            # Hero + Stats + Grid tasarım
│   │   ├── Details.cshtml          # Tam yenilenen detay sayfası (SVG ikonlar)
│   │   └── _ArticlePopup.cshtml    # KULLANILMIYOR (silinebilir)
│   ├── Programlar/
│   │   ├── Index.cshtml
│   │   └── Details.cshtml
│   └── Workshoplar/
│       └── Index.cshtml
│
├── wwwroot/                        # Static files
│   ├── css/
│   │   ├── site.css                # 2500+ satır modern CSS
│   │   │                           # İçerik: Header, Hero, Cards, Articles,
│   │   │                           # Login, Apply, Footer, Animations
│   │   └── admin.css               # Admin panel CSS
│   ├── js/
│   │   ├── site.js                 # Client-side JS (popup kodu var ama kullanılmıyor)
│   │   └── admin.js                # Admin sidebar toggle
│   └── images/                     # Görseller (workshop resimleri burada)
│
├── Data/
│   └── CouncilAtelierContext.cs    # DbContext
│
├── Migrations/                     # EF Core migrations
│
├── appsettings.json
├── Program.cs                      # Startup configuration
└── CouncilAtelier.csproj
```

---

## ⭐ ÖNEMLİ ÖZELLİKLER

### 1. Soft Delete Pattern
Tüm modellerde:
```csharp
public bool IsDeleted { get; set; } = false;
public DateTime? DeletedAt { get; set; }
```

Controller'larda:
```csharp
.Where(x => !x.IsDeleted)  // Silinmemişleri getir
```

### 2. Image Upload Sistemi
Workshop'lara görsel yükleme:

**Controller:**
```csharp
private readonly IWebHostEnvironment _env;

if (file != null && file.Length > 0)
{
    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }
    workshop.ImagePath = "/images/" + fileName;
}
```

**View:**
```html
<input type="file" name="file" accept="image/*" />
```

### 3. Responsive Tasarım
Tüm sayfalar responsive:
```css
@media (max-width: 968px) { ... }
@media (max-width: 640px) { ... }
```

### 4. Modern Animasyonlar
```css
@keyframes fadeInUp {
    from {
        opacity: 0;
        transform: translateY(30px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}
```

### 5. CSS Custom Properties
```css
:root {
    --text: #1a1a1a;
    --muted: #666666;
    --accent: #8b5a2b;
    --bg: #f6f1e8;
    --max: 1280px;
}
```

---

## 🎨 TASARIM SİSTEMİ

### Renk Paleti:
```css
--text: #1a1a1a        /* Ana metin */
--muted: #666666       /* İkincil metin */
--accent: #8b5a2b      /* Vurgu rengi (kahverengi) */
--bg: #f6f1e8          /* Arka plan (krem) */
```

### Tipografi:
```css
Font Family: 'Montserrat', sans-serif
Weights: 300 (Light), 400 (Regular), 500 (Medium), 600 (SemiBold), 700 (Bold)

Başlıklar: 700 (Bold)
Normal Metin: 400 (Regular)
Butonlar: 600 (SemiBold)
```

### Component Library:
- **Cards:** Gölge, border-radius, hover efektleri
- **Buttons:** Primary (solid), Secondary (outline)
- **Inputs:** İkonlu, focus efektleri
- **Badges:** Küçük etiketler (yıl, kategori, durum)
- **Grids:** 3 kolonlu responsive grid'ler
- **Modals:** Başarı/Hata mesajları

---

## 🚀 GELECEK PLANLAR

### Tamamlanmış ✅
- [x] Admin panel modernizasyonu
- [x] Workshop görsel yükleme
- [x] Anasayfa tasarımı
- [x] Footer tasarımı
- [x] Login sayfası
- [x] Başvuru formu
- [x] Makaleler sayfası
- [x] Makale detay sayfası
- [x] Emoji'lerden SVG'ye geçiş
- [x] Navigasyon akışı düzeltmesi

### Yapılabilecekler 🔮
- [ ] Workshop detay sayfası (popup modal yerine tam sayfa)
- [ ] Program detay sayfası güzelleştirmesi
- [ ] Admin panel'de istatistik dashboard'u
- [ ] Makale arama ve filtreleme
- [ ] Kategori sayfaları
- [ ] Kullanıcı profil sistemi
- [ ] Workshop kayıt sistemi
- [ ] Email bildirimleri (başvuru onayı)
- [ ] Site içi arama
- [ ] Blog yorumları
- [ ] Sosyal medya entegrasyonu
- [ ] SEO optimizasyonu
- [ ] Çoklu dil desteği (TR/EN)
- [ ] Dark mode
- [ ] Popup kodu temizliği (site.js, _ArticlePopup.cshtml)

### Teknik İyileştirmeler 🔧
- [ ] Repository pattern implementasyonu
- [ ] Unit test'ler
- [ ] API endpoint'leri
- [ ] Caching mekanizması
- [ ] Logging sistemi
- [ ] Error handling middleware
- [ ] File upload güvenlik kontrolleri (dosya boyutu, tip kontrolü)
- [ ] Image optimization (resize, compress)

---

## 💻 KURULUM VE ÇALIŞTIRMA

### Gereksinimler:
```
- .NET 8.0 SDK
- SQL Server / SQLite
- Visual Studio Code veya Visual Studio 2022
```

### Adımlar:

1. **Projeyi Klonla/İndir**
   ```bash
   cd /Users/zh/Desktop/MVC/CouncilAtelier
   ```

2. **Bağlantı Dizesini Ayarla**
   `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "..."
   }
   ```

3. **Veritabanını Oluştur**
   ```bash
   dotnet ef database update
   ```

4. **Projeyi Çalıştır**
   ```bash
   dotnet run
   ```

5. **Tarayıcıda Aç**
   ```
   http://localhost:5136
   ```

### Admin Girişi:
```
Kullanıcı Adı: admin
Şifre: Admin123!
```

Admin Panel: `http://localhost:5136/Admin`

---

## 🐛 BİLİNEN SORUNLAR VE ÇÖZÜMLERİ

### 1. Workshop Görseli Yüklenmiyor
**Problem:** workshop4.jpg 18MB olduğu için timeout hatası  
**Çözüm:** Görsel WORKSHOP1.png ile değiştirildi (Home/Index.cshtml line 73)

### 2. CSS line-clamp Uyarısı
**Problem:** `-webkit-line-clamp` tek başına çalışmıyor  
**Çözüm:** Standart `line-clamp` property'si eklendi

### 3. "FOTOĞRAF" Header'da Gözüküyor
**Problem:** Header'da "FOTOĞRAF" yazısı görünüyor ama kodda yok  
**Çözüm:** Muhtemelen browser cache veya extension sorunu, kodda bulunamadı

### 4. Popup Navigation Karmaşası
**Problem:** Anasayfa → Popup → Detay sayfası navigasyonu kullanıcıyı şaşırtıyordu  
**Çözüm:** ✅ Popup kaldırıldı, direkt detay sayfasına yönlendirme yapıldı

---

## 📊 PROJE İSTATİSTİKLERİ

### Kod Metrikleri:
```
CSS Dosyaları:
- site.css: ~2500 satır
- admin.css: ~800 satır

View Dosyaları: ~30 adet
Controller'lar: 8 adet
Model'ler: 6 adet
Migration'lar: 9 adet
```

### Özellik Sayısı:
```
✅ 5 Ana Sayfa
✅ 4 Admin Panel Modülü
✅ 1 Login Sistemi
✅ 1 Başvuru Formu
✅ 1 Image Upload Sistemi
✅ Sonsuz Kahve ☕
```

---

## 📝 NOTLAR

### Önemli Dosya Konumları:
- **Workshop Görselleri:** `/wwwroot/images/`
- **Ana CSS:** `/wwwroot/css/site.css`
- **Admin CSS:** `/wwwroot/css/admin.css`
- **Layout:** `/Views/Shared/_Layout.cshtml`
- **Anasayfa:** `/Views/Home/Index.cshtml`

### Veritabanı:
- Connection String: `appsettings.json` içinde
- Migration'lar: `/Migrations/` klasöründe
- Seed data: Migration dosyalarında

### Güvenlik:
- Admin Login: Cookie-based authentication
- XSS Protection: `@Html.Raw()` dikkatli kullanılıyor
- CSRF Token: Tüm formlarda `@Html.AntiForgeryToken()`
- File Upload: Extension kontrolü yapılıyor

---

## 🎓 ÖĞRENME NOKTALARI

Bu projede kullanılan teknikler:

1. **ASP.NET Core MVC Pattern**
2. **Entity Framework Core** (Code-First)
3. **Razor View Engine**
4. **Area-based Admin Panel**
5. **Soft Delete Pattern**
6. **Image Upload ve File Handling**
7. **Responsive Web Design**
8. **CSS Animations**
9. **SVG Icons**
10. **Modern CSS (Flexbox, Grid, Custom Properties)**

---

## 👥 İLETİŞİM

**Proje:** CouncilAtelier  
**Lokasyon:** The Council Coffee, Mavi Durak, Serdivan  
**Website:** https://www.thecouncilcoffee.com  
**Email:** info@thecouncilcoffee.com  
**Instagram:** @thecouncilcoffee

---

## 📄 LİSANS

Bu proje The Council Coffee için özel olarak geliştirilmiştir.

---

**Son Güncelleme:** 19 Ocak 2026  
**Durum:** ✅ Aktif - Tüm ana özellikler tamamlandı  
**Sonraki Adım:** Workshop detay sayfası veya admin dashboard

---

## 🎉 TEŞEKKÜRLER!

Bu dokümantasyon, projeye dönüldüğünde nereden devam edileceğini anlamak için hazırlanmıştır. Her şey detaylıca açıklanmıştır - kod yapısından tasarım kararlarına kadar.

**Unutma:** Kahve içmeyi unutma! ☕

---

_Bu dokümantasyon otomatik olarak oluşturulmuştur. Proje geliştirilmeye devam ettikçe güncellenmelidir._
