# Lab 04: Advanced API & Mail Service

## 📝 Mục tiêu
Triển khai tính năng Quên mật khẩu qua OTP, gửi Email thực tế và Upload ảnh đại diện (Avatar).

## 📂 Các thành phần chính (Logic & Vị trí)

| Tên File | Chức năng / Logic hoạt động | Vị trí thực tế trong Source Code |
| :--- | :--- | :--- |
| `EmailService.cs` | Dịch vụ gửi Email sử dụng SMTP (tích hợp MailKit). | `Solution > DotnetBackend > Services/EmailService.cs` |
| `APIGameController.cs` | Xử lý logic sinh OTP và kiểm tra OTP trước khi đổi mật khẩu. | `Solution > DotnetBackend > Controllers/APIGameController.cs (ForgotPassword/ResetPassword)` |
| `wwwroot/avatars` | Thư mục vật lý lưu trữ ảnh đại diện của người chơi sau khi Upload. | `Solution > DotnetBackend > wwwroot/avatars/` |


## 🚀 Cách kiểm tra (Test Case)
Bạn có thể sử dụng file `TestCase_Lab4.http` ngay tại thư mục này để gửi yêu cầu trực tiếp trên Visual Studio mà không cần mở trình duyệt/Postman.
