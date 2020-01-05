using System;
using System.Data;
using System.Windows.Forms;
using ScoreManagement.Enity;
using MySql.Data.MySqlClient;

namespace ScoreManagement.FormApplication
{
    public partial class User_info_display : Form
    {
        public User_info_display()
        {
            InitializeComponent();
        }
        
        DataSet ds;
        int MaxValue;
        int state;
        int sta;
        private void showInfo(int sta)
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
                    MySqlCommand mySqlCommand = new MySqlCommand("select * from user_information", con);// 创建SqlCommand
                    MySqlDataAdapter dat = new MySqlDataAdapter(mySqlCommand);   // 创建SqlDataAdapter
                    ds = new DataSet();
                    dat.Fill(ds);
                    //开始填充对象
                    string[] hobbies = new string[6];
                    string sex;
                    textBox2.Text = ds.Tables[0].Rows[sta][1].ToString();
                    sex = ds.Tables[0].Rows[sta][2].ToString();
                    if (sex == "男")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    dateTimePicker1.Value = Convert.ToDateTime(ds.Tables[0].Rows[sta][3].ToString());
                    comboBox1.Text = ds.Tables[0].Rows[sta][4].ToString();
                    textBox3.Text = ds.Tables[0].Rows[sta][5].ToString();
                    hobbies = ds.Tables[0].Rows[sta][6].ToString().Split('、');
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                    checkBox6.Checked = false;
                    foreach (string s in hobbies)
                    {
                        switch (s)
                        {
                            case "阅读": checkBox1.Checked = true; break;
                            case "长跑": checkBox2.Checked = true; break;
                            case "编程": checkBox3.Checked = true; break;
                            case "听音乐": checkBox4.Checked = true; break;
                            case "看电影": checkBox5.Checked = true; break;
                            default: checkBox6.Checked = true; break;
                        }
                    }
                    comboBox2.Text = ds.Tables[0].Rows[sta][7].ToString();
                    comboBox3.Text = ds.Tables[0].Rows[sta][8].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }
            
            con.Close();
        }
        //加载页面,通过用户名得到用户名的Id
        private void User_info_display_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;//默认不可以看其他用户信息
            // 设置日历控件选择时间范围，生日不得超过今天
            dateTimePicker1.MinDate = new DateTime(1990, 1, 1);
            dateTimePicker1.MaxDate = DateTime.Today;
            if (Globel.id_tag.Equals("ts"))
            {
                panel1.Visible = true;//如果是教学秘书可以查看其他人的信息
            }
            else
            {            }
            DBlink db = new DBlink();   //创建数据库连接对象
            if (db.DBconn())
            {          //创建数据库连接
                string Str1 = "select Id from login_info WHERE username = '" + Globel.username + "'";
                Globel.Id = db.Getstringsearch(Str1);
                string Str2 = "select name from user_information WHERE Id = '" + Globel.Id + "'";
                if (db.Getstringsearch(Str2).Equals(""))
                {
                    MessageBox.Show("您的个人信息还未录入，请先录入");
                    this.Close();
                }
                else
                {
                    string Str3 = "Select Count(*) from user_information";
                    MaxValue = db.GetIntsearch(Str3);
                    string Str4 = "select number from user_information WHERE Id = " + Globel.Id;
                    int number = db.GetIntsearch(Str4);
                    string Str5 = "SELECT count(*) FROM user_information WHERE number <=" + number;
                    sta = db.GetIntsearch(Str5);
                    showInfo(sta - 1);
                }                
            }
            db.DBclose();   //关闭数据库连接                        
        }
        //更新按钮
        private void button1_Click(object sender, EventArgs e)
        {         
            User_info.name = textBox2.Text;
            if (radioButton1.Checked)
                User_info.sex = radioButton1.Text;
            else
                User_info.sex = radioButton2.Text;
            User_info.birth = dateTimePicker1.Value.Date.ToLongDateString();
            User_info.College_Id = comboBox1.SelectedItem.ToString();
            User_info.Speciality = textBox3.Text;
            User_info.Hobby = "";
            if (checkBox1.Checked) User_info.Hobby += checkBox1.Text;
            if (checkBox2.Checked) User_info.Hobby += "、" + checkBox2.Text;
            if (checkBox3.Checked) User_info.Hobby += "、" + checkBox3.Text;
            if (checkBox4.Checked) User_info.Hobby += "、" + checkBox4.Text;
            if (checkBox5.Checked) User_info.Hobby += "、" + checkBox5.Text;
            if (checkBox6.Checked) User_info.Hobby += "、" + checkBox6.Text;
            User_info.Political_Outlook = comboBox2.SelectedItem.ToString();
            User_info.qualifications = comboBox3.SelectedItem.ToString();
            User_info.id_tag = Globel.id_tag;

            string uptsql;
            uptsql = "update user_information set name='" + User_info.name + "', sex='" + User_info.sex + "', birth='" + User_info.birth + "', College_Id='" + User_info.College_Id +
                "', Speciality='" + User_info.Speciality + "', Hobby='" + User_info.Hobby  + "', Political_Outlook ='" + User_info.Political_Outlook +
                "', qualifications='" + User_info.qualifications + "'where Id=" + Globel.Id;
            DBlink db3 = new DBlink();   //创建数据库连接对象
            if (db3.DBconn())
            { //连接数据库
                string Str1 = "select Id from login_info WHERE username = '" + Globel.username + "'";
                Globel.Id = db3.Getstringsearch(Str1);
                string Str2 = "select name from user_information WHERE Id = '" + Globel.Id + "'";
                if (!db3.Getstringsearch(Str2).Equals(textBox2.Text))
                {
                    MessageBox.Show("您不能修改其他用户信息");
                    this.Close();
                }
                else
                {
                    if (db3.UpdataDeletAdd(uptsql))
                    {
                        MessageBox.Show("用户信息更新成功", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("未更新，请检查！", "无更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } 
            }
            db3.DBclose();//关闭数据连接
            //showInfo(state);
        }
        //删除按钮
        private void button2_Click(object sender, EventArgs e)
        {
            string delsql = "delete from user_information where Id= " + Globel.Id;
            //delete from user_information where Id= 266;
            DBlink db4 = new DBlink();   //创建数据库连接对象
            if (db4.DBconn())
            { //连接数据库
                string Str1 = "select Id from login_info WHERE username = '" + Globel.username + "'";
                Globel.Id = db4.Getstringsearch(Str1);
                string Str2 = "select name from user_information WHERE Id = '" + Globel.Id + "'";
                if (db4.Getstringsearch(Str2).Equals(textBox2.Text))
                {
                    if (db4.UpdataDeletAdd(delsql) && MaxValue > 0)
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
                else
                {
                    MessageBox.Show("您不能删除其他用户信息");
                    this.Close();
                }
                
            }
            db4.DBclose();//关闭数据连接
        }
//注意只有教学秘书可以拥有这项权限
        //上一步按钮    
        private void button3_Click(object sender, EventArgs e)
        {
            if (state <= 0)
            {
                MessageBox.Show("已经是第一个", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                state = state - 1;
                showInfo(state);
            }
        }
        //下一步按钮
        private void button4_Click(object sender, EventArgs e)
        {
            if (state >= MaxValue - 1)
            {
                MessageBox.Show("已经是最后一个", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                state = state + 1;
                showInfo(state);
            }
        }
        //--------------------------------------------------------------窗体退出按钮被点击时---------------------------------------------
        private void User_info_display_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出本页面?", "确认...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
               
            }
        }     
        }
 }
