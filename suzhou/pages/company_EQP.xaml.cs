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

namespace suzhou.pages
{
    /// <summary>
    /// firm.xaml 的交互逻辑
    /// </summary>
    public partial class company_EQP : Page
    {
        // 数据库连接
        String connectServer = "server=121.239.200.28; port=3306; user=root; password=Szxtzx06090512.@; database=mould;";
        String connectStr = "server=127.0.0.1; port=3306; user=root; password=123456; database=displayptf;";
        public string cp_id { get; set; }
        public string cp_name { get; set; }
        public string EQP_ID { get; set; }
        public int state { get; set; } // 0-运行中 1-待机中 2-暂停中 3-异常停止
        int[] rate = new int[3];
        int[] mrate = new int[3];
        int[] chardata = new int[12];

        public company_EQP()
        {
            InitializeComponent();
        }

        private string getCompanyName(string eqp_id, MySqlConnection conn)
        {
            string cp_name = "";
            try
            {
                conn.Open();    // 每次先连接在查询，查询完立即关闭连接
                string sql = string.Format("select CP_ID from cp_eqp where EQP_ID = ('{0}')", eqp_id);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                int cp_id = 0;
                while (reader.Read())
                {
                    cp_id = (int)reader["CP_ID"];
                }
                conn.Close();

                conn.Open();    // 后续查询重新打开连接
                sql = string.Format("select CP_name from company where CP_ID = ('{0}')", cp_id);
                cmd = new MySqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cp_name = (string)reader["CP_name"];
                }
                conn.Close();
            }
            catch (MySqlException ex)   // 数据库连接异常处理
            {
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
            return cp_name;
        }

        private int searchMysql (string sql, string para)
        {
            int count = 0;
            //String connectStr = "server=127.0.0.1; port=3306; user=root; database=displayptf;";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                sql = string.Format(sql, para);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                conn.Close();   // 关闭数据库连接
            }
            catch (MySqlException ex)   // 数据库连接异常处理
            {
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
            return count;
        }
        // 加载企业页面时读取设备并显示
        //private void company_info()
        //{
        //    string sql = "";
        //    // 设置公司名称
        //    if(cp_id == null)
        //    {
        //        company_name.Text = "全平台信息汇总";
        //    }
        //    // 查询并设置企业设备数
        //    if(cp_id != null) sql = "select * from cp_eqp where CP_ID = ('{0}')";
        //    else sql = "select * from equipment";
        //    eqp_num.Text =  searchMysql(sql, cp_id).ToString();

        //    // 查询并设置企业订单数
        //    if (cp_id != null) sql = "select * from orders where CP_ID = ('{0}')";
        //    else sql = "select * from orders";
        //    ord_num.Text = searchMysql(sql, cp_id).ToString();

        //    // 查询并设置企业运行设备数
        //    if (cp_id != null) sql = "select EQP_ID from cp_eqp where EQP_ID in (select EQP_ID from eqp_md where No_off = 1) and CP_ID = ('{0}')";
        //    else sql = "select * from eqp_md where No_off = 1";
        //    excute_ord_num.Text = searchMysql(sql, cp_id).ToString();
        //}

        // 读取企业统计信息
        private void company_info()
        {

            string sql = "";

            // 查询并设置企业设备数
            if (cp_name != "平台所有设备总览") sql = "select * from cp_eqp where CP_ID = (select CP_ID from company where CP_name = ('{0}'))";
            else sql = "select * from equipment"; 
            eqp_num.Text = searchMysql(sql, cp_name).ToString();

            // 查询并设置企业订单数
            if (cp_name != "平台所有设备总览") sql = "select * from orders where CP_ID = (select CP_ID from company where CP_name = ('{0}'))";
            else sql = "select * from orders";
            ord_num.Text = searchMysql(sql, cp_name).ToString();

            // 查询并设置企业运行设备数
            if (cp_name != "平台所有设备总览") sql = "select * from equipment where EQP_state = '运行中' and EQP_ID in (select EQP_ID from cp_eqp where CP_ID = (select CP_ID from  company where CP_name = ('{0}')))";
            else sql = "select * from eqp_md where No_off = 1";
            excute_ord_num.Text = searchMysql(sql, cp_name).ToString();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            company_show_search();
            company_name.Text = cp_name;
            //EQP_company.Text = cp_name;
            company_info();
            // 显示每个公司的设备信息，点企业进去则显示平台所有的设备
            //Chart_cp_unm();
            radomunmber();
            state_mark();
            E0week_show();
            Chart_show();
            title_company.Text = cp_name;
        }

        private void company_show_search()
        {
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                string CP_name_sql = string.Format("select * from company where CP_ID is not null");
                MySqlCommand cmd = new MySqlCommand(CP_name_sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //reader.Read();                 
                    Add_company_button(reader);
                }
            }
            catch (MySqlException ex)   //数据库连接异常处理
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    default:
                        MessageBox.Show("其他数据库连接错误");
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
        }
        List<Button> btn_list = new List<Button>();  //申明按钮list
        int i = 0;

        //动态添加按钮函数
        //传入公司查看结构信息，
        //根据公司名称，创建按钮
        private void Add_company_button(MySqlDataReader item)
        {
            TextBlock tx = new TextBlock();
            tx.Text = item["CP_name"].ToString();
            tx.Name = "CP_btn" + i.ToString();

            tx.FontSize = 24;
            tx.Margin = new Thickness(15, 11, 15, 11);

            // 添加点击、鼠标移入、移出事件
            tx.MouseLeftButtonDown += new MouseButtonEventHandler(TextBlock_Click);
            tx.MouseEnter += new MouseEventHandler(enter);
            tx.MouseLeave += new MouseEventHandler(leave);

            company_List.Children.Add(tx);
        }

        // 公司列表点击、移入移出事件
        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            company companyPage = new company();
            companyPage.cp_name = tx.Text;
            this.NavigationService.Navigate(companyPage);
            //string connetStr = "server=127.0.0.1;port=3306;user=root;database=displayptf";
            //MySqlConnection conn = new MySqlConnection(connectStr);
            //try
            //{
            //    conn.Open();

            //    string CP_name_sql = string.Format("select * FROM company WHERE EQP_ID = ('{0}')", EQP_ID.ToString());


            //    MySqlCommand cmd = new MySqlCommand(CP_name_sql, conn);
            //    MySqlDataReader reader = cmd.ExecuteReader();
            //    EQP_list.Children.Clear();
            //    if (reader.HasRows)
            //    {
            //        int r = 0, c = 0;

            //        while (reader.Read()&&r < 8 && c < 8)
            //        {
            //         //MessageBox.Show("这是一个测试");
            //            Image image = new Image();
            //            BitmapImage bi3 = new BitmapImage();

            //            bi3.BeginInit();
            //            bi3.UriSource = new Uri("/image/equipment/EQP_online.png", UriKind.Relative);
            //            bi3.EndInit();
            //            image.Source = bi3;
            //            image.Height = 108;
            //            image.Margin = new Thickness(10, 0, 10, 0);
            //            //image.SetValue(Grid.RowProperty, 0);

            //            EQP_list.Children.Add(image);
            //            Grid.SetColumn(image, c % 2);
            //            Grid.SetRow(image, r / 2);
            //            TextBlock tb_brand = new TextBlock();
            //            tb_brand.Text = "品牌";
            //            tb_brand.Height = 20;
            //            tb_brand.Foreground = new SolidColorBrush(Colors.White);
            //            tb_brand.Margin = new Thickness(118, 0,0,40);
            //            EQP_list.Children.Add(tb_brand);
            //            Grid.SetColumn(tb_brand, c % 2);
            //            Grid.SetRow(tb_brand, r / 2);
            //            TextBlock tb_category = new TextBlock();
            //            tb_category.Text = "类型";
            //            tb_category.Height = 20;
            //            tb_category.Foreground = new SolidColorBrush(Colors.White);
            //            tb_category.Margin = new Thickness(118, 10, 0, 0);
            //            EQP_list.Children.Add(tb_category);
            //            Grid.SetColumn(tb_category, c % 2);
            //            Grid.SetRow(tb_category, r / 2);
            //            TextBlock tb_address = new TextBlock();
            //            tb_address.Text = "产地";
            //            tb_address.Height = 20;
            //            tb_address.Foreground = new SolidColorBrush(Colors.White);
            //            tb_address.Margin = new Thickness(118, 55, 0, 0);
            //            EQP_list.Children.Add(tb_address);
            //            Grid.SetColumn(tb_address, c % 2);
            //            Grid.SetRow(tb_address, r / 2);
            //            ++c;
            //            ++r;
            //        }

            //    }
            //}
            //catch (MySqlException ex)   //数据库连接异常处理
            //{
            //    switch (ex.Number)
            //    {
            //        case 0:
            //            MessageBox.Show("Cannot connect to server.  Contact administrator");
            //            break;
            //        default:
            //            MessageBox.Show("其他数据库连接错误");
            //            break;
            //    }
            //}
            //finally
            //{
            //    conn.Close();
            //}

        }
        private void enter(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.LawnGreen);
        }
        private void leave(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.White);
        }
        

        private void Chart_show()
        {
            char1.Height -= (chardata[0]*3);
            char1.Margin = new Thickness(144, 120+chardata[0]*3, 0, 0);
            char1_Copy.Height -= (chardata[1] * 3);
            char1_Copy.Margin = new Thickness(144 + 75, 120 + chardata[1] * 3, 0, 0);
            char1_Copy1.Height -= (chardata[2] * 3);
            char1_Copy1.Margin = new Thickness(144 + 75*2, 120 + chardata[2] * 3, 0, 0);
            char1_Copy2.Height -= (chardata[3] * 3);
            char1_Copy2.Margin = new Thickness(10, 120 + chardata[3] * 3, 0, 0);
            char1_Copy3.Height -= (chardata[4] * 3);
            char1_Copy3.Margin = new Thickness(10+ 75, 120 + chardata[4] * 3, 0, 0);
            char1_Copy4.Height -= (chardata[5] * 3);
            char1_Copy4.Margin = new Thickness(10 + 75 * 2, 120 + chardata[5] * 3, 0, 0);
            char1_Copy5.Height -= (chardata[6] * 3);
            char1_Copy5.Margin = new Thickness(10 + 75 * 3, 120 + chardata[6] * 3, 0, 0);
            char1_Copy6.Height -= (chardata[7] * 3);
            char1_Copy6.Margin = new Thickness(10 + 75 * 4, 120 + chardata[7] * 3, 0, 0);
            char1_Copy7.Height -= (chardata[8] * 3);
            char1_Copy7.Margin = new Thickness(10 + 75 * 5, 120 + chardata[8] * 3, 0, 0);
            char1_Copy8.Height -= (chardata[9] * 3);
            char1_Copy8.Margin = new Thickness(10 + 75 * 6, 120 + chardata[9] * 3, 0, 0);
            char1_Copy9.Height -= (chardata[10] * 3);
            char1_Copy9.Margin = new Thickness(10 + 75 * 7, 120 + chardata[10] * 3, 0, 0);
            char1_Copy10.Height -= (chardata[11] * 3);
            char1_Copy10.Margin = new Thickness(10 + 75 * 8, 120 + chardata[11] * 3, 0, 0);
            int mth = (int)DateTime.Now.Month;
            if (mth == 12)
            {
                mth = 0;
            }
            data1.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data2.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data3.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data4.Text = (++mth).ToString(); if (mth == 12)
            {
                mth = 0;
            }
            data5.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data6.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data7.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data8.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data9.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data10.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data11.Text = (++mth).ToString();
            if (mth == 12)
            {
                mth = 0;
            }
            data12.Text = (++mth).ToString();

        }
     
        private Object Mysql_search(string sql_string)
        {
            //string connetStr = "server=127.0.0.1;port=3306;user=root;database=displayptf";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                string CP_sum_sql = sql_string;
                MySqlCommand CP_sum_cmd = new MySqlCommand(CP_sum_sql, conn);
                Object CP_sum_result = CP_sum_cmd.ExecuteScalar();
                if (CP_sum_result != null)//查询结果不为空
                {
                    return CP_sum_result;
                }
            }
            catch (MySqlException ex)//数据库连接异常处理
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    default:
                        //MessageBox.Show("其他数据库连接错误");
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        private void E0week_show()
        {
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                string CP_name_sql = string.Format("select * from equipment where EQP_ID=('{0}')",EQP_ID.ToString());
                MySqlCommand cmd = new MySqlCommand(CP_name_sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    //reader.Read(); 
                   
                    model.Text = reader["EQP_model"].ToString();
                    category.Text = reader["EQP_category"].ToString();



                    
                    standby_rate.Text = rate[0].ToString() + "%";
                    run_rate.Text = (100-rate[1]-rate[0]-rate[2]).ToString() + "%";
                    E01_standby.Width = (rate[0]+rate[1]+rate[2]) * 7;
                    stop_rate.Text = rate[2].ToString() + "%";
                    E01_stop.Width = (rate[1]+rate[2]) * 7;
                    abnormal_rate.Text = (rate[1]).ToString() + "%";
                    E01_abnormal.Width = ( rate[1]) * 7;
                   

                }
            }
            catch (MySqlException ex)   //数据库连接异常处理
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    default:
                        MessageBox.Show("其他数据库连接错误");
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
        }
        private void E0month_show()
        {

            standby_rate.Text = mrate[0].ToString() + "%";
            run_rate.Text = (100 - mrate[1] - mrate[0] - mrate[2]).ToString() + "%";
            E01_standby.Width = (mrate[0] + mrate[1] + mrate[2]) * 7;
            stop_rate.Text = mrate[2].ToString() + "%";
            E01_stop.Width = (mrate[1] + mrate[2]) * 7;
            abnormal_rate.Text = (mrate[1]).ToString() + "%";
            E01_abnormal.Width = (mrate[1]) * 7;
        }

        // 返回点击事件
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            company companyPage = new company();
            companyPage.cp_name = cp_name;
            this.NavigationService.Navigate(companyPage);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.LawnGreen);
        }
        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.White);
        }
        private void radomunmber()
        {
            Random rd = new Random();
            rate[2] = rd.Next(1, 30);//停止时间
            rate[1] = rd.Next(1, 10);//异常
            rate[0] = rd.Next(10, 40);//待机
            mrate[2] = rd.Next(1, 30);//停止时间
            mrate[1] = rd.Next(1, 10);//异常
            mrate[0] = rd.Next(10, 40);//待机
            for(int i = 0; i < 12; i++)
            {
                chardata[i] = rd.Next(20, 70);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            E0week_show();
        }

        private void bt_week_Click(object sender, RoutedEventArgs e)
        {
            E0month_show();
        }
        private void state_mark()
        {
            if (state==1)
            {
                operating_status.Text = "待机中";
                operating_status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9093DC"));
            }
            else if (state==0)
            {
                operating_status.Text = "运行中";
            }
            else if (state== 2)
            {
                operating_status.Text = "暂停中";
                operating_status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC5B028"));
            }
            else if(state==3)
            {
                operating_status.Text = "异常停止";
                operating_status.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE63928"));
            }
        }


        // 搜索栏点击事件
        private void company_Search_Click(object sender, RoutedEventArgs e)
        {
            company company = new company();
            string content = et1.Text;
            MySqlConnection connection = new MySqlConnection(connectStr);

            bool isID(string para)  // 判断输入的是否是ID
            {
                Boolean flag = true;
                try { int.Parse(para); }
                catch { flag = false; }
                return flag;
            }

            void searchMysql(string mysql, MySqlConnection conn)
            {
                try
                {
                    conn.Open();
                    string sql = string.Format(mysql, content);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        company.cp_name = reader["CP_name"].ToString();
                        company.cp_id = reader["CP_ID"].ToString();
                        this.NavigationService.Navigate(company);
                    }
                    else
                    {
                        MessageBox.Show("您查找的公司不存在,请检查后重新查询!");
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
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
            }

            if (isID(content))
            {
                string mysql = "select * from company where CP_ID = ('{0}')";
                searchMysql(mysql, connection);
            }
            else
            {
                string mysql = "select * from company where CP_name = ('{0}')";
                searchMysql(mysql, connection);
            }
        }

        private void order_Search_Click(object sender, RoutedEventArgs e)
        {
            order order = new order();
            string content = et2.Text;
            MySqlConnection conn = new MySqlConnection(connectStr);
            string sql = string.Format("select * from orders where OD_ID = ('{0}')", content);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order.content = et2.Text;
                    this.NavigationService.Navigate(order);
                }
                else
                {
                    MessageBox.Show("您输入的订单号不存在，请重新输入!");
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
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
        }

        private void mould_Search_Click(object sender, RoutedEventArgs e)
        {
            string content = et3.Text;
            MySqlConnection conn = new MySqlConnection(connectServer);
            string sql = string.Format("show tables like '{0}'", content);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    mould mould = new mould(et3.Text);
                    this.NavigationService.Navigate(mould);
                }
                else
                {
                    MessageBox.Show("您输入的模具号不存在，请重新输入!");
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
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
        }

        private void logistics_Search_Click(object sender, RoutedEventArgs e)
        {
            logistics logistics = new logistics();
            string content = et4.Text;
            this.NavigationService.Navigate(logistics);
        }
    }
}

