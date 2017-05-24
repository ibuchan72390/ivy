:: All files referenced in this chain must be saved as UTF-8
:: If the files aren't saved as UTF-8, they WILL fail silently
:: If you notice any weirdness, there's a good chance you saved a sql script in Visual Studio

ECHO OFF

mysql -u root -p < %CD%\Create\Create_ib_framework.txt


ECHO -------------------------------------------
ECHO IB Framework Test Database has been published!
ECHO -------------------------------------------

PAUSE