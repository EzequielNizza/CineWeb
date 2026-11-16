# CineWeb
Aclaración para el correcto funcionamiento del programa:
Al abrir el proyecto, hacer click derecho en "Cine.Web" y luego "Establecer como proyecto de inicio".

====================

Guía para instalar la base de datos:
  1. Agregar el archivo "CIneDB.bak" en "Program Files/MicrosoftSQLServer/(SU SERVIDOR)/MSSQL/Backup.
  2. Iniciar Microsoft SQL Server Manager Studio y conectarse al servidor.
  3. Hacer click derecho en la carpeta "Base de Datos" y elegir la opción "Restaurar base de datos..."
  4. Elegir la opción Dispositivo y tocar el botón "..." de la misma fila.
  5. Con la opción "Archivo" ya seleccionada, tocar el botón "Agregar" y elegir "CIneDB.bak".
  6. Para finalizar, darle a "Aceptar" para restarurar el .bak para transformarlo en una base de datos funcional.
  
====================

Asignación de Roles: Una vez creado su usuario, se podrá asignar los roles de la siguiente forma: ('1') Admin ('2') Miembro

Iniciar Microsoft SQL Server Manager Studio.
Generar nueva consulta sobre CibeDB.
Insertar la siguiente query en la consulta:
INSERT INTO UsuariosRoles (IdUsuario, IdRol) SELECT u.IdUsuario, r.IdRol FROM Usuarios u CROSS JOIN Roles r WHERE u.Email = '(correo electrónico)' AND r.IdRol = '(ID)'

Una vez confirmado el cambio, cerrar la sesión activa con ese correo y volver a iniciar.
