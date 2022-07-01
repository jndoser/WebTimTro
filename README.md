Hướng dẫn cài đặt trên máy local:
Bước 1: Chạy file script-database.sql trên sql server để có dữ liệu
Bước 2: Giải nén file chương trình và vào file appsettings.json,
thay đổi giá trị ở tên Server thành tên Server của SQL Server 
đang lưu trữ dữ liệu của file sql vừa thực thi trong phần 
DefaultConnection trong ConnectionStrings
Bước 3: Sau khi đã có dữ liệu và thay đổi tên Server tương 
ứng với tên Server của SQL Server, tiến hành chạy chương trình
