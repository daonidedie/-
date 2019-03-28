using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BookStateInfo
    {
        private int _stateId;
        public int StateId
        {
            get
            {
                return _stateId;
            }
            set
            {
                _stateId = value;
            }
        }

        private string _stateName;
        public string StateName
        {
            get
            {
                return _stateName;
            }
            set
            {
                _stateName = value;
            }
        }


    }
}
