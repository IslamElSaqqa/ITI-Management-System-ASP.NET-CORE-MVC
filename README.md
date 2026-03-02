## Overview

**ITI Management System** is a comprehensive web-based application built with **ASP.NET Core MVC** and **Entity Framework Core** for managing student information, courses, and departments at ITI. The system is designed for **administrators, instructors, and students**, offering a user-friendly interface to manage academic data efficiently and securely.

The system implements **role-based authentication** to provide secure access, allowing different permissions for **Admins** and **Students**. Unauthorized access is automatically redirected to a custom **Access Denied** page.

---

## Key Features

### User Management
- **Registration and Login:** Students can register and log in securely.  
- **Role-Based Access:** Users are assigned roles (`Admin` or `Student`) to control access to different parts of the system.  
- **Password Security:** Passwords are hashed using **BCrypt** for secure storage.  

### Student Management
- Manage student profiles including **Name, Age, Department**.  
- Track student progress and courses enrolled.  

### Course Management
- Add, update, and delete courses (Admins only).  
- Assign students to courses and manage their grades.  
- Handles **Student-Course many-to-many relationships**.

### Department Management
- Add, edit, and remove departments (Admins only).  
- Departments appear in dropdowns for assigning students.  

### Role-Based Authorization
- **Admin:** Full control over students, courses, and departments.  
- **Student:** Can only view personal information and enrolled courses.  
- Unauthorized access redirects users to a custom **Access Denied** page.  

### User Interface
- Responsive design using **Bootstrap 5**.  
- Dynamic navbar updates based on login status and user role.  
- Card-based layout for forms and dashboards.  

---

## Technologies Used

- **ASP.NET Core MVC** – Web framework for building MVC web apps.  
- **Entity Framework Core** – ORM for database interaction.  
- **SQL Server** – Relational database to store students, courses, and departments.  
- **Bootstrap 5** – Frontend framework for responsive UI design.  
- **BCrypt.Net** – Password hashing library for secure authentication.  
- **Cookie Authentication** – Manages login sessions securely.  
- **JWT Authentication (Optional)** – Can be used for API endpoints if extended.  

---

## Database Structure

- **Students** – `ID`, `Name`, `Age`, `DeptId`  
- **Departments** – `DeptId`, `DeptName`  
- **Courses** – `CourseId`, `CourseName`  
- **StudentCourses** – Junction table for many-to-many relationship with `Degree`  
- **Users** – `Username`, `PasswordHash`, `Role`, `StudentId`  

> Relationships:  
> - One Department → Many Students  
> - Many Students ↔ Many Courses via StudentCourses  

---