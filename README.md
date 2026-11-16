# CineWeb
====================

Guía para instalar la base de datos:
  1. Agregar el archivo "CIneDB.bak" en "Program Files/MicrosoftSQLServer/(SU SERVIDOR)/MSSQL/Backup.
  2. Iniciar Microsoft SQL Server Manager Studio.
  3. Restarurar el .bak para transformarlo en una base de datos funcional.
====================

Asignación de Roles: Una vez creado su usuario, se podrá asignar los roles de la siguiente forma: ('1') Admin ('2') Miembro

Iniciar Microsoft SQL Server Manager Studio.
Generar nueva consulta sobre CibeDB.
Insertar la siguiente query en la consulta:
INSERT INTO UsuariosRoles (IdUsuario, IdRol) SELECT u.IdUsuario, r.IdRol FROM Usuarios u CROSS JOIN Roles r WHERE u.Email = '(correo electrónico)' AND r.Descripcion = '(ID)'

Una vez confirmado el cambio, cerrar la sesión activa con ese correo y volver a iniciar.
