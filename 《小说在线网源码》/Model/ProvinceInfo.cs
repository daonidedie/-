using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProvinceInfo
    {

        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _provinceID;
        public string provinceID
        {
            get
            {
                return _provinceID;
            }
            set
            {
                _provinceID = value;
            }
        }

        private string _province;
        public string province
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
    }
}
