-- Script Date: 22.04.2025 0:04  - ErikEJ.SqlCeScripting version 3.5.2.95
-- Database information:
-- Database: D:\C# Projects\ProfessorsSSU\professors.db
-- ServerVersion: 3.40.0
-- DatabaseSize: 28 KB
-- Created: 21.04.2025 23:35

-- User Table information:
-- Number of tables: 4
-- __EFMigrationsHistory: -1 row(s)
-- __EFMigrationsLock: -1 row(s)
-- Editors: -1 row(s)
-- Professors: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
DROP TABLE IF EXISTS [Professors];
DROP TABLE IF EXISTS [Editors];
CREATE TABLE [Professors] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Surname] text NOT NULL
, [DepartmentName] text NOT NULL
, [BirthYear] bigint NOT NULL
, [EmploymentYear] bigint NOT NULL
, [Position] text NOT NULL
, [AcademicDegree] text NOT NULL
, [AcademicRank] text NULL
);
CREATE TABLE [Editors] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Name] text NOT NULL
, [Password] text NOT NULL
);
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
1,'Шевченко','Кафедра інформатики',1975,2000,'Професор','Доктор наук','Професор');
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
2,'Ковальчук','Кафедра математики',1980,2005,'Доцент','Доктор філософії','Доцент');
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
3,'Бондаренко','Кафедра фізики',1978,2003,'Доцент','Доктор філософії',NULL);
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
4,'Ткаченко','Кафедра біології',1985,2010,'Старший викладач','Доктор філософії',NULL);
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
5,'Кравець','Кафедра хімії',1972,1998,'Завідувач кафедри','Доктор наук','Професор');
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
6,'Мельник','Кафедра економіки',1982,2007,'Доцент','Доктор філософії','Доцент');
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
7,'Сидоренко','Кафедра філології',1977,2002,'Професор','Доктор наук','Професор');
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
8,'Литвиненко','Кафедра історії',1983,2008,'Старший викладач','Доктор філософії',NULL);
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
9,'Поліщук','Кафедра психології',1981,2006,'Доцент','Доктор філософії','Доцент');
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
10,'Романюк','Кафедра соціології',1984,2009,'Доцент','Доктор філософії',NULL);
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
11,'Забужко','Кафедра філософії',1970,1995,'Професор','Доктор наук','Професор');
INSERT INTO [Professors] ([Id],[Surname],[DepartmentName],[BirthYear],[EmploymentYear],[Position],[AcademicDegree],[AcademicRank]) VALUES (
12,'Гриценко','Кафедра політології',1979,2004,'Старший викладач','Доктор філософії',NULL);
INSERT INTO [Editors] ([Id],[Name],[Password]) VALUES (
1,'admin','12345678');
COMMIT;

