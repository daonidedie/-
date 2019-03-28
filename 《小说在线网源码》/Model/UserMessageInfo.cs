using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UserMessageInfo
    {
        private int _messageId;
        public int MessageId
        {
            get
            {
                return _messageId;
            }
            set
            {
                _messageId = value;
            }
        }

        private string _messageTitle;
        public string MessageTitle
        {
            get
            {
                return _messageTitle;
            }
            set
            {
                _messageTitle = value;
            }
        }

        private string _contents;
        public string Contents
        {
            get
            {
                return _contents;
            }
            set
            {
                _contents = value;
            }
        }

        private string _sendDate;
        public string SendDate
        {
            get
            {
                return _sendDate;
            }
            set
            {
                _sendDate = value;
            }
        }

        private int _sendUserId;
        public int SendUserId
        {
            get
            {
                return _sendUserId;
            }
            set
            {
                _sendUserId = value;
            }
        }

        private int _receiveUserId;
        public int ReceiveUserId
        {
            get
            {
                return _receiveUserId;
            }
            set
            {
                _receiveUserId = value;
            }
        }

        private string _sendUserName;
        public string SendUserName
        {
            get
            {
                return _sendUserName;
            }
            set
            {
                _sendUserName = value;
            }
        }

        

    }
}
