using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybot.Collections.Sites
{

    /// <summary>
    /// 采集书信息抽象类
    /// </summary>
    public abstract class AbstractBookInfo
    {


        public string 小说简介URL { get; set; }
        public string 采集URL { get; set; }
        public string 小说目录URL { get; set; }
        public string 类别 { get; set; }
        public string 小说名称 { get; set; }
        public string 最新章节 { get; set; }
        public string 作者 { get; set; }
        public string 状态 { get; set; }



        /// <summary>
        /// 转换数据
        /// </summary>
        /// <returns></returns>
        public abstract TygModel.书名表 Convert();

    }
}
