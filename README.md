# KansasPPDMLoader
Loading of Kansas Geological Survey data into PPDM

The program will automatically download wellbore or markerpicks from the web for you and load into your PPDM SQL Server database. Only SQL Server is supported.

The release have a self contained executable that you can download. This does not have a certificate so you will get a warning when using it.

Usage:
  KansasPPDMLoaderConsole [options]

Options:

  --connection <connection> (REQUIRED)         Database connection string
  
  --datatype <Markerpick|Wellbore> (REQUIRED)  Data type to process: Wellbore or Markerpick
  
  --version                                    Show version information
  
  -?, -h, --help                               Show help and usage information

