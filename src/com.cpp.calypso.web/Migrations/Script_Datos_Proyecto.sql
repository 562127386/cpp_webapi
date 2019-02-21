-- EJEMPLO DE SCRIPT 

--Modulos y funcionalidades
SET IDENTITY_INSERT [SCH_USUARIOS].[modulos] ON
INSERT INTO [SCH_USUARIOS].[modulos] ([Id], [Codigo], [Descripcion], [Nombre]) VALUES (2, N'mod_proyectos', N'Proyectos', N'Proyectos')
SET IDENTITY_INSERT [SCH_USUARIOS].[modulos] OFF

--Funcionalidades de Proyectos
SET IDENTITY_INSERT [SCH_USUARIOS].[funcionalidades] ON
INSERT INTO [SCH_USUARIOS].[funcionalidades] ([Id], [Codigo], [Nombre], [Descripcion], [Controlador], [Estado], [ModuloId]) VALUES (10, N'fun_proyectos', N'Gestion de Proyectos', N'Gestion de Proyectos', N'proyecto', 1, 2)
SET IDENTITY_INSERT [SCH_USUARIOS].[funcionalidades] OFF

      
SET IDENTITY_INSERT [SCH_USUARIOS].[acciones] ON
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (20, N'GET', N'Visualizar ', 10)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (21, N'CREATE', N'Crear ', 10)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (22, N'UPDATE', N'Editar ', 10)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (23, N'DELETE', N'Eliminar ', 10)
INSERT INTO [SCH_USUARIOS].[acciones] ([Id], [Codigo], [Nombre], [FuncionalidadId]) VALUES (24, N'EXPORT', N'Exportar Datos (Excel,PDF)', 10)

SET IDENTITY_INSERT [SCH_USUARIOS].[acciones] OFF

-- Menus

SET IDENTITY_INSERT [SCH_USUARIOS].[menus] ON
INSERT INTO [SCH_USUARIOS].[menus] ([Id], [Descripcion], [Codigo], [Nombre], [ModuloId]) VALUES (10, N'Proyectos', N'PROYECTOS', N'Menu Proyectos',2)
SET IDENTITY_INSERT [SCH_USUARIOS].[menus] OFF


SET IDENTITY_INSERT [SCH_USUARIOS].[menuitems] ON
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (20, NULL, NULL, 1, 1, 1, N'icon-layers', 2, NULL, 5, N'PROYECTO-c', N'Proyectos Contenedor')
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (21, NULL, N'/Proyecto/Proyecto', 1, 2, 1, N'icon-layers', 2, 20, 5, N'PROYECTO-2', N'Proyectos Nivel 2')
INSERT INTO [SCH_USUARIOS].[menuitems] ([Id], [Descripcion], [Url], [Estado], [TipoId], [Orden], [Icono], [MenuId], [PadreId], [FuncionalidadId], [Codigo], [Nombre]) VALUES (22, NULL, N'/Proyecto/Proyecto', 1, 2, 1, N'icon-layers', 2, 20, 5, N'PROYECTO-3', N'Proyectos Nivel 2 - Foo')

SET IDENTITY_INSERT [SCH_USUARIOS].[menuitems] OFF


 


   