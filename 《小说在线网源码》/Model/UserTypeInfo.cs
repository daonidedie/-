using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserTypeInfo
    {
        private int _userTypeId;
        public int UserTypeId
        {
            get
            {
                return _userTypeId;
            }
            set
            {
                _userTypeId = value;
            }
        }

        private string _userTypeName;
        public string UserTypeName
        {
            get
            {
                return _userTypeName;
            }
            set
            {
                _userTypeName = value;
            }
        }


    }
}
