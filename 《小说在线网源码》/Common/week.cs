using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Myweeks
    {
        //存放周
        public static int Myweek;

        //登陆用户ID
        public static string userId;

        //存放验证码
        public static string PicValid;

        //数字转大写
        public static string changeNumber(string number)
        {
            string changedString = number;

            changedString = changedString.Replace("0", "零");
            changedString = changedString.Replace("1", "一");
            changedString = changedString.Replace("2", "二");
            changedString = changedString.Replace("3", "三");
            changedString = changedString.Replace("4", "四");
            changedString = changedString.Replace("5", "五");
            changedString = changedString.Replace("6", "六");
            changedString = changedString.Replace("7", "七");
            changedString = changedString.Replace("8", "八");
            changedString = changedString.Replace("9", "九");

            return changedString;
         }

    }
}
