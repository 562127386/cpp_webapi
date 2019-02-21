 
--Rol y Usuario Administrador
--Usuario
SET IDENTITY_INSERT  [SCH_USUARIOS].[usuarios] ON;
INSERT INTO   [SCH_USUARIOS].[usuarios](ID,CUENTA,IDENTIFICACION,NOMBRES,APELLIDOS,CORREO,ESTADO,CreationTime,IsDeleted,CantidadAccesoFallido,BloqueoHabilitado,[SecurityStamp],[Password]) VALUES(1,'admin','000','administrador','sistema','admin@dominio.com',1,GETDATE(),0,0,0,'ff821b36-b877-e705-f3a3-39e6dbd97555','AMZ5r53e7lcOyxYaD3KIlSvC3/jZUc1G3I/Ze397gNzGwOVOAwV34vPxigrlI4tsEA==');
SET IDENTITY_INSERT   [SCH_USUARIOS].[usuarios] OFF; 
 

--Rol
SET IDENTITY_INSERT  [SCH_USUARIOS].[roles] ON;
INSERT INTO  [SCH_USUARIOS].[roles](ID,CODIGO,NOMBRE,ESADMINISTRADOR,ESEXTERNO,CreationTime,CreatorUserId) VALUES(1,'Admin','Administrador',1,0,GETDATE(),1);
SET IDENTITY_INSERT   [SCH_USUARIOS].[roles]  OFF;
 
--Usuario - Rol
INSERT INTO [SCH_USUARIOS].[usuario_rol] ([usuarioId], [rolId]) VALUES (1, 1);

            

--Modulos y funcionalidades
SET IDENTITY_INSERT [SCH_USUARIOS].[modulos] ON
INSERT INTO [SCH_USUARIOS].[modulos] ([Id], [Codigo], [Descripcion], [Nombre]) VALUES (1, N'mod_usuarios', N'Usuarios', N'Usuarios')
SET IDENTITY_INSERT [SCH_USUARIOS].[modulos] OFF
    
--Usuario - Modulo
INSERT INTO [SCH_USUARIOS].usuario_modulo ([usuarioId], [moduloId]) VALUES (1, 1);


SET IDENTITY_INSERT [SCH_USUARIOS].[funcionalidades] ON
INSERT INTO [SCH_USUARIOS].[funcionalidades] ([Id], [Codigo], [Nombre], [Descripcion], [Controlador], [Estado], [ModuloId]) VALUES (1, N'fun_usuarios', N'Gestion de Usuarios', N'Gestion de Usuarios', N'usuario', 1, 1)
INSERT INTO [SCH_USUARIOS].[funcionalidades] ([Id], [Codigo], [Nombre], [Descripcion], [Controlador], [Estado], [ModuloId]) VALUES (2, N'fun_roles', N'Gestion de Roles', N'Gestion de Roles', N'rol', 1, 1)
INSERT INTO [SCH_USUARIOS].[funcionalidades] ([Id], [Codigo], [Nombre], [Descripcion], [Controlador], [Estado], [ModuloId]) VALUES (3, N'fun_parametros', N'Parámetros', N'Gestión de parámetros del sistema', N'ParametroSistema', 1, 1)
INSERT INTO [SCH_USUARIOS].[funcionalidades] ([Id], [Codigo], [Nombre], [Descripcion], [Controlador], [Estado], [ModuloId]) VALUES (4, N'fun_sessiones', N'Sesión', N'Visualización de Sesiones del Usuarios', N'Sesion', 1, 1)
SET IDENTITY_INSERT [SCH_USUARIOS].[funcionalidades] OFF


             
        
SET IDENTITY_INSERT [SCH_USUARIOS].[acciones] ON
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (1, N'GET', N'Visualizar Usuarios', 1)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (2, N'CREATE', N'Crear Usuarios', 1)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (3, N'UPDATE', N'Editar Usuarios', 1)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (4, N'DELETE', N'Eliminar Usuarios', 1)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (5, N'EXPORT', N'Exportar Datos (Excel,PDF)', 1)

INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (6, N'GET', N'Visualizar Roles y Permisos', 2)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (7, N'CREATE', N'Crear Roles y Permisos', 2)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (8, N'UPDATE', N'Editar Roles y Permisos', 2)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (9, N'DELETE', N'Eliminar Roles y Permisos', 2)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (10, N'EXPORT', N'Exportar Datos (Excel,PDF)', 2)

INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (11, N'GET', N'Visualizar Parámetro', 3)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (12, N'UPDATE', N'Editar Parámetro', 3)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (13, N'GET', N'Visualizar Sesión', 4)
SET IDENTITY_INSERT [SCH_USUARIOS].[acciones] OFF

            

--Parametros
SET IDENTITY_INSERT [SCH_USUARIOS].[parametros] ON
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (1, N'UI.PAGE_SIZE', N'Tamaño de la página en las listas', N'Tamaño de las grillas en los listados', 1, 1, N'20', 1, 0, N'2018-06-12 23:00:30', 1, N'2018-06-12 23:00:30', NULL)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (2, N'UI.NOMBRE_APLICACION', N'Nombre de la Aplicacion', N'Nombre de la Aplicacion', 1, 2, N'{0}', 1, 0, NULL, NULL, N'2018-06-04 22:57:18', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (3, N'UI.RECOLECTAR_INFO_ANALISIS', N'Recolectar informacion de UI', N'Recolectar informacion de UI', 1, 3, N'true', 1, 0, NULL, NULL, N'2018-06-04 22:57:18', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (4, N'DATA.ARCHIVOS.IMAGENES.CARPETA', N'Carpeta Imagenes', N'Carpeta para guardar las imagenes', 1, 2, N'|DataDirectory|', 1, 0, NULL, NULL, N'2018-06-04 22:57:18', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (5, N'DATA.ARCHIVOS.IMAGENES.URL', N'Url Base Imagenes', N'Url Base para visualizar imagenes', 1, 2, N'PENDIENTE', 1, 0, NULL, NULL, N'2018-06-04 22:57:18', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (6, N'SISTEMA.URL', N'Url Base del Sistema', N'URL Base del sistema', 1, 2, N'http://localhost:7090', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (7, N'SEGURIDAD.BLOQUEO_USUARIO.TIEMPO', N'Tiempo de Bloqueo (Minutos)', N'Tiempo de bloqueo en minutos a un usuario por itentos fallidos', 1, 1, N'5', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (8, N'SEGURIDAD.BLOQUEO_USUARIO.INTENTOS', N'Numero de intentos permisos, antes de bloqueo', N'Numero de intentos permitidos, antes de realizar un bloque al usuario por el tiempo determinado en configuracion', 1, 1, N'5', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (9, N'SEGURIDAD.CLAVE.MINIMO', N'Numero minino de longitud de la contraseña', N'Numero minino de  longitud de la contraseña', 1, 1, N'6', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (10, N'SEGURIDAD.CLAVE.REQUIERE.DIFERENTE_DIGITO_LETRA', N'La contraseña requiere un caracter diferente a letra y digito', N'La contraseña requiere un caracter diferente a letra y digito', 1, 3, N'true', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (11, N'SEGURIDAD.CLAVE.REQUIERE.DIGITO', N'La contraseña requiere un digito', N'La contraseña requiere un digito', 1, 3, N'true', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (12, N'SEGURIDAD.CLAVE.REQUIERE.LETRA_MINUSCULA', N'La contraseña requiere una letra minuscula', N'La contraseña requiere una letra minuscula', 1, 3, N'true', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
INSERT INTO [SCH_USUARIOS].[parametros] ([Id], [Codigo], [Nombre], [Descripcion], [Categoria], [Tipo], [Valor], [EsEditable], [TieneOpciones], [LastModificationTime], [LastModifierUserId], [CreationTime], [CreatorUserId]) VALUES (13, N'SEGURIDAD.CLAVE.REQUIERE.LETRA_MAYUSCULA', N'La contraseña requiere una letra mayuscula', N'La contraseña requiere una letra mayuscula', 1, 3, N'true', 1, 0, NULL, NULL, N'2018-06-06 00:00:00', 1)
SET IDENTITY_INSERT [SCH_USUARIOS].[parametros] OFF


             
-- Menus
SET IDENTITY_INSERT [SCH_USUARIOS].[menus] ON
INSERT INTO [SCH_USUARIOS].[menus] ([Id], [Descripcion], [Codigo], [Nombre], [ModuloId]) VALUES (1, N'Principal', N'MAIN', N'Menu Principal',1)
SET IDENTITY_INSERT [SCH_USUARIOS].[menus] OFF


SET IDENTITY_INSERT [SCH_USUARIOS].[menuitems] ON
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (2, NULL, N'/ParametroSistema', 1, 2, 1, N'fa fa-gear', 1, NULL, 3, N'PARAMETRO', N'Parámetros')
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (3, NULL, N'/Seguridad/Rol', 1, 2, 2, N'fa fa-unlock', 1, NULL, 2, N'ROLES', N'Roles y Permisos')
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (4, NULL, N'/Seguridad/Usuario', 1, 2, 3, N'fa fa-users', 1, NULL, 1, N'USUARIOS', N'Usuarios')
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (5, NULL, N'/Sesion', 1, 2, 4, N'fa fa-exchange', 1, NULL, 4, N'SESION', N'Sesiones')
SET IDENTITY_INSERT [SCH_USUARIOS].[menuitems] OFF



--VIew
SET IDENTITY_INSERT [SCH_USUARIOS].[views] ON
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (1, N'com.cpp.calypso.seguridad.aplicacion.RolDto.Tree', N'com.cpp.calypso.seguridad.aplicacion.RolDto, com.cpp.calypso.seguridad.aplicacion', N'<?xml version="1.0" encoding="utf-16"?>
                <Tree xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                  <Fields>
                    <Field Name="Id" String="Id" Invisible="true" />
                    <Field Name="Codigo" String="Código" Invisible="false" />
                    <Field Name="Nombre" String="Nombre" Invisible="false" />
	                <Field Name="EsAdministrador" String="EsAdministrador" Invisible="false" />
	                <Field Name="EsExterno" String="EsExterno" Invisible="false" />
                  </Fields>
                  <Buttons>
                    <Button Icon="fa fa-unlock-alt" String="Permisos" Type="Controler/Action" Name="Rol/CreatePermiso" /> 
                  </Buttons> 
                </Tree>')
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (2, N'com.cpp.calypso.seguridad.aplicacion.RolDto.Form', N'com.cpp.calypso.seguridad.aplicacion.RolDto, com.cpp.calypso.seguridad.aplicacion', N'<?xml version="1.0" encoding="utf-16"?>
                <Form xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                  <Fields>
                    <FieldForm Name="Id" Invisible="true" />
	                <FieldForm Name="Codigo" Invisible="false" />
	                <FieldForm Name="Nombre" Invisible="false" />
	                <FieldForm Name="EsAdministrador" Invisible="false" />
	                <FieldForm Name="EsExterno" Invisible="false" />
	                <FieldForm Name="Url" Invisible="false" /> 
	                 
                  </Fields>
                </Form>')
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (3, N'com.cpp.calypso.comun.dominio.ParametroSistema.Tree', N'com.cpp.calypso.comun.dominio.ParametroSistema, com.cpp.calypso.comun.dominio', N'<?xml version="1.0" encoding="utf-16"?>
                <Tree xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" Create="false" Delete="false">
                  <Fields>
                    <Field Name="Id" String="Id" Invisible="true" />
                    <Field Name="Nombre" String="Nombre" Invisible="false" />
	                <Field Name="Descripcion" String="Descripción" Invisible="false" />
	                <Field Name="Tipo" String="Tipo" Invisible="false" />
	                <Field Name="Valor" String="Valor" Invisible="false" />
	                <Field Name="EsEditable" String="Es Editable" Invisible="false" />
                  </Fields>
                </Tree>')
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (4, N'com.cpp.calypso.comun.dominio.Sesion.Tree', N'com.cpp.calypso.comun.dominio.Sesion, com.cpp.calypso.comun.dominio', N'<?xml version="1.0" encoding="utf-16"?>
<Tree xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
Create="false" Edit="false" Delete="false" Details="false" >
  <Fields>
	<Field Name="Id" String="Id" Invisible="true" />
	<Field Name="Cuenta" String="Cuenta" Invisible="false" />
	<Field Name="CreationTime" String="Fecha" Invisible="false" />
	<Field Name="ClientIpAddress" String="IP" Invisible="false" />
	<Field Name="BrowserInfo" String="Navegador" Invisible="false" />
	<Field Name="Result" String="Resultado" Invisible="false" /> 
  </Fields>
</Tree>')
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (5, N'com.cpp.calypso.comun.dominio.Sesion.Search', N'com.cpp.calypso.comun.dominio.Sesion, com.cpp.calypso.comun.dominio', N'<?xml version="1.0" encoding="utf-16"?>
                <Search xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                  <Fields>
                    <FieldSearch Name="Cuenta" String="Cuenta" Operator = "StartsWith"  />
	                <FieldSearch Name="CreationTime" String="Fecha"/>
                  </Fields>
                </Search>')
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (6, N'com.cpp.calypso.seguridad.aplicacion.UsuarioDto.Tree', N'com.cpp.calypso.seguridad.aplicacion.UsuarioDto, com.cpp.calypso.seguridad.aplicacion', N'<?xml version="1.0" encoding="utf-16"?>
<Tree xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"   >
  <Fields>
	<Field Name="Id" String="Id" Invisible="true" />
	<Field Name="Cuenta" String="Cuenta" Invisible="false" />
	<Field Name="Identificacion" String="Identificación" Invisible="false" />
	<Field Name="Nombres" String="Nombres" Invisible="false" />
	<Field Name="Apellidos" String="Apellidos" Invisible="false" />
	<Field Name="Correo" String="Correo" Invisible="false" />
	<Field Name="Estado" String="Estado" Invisible="false" /> 
  </Fields>
   <Buttons>
	<Button Icon="fa fa-unlock-alt" String="Resetear Contraseña" Type="Controler/Action" Name="Usuario/Reseteo" /> 
  </Buttons> 
</Tree>')
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (7, N'com.cpp.calypso.seguridad.aplicacion.UsuarioDto.Search', N'com.cpp.calypso.seguridad.aplicacion.UsuarioDto, com.cpp.calypso.seguridad.aplicacion', N'<?xml version="1.0" encoding="utf-16"?>
                <Search xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                  <Fields>
                    <FieldSearch Name="Identificacion" String="Identificación" Operator = "StartsWith"  />
                    <FieldSearch Name="Cuenta" String="Cuenta"   Operator = "Contains"  />
					<FieldSearch Name="Estado" String="Estado" /> 
                  </Fields>
                </Search>')
INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (8, N'com.cpp.calypso.seguridad.aplicacion.UsuarioDto.Form', N'com.cpp.calypso.seguridad.aplicacion.UsuarioDto, com.cpp.calypso.seguridad.aplicacion', N'<?xml version="1.0" encoding="utf-16"?>
				<Form xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
				  <Fields>
					<FieldForm Name="Id" Invisible="true" />
					<FieldForm Name="Cuenta" Invisible="false" />
					<FieldForm Name="Identificacion" Invisible="false" />
					<FieldForm Name="Nombres" Invisible="false" />
					<FieldForm Name="Apellidos" Invisible="false" />
					<FieldForm Name="Correo" Invisible="false" /> 
					<FieldForm Name="Clave" Invisible="false" /> 
					<FieldForm Name="Estado" Invisible="false" Widget="Enum" /> 
					<FieldForm Name="Modulos" Invisible="false" Widget="ListBox" /> 
					<FieldForm Name="Roles" Invisible="false" Widget="ListBox" /> 
					 
				  </Fields>
				</Form>')
SET IDENTITY_INSERT [SCH_USUARIOS].[views] OFF


