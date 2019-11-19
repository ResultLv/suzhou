using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Events;
using suzhou.ViewModel;

namespace suzhou.pages
{
    /// <summary>
    /// firm.xaml 的交互逻辑
    /// </summary>
    public partial class mould : Page
    {
        static String m_ID = null; // 模具ID(全局变量)
        List<List<String>> allData = new List<List<string>>(); // 查询出的数据列表
        List<List<String>> partData = new List<List<string>>(); // 查询出的日期段内的数据列表

        // 数据库连接
        String connectServer = "server=122.112.149.16; port=3306; user=root; password=Szxtzx06090512.@; database=mould;";
        String connectStr = "server=127.0.0.1; port=3306; user=root; password=123456; database=displayptf;";

        public SeriesCollection SeriesCollection1 { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels1 { get; set; }
        public List<string> Labels2 { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> Formatter { get; set; }

        // 构造函数
        public mould()
        {
            InitializeComponent();
            //mould_ID.Text = id; // 页面跳转传递模具ID
            //MessageBox.Show(id);

            infoLoad();     // 加载统计信息
            mouldList();    // 加载模具列表
        }

        // 带参构造函数，页面跳转时传入模具ID
        public mould(string id)
        {
            InitializeComponent();
            m_ID = id;
            mould_ID.Text = m_ID;

            infoLoad();     // 加载统计信息
            mouldList();    // 加载模具列表
            //设置默认查询时段
            start.Text = DateTime.Now.ToShortDateString();
            end.Text = DateTime.Now.ToShortDateString();
            showCurrentInfo();
            showStatisticsInfo();
        }



// ------------------ 分割线 -------------------------        
        public MySqlConnection getConn(String connectStr)
        {
            MySqlConnection conn = new MySqlConnection(connectStr);
            return conn;
        }
        
        // 动态生成模具列表
        private void mouldList()
        {
            // 连接服务器mould数据库
            MySqlConnection conn = getConn(connectServer);
            if (conn != null)
            {
                conn.Open();
                String sql = "show tables";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String temp = reader.GetString(0);
                    Console.WriteLine(temp);

                    TextBlock tx = new TextBlock();
                    tx.Text = temp;
                    tx.FontSize = 24;
                    tx.Margin = new Thickness(20, 11, 5, 11);
                    tx.Foreground = new SolidColorBrush(Colors.White);

                    tx.MouseLeftButtonDown += new MouseButtonEventHandler(mouldListClick);
                    tx.MouseEnter += new MouseEventHandler(enter);
                    tx.MouseLeave += new MouseEventHandler(leave);

                    mouldlist.Children.Add(tx);

                }
            }else MessageBox.Show("数据库连接异常");

            // 模具点击、鼠标移入移出事件
            void mouldListClick(object sender, RoutedEventArgs e)
            {
                TextBlock tx = sender as TextBlock;
                mould_ID.Text = tx.Text;
                m_ID = tx.Text;     // 给全局模具ID赋值
                showCurrentInfo();
                showStatisticsInfo();
            }

            void enter(object sender, RoutedEventArgs e)
            {
                TextBlock tx = sender as TextBlock;
                tx.Foreground = new SolidColorBrush(Colors.LawnGreen);
            }
            void leave(object sender, RoutedEventArgs e)
            {
                TextBlock tx = sender as TextBlock;
                tx.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void query_Confirm_Click(object sender, RoutedEventArgs e)
        {
            showStatisticsInfo();
        }

        // 显示当前最新一条的数据（性能概况）
        public void showCurrentInfo()
        {
            //读取最后一条数据，计算性能参数和模具产能
            MySqlConnection connection = getConn(connectServer);
            connection.Open();
            String sql = "select * from `" + m_ID + "` order by DtTm desc limit 1";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MdAv.Text = reader.GetString(5);
                LstAv.Text = reader.GetString(6);
                MdEf.Text = (double.Parse(reader.GetString(7)) * 100).ToString();
                LstEf.Text = (double.Parse(reader.GetString(8)) * 100).ToString();
            }
            connection.Close();
        }

        // 显示统计信息和图（模具产能）
        public void showStatisticsInfo()
        {
            if (m_ID == null)
            {
                MessageBox.Show("请先选择一个模具");
            }
            else if (start.Text != "" || end.Text != "")
            {
                allData.Clear();    // 先清空全局数据列表

                try
                {
                    MySqlConnection conn = getConn(connectServer);
                    conn.Open();
                    var endtime = Convert.ToDateTime(end.Text).AddDays(1).ToShortDateString();
                    String sql = "select * from `" + m_ID + "` where DtTm between '" + start.Text + "' and '" + endtime + "'";
                    Console.WriteLine(sql);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    int fieldCount = 0;
                    if (reader.HasRows) fieldCount = reader.FieldCount;
                    while (reader.Read())
                    {
                        List<String> temp = new List<string>();
                        for (int i = 3; i < fieldCount; i++)
                        {
                            temp.Add(reader.GetString(i));
                        }
                        allData.Add(temp);
                    }
                    Console.WriteLine(allData.Count);

                    // 更新ScrollableViewModel的数据
                    var vm = (ScrollableViewModel)DataContext;
                    vm.updataScrollableVM(allData);
                    vm.getBarStick(allData);


                    // 遍历选择时间段内的所有数据，计算拆除时间和怠速时间
                    String removedStart = null;
                    String removedEnd = null;
                    bool removedFlag = false;
                    String idlingStart = null;
                    String idlingEnd = null;

                    bool idlingFlag = false;

                    double removedTime = 0; // 累计移除时间
                    double idlingTime = 0;  // 累计怠速时间
                    double workingTime = 0; // 累计工作时间
                    int fixCount = 0;   // 维修次数
                    int mouldCount = 0; // 生产模次
                                        //Console.WriteLine(allData[0].Count);

                    for (int i = 0; i < allData.Count; i++)
                    {
                        // 计算移除时间
                        if (!removedFlag && allData[i][8].Equals("Removed"))
                        {
                            removedFlag = true;
                            removedStart = allData[i][0];
                        }
                        if (removedFlag && allData[i][8].Equals("Unremove"))
                        {
                            removedFlag = false;
                            removedEnd = allData[i][0];
                            removedTime += diffTime(removedStart, removedEnd);
                        }

                        // 计算怠速时间
                        if (!idlingFlag && double.Parse(allData[i][2]) >= 600)
                        {
                            idlingFlag = true;
                            idlingStart = allData[i][0];
                        }
                        if (idlingFlag && double.Parse(allData[i][2]) < 600)
                        {
                            idlingFlag = false;
                            idlingEnd = allData[i][0];
                            idlingTime += diffTime(idlingStart, idlingEnd);
                        }
                    }

                    // 计算工作时间， 计算该时间段内的生产模次和预防性维修次数
                    if (allData.Count > 0)
                    {
                        workingTime = diffTime(allData[0][0], allData[allData.Count - 1][0]) - removedTime - idlingTime;
                        fixCount = int.Parse(allData[allData.Count - 1][7]) - int.Parse(allData[0][7]);
                        mouldCount = int.Parse(allData[allData.Count - 1][1]) - int.Parse(allData[0][1]);
                    }

                    // 给界面上相应的TextBlock赋值
                    removedTB.Text = Math.Round((removedTime / 60), 1).ToString();
                    idlingTB.Text = Math.Round((idlingTime / 60), 1).ToString();
                    workingTB.Text = Math.Round((workingTime / 60), 1).ToString();
                    mouldCountTB.Text = mouldCount.ToString();
                    fixCountTB.Text = fixCount.ToString();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }

            }
            else
            {
                MessageBox.Show("请选择查询时间段");
            }
        }

        private double diffTime(String beforeTime, String afterTime)
        {
            double start = ConvertToUnixOfTime(Convert.ToDateTime(beforeTime));
            double end = ConvertToUnixOfTime(Convert.ToDateTime(afterTime));
            return end - start;
        }

        private double ConvertToUnixOfTime(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }



        // ------------------ 分割线 -------------------------        
        // 加载统计信息
        private void infoLoad()
        {
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
