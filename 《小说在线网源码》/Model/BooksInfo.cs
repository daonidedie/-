using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BooksInfo
    {
        public int SectiuonId { get; set; }
        public string SectionTitle { get; set; }
        public string Contents { get; set; }
        public string ValumeName { get; set; }
        public int VolumeId { get; set; }

        public int TypeId { get; set; }
        private int _bookId;
        public int BookId
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

        private int _authorId;
        public int AuthorId
        {
            get
            {
                return _authorId;
            }
            set
            {
                _authorId = value;
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

        private string _bookIntroduction;
        public string BookIntroduction
        {
            get
            {
                return _bookIntroduction;
            }
            set
            {
                _bookIntroduction = value;
            }
        }

        private DateTime _addTime;
        public DateTime AddTime
        {
            get
            {
                return _addTime;
            }
            set
            {
                _addTime = value;
            }
        }

        private int _bookState;
        public int BookState
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

        private int _bookType;
        public int BookType
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

        private int _recommand;
        public int Recommand
        {
            get
            {
                return _recommand;
            }
            set
            {
                _recommand = value;
            }
        }

        private int _ticketNumber;
        public int ticketNumber
        {
            get
            {
                return _ticketNumber;
            }
            set
            {
                _ticketNumber = value;
            }
        }

        private int _flowerNumber;
        public int flowerNumber
        {
            get
            {
                return _flowerNumber;
            }
            set
            {
                _flowerNumber = value;
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

        private string _typeName;
        public string TypeName
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

        private string _allsumClick;
        public string AllSumClick
        {
            get
            {
                return _allsumClick;
            }

            set
            {
                _allsumClick = value;
            }
        }

        private string _addTimeString;
        public string AddTimeString
        {
            get
            {
                return _addTimeString;
            }
            set
            {
                _addTimeString = value;
            }
        }

        private int _weeks;
        public int Weeks
        {
            get{
                return _weeks;
            }
            set{
                _weeks = value;
            }
        }


        private string _lastSection;
        public string LastSection
        {
            get
            {
                return _lastSection;
            }
            set
            {
                _lastSection = value;
            }
        }

    }
}
