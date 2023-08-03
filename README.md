# HR System

EF Migration =================================================== 
การ add ไฟล์สำหรับ migration ของ EF.Core สามารถทำได้โดย
 1. ไปที่ project : ".Api" ที่ต้องการ add migration เช่น Account.Api จากนั้นคลิ๊กขวาไปที่ project : ".Api" แล้วเลือก "Set as Startup Project"
 2. เปิด Package Manager Console โดยไปที่ Tools -> NuGet Package Manager -> Package Manager Console 
	จากนั้นเลือก Default project โดยเลือก project ที่เป็น Models(.Models)
 3. จากนั้นพิมพ์ "Add-Migration (ชื่อ migration) -Context (ชื่อ DbContext เช่น AccountDbContext) -OutputDir (Path ของ migrate เช่น Migrations\AccountDbContextMigrations)"
	จากนั้นกด enter

*** ตัวอย่าง คำสั่งของ TemplateDbContext
Add-Migration InitialCreate -Context TemplateDbContext -OutputDir Migrations\TemplateDbContextMigrations

*** ตัวอย่าง คำสั่งของ LogManagementDbContext
Add-Migration InitialCreate -Context LogManagementDbContext -OutputDir Migrations\LoggerMigrations

*** คำสั่งสำหรับเปลี่ยน env เพื่อทำการ migration  
$env:ASPNETCORE_ENVIRONMENT='Local'