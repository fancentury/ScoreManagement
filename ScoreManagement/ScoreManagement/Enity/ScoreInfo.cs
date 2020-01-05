using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections; 

namespace ScoreManagement.Enity
{
    class ScoreInfo
    {
        //创建实体
        public static string course_id;//课程编号
        public static string id;//学号
        public static string score;        //分数
        public static string course_name;      //课程名
        public static string name;    //名字
        //创建list存储查询的结果集
        public static ArrayList idList = new ArrayList();   //存储学号
        public static ArrayList nameList = new ArrayList();   //存储学生姓名        
        public static ArrayList scoreList = new ArrayList();//存储成绩表的成绩
        public static ArrayList course_idList = new ArrayList();//课程编号
        public static ArrayList course_nameList = new ArrayList();//课程名称
    }
}
