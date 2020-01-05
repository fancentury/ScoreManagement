using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoreManagement.Enity
{
    class User_info
    {
        //保存用户个人信息的数据实体
        public static string name;//用户姓名
        public static string sex;//性别
        public static string birth;        //生日
        public static string College_Id;      //学院编号
        public static string Speciality;    //特长
        public static string Hobby;    //兴趣
        public static string Political_Outlook;    //政治面貌
        public static string qualifications;    //学历
        public static string id_tag;  //身份标记
        public static string id;           //唯一编号
        public static Boolean isadmin = false;      //管理员标记
    }
}
