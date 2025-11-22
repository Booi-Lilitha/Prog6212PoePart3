# Prog6212PoePart3
# Contract Monthly Claim System
youtube video link : https://youtu.be/ja0OdbWwz0c

## 📋 Project Overview
A comprehensive web application for managing monthly contract claims in educational institutions. The system streamlines the entire claim submission and approval process with role-based access control and automated calculations.

## 🎯 System Purpose
To provide a centralized platform for lecturers to submit monthly claims, with a structured approval workflow involving Programme Coordinators and Academic Managers, all managed by HR administrators.

## 👥 User Roles & Permissions

### 1. **HR Administrator**
- Create and manage all user accounts
- Set lecturer hourly rates
- Full system oversight
- User lifecycle management

### 2. **Lecturer**
- Submit monthly claims with auto-calculation
- Upload supporting documents
- Track claim status
- View claim history

### 3. **Programme Coordinator**
- Review submitted claims
- First-level approval/rejection
- Monitor claim progress

### 4. **Academic Manager**
- Final approval authority
- Oversight of all claims
- Final decision on payments

## 🚀 Key Features

### 🔐 Authentication & Security
- Session-based authentication
- Role-based access control
- Secure redirect system
- 8-hour session timeout

### 💰 Smart Claim Management
- **Auto-calculation** using HR-set hourly rates
- **Real-time validation** (180-hour monthly limit)
- **Document upload** support
- **Multi-level approval workflow**

### 📊 Dashboard Portals
- **Lecturer Portal**: Claim submission and tracking
- **Coordinator Portal**: First-level approval
- **Manager Portal**: Final approval
- **HR Portal**: User management

### 🗃️ Data Management
- Entity Framework with SQL Server
- Relational database design
- File upload and storage
- Audit trail maintenance

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **Session Management**

### Frontend
- **Bootstrap 5**
- **JavaScript/jQuery**
- **Font Awesome Icons**
- **Responsive Design**

### Security
- **Session-based Authentication**
- **Role-based Authorization**
- **Input Validation**
- **Secure File Uploads**

## 📁 Project Structure

```
ContractMonthlyClaims/
├── Controllers/
│   ├── AccountController.cs     # Authentication
│   ├── HomeController.cs        # Main application logic
│   └── HRController.cs          # User management
├── Models/
│   ├── Claim.cs                 # Claim entity
│   ├── ClaimItem.cs            # Claim line items
│   ├── User.cs                 # User entity
│   └── Document.cs             # Document entity
├── Views/
│   ├── Home/                   # Application pages
│   ├── Account/                # Login page
│   └── HR/                     # User management
├── Data/
│   └── AppDbContext.cs         # Database context
└── wwwroot/
    └── uploads/                # Document storage
```

## 🗄️ Database Schema

### Core Tables
- **Users**: User accounts and profiles
- **Claims**: Main claim records
- **ClaimItems**: Individual claim line items
- **Documents**: Supporting documents

### Key Relationships
- One User → Many Claims
- One Claim → Many ClaimItems
- One Claim → Many Documents

## 🚀 Installation & Setup

### Prerequisites
- .NET 6.0 or later
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

### Installation Steps

1. **Clone or Download the Project**
   ```bash
   git clone <repository-url>
   cd ContractMonthlyClaims
   ```

2. **Database Setup**
   - The application will create the database automatically on first run
   - Default connection string uses LocalDB

3. **Configure Connection String**
   Update `appsettings.json` if needed:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ContractMonthlyClaims;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```
   Or press F5 in Visual Studio

5. **Access the Application**
   - Open: `(https://localhost:7228/)`
   - Use default credentials (see below)

## 🔑 Default Login Credentials

After first run, use these test accounts:

| Role | Username | Password | Access |
|------|----------|----------|---------|
| HR | `hradmin` | `admin123` | Full system access |
| Lecturer | `lect1` | `lect2` | `Pass123!` | Claim submission |
| Coordinator | `coordi1` | `Pass123!` | First approval |
| Manager | `man1` | `Pass123!` | Final approval |

## 📝 Usage Guide

### For Lecturers
1. **Login** to Lecturer Portal
2. **Click** "Create New Claim"
3. **Enter** month/year and description
4. **Add** claim items with hours worked
5. **Upload** supporting documents
6. **Submit** for approval

### For Coordinators/Managers
1. **Login** to respective portal
2. **Review** pending claims
3. **View** claim details and documents
4. **Approve/Reject** claims
5. **Monitor** approval workflow

### For HR
1. **Login** to HR Portal
2. **Manage** user accounts
3. **Set** hourly rates for lecturers
4. **Oversee** system operations

## ⚙️ Configuration

### Session Settings
- Timeout: 8 hours
- HTTP Only: Enabled
- Secure: Development mode

### File Upload
- Location: `wwwroot/uploads/`
- Secure naming with GUIDs
- Multiple file support
- Common document types allowed

### Validation Rules
- Maximum hours per month: 180
- Month range: 1-12
- Year range: 2000-2100
- Required fields enforcement

## 🐛 Troubleshooting

### Common Issues

1. **Database Connection Error**
   - Verify SQL Server is running
   - Check connection string in appsettings.json
   - Ensure database permissions

2. **File Upload Issues**
   - Check `wwwroot/uploads/` folder exists
   - Verify write permissions
   - Check file size limits

3. **Login Problems**
   - Verify user exists in database
   - Check role assignments
   - Clear browser cache/session

4. **Claim Submission Errors**
   - Verify hourly rate is set for lecturer
   - Check total hours don't exceed 180
   - Ensure required fields are completed

### Error Logs
- Check Visual Studio Output window
- Review browser console for client errors
- Monitor application logs

## 🔒 Security Notes

- Passwords stored in plain text (for demo purposes only)
- In production, implement proper password hashing
- Consider adding HTTPS enforcement
- Implement additional audit logging

## 📞 Support

For technical support or questions:
1. Check this README first
2. Review code comments
3. Check error logs
4. Contact development team

## 📄 License

This project is for educational purposes. Please ensure proper licensing for production use.

---

**Version**: 1.0  
**Last Updated**: 2024  
**Developed By**: Contract Monthly Claims Team
