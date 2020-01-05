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

namespace ScoreManagement
{
    public partial class MainMenu : Form
    {
        //生成注册界面的新窗口并将其隐藏，在管理员点击添加新用户时才使其显示
        regist RG = new regist();
        public MainMenu()
        {
            InitializeComponent();
        }
//--------------------------------------------------------------主菜单初始化---------------------------------------------
        private void MainMenu_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;    //隐藏课程信息
            panel4.Visible = false;    //隐藏成绩信息
            panel5.Visible = false;    //隐藏管理成绩信息
            panel6.Visible = false;    //隐藏查询成绩信息
            RG.Visible = false;             //隐藏注册面板
            stumnue.Visible = false;        //隐藏普通菜单
            adminmnue.Visible = false;      //隐藏管理员菜单
          
            MaximizeBox = false;            //关闭主菜单窗口的最大化功能
            dataGridView2.Visible = false;  //隐藏显示用户信息的dataGridView
            panel2.Visible = false;         //隐藏管理员进行审核操作的面板
            panel1.Visible = false;         //隐藏删除操作的面板
            label3.Text = "";               //提示信息为空
            label5.Text = "";               //提示信息为空
            button1.Enabled = false;        //审核按钮不可点击
            button2.Enabled = false;        //删除用户按钮不可点击

            comboBox1.SelectedIndex = 2;

            //根据登录系统的身份不同，提示不同欢迎语和显示主菜单
            if (LoginInfo.id_tag.Equals("s")){
                this.Text =LoginInfo.username+ "同学您好！欢迎使用学生成绩管理系统。";
                stumnue.Visible = true;     //显示普通菜单
            }
            else if (LoginInfo.id_tag.Equals("t")){
                this.Text = LoginInfo.username + "老师您好！欢迎使用学生成绩管理系统。";
                stumnue.Visible = true;     //显示普通菜单
            }
            else if (LoginInfo.id_tag.Equals("ts"))
            {
                this.Text = LoginInfo.username + "教学秘书您好！欢迎使用学生成绩管理系统。";
                stumnue.Visible = true;     //显示普通菜单
            }
            else if (LoginInfo.id_tag.Equals("a"))
            {
                this.Text = LoginInfo.username + "管理员您好！欢迎使用学生成绩管理系统。";
                adminmnue.Visible = true;   //显示管理员菜单
               
            }           
        }
        DataSet ds;
        private void BindsocreData(string sql)
        {            
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            Boolean DBtag = false;
            try
            {
                con.Open();
                DBtag = true;
                if (DBtag)
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(sql, con);// 创建SqlCommand
                    MySqlDataAdapter dat = new MySqlDataAdapter(mySqlCommand);   // 创建SqlDataAdapter
                    ds = new DataSet();
                    dat.Fill(ds);
                    dataGridView3.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            con.Close();
        }
        private void BindcourseData(string sql)
        {            
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = "server = localhost; uid = root; pwd = 052398; database = student_manage_system;";//连接字符串
            Boolean DBtag = false;
            try
            {
                con.Open();
                DBtag = true;
                if (DBtag)
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(sql, con);// 创建SqlCommand
                    MySqlDataAdapter dat = new MySqlDataAdapter(mySqlCommand);   // 创建SqlDataAdapter
                    ds = new DataSet();
                    dat.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            con.Close();
        }        
        //--------------------------------------------------------------管理员功能---------------------------------------------
        //一、审核管理
        //1.待审核
        //待审核列表
        public void 待审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBlink db = new DBlink();       //创建数据库连接实例
            if (db.DBconn())
            {              //打开数据库连接
                db.GetUserInfoData("select * from login_info where tag=0");
            }
            db.DBclose();//关闭数据库连接
            dataGridView2.Rows.Clear(); //初始化dataGridView
            for (int i = 0; i < LoginInfo.idList.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.dataGridView2.Rows.Add();
                //实现隔行换色
                if (i % 2 == 0)
                {
                    this.dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.Gainsboro;//双数行
                }
                else
                {
                    this.dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.GhostWhite;//单数行
                }
                this.dataGridView2.Rows[index].Cells[0].Value = LoginInfo.idList[i];      //填充编号
                this.dataGridView2.Rows[index].Cells[1].Value = LoginInfo.usernameList[i];//填充用户名
                this.dataGridView2.Rows[index].Cells[2].Value = LoginInfo.passwordList[i];//填充密码
                if (LoginInfo.id_tagList[i].Equals("s"))                                      //根据身份的值填充用户身份
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "学生";
                }
                else if (LoginInfo.id_tagList[i].Equals("t"))
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "教师";
                }
                else if (LoginInfo.id_tagList[i].Equals("ts"))
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "教学秘书";
                }
            }
            //控制显示用户信息数据的dataGridView出现的位置
            dataGridView2.Top = 25;
            dataGridView2.Left = 30;
            dataGridView2.Height = 350;
            dataGridView2.Visible = true;//显示数据集
            //控制操作的出现位置，给控件赋值
            panel2.Top = 400;
            panel2.Left = 30;
            panel2.Visible = true;  //显示审核操作面板
            panel1.Visible = false;     //隐藏删除操作面板
            RG.Visible = false;//隐藏注册窗体
            
        }
        //管理员输入审核用户名时验证该用户是否存在，并提示
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            DBlink db = new DBlink();
            if (db.DBconn())
            {
                string str = "select * from login_info where tag=0 and username='" + textBox1.Text + "'";
                if (db.search(str))
                {
                    label3.Text = "*该用户未审核！";
                    button1.Enabled = true;
                }
                else
                {
                    label3.Text = "*用户不存在！";
                    button1.Enabled = false;
                }
            }
        }
        //管理员审核按钮点击事件
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "update login_info set tag=1 where username='" + textBox1.Text + "'";
            DBlink db = new DBlink();
            if (db.DBconn())
            {
                if (db.UpdataDeletAdd(str))//修改当前用户的tag标志
                {
                    DialogResult dr = MessageBox.Show("用户" + textBox1.Text + "审核成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    待审核ToolStripMenuItem_Click(null, null);//重新加载数据
                    //将用户输入的内容和提示信息文本置空
                    textBox1.Text = "";
                    label3.Text = "";
                }
            }
        }
 //2.已审核
        private void 审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBlink db = new DBlink();       //创建数据库连接实例
            if (db.DBconn())
            {              //打开数据库连接
                db.GetUserInfoData("select * from login_info where tag=1");
            }
            db.DBclose();//关闭数据库连接
            dataGridView2.Rows.Clear(); //初始化dataGridView
            for (int i = 0; i < LoginInfo.idList.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.dataGridView2.Rows.Add();
                //实现隔行换色
                if (i % 2 == 0)
                {
                    this.dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.Gainsboro;//双数行
                }
                else
                {
                    this.dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.GhostWhite;//单数行
                }
                this.dataGridView2.Rows[index].Cells[0].Value = LoginInfo.idList[i];      //填充序号
                this.dataGridView2.Rows[index].Cells[1].Value = LoginInfo.usernameList[i];//填充用户名
                this.dataGridView2.Rows[index].Cells[2].Value = LoginInfo.passwordList[i];//填充密码
                if (LoginInfo.id_tagList[i].Equals("s"))                                      //根据身份的值填充用户身份
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "学生";
                }
                else if (LoginInfo.id_tagList[i].Equals("t"))
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "教师";
                }
                else if (LoginInfo.id_tagList[i].Equals("ts"))
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "教学秘书";
                }
                else
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "管理员";
                }
            }
            //控制显示用户信息数据的dataGridView出现的位置
            dataGridView2.Top = 25;
            dataGridView2.Left = 30;
            dataGridView2.Height = 350;
            dataGridView2.Visible = true;//显示数据集
            panel2.Visible = false;//隐藏操作面板
            panel1.Visible = false;//隐藏删除操作面板
            RG.Visible = false;//隐藏注册窗体
        }
 //二、用户管理
//1.添加用户
        private void 添加用户ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoginInfo.isadmin = true;//将数据实体类中的管理员表示置为true，注册时判断该值执行不同sql
            RG.Visible = true;//显示注册窗体
            //RG.Show();
        } 
//2.删除用户
        private void 删除用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBlink db = new DBlink();       //创建数据库连接实例
            if (db.DBconn())
            {              //打开数据库连接
                db.GetUserInfoData("select * from login_info");
            }
            db.DBclose();//关闭数据库连接
            dataGridView2.Rows.Clear(); //初始化dataGridView
            for (int i = 0; i < LoginInfo.idList.Count; i++)    //循环将数据实体类的数据存放到dataGridView中
            {
                int index = this.dataGridView2.Rows.Add();
                //实现隔行换色
                if (i % 2 == 0)
                {
                    this.dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.Gainsboro;//双数行
                }
                else
                {
                    this.dataGridView2.Rows[index].DefaultCellStyle.BackColor = Color.GhostWhite;//单数行
                }
                this.dataGridView2.Rows[index].Cells[0].Value = LoginInfo.idList[i];      //填充序号
                this.dataGridView2.Rows[index].Cells[1].Value = LoginInfo.usernameList[i];//填充用户名
                this.dataGridView2.Rows[index].Cells[2].Value = LoginInfo.passwordList[i];//填充密码
                if (LoginInfo.id_tagList[i].Equals("s"))                                      //根据SF的值填充用户身份
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "学生";
                }
                else if(LoginInfo.id_tagList[i].Equals("t")){
                    this.dataGridView2.Rows[index].Cells[3].Value = "教师";
                }
                else if (LoginInfo.id_tagList[i].Equals("ts"))
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "教学秘书";
                }
                else
                {
                    this.dataGridView2.Rows[index].Cells[3].Value = "管理员";
                }
            }
            //控制显示用户信息数据的dataGridView出现的位置
            dataGridView2.Top = 25;
            dataGridView2.Left = 30;
            dataGridView2.Height = 350;
            dataGridView2.Visible = true;//显示数据集
            //控制操作的出现位置，给控件赋值
            panel1.Top = 400;
            panel1.Left = 30;
            panel1.Visible = true;  //显示删除操作面板
            panel2.Visible = false; //隐藏审核操作面板
            RG.Visible = false;         //隐藏注册窗体
        }
        //管理员输入审核用户名时验证该用户是否存在，并提示
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            DBlink db = new DBlink();
            if (db.DBconn())
            {
                string str = "select * from login_info where username='" + textBox2.Text + "'";
                if (db.search(str))
                {
                    label5.Text = "*删除用户！" + textBox2.Text+"?请点击删除按钮";
                    button2.Enabled = true;
                }
                else
                {
                    label5.Text = "*用户不存在！";
                    button2.Enabled = false;
                }
            }
        }
        //删除用户的按钮事件
        private void button2_Click(object sender, EventArgs e)
        {

            string str = "delete from login_info where username='" + textBox2.Text + "' and SF!= 'a'";
            DBlink db = new DBlink();
            if (db.DBconn())
            {
                if (db.UpdataDeletAdd(str))//修改当前用户的tag标志
                {
                    DialogResult dr = MessageBox.Show("用户" + textBox1.Text + "删除成功！", "标题", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    删除用户ToolStripMenuItem_Click(null, null);//重新加载数据
                    //将用户输入的内容和提示信息文本置空
                    textBox2.Text = "";
                    label5.Text = "";

                }

            }
        }
//三、退出系统
        private void 退出登录ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
                this.Close();//返回登录界面
        }
        //--------------------------------------------------------------用户功能---------------------------------------------
        //一、成绩管理
        private void 成绩管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = true;    //隐藏成绩与课程信息

            DBlink db = new DBlink();   //创建数据库连接对象
            if (db.DBconn())
            {          //创建数据库连接
                string Str1 = "select Id from login_info WHERE username = '" + Globel.username + "'";
                Globel.Id = db.Getstringsearch(Str1);
            }
            db.DBclose();   //关闭数据库连接
            string sql = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 FROM score_info";
            if (LoginInfo.id_tag.Equals("s"))
            {
                BindsocreData("select score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where Id = '" + Globel.Id + "'");
            }
            else if(LoginInfo.id_tag.Equals("t"))
            {
                BindsocreData(sql);
                panel5.Visible = true;//教师可以管理成绩
                panel6.Visible = true;//教师可以查询成绩
            }
            else if (LoginInfo.id_tag.Equals("ts"))
            {
                BindsocreData(sql);
                panel6.Visible = true;//教学秘书可以查询成绩
            }
        }
        //刷新成绩
        private string selectstr2;
        private void button4_Click(object sender, EventArgs e)
        {
            bool bool3 = false;
            bool bool4 = false;
            bool bool5 = false;
            //学生编号
            if (textBox3.Text == "")
            {           }
            else
            {
                ScoreInfo.id = textBox3.Text;
                bool3 = true;
            }
            //学生姓名
            if (textBox4.Text == "")
            {            }
            else
            {
                ScoreInfo.name = textBox4.Text;
                bool4 = true;
            }
            //课程名
            if (textBox5.Text == "")
            {            }
            else
            {
                ScoreInfo.course_name = textBox5.Text;
                bool5 = true;
            }
            if (bool3 == true && bool4 == true && bool5 == true)
            {
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where name = '" + ScoreInfo.name + "'and Id = '" + ScoreInfo.id + "'and course_name = '" + ScoreInfo.course_name + "'";
            }
            else if (bool3 == true && bool4 == true)
            {
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where name = '" + ScoreInfo.name + "'and Id = '" + ScoreInfo.id + "'";
            }
            else if (bool4 == true && bool5 == true)
            {
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where name = '" + ScoreInfo.name + "'and course_name = '" + ScoreInfo.course_name + "'";
            }
            else if (bool3 == true && bool5 == true)
            {
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where Id = '" + ScoreInfo.id + "'and course_name = '" + ScoreInfo.course_name + "'";
            }
            else if (bool4 == true)
            {
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where name = '" + ScoreInfo.name + "'";
            }
            else if (bool3 == true)
            {
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where Id = '" + ScoreInfo.id + "'";
            }
            else if (bool5 == true)
            {
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info where course_name = '" + ScoreInfo.course_name + "'";
            }
            else
            {
                MessageBox.Show("请至少选择一项来查询");
                selectstr2 = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 from score_info";
            }
            
            panel4.Visible = true;    //隐藏成绩与课程信息        
            BindsocreData(selectstr2);
        }
        //退出
        //删除成绩
        private void button7_Click(object sender, EventArgs e)
        {
            ScoreInfo.id = textBox6.Text;
            ScoreInfo.course_id = textBox7.Text;
            string delsql = "delete from score_info where Id= '" + ScoreInfo.id + "'and Course_Id = '" + ScoreInfo.course_id + "'";
            DBlink db4 = new DBlink();   //创建数据库连接对象
            if (db4.DBconn())
            { //连接数据库
                if (db4.UpdataDeletAdd(delsql))
                {
                    MessageBox.Show("删除记录成功！");
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
            db4.DBclose();//关闭数据连接
            string sql = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 FROM score_info";
            BindsocreData(sql);
        }
        //更新成绩
        private void button6_Click(object sender, EventArgs e)
        {
            ScoreInfo.id = textBox6.Text;
            ScoreInfo.course_id = textBox7.Text;
            ScoreInfo.score = numericUpDown1.Value.ToString();            
            string Str3 = "select count(*) from score_info where Id = '" + ScoreInfo.id + "'and Course_Id = '" + ScoreInfo.course_id + "'";            
            string str5 = "update score_info set score = '" + ScoreInfo.score + "'where Course_Id = '" + ScoreInfo.course_id + "'and Id = '" + ScoreInfo.id + "'";
            DBlink db = new DBlink();   //创建数据库连接对象
            DBlink db4 = new DBlink();   //创建数据库连接对象
            if (db4.DBconn())
            { //连接数据库
                string Str1 = "select Course_name from course_information where Course_Id = " + ScoreInfo.course_id;
                string Str2 = "select name from user_information where Id = " + ScoreInfo.id;
                ScoreInfo.course_name = db4.Getstringsearch(Str1); 
                ScoreInfo.name = db4.Getstringsearch(Str2);
                //MessageBox.Show(ScoreInfo.course_id + ScoreInfo.course_name);
                int count = db4.GetIntsearch(Str3);
                if (count == 0)
                {
                    string str4 = "insert into score_info(name,Id,Course_name,Course_Id,score) " +
                    "values('" + ScoreInfo.name + "','" + ScoreInfo.id + "','" + ScoreInfo.course_name + "','" + ScoreInfo.course_id + "','"
                    + ScoreInfo.score + "');";
                    bool bool6 = db4.UpdataDeletAdd(str4);// 执行查询
                    if (bool6)
                        MessageBox.Show("更新记录成功！");
                    else
                        MessageBox.Show("更新记录失败！");
                }
                else
                {
                    bool bool7 = db4.UpdataDeletAdd(str5);// 执行查询
                    if (bool7)
                        MessageBox.Show("更新记录成功！");
                    else
                        MessageBox.Show("更新记录失败！");
                }
            }
            db4.DBclose();//关闭数据连接
            string sql = "SELECT score_info.Id as 学号, score_info.name as 姓名,score_info.course_name as 课程,score_info.score as 成绩 FROM score_info";
            BindsocreData(sql);
        }
        //退出成绩管理
        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //二、账号管理
        //1.录入信息

        private void 信息录入ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //生成信息录入新窗口
            User_information user_Information = new User_information();
            user_Information.Show();
        }
        //2.信息维护
        private void 信息维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //生成信息录入新窗口
            User_info_display user_info_display = new User_info_display();
            user_info_display.Show();
        }
        //三、课程管理
        private void 添加课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Course_information Course_information = new Course_information();
            Course_information.Show();
        }
        private void 查询课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;    //隐藏成绩与课程信息
            string sql = "SELECT Course_Id as 课程编号,Course_name as 课程名,Course_Type as 课程类别, Obligatory as 课程类型," +
                "credit as 学分,Theoretical_hours as 理论学时,Experimental_hours as 实验学时,Id as 任课教师编号 FROM course_information";
            BindcourseData(sql);
        }
        //刷新
        bool bool1 = false;
        bool bool2 = false;
        private string selectstr;
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {            }
            else
            {
                course_info.course_type = comboBox1.SelectedItem.ToString();
                //MessageBox.Show(comboBox1.SelectedIndex.ToString());
                bool1 = true;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {            }
            else
            {
                course_info.Obligatory = radioButton1.Checked ? "必修" : "选修";
                bool2 = true;
            }

            if (bool1 == true && bool2 == true)
            {
                selectstr = "select Course_Id as 课程编号,Course_name as 课程名,Course_Type as 课程类别, Obligatory as 课程类型," +
                "credit as 学分,Theoretical_hours as 理论学时,Experimental_hours as 实验学时,Id as 任课教师编号 from course_information where Obligatory = '" + course_info.Obligatory + "'and Course_Type = '" + course_info.course_type + "'";
            }
            else if (bool1 == true)
            {
                selectstr = "select Course_Id as 课程编号,Course_name as 课程名,Course_Type as 课程类别, Obligatory as 课程类型," +
                "credit as 学分,Theoretical_hours as 理论学时,Experimental_hours as 实验学时,Id as 任课教师编号 from course_information where Course_Type = '" + course_info.course_type + "'";
            }
            else if (bool2 == true)
            {
                selectstr = "select Course_Id as 课程编号,Course_name as 课程名,Course_Type as 课程类别, Obligatory as 课程类型," +
                "credit as 学分,Theoretical_hours as 理论学时,Experimental_hours as 实验学时,Id as 任课教师编号 from course_information where Obligatory = '" + course_info.Obligatory + "'";
            }
            else
                BindcourseData("select Course_Id as 课程编号, Course_name as 课程名, Course_Type as 课程类别, Obligatory as 课程类型,credit as 学分, Theoretical_hours as 理论学时, Experimental_hours as 实验学时, Id as 任课教师编号 from course_information");


            panel3.Visible = true;    //隐藏成绩与课程信息
            BindcourseData(selectstr);
        }
        //清空课程信息
        private void button5_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            bool1 = false;
            bool2 = false;
            comboBox1.SelectedIndex = 2;
            selectstr = "select Course_Id as 课程编号,Course_name as 课程名,Course_Type as 课程类别, Obligatory as 课程类型," +
                "credit as 学分,Theoretical_hours as 理论学时,Experimental_hours as 实验学时,Id as 任课教师编号 from course_information";
            panel3.Visible = true;    //隐藏成绩与课程信息
            string sql = "SELECT Course_Id as 课程编号,Course_name as 课程名,Course_Type as 课程类别, Obligatory as 课程类型," +
                "credit as 学分,Theoretical_hours as 理论学时,Experimental_hours as 实验学时,Id as 任课教师编号 FROM course_information";
            BindcourseData(sql);
        }        
        private void 课程管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Course_display Course_display = new Course_display();
            Course_display.Show();
        }
        //四、帮助
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }
        //五、退出登录
        private void 退出登录ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//--------------------------------------------------------------窗体退出按钮被点击时---------------------------------------------
        private void MainMenu_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出本系统?", "确认...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void 退出登录ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出本系统?", "确认...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                //e.Cancel = true;
            }
        }
        
    }
}