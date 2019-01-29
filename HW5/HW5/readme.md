1. Создать БД с именем Lesson7

2. Создать таблицы Employee и Department

CREATE TABLE[dbo].[Employee] (
                                    [Id] INT NOT NULL PRIMARY KEY,
									[LastName] NVARCHAR(50) NOT NULL, 
                                    [Name] NVARCHAR(50) COLLATE Cyrillic_General_CI_AS NOT NULL,
									[SecondName] NVARCHAR(50) NOT NULL,
                                    [Birthday] NVARCHAR(MAX) NOT NULL,
									[DepartmentID]    INT NOT NULL
                                );

CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Department] NVARCHAR(50) NOT NULL
);

3. Для заполнения тестовыми данными выполнить:

