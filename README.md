# CarRental

A Windows Forms desktop application for managing a car rental business, built with C# (.NET Framework 4.8), SQLite and MaterialSkin.

| Version | What it is |
|---|---|
| **v1.0 – School project** | Built for the Programming 1 course: cars, customers, reservations, payments and a dashboard |
| **v2.0 – Home upgrade** | Built afterwards at home on top of the school project: PDF documents, Excel export, charts, dark mode, English/Slovenian, user roles, calendar, reports, maintenance, damage reports, loyalty programme and more |

The app can be switched between **English and Slovenian** at any time (button at the top right or on the login screen).

---

## v1.0 – School project

The version submitted for the Programming 1 course.

- Login system with BCrypt password hashing
- Modern Material Design UI (MaterialSkin) with a dark sidebar
- **Cars** – add, edit, delete, image upload, search by brand/model/plate, filter by category and status
- **Customers** – add, edit, delete, email validation, search, initials avatar
- **Reservations** – automatic price calculation (days × daily rate), only available cars offered, status management (active / completed / cancelled), car status updated automatically
- **Payments** – cash / card / bank transfer, statistics cards, amount suggested from the reservation
- **Dashboard** – statistics cards, latest reservations, most popular cars

## v2.0 – Home upgrade

After the course I kept working on the project at home and added all the features from my "what could be added" list:

| Feature | Description |
|---|---|
| 🧾 **PDF invoices** | Professional invoices with VAT breakdown, discounts and recorded payments |
| 📄 **Rental contracts** | Printable PDF rental agreement with parties, vehicle details, terms and signature lines |
| 📊 **Excel export** | Every table can be exported to a formatted `.xlsx` file |
| 📈 **Charts** | Revenue and reservations by month on the dashboard and in reports |
| 🌙 **Dark mode** | Switch between light and dark theme with one click |
| 🌍 **English / Slovenian** | The whole UI, messages and PDF documents switch language at runtime |
| ✉️ **Email confirmations** | Reservation confirmations sent to customers via SMTP, optionally with the contract attached |
| 💾 **Backup & restore** | One-click backup of the database and all images to a ZIP file; automatic safety copy before restoring |
| 👥 **User roles** | *Admin* (full access) and *Agent* (no deleting, users or settings), user management and password change |
| 📅 **Reservation calendar** | Monthly timeline of all cars with colored booking bars and service days |
| 📑 **Advanced reports** | Any date range: revenue by month, by car (with utilisation), by customer, by category and by payment method – export to PDF or Excel |
| 🔧 **Maintenance tracking** | Service history per car, costs, odometer and next-service reminders (overdue / due soon) |
| ⭐ **Loyalty programme** | Bronze / Silver / Gold levels based on completed rentals, discount applied automatically to new reservations |
| 📷 **Photo gallery** | Multiple images per car with a selectable main image |
| ⚠️ **Damage reports** | Record and photograph damage between rentals; the app asks about damage when a car is returned |

### Other improvements in v2
- Dashboard warning bar: services due, open damages, returns today and overdue returns
- Foreign keys are enforced, so no more orphaned reservations
- The database is always stored next to the executable
- Images are loaded without locking files
- Existing v1 databases are upgraded automatically without losing data
- Fixed: editing a car no longer resets its category

## Screenshots

*Coming soon.*

## Tech stack

- C# / .NET Framework 4.8
- Windows Forms + MaterialSkin.2
- SQLite (System.Data.SQLite)
- BCrypt.Net-Next – password hashing
- ClosedXML – Excel export *(v2)*
- PDFsharp / MigraDoc – PDF documents *(v2)*
- System.Windows.Forms.DataVisualization – charts *(v2)*

## Getting started

1. Open `CarRental.sln` in Visual Studio 2022
2. Build the solution (`Ctrl+Shift+B`) – NuGet packages are restored automatically
3. Run (`F5`)

The SQLite database `izposoja.db` is created automatically next to the executable on first start.

**Default login**
- Admin: `admin@izposoja.si` / `admin123`

**Demo data:** in *Settings → General → Add demo data* (admin only) the app fills the database with 10 cars, 12 customers, a year of reservations, payments, services and damage reports, and creates an agent account (`agent@izposoja.si` / `agent123`) to try the role system.

## Project structure

```
CarRental/
├── Baza/        Database connection, schema and migrations
├── Modeli/      Data models
├── Obrazci/     Forms (screens and dialogs)
├── Pomocniki/   Helpers – settings, theme, translations, PDF, Excel, email, backup, loyalty
└── Slike/       Images (cars, damages)
```

> Code identifiers are in Slovenian, as required by the original school project.
