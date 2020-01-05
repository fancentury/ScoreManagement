using MySql.Data.MySqlClient;
using ScoreManagement.Enity;
using System;
using System.Windows.Forms;

namespace ScoreManagement.FormApplication
{
    //用户信息输入页面
    public partial class User_information : Form
    {
        bool bool1 = false;
        public User_information()
        {
            InitializeComponent();
        }
        //页面初始化
        private void User_information_entry_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;        //关闭注册窗口的最大化功能
            chooseCollege.SelectedIndex = 0;//设置下拉菜单的默认选项 
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            // 设置日历控件选择时间范围，生日不得超过今天
            dateTimePicker1.MinDate = new DateTime(1990, 1, 1);
            dateTimePicker1.MaxDate = DateTime.Today;
        }
        //执行检查
        private void textBox1_Leave(object sender, EventArgs e)
        {
            DBlink db = new DBlink();   //创建数据库连接对象
            if (db.DBconn())
            { //连接数据库
                string Str1 = "select count(*) from user_information where Id = " + textBox1.Text;
                string count = db.Getstringsearch(Str1);
                string Str2 = "select Id from login_info where username ='" + LoginInfo.username + "'";
                Globel.Id = db.Getstringsearch(Str2);
                if (count.Equals("1"))
                {
                    MessageBox.Show("您的信息已经录入，请移步信息维护界面");
                    this.Close();
                }
                else if (!Globel.Id.Equals(textBox1.Text))
                {
                    MessageBox.Show("您的学号输入错误，请核对");
                }
                else if (Globel.Id.Equals(textBox1.Text) && count.Equals("0"))
                    bool1 = true;
            }
            db.DBclose();//关闭数据连接            
        }
        //提交用户信息
        private void button1_Click(object sender, EventArgs e)
        {
            //格式化用户所提交之信息
            User_info.id = textBox1.Text;
            Globel.Id = User_info.id;
            User_info.name = Textname.Text;
            if (radioButton1.Checked)
                User_info.sex = radioButton1.Text;
            else
                User_info.sex = radioButton2.Text;           
            User_info.birth = dateTimePicker1.Value.Date.ToLongDateString();
            //MessageBox.Show(DateTime.Now.ToShortDateString().ToString());
            User_info.College_Id = chooseCollege.SelectedItem.ToString();
            User_info.Speciality = textBox2.Text;
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
            //满足条件才能提交信息
            if (bool1 == true)
            {
                DBlink db = new DBlink();   //创建数据库连接对象
                if (db.DBconn())
                { //连接数据库
                    string str1 = "insert into user_information(Id,name,sex,birth,College_Id,Speciality,Hobby,Political_Outlook,qualifications,id_tag) " +
                        "values('" + User_info.id + "','" + User_info.name + "','" + User_info.sex + "','" + User_info.birth + "','" + User_info.College_Id + "','" + User_info.Speciality + "','"
                        + User_info.Hobby + "','" + User_info.Political_Outlook + "','" + User_info.qualifications + "','" + User_info.id_tag + "');";
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
            else
                MessageBox.Show("您的学号输入错误，请核对");

        }
        //返回主界面
        private void button2_Click(object sender, EventArgs e)
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
