Declare @level int
declare @count int
declare @sql nvarchar(max)
declare @partname nvarchar (max)

SET @count = 1
SET @level = 1
SET @partname = ''

SET @sql = N' SELECT  
a.assembly_id, 
a.assembly_type, 
a.grouping_num, 
a.assembly_name, 
a.assembly_unique_tag, 
a.assembly_apr 
FROM pegged p 
join catalog_assembly ca0 
on p.assembly_id = ca0.assembly_id '

WHILE(@count <= @level)

BEGIN

SET @sql = @sql + N'
 join catalog_assembly ca' + CONVERT(nvarchar(1), @count) + ' 
 on ca' + CONVERT(nvarchar(1), @count-1) + '.parent_assembly_id = ca'+ CONVERT(nvarchar(1), @count) +'.assembly_id '

 IF(@count = @level)
 BEGIN
 
 set @sql = @sql + N' join [assembly] a
 on ca' + CONVERT(nvarchar(1), @count) +'.assembly_id = a.assembly_id 
 where p.pegged_partname = ' + '''' + @partname + ''''
 
END

SET @count = @count + 1

END

PRINT @sql

exec sp_executesql @sql
