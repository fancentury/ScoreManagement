using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections; 

namespace ScoreManagement.Enity
{
    class LoginInfo
    {
        //保存用户登录信息的数据实体
        public static string username;//用户名
        public static string password;//密码
        public static string tag;        //审核标记
        public static string id_tag;      //身份标记
        public static string id;           //唯一编号
        public static Boolean isadmin=false;      //管理员标记
        //创建list存储查询的用户信息结果集
        public static ArrayList usernameList = new ArrayList();//用户名
        public static ArrayList passwordList = new ArrayList();//登录密码
        public static ArrayList id_tagList = new ArrayList();  //用户身份
        public static ArrayList idList = new ArrayList();      //用户id
    }
}
