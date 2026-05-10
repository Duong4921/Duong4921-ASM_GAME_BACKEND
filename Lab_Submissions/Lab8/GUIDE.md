# Lab 08: Production Deployment (Somee)

## 📝 Mục tiêu
Triển khai ứng dụng lên môi trường Web Hosting thực tế (Somee.com) để Unity có thể kết nối từ xa.

## 📂 Các thành phần chính (Logic & Vị trí)

| Tên File | Chức năng / Logic hoạt động | Vị trí thực tế trong Source Code |
| :--- | :--- | :--- |
| `Publish` | Bộ file nén chứa toàn bộ code đã compiled để đẩy lên Hosting. | `Solution > DotnetBackend > bin/Release/publish/` |
| `Somee DB` | Thay đổi ConnectionString trỏ tới Database trên Somee. | `Solution > DotnetBackend > appsettings.json (Live Connection)` |


## 🚀 Cách kiểm tra (Test Case)
Bạn có thể sử dụng file `TestCase_Lab8.http` ngay tại thư mục này để gửi yêu cầu trực tiếp trên Visual Studio mà không cần mở trình duyệt/Postman.
