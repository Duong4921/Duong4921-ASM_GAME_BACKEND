# Lab 07: MVC Web Admin & Cookie Auth

## 📝 Mục tiêu
Xây dựng giao diện Web (MVC) dành cho Admin quản lý Game và dùng xác thực Cookie.

## 📂 Các thành phần chính (Logic & Vị trí)

| Tên File | Chức năng / Logic hoạt động | Vị trí thực tế trong Source Code |
| :--- | :--- | :--- |
| `HomeController.cs` | Xử lý các trang Razor View (Trang chủ Admin, Đăng nhập Admin). | `Solution > DotnetBackend > Controllers/HomeController.cs` |
| `GameController.cs` | Quản lý dữ liệu Cấp độ thông qua giao diện Web, bảo mật bằng quyền [Authorize(Roles='Admin')]. | `Solution > DotnetBackend > Controllers/GameController.cs (Admin Only)` |
| `_Layout.cshtml` | Giao diện chung (Theme) hiện đại, Glassmorphism cho trang quản trị. | `Solution > DotnetBackend > Views/Shared/_Layout.cshtml` |


## 🚀 Cách kiểm tra (Test Case)
Bạn có thể sử dụng file `TestCase_Lab7.http` ngay tại thư mục này để gửi yêu cầu trực tiếp trên Visual Studio mà không cần mở trình duyệt/Postman.
