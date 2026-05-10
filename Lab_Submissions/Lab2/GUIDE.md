# Lab 02: Lập trình Game Back-End với Entity Framework Core

## 📝 Mục tiêu
Thiết kế Database (Code First) và Mapping dữ liệu giữa Code C# và SQL Server.

## 📂 Các thành phần chính (Logic & Vị trí)

| Tên File | Chức năng / Logic hoạt động | Vị trí thực tế trong Source Code |
| :--- | :--- | :--- |
| `GameDbContext.cs` | Trái tim của hệ thống: Quản lý kết nối DB, định nghĩa các bảng (DbSet) và Seeding dữ liệu mẫu. | `Solution > DotnetBackend > Context/GameDbContext.cs` |
| `Models (Folder)` | Chứa các thực thể chính của Game: ApplicationUser, GameLevel, Question, Region, LevelResult. | `Solution > DotnetBackend > Models/` |
| `appsettings.json` | Lưu trữ ConnectionString để kết nối tới Database (MS SQL Server). | `Solution > DotnetBackend > appsettings.json` |


## 🚀 Cách kiểm tra (Test Case)
Bạn có thể sử dụng file `TestCase_Lab2.http` ngay tại thư mục này để gửi yêu cầu trực tiếp trên Visual Studio mà không cần mở trình duyệt/Postman.
