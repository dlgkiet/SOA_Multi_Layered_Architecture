SOA Multi-Layered Architecture
  
📌 Giới thiệu
Dự án SOA Multi-Layered Architecture là một hệ thống đánh giá phim/series được xây dựng theo kiến trúc nhiều tầng trên nền tảng .NET Core và MSSQL, hỗ trợ kiểm thử với Postman.
👥 Thành viên nhóm
Đoàn Thanh Lâm - GitHub
Dương Lâm Gia Kiệt - GitHub
Đỗ Trọng Hiếu - GitHub
🔗 Liên kết dự án
GitHub Repository
🏗️ Kiến trúc dự án
Dự án được chia thành các tầng sau:
Core Layer: Chứa các thực thể (Entities) và các đối tượng dữ liệu chung.
Data Access Layer (DAL): Quản lý truy xuất dữ liệu với Entity Framework Core và Stored Procedures.
Business Layer: Xử lý logic nghiệp vụ và kết hợp dữ liệu từ DAL.
Service Layer: Kết nối với Business Layer, đóng vai trò như một API trung gian.
Common Layer: Chứa các tiện ích chung như Logger, Error Handler, Validator.
API Layer: Cung cấp các endpoint API cho ứng dụng.
🔍 Các công nghệ sử dụng
Ngôn ngữ: C# .NET Core
Cơ sở dữ liệu: MSSQL Server
ORM: Entity Framework Core
Lưu trữ dữ liệu: Repository Pattern
Kiểm thử API: Postman & Newman
Unit Test: xUnit
🚀 Cách cài đặt & chạy dự án
1️⃣ Clone repository
git clone https://github.com/dlgkiet/SOA_Multi_Layered_Architecture.git
 cd SOA_Multi_Layered_Architecture

2️⃣ Cấu hình cơ sở dữ liệu
Khởi tạo database trên MSSQL Server
Cập nhật chuỗi kết nối trong appsettings.json
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
}

3️⃣ Chạy ứng dụng
dotnet build
 dotnet run

Ứng dụng sẽ chạy trên http://localhost:5000.
🛠️ Kiểm thử API với Postman
Import collection Postman (.postman_collection.json)
Kiểm thử API với các phương thức GET, POST, PUT, DELETE
Tự động hóa kiểm thử bằng Newman
newman run SOA_Multi_Layered_Architecture.postman_collection.json

📂 Thư mục dự án
SOA_Multi_Layered_Architecture/
│── CoreLayer/
│── DataAccessLayer/
│── BusinessLayer/
│── ServiceLayer/
│── API/
│── Common/
│── Tests/
│── README.md

✅ Kiểm thử & Đánh giá
Dự án bao gồm Unit Test với xUnit và API Testing với Postman.
Unit Test: Kiểm thử từng lớp Service và Repository.
Test Coverage: Đánh giá mức độ bao phủ mã nguồn.
📌 Đóng góp & Phát triển
Để đóng góp, vui lòng fork repository và gửi pull request.
git checkout -b feature-new-update
git commit -m "Mô tả cập nhật"
git push origin feature-new-update

📜 Giấy phép
Dự án được cấp phép theo MIT License.

✨ Cảm ơn bạn đã quan tâm đến dự án!

