using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScoreManagement.Enity;
using ScoreManagement.Tools;

namespace ScoreManagement.FormApplication
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        //登录窗口初始化
        private void Login_Load(object sender, EventArgs e)
        {
            label6.Text = "";
            identity.SelectedIndex = 0; //登录系统的身份默认是学生
            MaximizeBox = false;        //关闭登录窗口的最大化功能
            Verification_Code.Text= Verification.Code();  //动态生成验证码
            DoLogin.Enabled = false;    //默认在不输入验证码时无法点击登录按钮
            Globel.username = Tusername.Text;
        }
        
        //实时验证用户输入验证码是否正确
        private void Verification_Code_in_KeyUp(object sender, EventArgs e)
        {
            if (Verification_Code_in.Text != Verification_Code.Text)
            {
                label6.Text = "验证码错误！";
            }
            else
            {
                label6.Text = "";
                DoLogin.Enabled = true;
            }
        }
        //验证用户登录
        private void DoLogin_Click(object sender, EventArgs e)
        {
            DBlink db = new DBlink();//创建数据库连接对象
            Identification.identification(identity.SelectedIndex);//根据用户选择的值判断用户身份
            if (db.DBconn()) { 
                //数据库连接成功
                if (db.GetLoginData("select * from login_info where id_tag='" + Globel.id_tag + "'and username='" + Tusername.Text + "'"))
                {
                    //用户存在
                    if (int.Parse(LoginInfo.tag) == 1)//账号已被审核允许登录
                    {
                        if (Tpass.Text.Equals(LoginInfo.password))//登录成功
                        {
                            Globel.username = Tusername.Text;
                            new MainMenu().ShowDialog();//显示主菜单界面
                            this.Hide();
                            this.Visible = false;
                        }
                        else {  //密码错误
                            label6.Text = "密码错误！";
                        }
                    }
                    else {  //账号未审核
                        label6.Text = "该账号未审核，请联系管理员";
                    }
                }
                else {
                    label6.Text = "登录失败，用户名不存在！";
                }
            }
            db.DBclose();//关闭数据库连接
        }
        //用户点击退出事件
        private void DoNo_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //用户点击注册事件
        private void DoRegist_Click(object sender, EventArgs e)
        {
            //生成注册界面的新窗口
            regist RG = new regist();
            RG.Show();
        }

    }
}
