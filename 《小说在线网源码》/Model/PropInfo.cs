using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PropInfo
    {
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

        private string _createDate;
        public string createDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }

        private int _outNumber;
        public int outNumber
        {
            get
            {
                return _outNumber;
            }
            set
            {
                _outNumber = value;
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
