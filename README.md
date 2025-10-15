# DCS.User

[![MIT License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

## Overview

DCS.User is a comprehensive C# solution for managing all user-related functionalities within the Data Control System (DCS). It provides a robust backend for authentication, authorization, and advanced profile management.

This document serves as a technical overview for developers and future team members to understand the architecture and purpose of the `DCS.User` component within the main Data Control System (DCS).

## Features

*   **Secure User Authentication:** Handles user login, logout, and session management using modern security practices.

*   **Advanced Authorization:** Implements flexible Role-Based Access Control (RBAC). Permissions and data visibility are controlled through a dynamic management system.

*   **Group, Organization, and Role Management:** Allows the creation and administration of user groups and organizational structures to logically separate and manage users.

*   **Full User Profile Management:** Complete CRUD (Create, Read, Update, Delete) operations for user profiles.
*   **Secure Password Handling:** Uses strong password hashing and provides a secure mechanism for password resets.

*   **Integrated Logging:** Logs user activities and critical system events for monitoring and auditing purposes.

## Getting Started

To get the project up and running on your local machine, follow these steps.

### Prerequisites

*   .NET 7 SDK
*   Windows 7 or later
*   MS SQL Server

### Installation

1.  Clone the repository:
    ```sh
    git clone https://github.com/Verheyden-Tech/DCS.User.git
    ```
2.  Navigate to the project directory:
    ```sh
    cd DCS.User
    ```
3.  Restore the .NET dependencies:
    ```sh
    dotnet restore
    ```
4.  Ensure the database is configured and set up as required by the project.

## Usage Example

A primary use case is the creation and management of users within a specific domain. Roles can be dynamically assigned to users through the "UserEditor" interface. This allows for granular control over what data and functionalities specific employees of a customer can see and access within the system.

```csharp
// How to set the current user with the CurrentUserService.

// < Login logic >
if (Login(user) == true)
{
  CurrentUserService.Singleton.SetCurrentUser(user);
}
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For inquiries, please contact us at info@verheyden-tech.de.