using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserPropInfo
    {
        private int _iD;
        public int ID
        {
            get
            {
                return _iD;
            }
            set
            {
                _iD = value;
            }
        }

        private int _userID;
        public int userID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        private int _propId;
        public int PropId
        {
            get
            {
                return _propId;
            }
            set
            {
                _propId = value;
            }
        }

        private int _userPropNumber;
        public int userPropNumber
        {
            get
            {
                return _userPropNumber;
            }
            set
            {
                _userPropNumber = value;
            }
        }

        private string _propName;
        public string PropName
        {
            get
            {
                return _propName;
            }
            set
            {
                _propName = value;
            }
        }

        private System.Decimal _propPrice;
        public System.Decimal PropPrice
        {
            get
            {
                return _propPrice;
            }
            set
            {
                _propPrice = value;
            }
        }

        private string _propIntroduction;
        public string PropIntroduction
        {
            get
            {
                return _propIntroduction;
            }
            set
            {
                _propIntroduction = value;
            }
        }

        private string _propImage;
        public string PropImage
        {
            get
            {
                return _propImage;
            }
            set
            {
                _propImage = value;
            }
        }

    }
}
