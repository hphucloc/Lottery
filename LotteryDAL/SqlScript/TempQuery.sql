--select * from dbo.number a order by a.NumberId desc;

--select * from dbo.number a order by a.DatePublish, a. LotNumber desc;

select * from dbo.number a where a.NumberTypeId = 1 Order by a.LotNumber 