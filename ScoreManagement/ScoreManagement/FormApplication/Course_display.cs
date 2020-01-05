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
    public partial class Course_display : Form
    {
        public Course_display()
        {
            InitializeComponent();
        }
        int sta;
        //显示课程信息
        private void showInfo(int sta)
        {
            panel1.Visible = true;
            DataSet ds;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            Boolean DBtag = false;
            try
            {
                con.Open();
                DBtag = true;
                if (DBtag)
                {
                    MySqlCommand mySqlCommand = new MySqlCommand("select * from course_information", con);// 创建SqlCommand
                    MySqlDataAdapter dat = new MySqlDataAdapter(mySqlCommand);   // 创建SqlDataAdapter
                    ds = new DataSet();
                    dat.Fill(ds);
                    //开始填充对象
                    string Obligatory;
                    textBox5.Text = ds.Tables[0].Rows[sta][0].ToString();
                    textBox6.Text = ds.Tables[0].Rows[sta][1].ToString();
                    Obligatory = ds.Tables[0].Rows[sta][3].ToString();
                    if (Obligatory == "必修")
                        radioButton3.Checked = true;
                    else
                        radioButton4.Checked = true;
                    comboBox2.Text = ds.Tables[0].Rows[sta][2].ToString();
                    domainUpDown4.Text = ds.Tables[0].Rows[sta][4].ToString();
                    domainUpDown5.Text = ds.Tables[0].Rows[sta][5].ToString();
                    domainUpDown6.Text = ds.Tables[0].Rows[sta][6].ToString();
                    textBox7.Text = ds.Tables[0].Rows[sta][7].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
                // Console.WriteLine(ex.Message);
                //Console.WriteLine("数据库连接失败");
            }

            con.Close();
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            DBlink db3 = new DBlink();   //创建数据库连接对象
            if (db3.DBconn())
            { //连接数据库
                string Str1 = "select count(*) from course_information where Id = " + textBox7.Text;
                string count = db3.Getstringsearch(Str1);// 执行查询
                if (count.Equals("0"))
                {
                    MessageBox.Show("该老师不存在，请重新输入");
                }
            }
            db3.DBclose();//关闭数据连接             
        }
        private void Course_display_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;//未输入正确的课程编号情况下不显示信息 
            if (Globel.id_tag.Equals("ts"))
            {

            }
            else
            {
                MessageBox.Show("您没有权限操作！", "没有权限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            DBlink db3 = new DBlink();   //创建数据库连接对象
            if (db3.DBconn())
            { //连接数据库
                string Str1 = "select COUNT(*) from course_information WHERE Course_Id = " + textBox4.Text;
                string count = db3.Getstringsearch(Str1);// 执行查询
                string Str2 = "select number from course_information WHERE Course_Id = " + textBox4.Text;
                int counter = db3.GetIntsearch(Str2);
                if (count.Equals("0"))
                {
                    MessageBox.Show("抱歉未查询到该课程信息");
                    this.Close();
                }
                else if (count.Equals("1"))
                {
                    string Str4 = "SELECT count(*) FROM course_information WHERE number <= " + counter;
                    sta = db3.GetIntsearch(Str4);
                    showInfo(sta - 1);
                }
            }
            db3.DBclose();//关闭数据连接            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            course_info.course_id = textBox5.Text;
            course_info.course_name = textBox6.Text;
            course_info.course_type = comboBox2.SelectedItem.ToString();
            course_info.Obligatory = radioButton3.Checked ? "必修" : "选修";
            course_info.credit = domainUpDown4.Text;
            course_info.Theoretical_hours = domainUpDown5.Text;
            course_info.Experimental_hours = domainUpDown6.Text;
            course_info.id = textBox7.Text;

            string uptsql;
            uptsql = "update course_information set course_name='" + course_info.course_name + "', course_type='" + course_info.course_type + "', Obligatory='" + course_info.Obligatory +
                "', credit='" + course_info.credit + "', Theoretical_hours='" + course_info.Theoretical_hours + "', Experimental_hours ='" + course_info.Experimental_hours +
                "', Id='" + course_info.id + "'where Course_Id=" + course_info.course_id;
            DBlink db3 = new DBlink();   //创建数据库连接对象
            if (db3.DBconn())
            { //连接数据库
                if (db3.UpdataDeletAdd(uptsql))
                {
                    MessageBox.Show("课程信息更新成功", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("未更新，请检查！", "无更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            db3.DBclose();//关闭数据连接
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            course_info.course_id = textBox5.Text;
            course_info.course_name = textBox6.Text;
            course_info.course_type = comboBox2.SelectedItem.ToString();
            course_info.Obligatory = radioButton3.Checked ? "必修" : "选修";
            course_info.credit = domainUpDown4.Text;
            course_info.Theoretical_hours = domainUpDown5.Text;
            course_info.Experimental_hours = domainUpDown6.Text;
            course_info.id = textBox7.Text;
            string delsql = "delete from course_information where Course_Id= " + course_info.course_id;
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
    }
}
