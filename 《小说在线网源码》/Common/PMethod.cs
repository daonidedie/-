using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class PMethod
    {
        //返回非负随机数
        public static int GetRand()
        {
            Random r = new Random();
            return r.Next();
        }

        //返回不大于max的非负随机数
        public static int GetRand(int max)
        {
            Random r = new Random();
            return r.Next(max);
        }
        
        //返回不小于min不大于max的非负随机数
        public static int GetRand(int min, int max)
        {
            Random r = new Random();
            return r.Next(min, max);
        }
    }
}
