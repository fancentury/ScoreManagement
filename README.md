# c#课程设计-学生成绩管理系统
利用visual stdio2017+MySQL5.5开发，有系统管理员、学生、教师、教学秘书4种不同身份，可实现成绩、课程信息、用户信息的增删改查
利用这个小Demo,希望对大家c#和MySQL的学习有一点帮助
详细文档可查看实验报告（介绍了成绩信息的增删改查，分析了课程和用户信息的代码）

## 起步
### 1.下载MySQL，Navicat，创建数据库student_manage_system，导入数据库文件夹下student_manage_system.sql，直接运行即可
### 2.使用visual stdio 2017+版本，点击ScoreManagement文件夹下ScoreManagement.sin启动项目
### 3.进入IDE后在右侧解决方案中Tools文件夹下修改DBlink.cs中，如下图所示连接字符串中uid和password部分（*替换为自己的用户名和密码*）
![连接字符串](https://images.gitee.com/uploads/images/2019/0414/175116_449ed877_1758849.jpeg "1.jpg")
### 4.配置完成后，单击解决方案资源管理器中Program.cs（c#项目程序入口）文件运行项目
![系统登录](https://images.gitee.com/uploads/images/2019/0414/175116_449ed877_1758849.jpeg "1.jpg")

## 数据库结构如下
![数据库结构](https://images.gitee.com/uploads/images/2019/0414/175116_449ed877_1758849.jpeg "1.jpg")
