using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AreaInfo
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

        private string _cityID;
        public string cityID
        {
            get
            {
                return _cityID;
            }
            set
            {
                _cityID = value;
            }
        }

        private string _city;
        public string city
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

        private string _father;
        public string father
        {
            get
            {
                return _father;
            }
            set
            {
                _father = value;
            }
        }


    }
}
