--80333B06-1A9C-422E-AFCD-94F026144F8F
declare @projectId uniqueidentifier = '80333B06-1A9C-422E-AFCD-94F026144F8F';


with projectTable(projectId,departmentId, programId, leaderId, wayId)
as
(

select 
prj.ProjectID, prj.DepartmentID, prj.ProgramAreaID, ProjectPI, ProcessProjectWayID
from Projects prj
where prj.ProjectID = @projectId

)
,

 projectPlayer(UId, projectId, 
rosterId, rostertypeId, rosterName, rostertypeName,
roleId, roletypeId, roleName, roletypeName
)
as
(

select
pp.Uid, pp.ProjectID,
pp.RosterId, rpc.UId rostertypeId, rp.RosterName, rpc.Description rostertypeName,
pp.RoleId, rc.UId roletypeId,r.RoleName, rc.Description roletypeName

--cast( rp.RosterName as nvarchar(32) ) RosterName, cast(  r.RoleName as nvarchar(32) ) RoleName,
--len(rp.RosterName) rosterNameChars,
--len(r.RoleName) roleNameChars
from PlayerProject pp
inner join Roster rp on rp.RosterID = pp.RosterId
inner join RosterCategory rpc on rpc.UId = rp.rosterCategoryId
inner join roles r on r.RoleID = pp.RoleId
inner join RoleCategory rc on rc.UId = r.RoleCategoryId
where pp.ProjectID = @projectId
)


--select * from projecttable
select * from projectPlayer

/*
select
cast( rp.RosterName as nvarchar(32) ) RosterName, cast(  r.RoleName as nvarchar(32) ) RoleName,
len(rp.RosterName) rosterNameChars,
len(r.RoleName) roleNameChars
from PlayerProject pp
inner join Roster rp on rp.RosterID = pp.RosterId
inner join roles r on r.RoleID = pp.RoleId


select 
prj.ProjectID
from Projects prj
where prj.ProjectNumber like 'PN2704-8'

select
cast( rp.RosterName as nvarchar(32) ) RosterName, cast(  r.RoleName as nvarchar(32) ) RoleName,
len(rp.RosterName) rosterNameChars,
len(r.RoleName) roleNameChars
from PlayerProject pp
inner join Roster rp on rp.RosterID = pp.RosterId
inner join roles r on r.RoleID = pp.RoleId
*/
--order by rosterNameChars desc