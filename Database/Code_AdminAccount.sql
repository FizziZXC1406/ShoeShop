use DatabaseProjectFinalExam
Create table AdminAccount (
	ID varchar(10) Primary key,
	AdminName varchar(100),
	Password varchar(100),
)

insert into AdminAccount(ID, AdminName, Password) 
values ('AD0101', '1', '1')