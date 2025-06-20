# 🏥 Clinic Management System

## 🎯 Objective
This project aims to automate and streamline daily operations in a medical clinic, managing patient records, appointments, billing, staff, and inventory to improve workflow efficiency and data accuracy.

---

## 🧰 Tech Stack

| Category       | Details                         |
|----------------|---------------------------------|
| Language       | C# (C-Sharp)                   |
| Framework      | .NET Framework / .NET 6+       |
| UI Technology  | Windows Forms (WinForms)       |
| IDE            | Microsoft Visual Studio 2022   |
| Database       | Microsoft SQL Server           |
| Reporting      | Crystal Reports               |
| Data Access    | ADO.NET                       |
| UI Enhancements| Guna.UI Framework             |

---

## 📦 Main Functionalities

- ✅ Patient Management  
- ✅ Appointment Scheduling  
- ✅ Doctor/Staff Management  
- ✅ Billing & Payment Management  
- ✅ Inventory Management (Medicines & Supplies)  
- ✅ Supplier Management  
- ✅ Tab-Based Navigation and User Profile Management  

---

## 🖥 System Overview

The system uses a **tab control interface** to organize core modules:

### 🔹 Patient Management
- Add, update, delete, and view patient profiles  
- **Fields:**  
  - Patient ID  
  - Name  
  - Date of Birth  
  - Phone  
  - Address  
  - Medical History Notes  

### 🔹 Appointment Scheduling
- Book, update, cancel, and view appointments  
- Link patients with doctors and timeslots  
- **Fields:**  
  - Appointment ID  
  - Patient ID  
  - Doctor ID  
  - Date & Time  
  - Status  

### 🔹 Doctor and Staff Management
- Manage clinic employees including doctors, nurses, and admin staff  
- **Fields:**  
  - Employee ID  
  - Name  
  - Position  
  - Phone  
  - Email  
  - Hire Date  

### 🔹 Billing and Payment Management
- Manage invoices for consultations, treatments, and medicines  
- **Fields:**  
  - Invoice ID  
  - Patient ID  
  - Appointment ID  
  - Amount  
  - Payment Status  

### 🔹 Inventory Management
- Track medicines and medical supplies  
- **Fields:**  
  - Item ID  
  - Name  
  - Quantity  
  - Expiry Date  
  - Supplier ID  

### 🔹 Supplier Management
- Manage suppliers for medical supplies and equipment  
- **Fields:**  
  - Supplier ID  
  - Name  
  - Phone  
  - Email  
  - Supplies Provided  

---

## ⚙️ Installation & Setup

### ✅ 1. Download and Install Visual Studio
- [Download Visual Studio 2022](https://visualstudio.microsoft.com/downloads)  
- Select **.NET Desktop Development** workload  
- Install and restart if needed  

### ✅ 2. Create or Open the Project

#### New Project:
- Create a new project → Search: **Windows Forms App**  
- Choose **.NET Framework** or **.NET 6/7/8**  
- Name the project `ClinicManagementSystem`  
- Set the project location  
- Click **Create**  

#### Open Existing:
- Open the `.sln` file from the project folder  

### ✅ 3. Design the UI
- Use Windows Forms Designer  
- Add controls and a **TabControl** for modules  
- Double-click controls to create event handlers in the code-behind  

### ✅ 4. Build and Run
- Build the project (`Ctrl + Shift + B`)  
- Run the app (`F5`)  

---

## 📊 Database Setup

This project uses **Microsoft SQL Server** for data storage.

### Prerequisites
- SQL Server installed (Express or Developer edition recommended)  
- SQL Server Management Studio (SSMS) installed  

### Steps to Set Up the Database

1. Open **SSMS** and connect to your SQL Server instance.  
2. Create a new database named **ClinicDB**.  
3. Create tables for patients, appointments, employees, billing, inventory, and suppliers as required.

---

Feel free to extend this README with database scripts, screenshots, or additional usage instructions as needed.

