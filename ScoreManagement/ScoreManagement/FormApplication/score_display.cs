using MySql.Data.MySqlClient;
using ScoreManagement.Enity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScoreManagement.FormApplication
{
    public partial class score_display : Form
    {
        public score_display()
        {
            InitializeComponent();
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
            MySqlCommand sqlCmd3 = new MySqlCommand();       //创建执行sql的对象  
            MySqlCommand sqlCmd4 = new MySqlCommand();       //创建执行sql的对象 
            MySqlCommand sqlCmd5 = new MySqlCommand();       //创建执行sql的对象 
            sqlCnn.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            string Str1 = "select Course_name from course_information where Course_Id = " + textBox2.Text;
            string Str2 = "select name from user_information where Id = " + textBox1.Text;
            string str3 = "select count(*) from score_info where Id = '" + ScoreInfo.id + "'and Course_Id = '" + ScoreInfo.course_id + "'";
            string str4 = "insert into score_info(Id,Course_Id,score) " +
                    "values('" + ScoreInfo.id + "','" + ScoreInfo.course_id + "','"
                    + ScoreInfo.score + "');";
            string str5 = "update score_info set score = '" + ScoreInfo.score + "'where Course_Id = '" + ScoreInfo.course_id + "'and Id = '" + ScoreInfo.id + "'";
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
                    sqlCmd3.Connection = sqlCnn;
                    sqlCmd3.CommandText = str3;
                    int count = Convert.ToInt32(sqlCmd3.ExecuteScalar());// 执行查询
                    //MessageBox.Show(count.ToString());
                    if (count < 0)
                    {
                        sqlCmd4.Connection = sqlCnn;
                        sqlCmd4.CommandText = str4;
                        int booll = sqlCmd4.ExecuteNonQuery();// 执行查询
                        if (booll > 0)
                            MessageBox.Show("修改记录成功！");
                        else
                            MessageBox.Show("修改记录失败！");
                    }
                    else
                    {
                        sqlCmd5.Connection = sqlCnn;
                        sqlCmd5.CommandText = str5;
                        int bool2 = sqlCmd5.ExecuteNonQuery();// 执行查询
                        if (bool2 > 0)
                            MessageBox.Show("修改记录成功！");
                        else
                            MessageBox.Show("修改记录失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            sqlCnn.Close();
            //提交信息
            /*DBlink db = new DBlink();   //创建数据库连接对象
            if (db.DBconn())
            { //连接数据库
                string str1 = "update score set Id = '" + ScoreInfo.id + "',Course_Id = '" + ScoreInfo.course_id + "', score = '" + ScoreInfo.score + "'";               
                string info = "修改成功！";
                if (db.UpdataDeletAdd(str1))
                {
                    DialogResult dr = MessageBox.Show(info, "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        this.Visible = false;//隐藏当前窗口
                    }
                }
            }

            db.DBclose();//关闭数据连接*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ScoreInfo.id = textBox1.Text;
            ScoreInfo.course_id = textBox2.Text;
            string delsql = "delete from score_info where Id= '" + ScoreInfo.id + "'and Course_Id = '" + ScoreInfo.course_id + "'";
            //delete from user_information where Id= 266;
            DBlink db4 = new DBlink();   //创建数据库连接对象
            if (db4.DBconn())
            { //连接数据库
                if (db4.UpdataDeletAdd(delsql))
                {
                    MessageBox.Show("删除记录成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                    this.Close();
                }
            }
            db4.DBclose();//关闭数据连接
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void score_display_Load(object sender, EventArgs e)
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
    }
}
