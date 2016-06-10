# CasketStatus
Test task for bright box company

# How to run this mess
1. Create new database, name it whatever you want, write down connectionstring of following template: "Data Source=SQLSERVER_INSTANCE_PATH;Initial Catalog=DATABASE_NAME;Integrated Security=True;User Id=USERNAME;Password=PASSWORD;".
When it's done, change Web.config -> connectionStrings tag -> add tag's connectionString attribute to what you just have written down.
2. Now you are free to compile code and run it, everyting must work from this moment on. Problems can occur if you're hosting application to public host (This is related to virtual path conflicts which can occur on release build. This problem is relevant even for sample ASP.NET MVC projects).

There is only 1 user (admin actually) account for this app, it gets created automatically. Login: "sa", Password: "pass". Initial password can be changed via Web.config's appSetting "SuperAdminPassword", login is hard-coded.

To run emulator, just compile and run it, it's pretty intuitive to use it (I believe)

Result screenshot (app was hosted under local machine)
![alt tag](https://raw.githubusercontent.com/UltimaBeaR/CasketStatus/master/result.png)
