# CarRental

Windows Forms desktop application for managing a car rental business — C# (.NET Framework 4.8), SQLite, MaterialSkin.

School project for the Programming 1 course, built in three phases:

- **V1** – basic Windows Forms app (login, cars, customers, reservations)
- **V2** – MaterialSkin UI, image uploads, BCrypt passwords, automatic prices, payments, dashboard
- **V3** – everything from the "what could be added" list (see below)

## Features

### Core (V1 + V2)
- Login with BCrypt password hashing
- Cars, customers, reservations and payments (CRUD, search, filters)
- Automatic price calculation, car status tracking
- Dashboard with statistics, latest reservations and most popular cars

### New in V3
| Feature | Where |
|---|---|
| PDF invoices (with VAT breakdown and payments) | Reservations → 🧾 RAČUN (completed reservations) |
| Printable rental contracts (PDF) | Reservations → 📄 POGODBA |
| Excel export (.xlsx) of every table | 📊 EXCEL button on each screen |
| Charts – revenue and reservations by month | Dashboard, Reports |
| Dark mode | 🌙 / ☀ button top right |
| English / Slovenian | EN / SL button top right (also on the login screen) |
| Email confirmations (SMTP, optional PDF contract attachment) | Reservations → ✉, after creating a reservation; set up in Settings → Email |
| Backup / restore (ZIP with database + images) | Settings → Varnostna kopija |
| User roles (admin / agent) and user management | Uporabniki (admin only) |
| Reservation calendar (cars × days timeline) | Koledar |
| Date-range reports (by month, car, customer, category, payment method) + PDF/Excel | Poročila |
| Car maintenance tracking with next-service reminders | Servisi, car details → 🔧 |
| Customer loyalty programme (Bronze/Silver/Gold discounts) | Applied automatically to new reservations; thresholds in Settings |
| Photo gallery per car | Car details → 📷 GALERIJA (or click the image) |
| Damage reports with photos | Poškodbe, reservation details, and asked when completing a reservation |

Other improvements: foreign keys are enforced, the database path no longer depends on the working directory, images are loaded without locking files, a dashboard warning bar (services due, open damages, returns today/overdue), and a fix for the car edit dialog always resetting the category.

### Roles
- **admin** – everything, including deleting, users, settings and backups
- **agent** – day-to-day work (add/edit, reservations, payments); cannot delete or change settings

## Tech stack
- C# / .NET Framework 4.8, Windows Forms + MaterialSkin.2
- SQLite (System.Data.SQLite)
- BCrypt.Net-Next – password hashing
- ClosedXML – Excel export
- PDFsharp / MigraDoc – PDF documents
- System.Windows.Forms.DataVisualization – charts (built into .NET Framework)

NuGet packages use `PackageReference`, so Visual Studio restores them automatically on build.

## Build & run
1. Open `CarRental.sln` in Visual Studio 2022
2. Build (Ctrl+Shift+B) – packages are restored automatically
3. Run (F5)

The database `izposoja.db` is created (or upgraded) next to the `.exe` on first start. Existing V2 databases are upgraded automatically without losing data.

**Default login:** `admin@izposoja.si` / `admin123`

## Project structure
```
CarRental/
├── Baza/        PovezavaBaze.cs – connection, schema, migrations
├── Modeli/      data models
├── Obrazci/     forms (screens and dialogs)
├── Pomocniki/   helpers – settings, theme, translations, PDF, Excel, email, backup, loyalty ...
└── Slike/       images (avtomobili/, poskodbe/)
```
