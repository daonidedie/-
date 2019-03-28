using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skybot.Collections;

namespace Skybot.Collections
{
    /// <summary>
    /// 指定命令行的數據獲取
    /// </summary>
    public class CommandCollection
    {
        //需要處理的數據
        //UpdateTop50SourceFromBaiduCollectSourceBy86zw zw = new UpdateTop50SourceFromBaiduCollectSourceBy86zw();
        UpdateTop50SourceFromBingCollectSourceByXs52 zw = new UpdateTop50SourceFromBingCollectSourceByXs52();
       
        /// <summary>
        /// 从命令行开始进行数据工作
        /// 状态:采集:{0},完成
        /// </summary>
        /// <param name="guid"></param>
        public void Run(string guid)
        {
            zw.Update(guid,"管理器");

            System.Diagnostics.UDPGroup.SendStrGB2312(string.Format("状态:采集完成:{0}", guid));
        
        }

        /// <summary>
        /// 更新前50
        /// </summary>
        public void UpdateTop50(string key)
        {

            System.Diagnostics.UDPGroup.SendStrGB2312(key + "|" + DateTime.Now + "更新前50");
            zw.Update();
            //生成首页
            System.Diagnostics.UDPGroup.SendStrGB2312(key + "|" + DateTime.Now + "生成首页");
            zw.UpdateDefaultPage();
            //生成目录
            System.Diagnostics.UDPGroup.SendStrGB2312(key + "|" + DateTime.Now + "生成目录");
            zw.RunTo50();
            System.Diagnostics.UDPGroup.SendStrGB2312(string.Format("状态:采集完成:{0}", key));
        }


    }
}
