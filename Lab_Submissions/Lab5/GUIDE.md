# Lab 05: Security & JWT Authentication

## 📝 Mục tiêu
Bảo mật hệ thống bằng Json Web Token (JWT). Yêu cầu người dùng phải đăng nhập mới được lấy kết quả thi đấu.

## 📂 Các thành phần chính (Logic & Vị trí)

| Tên File | Chức năng / Logic hoạt động | Vị trí thực tế trong Source Code |
| :--- | :--- | :--- |
| `Program.cs (JWT Config)` | Định nghĩa thuật toán mã hóa Token, Issuer và Key trong Authentication Pipeline. | `Solution > DotnetBackend > Program.cs` |
| `[Authorize]` | Sử dụng Attribute [Authorize] để chặn các yêu cầu không có Token hợp lệ. | `Solution > DotnetBackend > Controllers/APIGameController.cs (GetAllResultByUser)` |


## 🚀 Cách kiểm tra (Test Case)
Bạn có thể sử dụng file `TestCase_Lab5.http` ngay tại thư mục này để gửi yêu cầu trực tiếp trên Visual Studio mà không cần mở trình duyệt/Postman.
