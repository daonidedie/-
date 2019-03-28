SELECT  *
      
  FROM [Tyg].[dbo].[文章表]
  
  
  where datalength(内容)<500; --datalength 查看 text 的长度

  -- 去除重复记录
--delete from [书名表] where ID not in(select min(ID) from [书名表] group by [书名] having count([书名])>=1);
--清空表数据
--truncate table [文章表]
--update  [Tyg].[dbo].[书名表] set 采集用的URL1='http://www.86zw.com/Html/Book/32/32600/Index.shtml'  where [书名]='永生'
--update  [Tyg].[dbo].[书名表] set 采集用的URL1='http://www.86zw.com/Html/Book/32/32603/Index.shtml'  where [书名]='吞噬星空'
--update  [Tyg].[dbo].[书名表] set 采集用的URL1='http://www.86zw.com/Html/Book/29/29051/Index.shtml'  where [书名]='仙逆'
 update   [Tyg].[dbo].书名表 set 包含有效章节= (select count(*) from [Tyg].[dbo].文章表 where  [Tyg].[dbo].文章表.[GUID]=[Tyg].[dbo].书名表.[GUID]) 