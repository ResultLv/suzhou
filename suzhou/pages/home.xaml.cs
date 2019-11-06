using System;
using System.Collections.Generic;
using System.Windows.Controls.DataVisualization.Charting;
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
    /// home.xaml 的交互逻辑
    /// </summary>
    public partial class home : Page
    {
        // 数据库连接
        String connectServer = "server=121.239.200.28; port=3306; user=root; password=Szxtzx06090512.@; database=mould;";
        String connectStr = "server=127.0.0.1; port=3306; user=root; password=123456; database=displayptf;";
        public home()
        {
            InitializeComponent();
        }
        //public int i = 0;
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            String rootpath = AppDomain.CurrentDomain.BaseDirectory;
            rootpath = rootpath.Substring(0, rootpath.LastIndexOf("\\"));
            rootpath = rootpath.Substring(0, rootpath.LastIndexOf("\\"));
            rootpath = rootpath.Substring(0, rootpath.LastIndexOf("\\"));
            string url = rootpath + "\\map.htm";
            Uri uri = new Uri(url);
            webBrowser.Navigate(uri);

            company_show_search();
            //加入企业数量变化图表绘制
            Chart_cp_unm();
            //近12个月内，各月订单数量变化
            Chart_order_num();
            Chartpie_EQP();
            string CP_sum_sql = string.Format("select count(*) from company where CP_ID is not null");//查询企业数
            CP_sum.Text += Mysql_search(CP_sum_sql).ToString();//给企业数量显示栏赋值
            //查询并显示设备总数
            string EQP_sum_sql = string.Format("select count(*) from equipment");
            EQP_sum.Text += Mysql_search(EQP_sum_sql).ToString();
            //查询并显示订单总数
            string Order_sum_sql = string.Format("select count(*) from orders");
            Order_sum.Text += Mysql_search(Order_sum_sql);
            //查询并显示正在执行的订单数量
            string EXE_Order_sum_sql = string.Format("select count(OD_state) from orders where OD_state = 1");
            EXE_Order.Text += Mysql_search(EXE_Order_sum_sql);

        }

        //加载完成事件，
        //触发右侧企业按钮，
        //    实现地图标点
        private void Mapview_loadedCompleted(object sender, NavigationEventArgs e)
        {
            all_company();  // 页面加载完成后标记所有公司
            webBrowser.InvokeScript("setCenter");
        }

//公司显示函数
//在右侧显示公司列表，
//调用了动态生成按钮函数。
        private void company_show_search()
        {
            //string connetStr = "server=127.0.0.1;port=3306;user=root;database=displayptf";
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
        List<TextBlock> btn_list = new List<TextBlock>();  //申明按钮list
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
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.Background = System.Windows.Media.Brushes.Transparent;  // 设置按钮背景为透明
            tx.Foreground = System.Windows.Media.Brushes.White;
            tx.Margin = new Thickness(15, 11, 15, 11);
            tx.MouseEnter += new MouseEventHandler(enter);
            tx.MouseLeave += new MouseEventHandler(leave);

            tx.MouseLeftButtonDown += new MouseButtonEventHandler(btn_click);
            tx.MouseRightButtonDown += new MouseButtonEventHandler(btn_click2);
            company_show.Children.Add(tx);
            btn_list.Add(tx);
            ++i;
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

        // 地图上标记所有公司
        private void all_company()
        {
            MySqlConnection conn = new MySqlConnection(connectStr);
            // 数据库连接
            try
            {
                conn.Open();
                string sql = string.Format("select * from company");
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = (int)reader["CP_ID"];
                    decimal lon = (decimal)reader["CP_longitude"];
                    decimal lat = (decimal)reader["CP_latitude"];
                    string name = (string)reader["CP_name"];
                    int phoneNum = (int)reader["CP_tpnumber"];
                    string address = (string)reader["CP_address"];
                    webBrowser.InvokeScript("allCompany", lon, lat, name, phoneNum, address);
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
            catch (InvalidCastException ex)   // 数据库连接异常处理
            {
                Console.WriteLine(ex);
            }
        }

        //右侧公司右键单击事件
        //双击一次，传该公司的ID
        //到公司页面；
        private void btn_click2(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            company company = new company();
            //string connetStr = "server=127.0.0.1;port=3306;user=root;database=displayptf";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();

                string CP_name_sql = string.Format("select CP_ID from company where CP_name=('{0}')", tx.Text.ToString());
                MySqlCommand cmd = new MySqlCommand(CP_name_sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    company.cp_id = reader["CP_ID"].ToString();//返回该公司ID给company主页
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
            company.cp_name = tx.Text.ToString();
            this.NavigationService.Navigate(company);
        }

        //右侧公司名称单击事件
         //单机一个公司按钮，将
         //该公司在地图上标点，并
         //显示地址，名称，电话等
         //信息
        public void btn_click(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            //string connetStr = "server=127.0.0.1;port=3306;user=root;database=displayptf";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();

                string CP_name_sql = string.Format("select * from company where CP_name=('{0}')", tx.Text.ToString());
                MySqlCommand cmd = new MySqlCommand(CP_name_sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    if (reader.Read())
                    {
                        //webBrowser.InvokeScript("allCompany", reader["CP_longitude"], reader["CP_latitude"], reader["CP_name"], reader["CP_tpnumber"], reader["CP_address"]);

                        webBrowser.InvokeScript("addCompany", reader["CP_longitude"], reader["CP_latitude"], reader["CP_name"], reader["CP_tpnumber"], reader["CP_address"]);
                        ++i;
                    }
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

        //数据库查询函数，
        //查询数据统计结果，
        //    返回一个数值；
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

        private MySqlDataReader Mysql_search2(string sql_string)
        {
            //string connetStr = "server=127.0.0.1;port=3306;user=root;database=displayptf";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                string CP_name_sql = sql_string;
                MySqlCommand cmd = new MySqlCommand(CP_name_sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    return reader;
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
            return null;
        }

        private void Chart_cp_unm()
        {
            string CP_sum_sql = string.Format("select count(*) from company where CP_ID is not null");//查询企业数
            int cp_sum = Convert.ToInt32(Mysql_search(CP_sum_sql));    // 企业总数

            int mth = (int)DateTime.Now.Month;
            int y = (int)DateTime.Now.Year;
            Dictionary<string, int> d = new Dictionary<string, int>();
            for(int j = 0, k = mth+1; j < 12; ++j)
            {
                if (k == 1)
                {
                    k = 12;
                    y--;
                    
                }
                else
                {
                    --k;
                }
                string chart_str = string.Format("select count(*) from company where  month(CP_joindate)='" + k.ToString() + "' and year(CP_joindate)='" + y.ToString() + "'");
                Object temp;
                temp = Mysql_search(chart_str);
                if (temp != null)
                {
                    d.Add(k.ToString(), cp_sum);
                    cp_sum = cp_sum - Convert.ToInt32(temp);
                }
                else
                {
                    d.Add(k.ToString(), 0);
                }
            }


            //定义横坐标标识值
            string[] month = new string[12];
            int index = 11;
            foreach (string key in d.Keys)
            {
                month[index] = key;
                index--;
            }

            int perWidth = 0;
            for (int j = 0; j < 12; j++)
            {
                TextBlock Tb = new TextBlock();
                Tb.Text = month[j];
                Tb.Margin = new Thickness(70 + perWidth, 325, 0, 0);
                perWidth += 40;
                grid_chart.Children.Add(Tb);
            }

            int max = 0, si = 11;
            float interval = 0;
            int[] val = new int[12];
            foreach (int j in d.Values)
            {
                val[si--] = j;
                if (j > max)
                {
                    max = j;
                }
            }
            interval = max / 10 + 1;
            for (int j = 0; j < 10; ++j)
            {
                TextBlock Tb = new TextBlock();

                Tb.Text = (interval * j).ToString();

                Tb.Margin = new Thickness(30, 313 - 27 * j, 0, 0);

                grid_chart.Children.Add(Tb);

            }
            float x = 27/interval;  //横坐标之间的像素点是27位，横坐标数值是interval；其商代表一个数据库值代表的像素点数。
            int j1 = 0;
            chart1_Re.Height = val[j1] * x;
            chart1_Re.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy.Height = val[j1] * x;
            chart1_Re_Copy.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy1.Height = val[j1] * x;
            chart1_Re_Copy1.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy2.Height = val[j1] * x;
            chart1_Re_Copy2.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy3.Height = val[j1] * x;
            chart1_Re_Copy3.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy4.Height = val[j1] * x;
            chart1_Re_Copy4.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy5.Height = val[j1] * x;
            chart1_Re_Copy5.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy6.Height = val[j1] * x;
            chart1_Re_Copy6.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy7.Height = val[j1] * x;
            chart1_Re_Copy7.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy8.Height = val[j1] * x;
            chart1_Re_Copy8.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy9.Height = val[j1] * x;
            chart1_Re_Copy9.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);
            chart1_Re_Copy10.Height = val[j1] * x;
            chart1_Re_Copy10.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * x, 0, 0);

        }

        private void Chart_order_num()
        {
            string OD_sum_sql = string.Format("select count(*) from orders");//查询订单数
            int od_sum = Convert.ToInt32(Mysql_search(OD_sum_sql));    // 订单总数

            int mth = (int)DateTime.Now.Month;
            int y = (int)DateTime.Now.Year;
            Dictionary<string, int> d = new Dictionary<string, int>();
            for (int j = 0, k = mth + 1; j < 12; ++j)
            {
                if (k == 1)
                {
                    k = 12;
                    y--;

                }
                else
                {
                    --k;
                }
                string chart_str = string.Format("select count(*) from orders where  month(OD_date)='" + k.ToString() + "' and year(OD_date)='" + y.ToString() + "'");
                Object temp;
                temp = Mysql_search(chart_str);
                if (temp != null)
                {
                    d.Add(k.ToString(), od_sum);
                    od_sum = od_sum - Convert.ToInt32(temp);
                }
                else
                {
                    d.Add(k.ToString(), 0);
                }
            }

            //定义横坐标标识值
            string[] month = new string[12];
            int index = 11;
            foreach (string key in d.Keys)
            {
                month[index] = key;
                index--;
            }

            int perWidth = 0;
            for (int j = 0; j < 12; j++)
            {
                TextBlock Tb = new TextBlock();
                Tb.Text = month[j];
                Tb.Margin = new Thickness(70 + perWidth, 325, 0, 0);
                perWidth += 40;
                grid_chart2.Children.Add(Tb);
            }

            int max = 0, si = 11;
            float interval = 0;
            int[] val = new int[12];
            foreach (int j in d.Values)
            {
                val[si--] = j;
                if (j > max)
                {
                    max = j;
                }
                Console.WriteLine(j);

            }
            interval = max / 10 + 1;
            for (int j = 0; j < 10; ++j)
            {
                TextBlock Tb = new TextBlock();

                Tb.Text = (interval * j).ToString();

                Tb.Margin = new Thickness(30, 313 - 27 * j, 0, 0);

                grid_chart2.Children.Add(Tb);

            }

            int j1 = 0;
            chart2_Re.Height = val[j1] * 27;
            chart2_Re.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy.Height = val[j1] * 27;
            chart2_Re_Copy.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy1.Height = val[j1] * 27;
            chart2_Re_Copy1.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy2.Height = val[j1] * 27;
            chart2_Re_Copy2.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy3.Height = val[j1] * 27;
            chart2_Re_Copy3.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy4.Height = val[j1] * 27;
            chart2_Re_Copy4.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy5.Height = val[j1] * 27;
            chart2_Re_Copy5.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy6.Height = val[j1] * 27;
            chart2_Re_Copy6.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy7.Height = val[j1] * 27;
            chart2_Re_Copy7.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy8.Height = val[j1] * 27;
            chart2_Re_Copy8.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy9.Height = val[j1] * 27;
            chart2_Re_Copy9.Margin = new Thickness(64 + 40 * j1, 320 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy10.Height = val[j1] * 27;
            chart2_Re_Copy10.Margin = new Thickness(64 + 40 * j1, 319 - val[j1++] * 27, 0, 0);
        }

        private void Chartpie_EQP()
        {
            //Dictionary<string, int> d = new Dictionary<string, int>();
            //d.Add("运转设备占比", 6 / 10);
            //d.Add("停机设备占比", 4 / 10);
            //pie.ItemsSource = d;

            int temp1, temp2;
            string chart_str = string.Format("select count(EQP_ID) from equipment where EQP_offon=1");
            string chart_str1 = string.Format("select count(*) from equipment");
            temp1 = Convert.ToInt16(Mysql_search(chart_str));
            temp2 = Convert.ToInt16(Mysql_search(chart_str1)) - temp1;


            ((PieSeries)mcChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("运转设备",temp1),
                    new KeyValuePair<string, int>("停机设备",temp2)
                };

            // 添加图例
            TextBlock tx1 = new TextBlock();
            TextBlock tx2 = new TextBlock();
            ratioGrid.Children.Add(tx1);
            ratioGrid.Children.Add(tx2);

            tx1.Text = "运行";
            tx2.Text = "停止";
            tx1.FontSize = 16;
            tx2.FontSize = 16;
            tx1.Margin = new Thickness(46, 50, 0, 0);
            tx2.Margin = new Thickness(46, 80, 0, 0);

            Rectangle r1 = new Rectangle();
            Rectangle r2 = new Rectangle();
            ratioGrid.Children.Add(r1);
            ratioGrid.Children.Add(r2);

            r1.Fill = new SolidColorBrush(Color.FromRgb(84, 117, 154));
            r2.Fill = new SolidColorBrush(Color.FromRgb(159, 89, 88));
            r1.Height = 18;
            r1.Width = 18;
            r2.Height = 18;
            r2.Width = 18;
            r1.Margin = new Thickness(16, 52, 0, 0);
            r2.Margin = new Thickness(16, 82, 0, 0);
            r1.VerticalAlignment = VerticalAlignment.Top;
            r1.HorizontalAlignment = HorizontalAlignment.Left;
            r2.VerticalAlignment = VerticalAlignment.Top;
            r2.HorizontalAlignment = HorizontalAlignment.Left;

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
