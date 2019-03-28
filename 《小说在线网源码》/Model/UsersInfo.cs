using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UsersInfo
    {
        public string province { get; set; }
        public string city { get; set; }

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

        private string _accountNumber;
        public string AccountNumber
        {
            get
            {
                return _accountNumber;
            }
            set
            {
                _accountNumber = value;
            }
        }

        private string _accountPassword;
        public string AccountPassword
        {
            get
            {
                return _accountPassword;
            }
            set
            {
                _accountPassword = value;
            }
        }

        private int _userType;
        public int UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                _userType = value;
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

        private System.Decimal _bookMoney;
        public System.Decimal BookMoney
        {
            get
            {
                return _bookMoney;
            }
            set
            {
                _bookMoney = value;
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

        private string _identityCardNumber;
        public string IdentityCardNumber
        {
            get
            {
                return _identityCardNumber;
            }
            set
            {
                _identityCardNumber = value;
            }
        }

        private string _identityCardImageFace;
        public string IdentityCardImageFace
        {
            get
            {
                return _identityCardImageFace;
            }
            set
            {
                _identityCardImageFace = value;
            }
        }

        private string _identityCardIamgeRear;
        public string IdentityCardIamgeRear
        {
            get
            {
                return _identityCardIamgeRear;
            }
            set
            {
                _identityCardIamgeRear = value;
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

        private string _typeName;
        public string UserTypeName
        {
            get
            {
                return _typeName;
            }
            set
            {
                _typeName = value;
            }
        }


        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }


        private string _province;
        public string Province
        {
            get
            {
                return _province;
            }
            set
            {
                _province = value;
            }
        }

        private string _ipAddress;
        public string IpAddress
        {
            get 
            {
                return _ipAddress;
            }

            set 
            {
                _ipAddress = value;
            }
        }
    }
}
