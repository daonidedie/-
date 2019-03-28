using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BookReplayInfo
    {
        public string UserTypeName { get; set; }
        public string UsetImage { get; set; }

        private int _replayId;
        public int ReplayId
        {
            get
            {
                return _replayId;
            }
            set
            {
                _replayId = value;
            }
        }

        private string _replayContext;
        public string ReplayContext
        {
            get
            {
                return _replayContext;
            }
            set
            {
                _replayContext = value;
            }
        }

        private string _replayTime;
        public string ReplayTime
        {
            get
            {
                return _replayTime;
            }
            set
            {
                _replayTime = value;
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

        private int _replayBookId;
        public int ReplayBookId
        {
            get
            {
                return _replayBookId;
            }
            set
            {
                _replayBookId = value;
            }
        }


        private string _bookName;
        public string BookName
        {
            get
            {
                return _bookName;
            }
            set
            {
                _bookName = value;
            }
        }

        private string _bookState;
        public string StateName
        {
            get
            {
                return _bookState;
            }
            set
            {
                _bookState = value;
            }
        }

        private string _bookType;
        public string TypeName
        {
            get
            {
                return _bookType;
            }
            set
            {
                _bookType = value;
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

        private string _images;
        public string Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
            }
        }

    }
}
