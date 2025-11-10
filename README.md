# 🎓 Acunmedya Akademi Bitirme Projesi – Order Management API

Bu proje, **Acunmedya Akademi / AkademIQ C# ile Yazılım Geliştirme Eğitimi** kapsamında geliştirilmiştir.  
Amaç, müşteri siparişlerini kaydeden ve sorgulayan basit bir RESTful API oluşturmaktır.

---

## 🧩 Teknolojiler
- ASP.NET Core 7.0
- Microsoft SQL Server
- C#
- JSON tabanlı Web API yapısı

---

## 🚀 Özellikler
- Yeni sipariş oluşturma (`POST /Order`)
- Siparişi ID’ye göre getirme (`GET /Order/{id}`)
- SQL bağlantısı ile veritabanı işlemleri
- Hata yönetimi (try-catch)
- Basit ve temiz katmanlı yapı (`Models` ve `Controllers` klasörleri)

---

## 🗂️ Proje Yapısı
```
AcunmedyaSunum/
 ├── Controllers/
 │    └── OrderController.cs
 ├── Models/
 │    └── CreateOrderModel.cs
 ├── appsettings.json
 ├── Program.cs
 └── README.md
```

---

## ⚙️ Veritabanı Tablosu
```sql
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerFirstName NVARCHAR(100),
    CustomerLastName NVARCHAR(100),
    Email NVARCHAR(255),
    Address NVARCHAR(255),
    PhoneNumber NVARCHAR(50),
    ProductName NVARCHAR(255),
    Barcode NVARCHAR(100),
    InvoiceAmount DECIMAL(18,2),
    CreatedAt DATETIME DEFAULT GETDATE()
);
```

---

## 📬 API Kullanımı

### ➕ Yeni Sipariş Oluşturma
```http
POST /Order
Content-Type: application/json

{
  "CustomerFirstName": "Yiğit",
  "CustomerLastName": "Yaşar",
  "Email": "yigit@example.com",
  "Address": "İstanbul",
  "PhoneNumber": "05551234567",
  "ProductName": "Özel Hediye Kutusu",
  "Barcode": "123456789",
  "InvoiceAmount": 199.90
}
```

### 🔍 Sipariş Getirme
```http
GET /Order/1
```

---

## 💾 Bağlantı Dizesi
Veritabanı bağlantısı `appsettings.json` içinde tanımlanır:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AcunmedyaOrders;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## 🧠 Geliştirici
**Yiğit [@yigityasar]**  
📧 yigityasar.dev@gmail.com  
🧩 Acunmedya Akademi - C# ile Yazılım Geliştirme Eğitimi


