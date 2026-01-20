# The Council Atelier 🏛️

The Council Atelier, modern bir moda atölyesi veya tasarım stüdyosu için geliştirilmiş, kapsamlı bir **ASP.NET Core MVC** tabanlı web yönetim sistemidir. Kullanıcıların tasarımları inceleyebileceği bir arayüz ve yöneticilerin tüm içeriği kontrol edebileceği gelişmiş bir Admin paneli sunar.

## 🚀 Teknik Özellikler

- **Framework:** .NET 8.0 / 7.0 (ASP.NET Core MVC)
- **Database:** SQL SERVER , Entity Framework Core (Code First Yaklaşımı)
- **Güvenlik:** Şifreleme algoritmaları ile desteklenmiş kullanıcı doğrulama sistemi (HashGenerator).
- **Mimari:** - **Areas:** Admin ve User arayüzleri birbirinden tamamen izole edilmiştir.
  - **Repository Pattern & Dependency Injection:** Esnek ve test edilebilir bir kod yapısı.
- **Frontend:** HTML5, CSS3, JavaScript ve Razor Pages.

## 📂 Proje Yapısı

- `Areas/Admin`: Yönetici paneli kontrolcüleri ve görünümleri.
- `Controllers`: Uygulamanın ana iş mantığının yönetildiği merkez.
- `Models`: Veritabanı tablolarının ve View-Model yapılarının tanımı.
- `Data`: Veritabanı bağlamı (Context) ve migration dosyaları.
- `wwwroot`: CSS, JS ve görseller gibi statik dosyalar.
- `HashGenerator.cs`: Veri güvenliği için özel şifreleme sınıfı.
- 
├── Areas/Admin               # Admin section and controllers
├── Controllers               # App controllers
├── Data                      # DbContext and data logic
├── Migrations                # Database migrations (EF Core)
├── Models                    # Domain models
├── Views                     # Razor Views for UI
├── wwwroot                   # Static assets
├── appsettings.json          # Configuration
├── CouncilAtelier.sln        # Solution file
## 🛠️ Kurulum

1. Bu depoyu klonlayın:
   ```bash
   git clone [https://github.com/zeyneperiss/The-Council-Atelier.git](https://github.com/zeyneperiss/The-Council-Atelier.git)

  ## Proje Görselleri:

   <img width="1392" height="712" alt="makale" src="https://github.com/user-attachments/assets/10fdb0fe-13fe-4854-9e20-8bccc99b7c38" />
  
<img width="1198" height="707" alt="workshops" src="https://github.com/user-attachments/assets/fd2f7c26-d897-4970-9749-5b1772ba04c7" />

   <img width="1191" height="592" alt="Screenshot 2026-01-21 at 00 28 07" src="https://github.com/user-attachments/assets/dfeb29a0-7248-413a-9aa4-c63adce454ae" />

<img width="1418" height="779" alt="hero" src="https://github.com/user-attachments/assets/174b293b-3365-4c4d-bab4-21334b74207f" />

<img width="1392" height="712" alt="Screenshot 2026-01-21 at 00 28 33" src="https://github.com/user-attachments/assets/159538d5-3bc5-4e5a-8f01-78bbf70984fd" />

## youtube link: https://youtu.be/UozV0xac1eQ?si=nZBuO6uLveZBIL16

