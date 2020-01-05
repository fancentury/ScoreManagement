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
using ScoreManagement.FormApplication;

namespace ScoreManagement.FormApplication
{
    public partial class Course_information : Form
    {
        bool bool1 = false;
        bool bool2 = false;
        public Course_information()
        {
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            DBlink db3 = new DBlink();   //创建数据库连接对象
            if (db3.DBconn())
            { //连接数据库
                string Str1 = "select count(*) from course_information where Course_Id = " + textBox1.Text;
                string count = db3.Getstringsearch(Str1);
                if (count.Equals("1"))
                {
                    MessageBox.Show("该课程信息已经录入，请移步信息维护界面");
                    this.Close();
                }
                else
                    bool1 = true;
            }
            db3.DBclose();//关闭数据连接           
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            DBlink db3 = new DBlink();   //创建数据库连接对象
            if (db3.DBconn())
            { //连接数据库
                string Str1 = "select count(*) from course_information where Id = " + textBox3.Text;
                string count = db3.Getstringsearch(Str1);
                if (count.Equals("0"))
                {
                    MessageBox.Show("该老师不存在，请重新输入");
                }
                else
                    bool2 = true;
            }
            db3.DBclose();//关闭数据连接  
        }
                       
        //更换字体
        private void button1_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = richTextBox1.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
            }

        }
        //更换颜色
        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = richTextBox1.ForeColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog1.Color;
            }

        }
        //确定
        private void button3_Click(object sender, EventArgs e)
        {
            DBlink db = new DBlink();   //创建数据库连接对象
            if (db.DBconn())
            { //连接数据库
                string str1 = "insert into course_information(Course_Id,course_name,course_type,Obligatory,credit,Theoretical_hours,Experimental_hours,id) " +
                    "values('" + course_info.course_id + "','" + course_info.course_name + "','" + course_info.course_type + "','" + course_info.Obligatory + "','" 
                    + course_info.credit + "','" + course_info.Theoretical_hours + "','"
                    + course_info.Experimental_hours + "','"  + course_info.id + "');";
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

        private void button4_Click(object sender, EventArgs e)
        {
            //所有信息都填完整后方可进入确认信息页面
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedIndex < 0 || (radioButton1.Checked == false && radioButton2.Checked == false) ||
                domainUpDown1.Text == "0" || domainUpDown2.Text == "0" || domainUpDown3.Text == "0" || textBox3.Text == "" || bool1 == false || bool2 == false)
            {
                MessageBox.Show("输入信息不完整或不正确！", "信息不完整", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                tabControl1.SelectedTab = tabPage2;//显示“确认信息”选项卡
            }           
        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //所有信息都填完整后方可进入确认信息页面
            if (tabControl1.SelectedIndex == 1)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedIndex < 0 || (radioButton1.Checked == false && radioButton2.Checked == false) ||
                domainUpDown1.Text == "0" || domainUpDown2.Text == "0" || domainUpDown3.Text == "0" || textBox3.Text == "" || bool1 == false || bool2 == false)
                {
                    MessageBox.Show("输入信息不完整或不正确！", "信息不完整", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tabControl1.SelectedTab = tabPage1;
                }
                else
                {
                    course_info.course_id = textBox1.Text;
                    course_info.course_name = textBox2.Text;
                    course_info.course_type = comboBox1.SelectedItem.ToString();
                    course_info.Obligatory = radioButton1.Checked ? "必修" : "选修";
                    course_info.credit = domainUpDown1.Text;
                    course_info.Theoretical_hours = domainUpDown2.Text;
                    course_info.Experimental_hours = domainUpDown3.Text;
                    course_info.id = textBox3.Text;
                    string message = String.Format("课程编号:{0}\n课程名称:{1}\n课程类别:{2}\n课程性质:{3}\n学分:{4}\n理论学时:{5}\n实验学时:{6}\n任课教师:{7}" +
                        "", course_info.course_id, course_info.course_name, course_info.course_type, course_info.Obligatory, course_info.credit, course_info.Theoretical_hours,
                        course_info.Experimental_hours, course_info.id);
                    richTextBox1.Text = message;
                }
            }
        }
        
        
        private void Course_information_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;//设置下拉菜单的默认选项 
            if (Globel.id_tag.Equals("ts"))
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