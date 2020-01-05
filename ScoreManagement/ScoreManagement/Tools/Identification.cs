using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoreManagement.Enity;

namespace ScoreManagement.Tools
{
    class Identification
    {
        //创建判断用户身份的工具类
        public static void identification( int temp) { 
            if(temp==0){
                Globel.id_tag = "s";
            }
            else if(temp==1){
                Globel.id_tag = "t";
            }
            else if(temp==2){
                Globel.id_tag = "ts";
            }
            else{
                Globel.id_tag = "a";
            }
        }
    }
}
