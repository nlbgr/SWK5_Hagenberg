# $SQLCMD = "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE"
$SQLCMD = "sqlcmd.exe"

& $SQLCMD -S "(localdb)\MSSQLLocalDB" -i init-db.sql
