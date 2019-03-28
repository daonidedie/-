using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 采集应用程序.Capability;
using System.Diagnostics;
using System.Reflection;

namespace 采集应用程序
{
    public class MainWindowModel : System.ComponentModel.INotifyPropertyChanged
    {

        #region 实现 INotifyPropertyChanged
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 注册属性发生改变的事件
        /// </summary>
        /// <param name="PropertyName"></param>
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(PropertyName));
                }
                catch (Exception ex)
                {
                    //打印状态
                    System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + "更新界面内存数据出现问题：" + ex.Message + (ex.StackTrace == null ? " " : " " + ex.StackTrace));

                }
            }


        }
        #endregion
        /// <summary>
        /// 创建一个新的Model对像
        /// </summary>
        public MainWindowModel()
        {
            timer.Interval = System.TimeSpan.FromSeconds(2*60);
            timer.Tick += (s, e) => {
                OnPropertyChanged("Memory");
            };
           // timer.Start();
        }


       ///// <summary>
       ///// 更新86zw 的索引
       ///// </summary>
       // private UpdateBookIndexFrom86zw_com _BookIndexUpdate = new UpdateBookIndexFrom86zw_com();

       // /// <summary>
       // /// 更新86zw 的索引
       // /// </summary>
       // public UpdateBookIndexFrom86zw_com BookIndexUpdate
       // {
       //     get { return _BookIndexUpdate; }
       // }


        /// <summary>
        /// 更新xs52 的索引
        /// </summary>
        private UpdateBookIndexFromxs52_com _BookIndexUpdate = new UpdateBookIndexFromxs52_com();

        /// <summary>
        /// 更新86zw 的索引
        /// </summary>
        public UpdateBookIndexFromxs52_com BookIndexUpdate
        {
            get { return _BookIndexUpdate; }
        }
        ///// <summary>
        ///// 更新dawenxue 的索引
        ///// </summary>
        //private UpdateBookIndexFromdawenxue_org _BookIndexUpdate = new UpdateBookIndexFromdawenxue_org();

        ///// <summary>
        ///// 更新86zw 的索引
        ///// </summary>
        //public UpdateBookIndexFromdawenxue_org BookIndexUpdate
        //{
        //    get { return _BookIndexUpdate; }
        //}
       /// <summary>
       /// 更新搜索最多的前50个小说章节
       /// </summary>
      // private Skybot.Collections.UpdateTop50SourceFromBaiduCollectSourceBy86zw _UpdateTop50 = new Skybot.Collections.UpdateTop50SourceFromBaiduCollectSourceBy86zw();

      ///// <summary>
      // ///   更新搜索最多的前50个小说章节
      ///// </summary>
      // public Skybot.Collections.UpdateTop50SourceFromBaiduCollectSourceBy86zw UpdateTop50
      // {
      //     get { return _UpdateTop50; }
      // }
        private Skybot.Collections.UpdateTop50SourceFromBingCollectSourceByXs52 _UpdateTop50 = new Skybot.Collections.UpdateTop50SourceFromBingCollectSourceByXs52();

      /// <summary>
       ///   更新搜索最多的前50个小说章节
      /// </summary>
        public Skybot.Collections.UpdateTop50SourceFromBingCollectSourceByXs52 UpdateTop50
       {
           get { return _UpdateTop50; }
       }
        /// <summary>
        /// 定时器
        /// </summary>
       private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

       PropertyInfo[] proinfos = typeof(ProcessThread).GetProperties();

        /// <summary>
        /// 可以使用的最大内存
        /// </summary>
       protected const int MaxMemonyUse=1024;

       public double Memory {

           get
           {
               //进程
               System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
               double memory = (double)(proc.PeakWorkingSet64) / 1024 / 1024;
              //如果内存大于1GB
               if (memory > MaxMemonyUse)
               {
                   System.Diagnostics.UDPGroup.SendStrGB2312(string.Format("当前时间:{0},内存使用超过限制{1}MB进程无限停止", DateTime.Now, MaxMemonyUse));
                  // proc.WaitForExit();
                   
               }
               
               return memory;
               
           }
       }
    }
}
