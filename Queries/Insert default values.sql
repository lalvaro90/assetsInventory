select * from Providers
select * from Depreciations
select * from Persons

INSERT into Depreciations (Name, [Percentage],NextRun,LastRun) VALUES ('No Asignado',0,GETDATE(),GETDATE())
insert into Providers (Name, Email, Phone) VALUES ('No Asignado','','')
INSERT Into Persons (Name, LastName, Email, Phone, NationalId, [Status]) VALUES ('No Asignado','','','','',1)