

SET IDENTITY_INSERT [SCH_USUARIOS].[funcionalidades] ON
INSERT INTO [SCH_USUARIOS].[funcionalidades] ([Id], [Codigo], [Nombre], [Descripcion], [Controlador], [Estado], [ModuloId]) VALUES (5, N'fun_auditoria', N'Auditoría', N'Visualización de auditoría', N'Auditoria', 1, 1)
SET IDENTITY_INSERT [SCH_USUARIOS].[funcionalidades] OFF

        
        
SET IDENTITY_INSERT [SCH_USUARIOS].[acciones] ON
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (15, N'GET', N'Visualizar Auditoría', 5)
SET IDENTITY_INSERT [SCH_USUARIOS].[acciones] OFF 


SET IDENTITY_INSERT [SCH_USUARIOS].[menuitems] ON
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (6, NULL, N'/Seguridad/Auditoria', 1, 2, 4, N'fa fa-exchange', 1, NULL, 5, N'AUDITORIA', N'Auditoría')
SET IDENTITY_INSERT [SCH_USUARIOS].[menuitems] OFF

--VIew
SET IDENTITY_INSERT [SCH_USUARIOS].[views] ON 

INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (20, N'com.cpp.calypso.seguridad.aplicacion.AuditoriaDto.Tree', N'com.cpp.calypso.seguridad.aplicacion.AuditoriaDto, com.cpp.calypso.seguridad.aplicacion', N'<?xml version="1.0" encoding="utf-16"?>
                <Tree xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" default_order="CreatedDate DESC">
                  <Fields>
                    <Field Name="CreatedBy" String="Usuario" Invisible="false" />
                    <Field Name="CreatedDate" String="Fecha" Invisible="false" />
                    <Field Name="EntityTypeName" String="Entidad" Invisible="false" />
	                <Field Name="StateName" String="Estado" Invisible="false" /> 
                  </Fields>
                
                </Tree>')


INSERT INTO [SCH_USUARIOS].[views] ([Id], [Name], [Model], [Arch]) VALUES (21, N'com.cpp.calypso.seguridad.aplicacion.AuditoriaDto.Search', N'com.cpp.calypso.seguridad.aplicacion.AuditoriaDto, com.cpp.calypso.seguridad.aplicacion', N'<?xml version="1.0" encoding="utf-16"?>
                <Search xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                  <Fields>
                    <FieldSearch Name="CreatedBy" String="Usuario" Operator = "StartsWith"  />
					<FieldSearch Name="EntityTypeName" String="Entidad"/>
	                <FieldSearch Name="CreatedDate" String="Fecha"/>
					<FieldSearch Name="State" String="Estado"/>
                  </Fields>
                </Search>')

SET IDENTITY_INSERT [SCH_USUARIOS].[views] OFF


