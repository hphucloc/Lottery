USE [Lottery]
GO
/****** Object:  StoredProcedure [dbo].[GetNextAppear]    Script Date: 1/28/2022 3:11:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[GetNextAppear]
@LeadOffset int,
@number nvarchar(200),
@numberTypeId int,
@numberWinLevelId int
AS
BEGIN
	select a.NumberId, a.NumberTypeId, a.NumberWinLevelId, a.LotNumber, a.DatePublish, 
	LEAD(DatePublish, @LeadOffset) OVER(ORDER BY a.DatePublish) 'NextPublishDate'
	from Number a
	where a.NumberTypeId = @numberTypeId and NumberWinLevelId = @numberWinLevelId
	order by a.DatePublish;
END