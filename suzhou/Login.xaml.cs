using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace suzhou
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_login(object sender, RoutedEventArgs e)
        {
            String username = txtBoxName.Text;
            String pw = pwBox.Password;
            // 数据库连接
            String connectStr = "server=127.0.0.1; port=3306; user=root; password=123456; database=displayptf;";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                string sql = string.Format("select * from user where User_name=('{0}') and User_pword=('{1}')", username, pw);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())  // 如果能查询到一条语句则用户名和密码正确,跳转至主界面
                {
                    Platform platform = new Platform();
                    platform.username = username;    // 给新窗口platform赋值username属性
                    this.Close();
                    platform.ShowDialog();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }
            }
            catch (MySqlException ex)   // 数据库连接异常处理
            {
                Console.WriteLine(ex);
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("连接数据库失败，请联系管理员");
                        break;
                    default:
                        MessageBox.Show("无效用户名或密码，请重试");
                        break;
                }
            }
            conn.Close();   // 关闭数据库连接
        }
        // 重置密码框事件
        private void Button_Click_cancer(object sender, RoutedEventArgs e) { 
            pwBox.Clear();
        }

    }
}
