# Lab 03: API CRUD & Authentication (Unity Integration)

## 📝 Mục tiêu
Xây dựng các API cơ bản để Unity có thể lấy dữ liệu Cấp độ (Level), Câu hỏi (Question) và thực hiện Đăng ký/Đăng nhập.

## 📂 Các thành phần chính (Logic & Vị trí)

| Tên File | Chức năng / Logic hoạt động | Vị trí thực tế trong Source Code |
| :--- | :--- | :--- |
| `APIGameController.cs` | Chứa các API Register, Login, SaveResult, Rating... là điểm giao tiếp chính của Unity. | `Solution > DotnetBackend > Controllers/APIGameController.cs` |
| `DTOs (Folder)` | Data Transfer Objects: Các class dùng để nhận dữ liệu đầu vào từ Unity (Vd: RegisterDTO, LoginDTO). | `Solution > DotnetBackend > DTO/` |


## 🚀 Cách kiểm tra (Test Case)
Bạn có thể sử dụng file `TestCase_Lab3.http` ngay tại thư mục này để gửi yêu cầu trực tiếp trên Visual Studio mà không cần mở trình duyệt/Postman.
