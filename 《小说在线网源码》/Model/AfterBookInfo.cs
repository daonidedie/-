using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AfterBookInfo
    {
        private int _afterBookId;
        public int AfterBookId
        {
            get
            {
                return _afterBookId;
            }
            set
            {
                _afterBookId = value;
            }
        }

        private int _userId;
        public int userId
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

        private DateTime _createTime;
        public DateTime createTime
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


    }
}
