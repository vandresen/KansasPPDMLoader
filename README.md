# KansasPPDMLoader
Loading of Kansas Geological Survey data into PPDM

The program will automatically download wellbore for you and load into your PPDM SQL Server database. Only SQL Server is supported.

The release have a self contained executable that you can download. This does not have a certificate so you will get a warning when using it.

You will need an appsettings.json file for any parameters used by the application such as:
ConnectionString: This will contain the connection string to your SQL Server database


