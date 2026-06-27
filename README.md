# 🏨 Hotel Management System

A desktop hotel management application developed using **C# WinForms**, **SQL Server**, **ADO.NET**, and **Crystal Reports**. The system supports hotel staff in managing room bookings, customer information, services, check-in/check-out, invoices, and reports.

---

# 📌 Project Overview

This project was developed as a team project to practice object-oriented programming, desktop application development, and database management with SQL Server.

---

# 🛠 Technologies

* C#
* .NET Framework WinForms
* SQL Server
* ADO.NET
* Crystal Reports
* Visual Studio

---

# 📂 Project Structure

```text
Hotel-Management-System
│
├── QuanLyDatPhongKhachSan
├── README.md
├── .gitignore
├── .gitattributes
└── QuanLyDatPhongKhachSan.sln
```

---

# 🖥 Forms

## Authentication

| Form        | Description    |
| ----------- | -------------- |
| FrmDangNhap | User Login     |
| FrmMain     | Main Dashboard |

---

## Booking Management

| Form                          | Description                   |
| ----------------------------- | ----------------------------- |
| FrmTaoDatPhongKH              | Create Room Booking           |
| FrmDanhSachDatPhongKH         | Customer Booking List         |
| FrmDanhSachDatPhongNV         | Employee Booking List         |
| FrmChiTietDatPhong            | Booking Details               |
| FrmChiTietDatPhongDaThanhToan | Paid Booking Details          |
| FrmDanhSachYeuCauHuy          | Booking Cancellation Requests |

---

## Check-in / Check-out

| Form        | Description        |
| ----------- | ------------------ |
| FrmCheckIn  | Customer Check-in  |
| FrmCheckOut | Customer Check-out |

---

## Customer Management

| Form                   | Description          |
| ---------------------- | -------------------- |
| FrmThongTinKhachHangKH | Customer Information |

---

## Deposit Management

| Form           | Description        |
| -------------- | ------------------ |
| FrmDatCocKH    | Deposit Management |
| FrmPhieuDatCoc | Deposit Receipt    |

---

## Room Management

| Form             | Description    |
| ---------------- | -------------- |
| FrmXemPhongNV&QL | Room List      |
| FrmGiaHanPhong   | Room Extension |

---

## Service Management

| Form              | Description            |
| ----------------- | ---------------------- |
| FrmQuanLyDichVu   | Service Management     |
| FrmSuDungDichVu   | Customer Service Usage |
| FrmSuDungDichVuNV | Employee Service Usage |

---

## Invoice

| Form      | Description        |
| --------- | ------------------ |
| FrmHoaDon | Invoice Management |

---

## Reports

| Report             | Description               |
| ------------------ | ------------------------- |
| CrReportHoaDon     | Invoice Report            |
| rptThongKeDoanhThu | Revenue Statistics Report |
| rptDatCoc          | Deposit Report            |

---

# ⚙ Core Classes

| Class          | Description                                   |
| -------------- | --------------------------------------------- |
| DatabaseHelper | SQL Server connection and database operations |
| CurrentUser    | Store current logged-in user information      |
| Program        | Application entry point                       |

---

# ✨ Features

* User authentication
* Room booking management
* Customer management
* Room information management
* Check-in / Check-out
* Deposit management
* Invoice management
* Service management
* Revenue reporting
* Crystal Report integration

---

# 🗄 Database

Database: **SQL Server**

Recommended upload:

```
Database/
    Database.sql
```

The SQL script should include:

* CREATE DATABASE
* CREATE TABLE
* PRIMARY KEY
* FOREIGN KEY
* Sample Data

---

# 📷 Screenshots

You can add screenshots in a folder named:

```
Screenshots/
```

Recommended screenshots:

* Login Screen
* Dashboard
* Room Management
* Booking Management
* Check-in
* Invoice
* Revenue Report

---

# 🚀 Installation

1. Clone the repository.

2. Restore the SQL Server database.

3. Open the solution using Visual Studio.

4. Update the SQL Server connection string.

5. Build and run the project.

---

# 👨‍💻 Author

**Tran Van Doanh**

* Student majoring in Information Technology
* Hanoi Open University

---

# 📄 License

This project is developed for learning purposes.
