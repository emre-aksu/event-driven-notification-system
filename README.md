# 🔐 Event Driven Notification System + Password Reset Flow

Bu proje, modern .NET teknolojileri kullanılarak geliştirilmiş, **event-driven bildirim altyapısı** ve **gerçek bir şifre sıfırlama akışı** içeren backend sistemidir. Kullanıcılar bildirim alabilir, şifrelerini sıfırlayabilir ve tüm bu süreçler asenkron şekilde yönetilir.

---

## 🚀 Özellikler

- 📬 Email, SMS, Push gibi bildirimleri kuyruk üzerinden iletme
- 🔁 3 kez yeniden deneme (retry) mekanizması
- 📝 Her bildirim için audit (log) kaydı
- 🔐 Şifre sıfırlama akışı (token üretimi + şifre güncelleme)
- 🌐 HTML tabanlı basit kullanıcı arayüzü (reset-password.html)
- 📦 Clean Architecture / NTier yapı

---

## 📁 Proje Mimarisi

```
EventNotificationSystem/
├── Notification.API             # Web API (Minimal API + Static file support)
├── Notification.Worker         # Kuyruğu dinleyip bildirimleri işleyen servis
├── Notification.Application    # DTO'lar, servis arayüzleri
├── Notification.Domain         # Modeller: NotificationEvent, Audit, PasswordResetToken
├── Notification.Infrastructure # Kuyruk ve reset servisleri (In-Memory)
└── wwwroot/reset-password.html # Şifre sıfırlama için basit arayüz
```

---

## 🔧 Kurulum

### Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- Visual Studio veya VSCode

### Uygulamayı Başlatmak

```bash
dotnet build
dotnet run --project Notification.API
```

Swagger UI:  
```
https://localhost:7267/swagger
```

---

## 📬 Bildirim Gönderme

### `POST /api/notifications/send`

```json
{
  "type": "Email",
  "recipient": "test@example.com",
  "content": "Şifrenizi sıfırlamak için buraya tıklayın."
}
```

🔁 Arka planda worker kuyruktan alır → bildirimi yollar → audit kaydı oluşturur.

---

## 🔐 Şifre Sıfırlama Akışı

### 1️⃣ Token Oluştur (Şifre Sıfırlama Talebi)

`POST /api/auth/request-reset`

```json
{
  "email": "test@example.com"
}
```

🧠 Dönen cevap:

```json
{
  "message": "Şifre sıfırlama linki üretildi.",
  "token": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
}
```

---

### 2️⃣ HTML Arayüz Üzerinden Şifre Sıfırlama

Tarayıcıda şu linki aç:

```
https://localhost:7267/reset-password.html?token=BURAYA_TOKEN
```

📝 Yeni şifre gir → “Şifremi Sıfırla” → işlem tamam.

---

### 3️⃣ Arka Plan İşleyiş

- Token’lar RAM’de tutulur (In-Memory)
- Her token 15 dakika geçerlidir
- Tek kullanımlıktır

---

## 🌐 HTML UI

`Notification.API/wwwroot/reset-password.html`  
Basit şifre sıfırlama arayüzü içerir.

```html
<input type="password" id="newPassword" />
<button onclick="resetPassword()">Şifremi Sıfırla</button>
```

---

## ✨ Geliştirme İçin Öneriler

| Özellik                         | Durum   |
|---------------------------------|---------|
| SMTP mail servisi               | ✖ (simüle ediliyor)  
| Redis/Mongo ile token saklama   | ✖ (In-memory)
| Serilog + Seq loglama           | ✖ (Konsol logu)
| Gerçek kullanıcı DB             | ✖ (email + şifre dummy)

---

## 📜 Lisans

MIT License — dilediğiniz gibi kullanabilir ve geliştirebilirsiniz.

---

## 👨‍💻 Geliştirici

**Emre Aksu**  
.NET Backend Developer  
📫 GitHub: [github.com/kullanici](https://github.com/emre-aksu)