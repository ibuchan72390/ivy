This is technically the MySQL Database project for this segment.

Visual Studio does not support MySQL in the context of SQL projects.
Attempting to use MySQL with a SQL project leads to catastrophic failures.

Every save you make in Visual Studio automatically converts the encoding to ANSI.
In order to work with MySQL, you 100% need to use UTF-8 encoding style.

DO NOT SAVE A MYSQL FILE IN VISUAL STUDIO!!!