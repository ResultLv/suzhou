using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
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
    public partial class order : Page
    {
        // 数据库连接
        String connectServer = "server=121.239.200.28; port=3306; user=root; password=Szxtzx06090512.@; database=mould;";
        String connectStr = "server=127.0.0.1; port=3306; user=root; password=123456; database=displayptf;";
        List<string> csidlist;//客户id
        List<int> odidlist;//订单id
        List<int> odstatelist;//订单完成状态
        List<Decimal> odvaluelist;//订单价值

        List<int> modleidlist; // 用订单id查到的模具id

        List<int> mdstatelist; // 单个订单下的所有模具的加工状态

        List<string> mdnamelist; // 单个订单下的所有模具的名称

        List<StackPanel> childProgressSpList = new List<StackPanel>();

        List<int> flag = new List<int>();//列表开关状态

        public string content { set; get; }
        //public string orderID { set; get; }
        //public string customerName { set; get; }
        //public string orderProgress { set; get; }
        //public string orderValue { set; get; }
        //public string orderCompany { set; get; }
        public order()
        {
            InitializeComponent();

            //加入企业数量变化图表绘制
            Chart_cp_unm();
            //近12个月内，各月订单数量变化
            Chart_order_num();
            //设备运行比例图
            Chartpie_EQP();
        }

        // 订单页面加载时
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            infoLoad(); // 加载统计信息

            readOrderInfo(content);

            int length = odstatelist.Count();
            for (int i = 0; i < length; i++)
            {
                addProgress(i, odstatelist[i]);
                flag.Add(0);
            }

        }


        // 添加订单进度条
        public void addProgress(int odindex,int state)
        {
            int total_process = 0;  // 订单完成总进度
            findMdState(odindex);   //更新了modestatelist(该订单的模具状态列表)
            for (int i = 0; i < modleidlist.Count; i++)
            {
                total_process += mdstatelist[i];
                if (i == modleidlist.Count - 1)
                {
                    total_process = total_process / modleidlist.Count;
                    state = total_process;
                }
            }
            // 如果订单完成进度到100，则更新订单状态（总完成率100%时订单即为已完成，否则为执行中）
            if (total_process == 100)
            {
                MySqlConnection conn = new MySqlConnection(connectStr);
                try
                {
                    conn.Open();
                    modleidlist = new List<int>();  //存储当前订单的所有模具id
                    String sql = string.Format("update orders set OD_state = 0 where OD_ID=('{0}')", odindex+1);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                catch (MySqlException e)
                {
                    switch (e.Number)
                    {
                        case 0:
                            MessageBox.Show("updateorder时连接数据库失败，请联系管理员");
                            break;
                        default:
                            MessageBox.Show("无效用户名或密码，请重试");
                            break;
                    }
                    conn.Close();
                }
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(connectStr);
                try
                {
                    conn.Open();
                    modleidlist = new List<int>();  //存储当前订单的所有模具id
                    String sql = string.Format("update orders set OD_state = 1 where OD_ID=('{0}')", odindex+1);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                catch (MySqlException e)
                {
                    switch (e.Number)
                    {
                        case 0:
                            MessageBox.Show("updateorder时连接数据库失败，请联系管理员");
                            break;
                        default:
                            MessageBox.Show("无效用户名或密码，请重试");
                            break;
                    }
                    conn.Close();
                }
            }

            Expander ex = new Expander();
            ex.Height = 50;
            ex.Width = 50;
            StackPanel childProgressSp = new StackPanel();//每个订单完成状态下用来加载子模具完成状态的面板

            WrapPanel wp = new WrapPanel();
            wp.Height = 50;

            Label l = new Label();
            l.Margin = new Thickness(30, 0, 0, 0);
            l.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            l.Content = "订单" + (odindex + 1).ToString() + "";
            l.FontSize = 20;
            l.Height = 38;

            //添加进度条
            Rectangle r1 = new Rectangle();
            LinearGradientBrush scb = new LinearGradientBrush(Color.FromRgb(253, 204, 156), Color.FromRgb(252, 152, 36), 0);
            r1.Margin = new Thickness(40,0,0,0);
            r1.Width = state*10;
            r1.Height = 28;
            r1.Fill = scb;
            r1.MouseLeftButtonDown += new MouseButtonEventHandler(rec_click);

            
            void rec_click(object sender, EventArgs e)
            {
                findMdState(odindex);//更新了modestatelist(该订单的模具状态列表)

                //// 给订单信息框赋值
                //int total_process = 0;
                //for (int i = 0; i < modleidlist.Count; i++)
                //{
                //    total_process += mdstatelist[i];
                //    if(i == modleidlist.Count - 1)
                //    {
                //        total_process = total_process / modleidlist.Count;
                //    }
                //}
                order_csid.Text = csidlist[odindex];
                order_id.Text = odidlist[odindex].ToString();
                order_state.Text = total_process.ToString() + "%";
                order_value.Text = odvaluelist[odindex].ToString();

                //MessageBox.Show("xxxxx");

                childProgressSp.Children.Clear();//防止重复点击多次加载子进度条

                

                //点击订单进度条要加载订单包含的模具的完成进度,根据查找到的一个订单包含的模具数量初始化模具进度条

                //实现再次点击关闭列表
                if (flag[odindex] == 0)
                {
                    //实现打开当前列表，关闭其它模具列表
                    
                    //for (int i = 0; i < childProgressSpList.Count; i++)
                    //{
                    //    childProgressSpList[i].Children.Clear();
                    //    flag[i] = 0;
                    //}


                    for (int i = 0; i < modleidlist.Count; i++)
                    {
                        addChildProgress(odindex, i, mdstatelist[i]);
                    }
                    flag[odindex] = 1;//记录订单的模具列表打开了
                }
                else
                {
                    childProgressSp.Children.Clear();
                    flag[odindex] = 0;
                }

                
            }

            Label l2 = new Label();
            l2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            l2.Content = state.ToString()+"%";
            l2.Height = 28;

            wp.Children.Add(l);
            wp.Children.Add(r1);
            wp.Children.Add(l2);
            progressSp.Children.Add(wp);


            //模具进度条面板设置
            childProgressSp.Margin = new Thickness(55,5,0,5);
            childProgressSpList.Add(childProgressSp);
            progressSp.Children.Add(childProgressSp);

            childProgressSp.MouseLeftButtonDown += new MouseButtonEventHandler(close_childProgress);

            void close_childProgress(object sender, EventArgs e)
            {
                //再次单击子进度条关闭子进度条
                childProgressSp.Children.Clear();
            }
        }

        //传入该订单在List中的下标
        public void findMdState(int i)
        {
            //String connectstr = "server=127.0.0.1;port=3306;user=root;database=11";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                modleidlist = new List<int>();  //存储当前订单的所有模具id
                String sql = string.Format("select * from od_md where OD_ID=('{0}')", odidlist[i]);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    modleidlist.Add((int)reader["MD_ID"]);
                }

                conn.Close();//拿到模具id后关闭链接，准备新建查询通过模具id拿到模具状态(不关再次查询会报错)


                //根据模具id查找模具表中的模具名称
                mdnamelist = new List<string>();
                for (int j = 0; j < modleidlist.Count; j++)
                {
                    conn.Open();
                    String sql1 = string.Format("select * from mould where MD_ID=('{0}')", modleidlist[j]);
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    //MessageBox.Show(mdstatelist.Count.ToString());
                    MySqlDataReader reader1 = cmd1.ExecuteReader();

                    while (reader1.Read())
                    {
                        mdnamelist.Add((string)reader1["MD_name"]);
                    }
                    conn.Close();
                }

                //根据模具id查找设备模具表中的模具加工状态信息
                mdstatelist = new List<int>();
                for (int j = 0; j < modleidlist.Count; j++)
                {
                    conn.Open();
                    String sql1 = string.Format("select * from eqp_md where MD_ID=('{0}')", modleidlist[j]);
                    MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                    //MessageBox.Show(mdstatelist.Count.ToString());
                    MySqlDataReader reader1 = cmd1.ExecuteReader();

                    while (reader1.Read())
                    {
                        int mdstate = (int)reader1["MD_Progress"];
                        mdstatelist.Add(mdstate);
                    }
                    conn.Close();
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("findstate连接数据库失败，请联系管理员");
                        break;
                    default:
                        MessageBox.Show("无效用户名或密码，请重试");
                        break;
                }
                conn.Close();
            }
        }

        // 添加模具子进度条
        public void addChildProgress(int whichorder, int mdindex, int state)
        {
            WrapPanel wp = new WrapPanel();
            wp.Height = 50;

            Label l = new Label();
            l.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            //l.Content = "模具" + (mdindex + 1).ToString() + "";
            l.Content = mdnamelist[mdindex];
            l.FontSize = 16;
            l.Height = 28;

            //添加进度条
            Rectangle r1 = new Rectangle();
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb(94, 184, 96));
            //LinearGradientBrush scb = new LinearGradientBrush(Color.FromRgb(10, 102, 53),  Color.FromRgb(106, 205, 154),  0);
            r1.Margin = new Thickness(24, 0, 0, 0);
            r1.Width = state * 8;
            r1.Height = 20;
            r1.Fill = scb;

            Label l2 = new Label();
            l2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            l2.Content = state.ToString() + "%";
            l2.Height = 28;

            wp.Children.Add(l);
            wp.Children.Add(r1);
            wp.Children.Add(l2);
            childProgressSpList[whichorder].Children.Add(wp);
        }

        // 读取并保存订单信息
        public void readOrderInfo(string content)
        {
            if (content == null) content = "1";
            int index = int.Parse(content) - 1;
            //String connectstr = "server=127.0.0.1;port=3306;user=root;database=11";
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                String sql = "select * from orders";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                csidlist = new List<string>();
                odidlist = new List<int>();
                odstatelist = new List<int>();
                odvaluelist = new List<Decimal>();
                while (reader.Read())
                {
                    csidlist.Add((string)reader["Customer_ID"]);
                    odidlist.Add((int)reader["OD_ID"]);
                    odstatelist.Add((int)reader["OD_compstaus"]);
                    odvaluelist.Add((Decimal)reader["OD_value"]);
                }
                conn.Close();
                order_csid.Text = csidlist[index];
                order_id.Text = odidlist[index].ToString();
                order_state.Text = odstatelist[index].ToString()+"%";
                order_value.Text = odvaluelist[index].ToString();

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
                conn.Close();
            }

        }

        // 加盟企业累计增长量
        private void Chart_cp_unm()
        {
            string CP_sum_sql = string.Format("select count(*) from company where CP_ID is not null");//查询企业数
            int cp_sum = Convert.ToInt32(Mysql_search(CP_sum_sql));    // 企业总数

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
                Tb.Margin = new Thickness(70 + perWidth, 339, 0, 0);
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

                Tb.Margin = new Thickness(30, 317 - 27 * j, 0, 0);

                grid_chart.Children.Add(Tb);

            }
            float x = 27 / interval;  //横坐标之间的像素点是27位，横坐标数值是interval；其商代表一个数据库值代表的像素点数。
            int j1 = 0;
            chart1_Re.Height = val[j1] * x;
            chart1_Re.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy.Height = val[j1] * x;
            chart1_Re_Copy.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy1.Height = val[j1] * x;
            chart1_Re_Copy1.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy2.Height = val[j1] * x;
            chart1_Re_Copy2.Margin = new Thickness(64 + 40 * j1, 329 - val[j1++] * x, 0, 0);
            chart1_Re_Copy3.Height = val[j1] * x;
            chart1_Re_Copy3.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy4.Height = val[j1] * x;
            chart1_Re_Copy4.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy5.Height = val[j1] * x;
            chart1_Re_Copy5.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy6.Height = val[j1] * x;
            chart1_Re_Copy6.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy7.Height = val[j1] * x;
            chart1_Re_Copy7.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy8.Height = val[j1] * x;
            chart1_Re_Copy8.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy9.Height = val[j1] * x;
            chart1_Re_Copy9.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);
            chart1_Re_Copy10.Height = val[j1] * x;
            chart1_Re_Copy10.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * x, 0, 0);

        }

        private void Chart_order_num()
        {
            int mth = (int)DateTime.Now.Month;
            int y = (int)DateTime.Now.Year;
            --y;
            Dictionary<string, int> d = new Dictionary<string, int>();
            for (int j = 0, k = mth; j < 12; ++j)
            {
                if (k == 12)
                {
                    k = 1;
                    ++y;
                }
                else
                {
                    ++k;
                }
                string chart_str = string.Format("select count(*) from orders where  month(OD_date)='" + k.ToString() + "' and year(OD_date)='" + y.ToString() + "'");
                Object temp;
                temp = Mysql_search(chart_str);
                if (temp != null)
                {
                    d.Add(k.ToString(), Convert.ToInt32(temp));
                }
                else
                {
                    d.Add(k.ToString(), 0);
                }

            }
            //line2.ItemsSource = d;
            int i = 0;
            foreach (string key in d.Keys)
            {
                TextBlock Tb = new TextBlock();

                Tb.Text = key.ToString();

                Tb.Margin = new Thickness(70 + i, 339, 0, 0);
                i += 40;
                grid_chart2.Children.Add(Tb);

            }
            int max = 0, si = 0;
            int interval = 0;
            int[] val = new int[12];
            foreach (int j in d.Values)
            {
                val[si++] = j;
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

                Tb.Margin = new Thickness(30, 325 - 27 * j, 0, 0);
                grid_chart2.Children.Add(Tb);

            }
            int j1 = 0;
            chart2_Re.Height = val[j1] * 27;
            chart2_Re.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy.Height = val[j1] * 27;
            chart2_Re_Copy.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy1.Height = val[j1] * 27;
            chart2_Re_Copy1.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy2.Height = val[j1] * 27;
            chart2_Re_Copy2.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy3.Height = val[j1] * 27;
            chart2_Re_Copy3.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy4.Height = val[j1] * 27;
            chart2_Re_Copy4.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy5.Height = val[j1] * 27;
            chart2_Re_Copy5.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy6.Height = val[j1] * 27;
            chart2_Re_Copy6.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy7.Height = val[j1] * 27;
            chart2_Re_Copy7.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy8.Height = val[j1] * 27;
            chart2_Re_Copy8.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy9.Height = val[j1] * 27;
            chart2_Re_Copy9.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
            chart2_Re_Copy10.Height = val[j1] * 27;
            chart2_Re_Copy10.Margin = new Thickness(64 + 40 * j1, 333 - val[j1++] * 27, 0, 0);
        }

        private void Chartpie_EQP()
        {
            //Dictionary<string, int> d = new Dictionary<string, int>();
            //d.Add("运转设备占比", 6 / 10);
            //d.Add("停机设备占比", 4 / 10);
            //pie.ItemsSource = d;

            int temp1, temp2;
            string chart_str = string.Format("select count(OD_ID) from orders where OD_state=1");
            string chart_str1 = string.Format("select count(*) from orders");
            temp1 = Convert.ToInt16(Mysql_search(chart_str));
            temp2 = Convert.ToInt16(Mysql_search(chart_str1)) - temp1;


            ((PieSeries)mcChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("执行订单",temp1),
                    new KeyValuePair<string, int>("已完成订单",temp2)
                };

            // 添加图例
            TextBlock tx1 = new TextBlock();
            TextBlock tx2 = new TextBlock();
            ratioGrid.Children.Add(tx1);
            ratioGrid.Children.Add(tx2);

            tx1.Text = "执行中";
            tx2.Text = "已完成";
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
