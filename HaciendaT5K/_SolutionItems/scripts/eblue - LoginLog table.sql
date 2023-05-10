--Flags -> 
	--deny,  when user isblocked and has informed before
	--grant, when user has login.succes
	--attempt, when user try login access 
create table LoginLog
(
  UId uniqueidentifier primary key,
  UserId uniqueidentifier references Users(UId),
  Flags int not null default(0),
  Tag nvarchar(max), --used for  deny,grant, attempt |email, ?..
  CreatedDate datetime not null default(getdate())
)
alter table LoginLog
add OrderLine int identity(1,1)

insert into LoginLog(UId, UserId, Flags, Tag)
values 
	  (@UId, @userId, @flags, 
	  case (@flags)
	  when  1 then 'grant'
	  when  2 then 'deny'
	  when  3 then ''
	  end
	   )