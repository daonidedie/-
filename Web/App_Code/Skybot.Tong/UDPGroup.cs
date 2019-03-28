using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Diagnostics
{
    /// <summary>
    /// Socket 广播
    /// </summary>
    public class SocketBroadcast
    { 
        /// <summary>
       /// UDPSocket
       /// </summary>
       private System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);

       public SocketBroadcast()
       {
           //System.Net.IPAddress.Parse("239.1.226.1")
           //可以发送广播数据
           socket.SetSocketOption(Net.Sockets.SocketOptionLevel.Socket, Net.Sockets.SocketOptionName.Broadcast, true);
       }

       /// <summary>
       /// 组播地址与端口
       /// </summary>
       public System.Net.EndPoint ep = new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, 46950);

       /// <summary>
       /// 发送到数据中
       /// </summary>
       /// <param name="str"></param>
       public  void SendStrGB2312(string str)
       {

           socket.SendTo(System.Text.Encoding.GetEncoding("GB2312").GetBytes(str), ep);
       }
    }


    public class UdpGroupSend
    {
        /// <summary>
        /// 组播地址与端口
        /// </summary>
        public System.Net.IPEndPoint ep = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(System.Configuration.ConfigurationManager.AppSettings["状态信息组播"]), int.Parse(System.Configuration.ConfigurationManager.AppSettings["端口"].ToString()));
        System.Net.Sockets.UdpClient udpClient = new Net.Sockets.UdpClient();

        /// <summary>
        /// 发送到数据中
        /// </summary>
        /// <param name="str"></param>
        public void SendStrGB2312(string str)
        {
            System.Diagnostics.Debug.WriteLine(str);
            byte[] buff = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str);
            udpClient.Send(buff, buff.Length, ep);

        }
    }

    /// <summary>
    /// UDP组播
    /// </summary>
   public class UDPGroup
    {



       //public static SocketBroadcast group = null;
public static UdpGroupSend group =new  UdpGroupSend();

       /// <summary>
       /// 发送到数据中
       /// </summary>
       /// <param name="str"></param>
       public static void SendStrGB2312(string str)
       {

           System.Console.WriteLine(str);
           group.SendStrGB2312(str);
       }
   }
}
