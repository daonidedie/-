using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class NovelNewsInfo
    {
        private int _newsId;
        public int NewsId
        {
            get
            {
                return _newsId;
            }
            set
            {
                _newsId = value;
            }
        }

        private string _newsTitle;
        public string NewsTitle
        {
            get
            {
                return _newsTitle;
            }
            set
            {
                _newsTitle = value;
            }
        }

        private string _newsContens;
        public string NewsContens
        {
            get
            {
                return _newsContens;
            }
            set
            {
                _newsContens = value;
            }
        }

        private string _newsImages;
        public string NewsImages
        {
            get
            {
                return _newsImages;
            }
            set
            {
                _newsImages = value;
            }
        }

        private string _fromWhere;
        public string FromWhere
        {
            get
            {
                return _fromWhere;
            }
            set
            {
                _fromWhere = value;
            }
        }

        private DateTime _addTime;
        public DateTime AddTime
        {
            get
            {
                return _addTime;
            }
            set
            {
                _addTime = value;
            }
        }


        private string _addTimeString;
        public string AddTimeString
        {
            get
            {
                return _addTimeString;
            }
            set
            {
                _addTimeString = value;
            }
        }



    }
}
