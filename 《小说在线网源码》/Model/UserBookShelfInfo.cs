using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Model
{
    public class UserBookShelfInfo
    {
        private int _bookShelfId;
        public int BookShelfId
        {
            get
            {
                return _bookShelfId;
            }
            set
            {
                _bookShelfId = value;
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

        private string _images;
        public string Images
        {
            get {
                return _images;
            }

            set {
                _images = value;
            }
        }

    }
}
