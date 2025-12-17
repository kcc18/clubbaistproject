# CLUBBAIST Project (BAIS3230)

This is a back-end focused ASP.NET Core Razor Pages application developed as part of the BAIS3230 coursework. The goal of this project was to implement core business use cases and explore server-side concepts such as identity, authorization, validation, and role-based access control.

> Note: This project is primarily focused on **back-end logic and functionality**. The UI is functional but not polished, as front-end design was not the objective.

----

##  Features Implemented

- **User Authentication & Authorization** using ASP.NET Core Identity
- **Role-based Access Control** (Admins, Members)
- **Reservation System** with full CRUD for users (based on role)
- **Entity Validation** on both front-end and server-side
- **Admin Functionality** for managing tee times and memberships
- **Event Scheduling** that affects reservation availability

---

##  Use Cases Completed (from course requirements)

- Users can register/login/logout
- Authenticated users can view and manage their own reservations
- Admins can create, update, and delete tee times
- Entity-based validation rules are applied across forms
- Events block off tee time availability as expected

---

##  Known Gaps / To-Do

This project met the course use case requirements, but some items are either incomplete or left for future enhancement:

- [ ] MyReservations page to isolate each user’s data and allow full CRUD
- [ ] Complete front-end styling for better user experience
- [ ] Additional validation testing for edge cases
- [ ] Finalize role-based page restrictions

---

##  Tech Stack

- **.NET 9.0**
- **Razor Pages**
- **Entity Framework Core**
- **SQL Server**
- **Identity & Role Management**

---

