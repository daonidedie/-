using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;

namespace Skybot.Collections.Analyse
{

    /// <summary>
    ///采集文章列表分析器  yankuai.com
    /// </summary>
    public class YankuaiComListPageAnalyse:IAnalyse
    {

       /// <summary>
       /// 175
       /// </summary>
        private string _MaxPageNum = "175";

        /// <summary>
        /// 目前最大为175
        /// </summary>
        public string MaxPageNum
        {
            get { return _MaxPageNum; }
            set { _MaxPageNum = value; }
        } 

        //基url
        public const string BaseUrl = "http://www.yankuai.com/book/toplastupdate/0/{0}.htm";//1-175


        private List<TygModel.书名表> books = new List<TygModel.书名表>();
        /// <summary>
        /// 书名表
        /// </summary>
        public List<TygModel.书名表> Books
        {
            get { return books; }
            set { books = value; }
        }



        public string PathExpression
        {
            get { throw new NotImplementedException(); }
        }

        public object UserToKen
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 返回采集到的书名集合
        /// </summary>
        /// <returns></returns>
        public object GetResult()
        {
            return Books;
        }

        public string GetPathExpression(ListPageContentUrl IndexPageUrl, IEnumerable<ListPageContentUrl> ContentPageUrls)
        {
            throw new NotImplementedException();
        }

        public string GetContent(string Content)
        {
            throw new NotImplementedException();
        }
    }
}