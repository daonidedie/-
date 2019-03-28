/******************************************************
* 文件名：Serialize.cs
* 文件功能描述：序列化和反序列化 命名空间 Serialize
* 
* 创建标识：周渊 2008-2-22
* 
* 修改标识：周渊 2008-4-26
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text;   
using System.Runtime.Serialization.Formatters.Binary;

namespace Skybot.Tong.Serialize
{
    #region 序列化和反序列化

    //using Skybot.Serialize;
    //加入序列化類型
    //二進制序列化

    /// <summary>
    /// XML序列化类型
    /// </summary>
    public class SerializeXML
    {
        /// <summary>
        /// 用于序列化的 write 
        /// </summary>
        private System.IO.StringWriter write = new System.IO.StringWriter();

        /// <summary>
        /// 开始定义用于序列化的新实例
        /// </summary>
        private System.Xml.Serialization.XmlSerializer mySerializeXml;

        /// <summary>
        /// 用於資料讀出的資料實例
        /// </summary>
        private System.IO.StringReader sR;

        /// <summary>
        /// 使用XML序列化
        /// </summary>
        /// <param name="type">要傳入的類型的實例</param>
        /// <returns></returns>
        public string SerializeClassToXML(object type)
        {
            mySerializeXml = new System.Xml.Serialization.XmlSerializer(type.GetType()); ;
            mySerializeXml.Serialize(write, type);
            return write.ToString();
        }

        /// <summary>
        /// 類型反序列化
        /// </summary>
        /// <param name="xMLstring">類型序列化後的XML字符串</param>
        /// <param name="type">傳入當前類型的類型</param>
        /// <returns></returns>
        public object SerializeXMLToClass(string xMLstring, Type type)
        {
            sR = new System.IO.StringReader(xMLstring);
            mySerializeXml = new System.Xml.Serialization.XmlSerializer(type);
            return mySerializeXml.Deserialize(sR);
        }
    }

    /// <summary>
    /// 以SOAP格式對對象 序列化的類
    /// </summary>
    public class SerializeSOAP
    {
        /// <summary>
        /// 流對象用於對資料的返回
        /// </summary>
        System.IO.MemoryStream streamMemory;

        /// <summary>
        /// 用SOAP序列化對像的實例操作方法
        /// </summary>
        System.Runtime.Serialization.Formatters.Soap.SoapFormatter mySerializeSOAP;

        /// <summary>
        /// 傳入一個對像把它返回成 一個記憶體的流
        /// </summary>
        /// <param name="objectModel">序列化的對像</param>
        /// <returns></returns>
        public System.IO.MemoryStream SerializeClassToStream(object objectModel)
        {
            //實例化操作的各對象
            streamMemory = new MemoryStream();
            mySerializeSOAP = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
            //開始執行操作
            mySerializeSOAP.Serialize(streamMemory, objectModel);

            return streamMemory;
        }


        /// <summary>
        ///  傳入一個記憶體的流把它返回成 一個對像
        /// </summary>
        /// <param name="streamobj">返序列化的流</param>
        /// <returns></returns>
        public object SerializeStreamToClass(MemoryStream streamobj)
        {
            //用於返回反序列華的類型的結果
            object Serializeobject;
            //實例化操作各對的對像
            mySerializeSOAP = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
            Serializeobject = mySerializeSOAP.Deserialize(streamMemory);
            streamMemory.Close();//關了
            streamMemory.Dispose();//清理

            return Serializeobject;
        }
    }
    #endregion
}