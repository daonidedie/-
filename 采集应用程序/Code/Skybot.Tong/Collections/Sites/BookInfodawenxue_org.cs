using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections.Analyse;

namespace Skybot.Collections.Sites
{
    /// <summary>
    /// 86zw 书信息
    /// </summary>
    public class BookInfodawenxue_org : Skybot.Collections.Sites.AbstractBookInfo
    {

        public string 更新 { get; set; }

        /// <summary>
        /// 转换数据
        /// </summary>
        /// <returns></returns>
        public override TygModel.书名表 Convert()
        {
            DateTime updateTime = DateTime.Parse(更新);
            //书本
            TygModel.书名表 book = null;
            //看看分类表里有没有这个分类如果没有则进行分类添加

            using (TygModel.Entities tygdb = new TygModel.Entities())
            {
                //分类表
                var classItems = tygdb.分类表.Where(p => p.分类名称.Trim() == 类别.Trim());
                //当前分类
                TygModel.分类表 classItem = null;
                //如果分类不存在
                if (classItems.Count() == 0)
                {
                    try
                    {
                        classItem = new TygModel.分类表()
                        {
                            分类标识 = 类别.Trim(),
                            分类名称 = 类别.Trim(),
                            分类说明 = 类别.Trim(),
                            备注 = "来自 www.dawenxue.org",
                            通用分类 = 类别.Trim()

                        };
                        tygdb.AddTo分类表(classItem);
                        //保存分类
                        tygdb.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));
                    }
                }
                else
                {
                    classItem = classItems.FirstOrDefault();
                }





                book = new TygModel.书名表()
                {
                   
                    分类标识 = classItem.分类标识,
                    分类表ID = classItem.ID,
                    GUID = Guid.NewGuid(),
                    采集用的URL1 = 小说目录URL,
                    采集用的URL2 = 小说简介URL,
                    创建时间 = DateTime.Now,
                    最新章节 = 最新章节,
                    作者名称 = 作者,
                    说明 = "",
                    书名 = 小说名称.Replace("》", "").Replace("《", ""),
                    最后更新时间 = updateTime,
                    完本 = 状态.Trim() == "连载" ? false : true,
                    配图 = "/images/noimg.gif",
                };
                //修改配图或者说明

                if (小说简介URL != null)
                {




                    book.说明 = string.Format("小说《{0}》{1}/著", 小说名称, 作者);

                    


                    
                }
            }


            return book;
        }
    }
}
