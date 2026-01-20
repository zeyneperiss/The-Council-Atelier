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

## 🛠️ Kurulum

1. Bu depoyu klonlayın:
   ```bash
   git clone [https://github.com/zeyneperiss/The-Council-Atelier.git](https://github.com/zeyneperiss/The-Council-Atelier.git)
