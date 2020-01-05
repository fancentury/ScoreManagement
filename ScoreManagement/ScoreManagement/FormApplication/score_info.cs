using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ScoreManagement.Enity;

namespace ScoreManagement.FormApplication
{
    public partial class score_info : Form
    {
        public score_info()
        {
            InitializeComponent();
        }

        private void score_info_Load(object sender, EventArgs e)
        {
            if (Globel.id_tag.Equals("t"))
            {

            }
            else
            {
                MessageBox.Show("您没有权限操作！", "没有权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            Boolean DBtag = false;
            MySqlConnection sqlCnn = new MySqlConnection(); //创建数据库连接对象
            MySqlCommand sqlCmd1 = new MySqlCommand();       //创建执行sql的对象  
            sqlCnn.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            string Str1 = "select count(*) from user_information where Id = " + textBox1.Text;
            try
            {
                sqlCnn.Open();
                DBtag = true;
                if (DBtag)
                {
                    sqlCmd1.Connection = sqlCnn;
                    sqlCmd1.CommandText = Str1;
                    string count = Convert.ToString(sqlCmd1.ExecuteScalar());// 执行查询
                    if (count.Equals("1"))
                    {

                    }
                    else
                    {
                        MessageBox.Show("该学生不存在，请重新输入");
                        //this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            sqlCnn.Close();
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            Boolean DBtag = false;
            MySqlConnection sqlCnn = new MySqlConnection(); //创建数据库连接对象
            MySqlCommand sqlCmd1 = new MySqlCommand();       //创建执行sql的对象  
            sqlCnn.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            string Str1 = "select count(*) from course_information where Course_Id = " + textBox2.Text;
            try
            {
                sqlCnn.Open();
                DBtag = true;
                if (DBtag)
                {
                    sqlCmd1.Connection = sqlCnn;
                    sqlCmd1.CommandText = Str1;
                    string count = Convert.ToString(sqlCmd1.ExecuteScalar());// 执行查询
                    if (count.Equals("1"))
                    {

                    }
                    else
                    {
                        MessageBox.Show("该课程不存在，请重新输入");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            sqlCnn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ScoreInfo.id = textBox1.Text;
            ScoreInfo.course_id = textBox2.Text;
            ScoreInfo.score = textBox3.Text;
            Boolean DBtag = false;
            MySqlConnection sqlCnn = new MySqlConnection(); //创建数据库连接对象
            MySqlCommand sqlCmd1 = new MySqlCommand();       //创建执行sql的对象  
            MySqlCommand sqlCmd2 = new MySqlCommand();       //创建执行sql的对象  
            sqlCnn.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            string Str1 = "select Course_name from course_information where Course_Id = " + textBox2.Text;
            string Str2 = "select name from user_information where Id = " + textBox1.Text;
            try
            {
                sqlCnn.Open();
                DBtag = true;
                if (DBtag)
                {
                    sqlCmd1.Connection = sqlCnn;
                    sqlCmd1.CommandText = Str1;
                    ScoreInfo.course_name = Convert.ToString(sqlCmd1.ExecuteScalar());// 执行查询
                    sqlCmd2.Connection = sqlCnn;
                    sqlCmd2.CommandText = Str2;
                    ScoreInfo.name = Convert.ToString(sqlCmd2.ExecuteScalar());// 执行查询
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            sqlCnn.Close();
            //提交信息
            DBlink db = new DBlink();   //创建数据库连接对象
            if (db.DBconn())
            { //连接数据库
                string str1 = "insert into score_info(name,Id,course_name,Course_Id,score) " +
                    "values('" + ScoreInfo.name + "','" + ScoreInfo.id + "','" + ScoreInfo.course_name + "','" + ScoreInfo.course_id + "','"
                    + ScoreInfo.score + "');";
                string info = "提交成功！";
                if (db.UpdataDeletAdd(str1))
                {
                    DialogResult dr = MessageBox.Show(info, "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        this.Visible = false;//隐藏当前窗口
                    }
                }
            }

            db.DBclose();//关闭数据连接
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
