# 🚗 CarRental

> A desktop application for managing car rental operations, built with C# Windows Forms and SQLite.

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

## 📋 About

CarRental is a Windows Forms desktop application developed as a school project for the **Programming 1** course. It provides a complete solution for managing a car rental business, including vehicle inventory, customer records, reservations, and payments.




## ✨ Features

- 🔐 **Secure login** with BCrypt password hashing
- 🚙 **Car management** with image uploads and status tracking
- 👥 **Customer registration** with search and filtering
- 📅 **Reservation system** with automatic price calculation
- 💰 **Payment tracking** with real-time statistics
- 📊 **Dashboard** showing business metrics and popular cars
- 🔍 **Search and filtering** across all data tables
- 🎨 **Modern Material Design UI** via MaterialSkin library

## 🛠️ Tech Stack

| Technology | Purpose |
|------------|---------|
| **C#** | Programming language |
| **.NET Framework 4.8** | Runtime |
| **Windows Forms** | GUI framework |
| **SQLite** | Local database |
| **MaterialSkin.2** | Modern UI styling |
| **BCrypt.Net-Next** | Password hashing |

## 📁 Project Structure

```
CarRental/
├── Obrazci/          # Windows Forms (UI)
│   ├── PrijavaObrazec.cs
│   ├── GlavniObrazec.cs
│   ├── AvtomobiliObrazec.cs
│   ├── StrankeObrazec.cs
│   ├── RezervacijeObrazec.cs
│   ├── PlacilaObrazec.cs
│   └── DashboardObrazec.cs
├── Baza/             # Database connection
│   └── PovezavaBaze.cs
├── Modeli/           # Data models
│   ├── Avtomobil.cs
│   ├── Stranka.cs
│   ├── Rezervacija.cs
│   └── Placilo.cs
├── Pomocniki/        # Helper classes
└── Slike/            # Car images
    └── avtomobili/
```

## 🗄️ Database Schema

The SQLite database (`izposoja.db`) contains 6 interconnected tables:

- `kategorije` — Car categories (Economy, SUV, Luxury)
- `avtomobili` — Cars with details and image paths
- `stranke` — Customers
- `zaposleni` — Employees (for login)
- `rezervacije` — Reservations linking customers and cars
- `placila` — Payments linked to reservations

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022 (Community Edition is fine)
- .NET Framework 4.8
- Windows 10/11

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/kevinm124/CarRental.git
   ```

2. **Open the solution**
   - Open `CarRental.sln` in Visual Studio 2022

3. **Restore NuGet packages**
   - Right-click on Solution → **Restore NuGet Packages**

4. **Build and run**
   - Press `F5` or click **Start**

### Default Login

- **Email:** `admin@izposoja.si`
- **Password:** `admin123`

The database is automatically created on first launch.

## 📸 Screenshots

<table>
  <tr>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/e3f4ad65-6077-4af3-997d-1e22df40d283" width="400" />
      <br />
      <sub><b>🔐 Login Screen</b></sub>
    </td>
    <td align="center">
      <img src="https://github.com/user-attachments/assets/e2be4f75-9d71-40c9-a9e3-4d8db60fd196" width="400" />
      <br />
      <sub><b>➕ Add New Car</b></sub>
    </td>
  </tr>
</table>

### 📊 Full Application Views

<img src="https://github.com/user-attachments/assets/7d911b74-2183-4527-acab-d6391efcc33c" width="100%" />
<p align="center"><sub><b>Dashboard with Statistics</b></sub></p>

<img src="https://github.com/user-attachments/assets/c206b489-c4e7-4bcd-9216-2119f445485c" width="100%" />
<p align="center"><sub><b>Car Management</b></sub></p>

<img src="https://github.com/user-attachments/assets/f88b2f87-3950-417c-ad1b-2d146bf5e757" width="100%" />
<p align="center"><sub><b>Customers View</b></sub></p>


## 🎓 Learning Outcomes

Through this project, I gained hands-on experience with:

- Object-oriented programming in C#
- Windows Forms development and event-driven programming
- SQLite database design and SQL queries
- File system operations (image handling)
- Password security with BCrypt hashing
- Third-party library integration (MaterialSkin)
- Working with AI tools for development assistance

## 🐛 Known Issues / Future Improvements

- [ ] PDF invoice generation
- [ ] Excel export functionality
- [ ] Revenue charts by month
- [ ] Dark mode toggle
- [ ] Multi-language support

## 📝 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

## 👤 Author

**Kevin Markoja**

- School: Ekonomska šola Murska Sobota, Višja strokovna šola
- Program: Informatika in računalništvo
- Course: Programming 1

## 🙏 Acknowledgments

- **Claude AI** — For assistance with the V2 upgrade and advanced features
- **MaterialSkin** by leocb — For the modern UI library
- **System.Data.SQLite** — For the database engine

---

⭐ If you found this project helpful or interesting, consider giving it a star!
