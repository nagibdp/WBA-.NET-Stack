CREATE DATABASE WBAV6;
USE WBAV6;

--CREATE TABLES

CREATE TABLE proyects(
id int identity(1,1) not null primary key,
title nvarchar(255),
description nvarchar(max),
keyword nvarchar(255),
document nvarchar(255),
);

CREATE TABLE users(
id int identity(1,1) not null primary key,
idProyect int,
name nvarchar(30),
lastNameF nvarchar(30),
lastNameM nvarchar(30),
pass nvarchar(255) not null,
email nvarchar(255) not null unique,
career nvarchar(30),
role nvarchar(255) not null,
constraint fkProyect foreign key(idProyect) 
references proyects(id),
);	

CREATE TABLE profiles(
id int identity(1,1) not null primary key,
userId int unique,
cel nvarchar(10),
picture nvarchar(255),
description nvarchar(max),
estudy nvarchar(max),
academy nvarchar(255),
available tinyint,
place_at nvarchar(255),
dt_visible tinyint,
keyword1 nvarchar(255),
keyword2 nvarchar(255),
keyword3 nvarchar(255),
keyword4 nvarchar(255),
keyword5 nvarchar(255),
constraint fkTeacher foreign key(userId) 
references users(id)
);		

CREATE TABLE validation(
id int primary key not null,
email varchar(255),
code varchar(255),
);

--VIEWS


--create view vwUsers
--as 
--select 





--STORED PROCEDURES

create procedure spCUDUsers(
@id int, 
@idProyect int, 
@name nvarchar(30), 
@lastNameF nvarchar(30), 
@lastNameM nvarchar(30), 
@pass nvarchar(255), 
@email nvarchar(255), 
@career nvarchar(30), 
@role nvarchar(30), 
@operation smallint)
as
begin
	if(@operation = 1) --CREATE
	begin
		insert into users values (@idProyect, @name, @lastNameF, @lastNameM, @pass, @email, @career, @role);
	end
	if(@operation = 2) --UPDATE
	begin
		update users set idProyect = @idProyect, name = @name, lastNameF = @lastNameF, lastNameM = @lastNameM, pass = @pass, 
						email = @email, career = @career, role = @role
		where id = @id;
	end
	if(@operation = 3) --DELETE
	begin
		delete from users where id = @id
	end
end;

create procedure spCUDProfiles(
@id int, 
@userId int, 
@cel varchar(10), 
@picture varchar(255), 
@description text, 
@estudy text, 
@academy varchar(255), 
@available tinyint, 
@place_at varchar(255), 
@dt_visible tinyint, 
@keyword1 varchar(255), 
@keyword2 varchar(255), 
@keyword3 varchar(255), 
@keyword4 varchar(255), 
@keyword5 varchar(255),
@operation tinyint)
as
begin
	if(@operation = 1) --CREATE
	begin
		insert into profiles values (@userId, @cel, @picture, @description, @estudy, @academy, @available, @place_at, @dt_visible, @keyword1,
		@keyword2, @keyword3, @keyword4, @keyword5);
	end
	if(@operation = 2) --UPDATE
	begin
		update profiles set userId = @userId, cel = @cel, picture = @picture, description = @description, 
						estudy = @estudy, academy = @academy, available = @available, place_at = @place_at, 
						dt_visible = @dt_visible, keyword1 = @keyword1, keyword2 = @keyword2, keyword3 = @keyword3, 
					 	@keyword4 = @keyword4, keyword5 = @keyword5
		where id = @id;
	end
	if(@operation = 3) --DELETE
	begin
		delete from profiles where id = @id
	end
end;

create procedure spCUDProyects(
@id int, 
@title varchar(255), 
@description text, 
@keyword varchar(255), 
@document varchar(255), 
@operation smallint)
as
begin
	if(@operation = 1) --CREATE
	begin
		insert into proyects values (@title, @description, @keyword, @document);
	end
	if(@operation = 2) --UPDATE
	begin
		update proyects set title = @title, description = @description, keyword = @keyword, document = @document
		where id = @id;
	end
	if(@operation = 3) --DELETE
	begin
		delete from proyects where id = @id
	end
end;



create procedure spValidate(
@email nvarchar(255),
@pass nvarchar(500))
as
begin
	if (exists(select * from users where email = @email and pass = @pass))
		select id from users where email = @email and pass = @pass
	else
		select '0'
end;


select * from users 
select * from profiles 
select * from proyects

delete from proyects where id=1013

select idProyect, COUNT(id) from users group by idProyect

select U.id, Proy.id, Proy.title, Proy.description, Proy.document, Proy.keyword, 
U.name, U.lastNameF, U.lastNameM, U.email, U.career, U.role from users U join proyects Proy on U.idProyect = Proy.id

select U.id, U.name, Pr.userId, Pr.description from users U join profiles Pr on U.id = Pr.userId

select * from users U join profiles P on U.id = P.userId

