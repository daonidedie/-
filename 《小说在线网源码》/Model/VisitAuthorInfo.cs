using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VisitAuthorInfo
    {
        private int _visitId;
        public int VisitId
        {
            get
            {
                return _visitId;
            }
            set
            {
                _visitId = value;
            }
        }

        private string _visitTitle;
        public string VisitTitle
        {
            get
            {
                return _visitTitle;
            }
            set
            {
                _visitTitle = value;
            }
        }

        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        private string _contents;
        public string Contents
        {
            get
            {
                return _contents;
            }
            set
            {
                _contents = value;
            }
        }

        private string _visitDate;
        public string VisitDate
        {
            get
            {
                return _visitDate;
            }
            set
            {
                _visitDate = value;
            }
        }

        private string _userName;
        public string UserName
        {
            get 
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }

        private int _userSex;
        public int UserSex
        {
            get
            {
                return _userSex;
            }
            set
            {
                _userSex = value;
            }
        }

        private DateTime _userBrithday;
        public DateTime UserBrithday
        {
            get
            {
                return _userBrithday;
            }
            set
            {
                _userBrithday = value;
            }
        }


        private string _usetImage;
        public string UsetImage
        {
            get
            {
                return _usetImage;
            }
            set
            {
                _usetImage = value;
            }
        }

        private DateTime _createTime;
        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }

        private string _userAddress;
        public string userAddress
        {
            get
            {
                return _userAddress;
            }
            set
            {
                _userAddress = value;
            }
        }

        private int _provinceId;
        public int ProvinceId
        {
            get
            {
                return _provinceId;
            }
            set
            {
                _provinceId = value;
            }
        }

        private int _areaId;
        public int areaId
        {
            get
            {
                return _areaId;
            }
            set
            {
                _areaId = value;
            }
        }


        private string _authorPhoto;
        public string AuthorPhoto
        {
            get
            {
                return _authorPhoto;
            }

            set
            {
                _authorPhoto = value;
            }
        }

        private String _visitDateString;
        public String VisitDateString
        {
            get
            {
                return _visitDateString;
            }
            set
            {
                _visitDateString = value;
            }
        }

        private String _userBrithdayString;
        public String UserBrithdayString
        {
            get
            {
                return _userBrithdayString;
            }
            set
            {
                _userBrithdayString = value;
            }
        }
    }
}
