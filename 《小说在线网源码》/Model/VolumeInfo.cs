using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VolumeInfo
    {
        private int _volumeId;
        public int VolumeId
        {
            get
            {
                return _volumeId;
            }
            set
            {
                _volumeId = value;
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

        private string _valumeName;
        public string ValumeName
        {
            get
            {
                return _valumeName;
            }
            set
            {
                _valumeName = value;
            }
        }

        private int _valumeNumber;
        public int ValumeNumber
        {
            get
            {
                return _valumeNumber;
            }
            set
            {
                _valumeNumber = value;
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


    }
}
