using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BallotInfo
    {
        private int _ballotId;
        public int ballotId
        {
            get
            {
                return _ballotId;
            }
            set
            {
                _ballotId = value;
            }
        }

        private int _bookId;
        public int bookId
        {
            get
            {
                return _bookId;
            }
            set
            {
                _bookId = value;
            }
        }

        private int _ballotNumber;
        public int ballotNumber
        {
            get
            {
                return _ballotNumber;
            }
            set
            {
                _ballotNumber = value;
            }
        }


    }
}
