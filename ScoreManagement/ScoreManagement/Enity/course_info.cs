using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoreManagement.Enity
{
    class course_info
    {
        public static string course_id;//课程编号
        public static string course_name;//课程名称
        public static string course_type;        //课程类别
        public static string Obligatory;      //必修
        public static string credit;    //学分
        public static string Theoretical_hours;    //理论学时
        public static string Experimental_hours;    //实验学时
        public static string id;    //任课教师

        public static ArrayList course_idList = new ArrayList();//课程编号
        public static ArrayList course_nameList = new ArrayList();//课程名称
        public static ArrayList course_typeList = new ArrayList();  //课程类别
        public static ArrayList ObligatoryList = new ArrayList();      //必修
        public static ArrayList creditList = new ArrayList();//学分
        public static ArrayList Theoretical_hoursList = new ArrayList();//理论学时
        public static ArrayList Experimental_hoursList = new ArrayList();  //实验学时
        public static ArrayList idList = new ArrayList();      //任课教师
    }
}
