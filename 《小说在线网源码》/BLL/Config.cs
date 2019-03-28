using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BLL
{
    public static class Config
    {
        //站点名称
        private static string _siteName;
        private static int _pageSize;
        private static int _updateSectionExpireDays;

        //静态块，调用该类时自动执行一次，且只执行一次
        static Config()
        {
            //从配置文件读取站点名称
            _siteName = ConfigurationManager.AppSettings["siteName"];
            _pageSize =Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            _updateSectionExpireDays = Convert.ToInt32(ConfigurationManager.AppSettings["updateSectionExpireDays"]);
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        public static string SiteName
        {
            get { return _siteName; }
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        public static int PageSize
        {
            get { return _pageSize; }
        }

        /// <summary>
        /// 更新章节的日期
        /// </summary>
        public static int UpdateSectionExpireDays
        {
            get { return _updateSectionExpireDays; }
        }

    }
}
