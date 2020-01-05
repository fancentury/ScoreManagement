using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Forms;
using ScoreManagement.Enity;

namespace ScoreManagement
{
    //创建数据库连接的工具类
    class DBlink
    {
        MySqlConnection sqlCnn = new MySqlConnection(); //创建数据库连接对象
        MySqlCommand sqlCmd = new MySqlCommand();       //创建执行sql的对象
        //LoginInfo lgI = new LoginInfo();    //创建登录数据的实例
        public Boolean DBtag=false;
        //创建数据库连接
        public Boolean DBconn()
        {
            sqlCnn.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            sqlCmd.Connection = sqlCnn;
            try
            {
                sqlCnn.Open();
                DBtag = true;
                if (DBtag) {
                    //Console.WriteLine("数据库连接成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            finally
            {
                //textBox2.AppendText("数据库连接关闭！");
                
            }
            return DBtag;
        }
       
        //执行查询的sql
        public Boolean search(String str)
        {
            sqlCmd.CommandText = str;
            Boolean tag = false;
            MySqlDataReader rec = sqlCmd.ExecuteReader();//创建结果集
            while (rec.Read())
            {
                tag = true;
            }
            return tag;
        }
        //执行增删改的sql
        public Boolean UpdataDeletAdd(string str)
        {
            sqlCmd.CommandText = str;
            return sqlCmd.ExecuteNonQuery()>0;  
        }
        public int GetIntsearch(string str)
        {
            sqlCmd.CommandText = str;
            return Convert.ToInt32(sqlCmd.ExecuteScalar());
        }
        public string Getstringsearch(string str)
        {
            sqlCmd.CommandText = str;
            return Convert.ToString(sqlCmd.ExecuteScalar());
        }
        //关闭数据库连接
        public void DBclose()
        {
            sqlCnn.Close();
        }
        //获取login所需要的数据
        public Boolean GetLoginData(string str) {
            sqlCmd.CommandText = str;
            MySqlDataReader rec = sqlCmd.ExecuteReader();//创建结果集
            while (rec.Read())  //查询到数据结果
            {
                LoginInfo.id = rec.GetString(0);      //将学号存放在数据实体类
                LoginInfo.username = rec.GetString(1);  //将用户名存放到数据实体类
                LoginInfo.password = rec.GetString(2);  //将密码存放在数据实体类
                LoginInfo.id_tag = rec.GetString(3);      //将身份标记存放到数据实体类
                LoginInfo.tag = rec.GetString(4);     //将审核标记存放在数据实体类           
                Console.WriteLine("查询到的信息：用户名{0},密码{1},身份标记{2},审核标记{3}", LoginInfo.username, LoginInfo.password, LoginInfo.id_tag, LoginInfo.tag);
                return true;    //查询到用户数据
            }
            return false;
        }        
        //获取用户信息表中的数据
        public Boolean GetUserInfoData(string str)
        {
            sqlCmd.CommandText = str;
            Boolean tag = false;
            MySqlDataReader rec = sqlCmd.ExecuteReader();//创建结果集
            //初始化数据
            LoginInfo.idList.Clear();
            LoginInfo.usernameList.Clear();
            LoginInfo.passwordList.Clear();
            LoginInfo.id_tagList.Clear();
            while (rec.Read())  //查询到数据结果
            {
                LoginInfo.idList.Add(rec.GetString(0));     //将查询到的id存储到数据实体类
                LoginInfo.usernameList.Add(rec.GetString(1));//将查询到的用户名存储到数据实体类
                LoginInfo.passwordList.Add(rec.GetString(2));//将查询到的密码存储到数据实体类
                LoginInfo.id_tagList.Add(rec.GetString(3));//将查询到的身份存储到数据实体类
                tag = true;    //查询到用户数据
            }
            return tag;
        }
    }
}
