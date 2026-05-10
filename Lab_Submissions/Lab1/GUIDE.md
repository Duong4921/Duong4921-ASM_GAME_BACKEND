# Lab 01: Làm quen với ASP.NET Core và Web API

## 📝 Mục tiêu
Giới thiệu về .NET Core, cách tạo dự án, và viết API đơn giản trả về dữ liệu cơ bản.

## 📂 Các thành phần chính (Logic & Vị trí)

| Tên File | Chức năng / Logic hoạt động | Vị trí thực tế trong Source Code |
| :--- | :--- | :--- |
| `CourseController.cs` | Controller chính xử lý yêu cầu GET chuỗi text và GET thông tin khóa học (JSON). | `Solution > DotnetBackend > Controllers/CourseController.cs` |
| `CourseModel.cs` | Model chứa thuộc tính (Name, StudentCode, Class...) để đóng gói dữ liệu trả về. | `Solution > DotnetBackend > Models/CourseModel.cs` |
| `Program.cs` | Cấu hình Services và Pipeline của ứng dụng, nạp Controller vào hệ thống. | `Solution > DotnetBackend > Program.cs` |
| `ResponseApi.cs` | Công cụ đóng gói dữ liệu chuẩn (IsSuccess, Notification, Data) cho toàn bộ API. | `Solution > DotnetBackend > Models/ResponseApi.cs` |


## 🚀 Cách kiểm tra (Test Case)
Bạn có thể sử dụng file `TestCase_Lab1.http` ngay tại thư mục này để gửi yêu cầu trực tiếp trên Visual Studio mà không cần mở trình duyệt/Postman.
