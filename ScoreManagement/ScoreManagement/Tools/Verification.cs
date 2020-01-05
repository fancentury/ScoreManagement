using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoreManagement.Tools
{
    class Verification
    {
        //随机生成验证码
        public static string Code()
        {
            string code = "";           //验证码字符串
            Random ran = new Random();
            for (int i = 0; i < 6; i++) //随机生成六位验证码功能
            {
                int n = ran.Next(9);
                code += n;
            }
            return code;
        }
    }
}
