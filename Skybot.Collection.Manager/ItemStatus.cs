using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiduMapConvertManager
{

    public class ItemStatus : System.ComponentModel.INotifyPropertyChanged
    {

        /// <summary>
        ///用户标识
        /// </summary>
        public object UserToKen { get; set; }

        /// <summary>
        /// 按钮事件
        /// </summary>
        public Command ButtonCommand { get; set; }


        /// <summary>
        /// 当前的状态
        /// </summary>
        private string _状态 = string.Empty;

        public string 状态
        {
            get { return _状态; }
            set
            {
                _状态 = value;
                OnPropertyChanged("状态");
            }
        }
        /// <summary>
        /// 当前的范围
        /// </summary>
        private string _范围 = string.Empty;

        public string 范围
        {
            get { return _范围; }
            set
            {
                _范围 = value;
                OnPropertyChanged("范围");
            }
        }

        /// <summary>
        /// 当前初始化的进程
        /// </summary>
        public System.Diagnostics.Process CurrentProcess { get; set; }


        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="Name"></param>
        protected void OnPropertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(Name));


            }
        }


        #region INotifyPropertyChanged 成员

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    /// <summary>
    ///  命令
    /// </summary>
    public class Command : System.Windows.Input.ICommand
    {
        /// <summary>
        /// 进程
        /// </summary>
        public Action Action { get; set; }

        #region ICommand 成员

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (Action != null)
            {
                Action.Invoke();
            }
        }

        #endregion
    }

}
