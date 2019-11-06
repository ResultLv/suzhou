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
    public partial class company : Page
    {
        // 数据库连接
        String connectServer = "server=121.239.200.28; port=3306; user=root; password=Szxtzx06090512.@; database=mould;";
        String connectStr = "server=127.0.0.1; port=3306; user=root; password=123456; database=displayptf;";

        public string cp_id { get; set; }
        public string cp_name { get; set; }
        public string our = "苏州协同智能制造创新";
        public company()
        {
            InitializeComponent();
        }

        //private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    ScrollViewer scrollviewer = sender as ScrollViewer;
        //    if (e.Delta > 0)
        //        scrollviewer.LineLeft();
        //    else
        //        scrollviewer.LineRight();
        //    e.Handled = true;
        //}

        //private void load_equipment(int category, string cp_id)
        //{
        //    //String connectStr = "server=127.0.0.1; port=3306; user=root; database=displayptf;";
        //    MySqlConnection conn = new MySqlConnection(connectStr);
        //    // 数据库连接
        //    try
        //    {
        //        conn.Open();
        //        string sql;
        //        if(cp_id != null)
        //        {
        //            // 根据公司ID查询一个公司的所有设备
        //            // select * from equipment,cp_eqp where cp_eqp.CP_ID = '1' and cp_eqp.EQP_ID = equipment.EQP_ID

        //            // 根据公司ID和设备类别查询一个公司所有该类设备
        //            sql = string.Format("select * from equipment,cp_eqp where cp_eqp.CP_ID = ('{0}') " +
        //                "and cp_eqp.EQP_ID = equipment.EQP_ID and EQP_category = ('{1}')", cp_id, category);
        //        }
        //        else
        //        {
        //            sql = string.Format("select * from equipment where EQP_category=('{0}')", category);

        //        }
        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        MySqlDataReader reader = cmd.ExecuteReader();
        //        //MessageBox.Show("连接正常");
        //        while (reader.Read())
        //        {
        //            var temp = (int)reader["EQP_ID"];
        //            var eqp_id = temp.ToString();
        //            add_equipments_controls(category, eqp_id, conn);
        //        }
        //        conn.Close();   // 关闭数据库连接
        //    }
        //    catch (MySqlException ex)   // 数据库连接异常处理
        //    {
        //        switch (ex.Number)
        //        {
        //            case 0:
        //                MessageBox.Show("连接数据库失败，请联系管理员");
        //                break;
        //            default:
        //                MessageBox.Show("无效用户名或密码，请重试");
        //                break;
        //        }
        //    }
        //}

        //private void add_equipments_controls(int category, string eqp_id, MySqlConnection conn)   // 后续判断设备运行状态，再传入其他参数
        //{
        //    Grid grid = new Grid();
        //    // 动态生成grid行定义
        //    grid.RowDefinitions.Add(new RowDefinition());
        //    // 动态设置第二个行定义的Height=“auto”
        //    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20, GridUnitType.Auto) });
        //    grid.HorizontalAlignment = HorizontalAlignment.Left;
        //    // grid添加图片
        //    Image image = new Image();
        //    BitmapImage bmp = new BitmapImage();
        //    BitmapImage bi3 = new BitmapImage();
        //    bi3.BeginInit();
        //    bi3.UriSource = new Uri("/image/equipment/equipment.ico", UriKind.Relative);
        //    bi3.EndInit();
        //    image.Source = bi3;
        //    image.Height = 108;
        //    image.Margin = new Thickness(10, 0, 10, 0);
        //    image.SetValue(Grid.RowProperty, 0);

        //    image.MouseLeftButtonDown += new MouseButtonEventHandler(mousedown);
        //    // 动态绑定鼠标事件
        //    void mousedown(object sender, RoutedEventArgs e)
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string sql = string.Format("select * from equipment where EQP_id=('{0}')", eqp_id); // 查询id只有一条记录
        //            MySqlCommand cmd = new MySqlCommand(sql, conn);
        //            MySqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read()) {
        //                //string e_id = ((int)reader["EQP_ID"]).ToString();
        //                string eqp_name = (string)reader["EQP_name"];
        //                string eqp_brand = (string)reader["EQP_brand"];
        //                string eqp_model = (string)reader["EQP_model"];
        //                DateTime eqp_dateofmf = (DateTime)reader["EQP_Dateofmf"];
        //                string epq_orgin = (string)reader["EQP_orgin"];

        //                EQP_id.Text = eqp_id;
        //                EQP_name.Text = eqp_name;
        //                EQP_brand.Text = eqp_brand;
        //                EQP_model.Text = eqp_model;
        //                EQP_Dataofmf.Text = eqp_dateofmf.ToString("yyyy-MM-dd");
        //                EQP_orgin.Text = epq_orgin;

        //            }
        //            conn.Close();   // 关闭数据库连接
        //            EQP_company.Text = getCompanyName(eqp_id, conn);    // 取设备公司名
        //        }
        //        catch (MySqlException ex)   // 数据库连接异常处理
        //        {
        //            switch (ex.Number)
        //            {
        //                case 0:
        //                    MessageBox.Show("连接数据库失败，请联系管理员");
        //                    break;
        //                default:
        //                    MessageBox.Show("无效用户名或密码，请重试");
        //                    break;
        //            }
        //        }
        //    }

        //    grid.Children.Add(image);
        //    // grid添加设备ID
        //    TextBlock tb = new TextBlock();
        //    tb.Text = eqp_id; // 需要绑定右侧栏ID
        //    tb.TextAlignment = TextAlignment.Center;
        //    tb.FontSize = 40;
        //    tb.SetValue(Grid.RowProperty, 1);
        //    grid.Children.Add(tb);

        //    // stackpanel添加grid
        //    if(category == 0)
        //    {
        //        dockpanel_0.Children.Add(grid); // 添加CNC型设备
        //    }else if(category == 1)
        //    {
        //        dockpanel_1.Children.Add(grid); // 添加线切割设备
        //    }
        //    else
        //    {
        //        dockpanel_2.Children.Add(grid); // 添加火花机设备
        //    }
        //}

        //private string getCompanyName(string eqp_id, MySqlConnection conn)
        //{
        //    string cp_name = "";
        //    try
        //    {
        //        conn.Open();    // 每次先连接在查询，查询完立即关闭连接
        //        string sql = string.Format("select CP_ID from cp_eqp where EQP_ID = ('{0}')", eqp_id);
        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        MySqlDataReader reader = cmd.ExecuteReader();
        //        int cp_id = 0;
        //        while (reader.Read())
        //        {
        //            cp_id = (int)reader["CP_ID"];
        //        }
        //        conn.Close();

        //        conn.Open();    // 后续查询重新打开连接
        //        sql = string.Format("select CP_name from company where CP_ID = ('{0}')", cp_id);
        //        cmd = new MySqlCommand(sql, conn);
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            cp_name = (string)reader["CP_name"];
        //        }
        //        conn.Close();
        //    }
        //    catch (MySqlException ex)   // 数据库连接异常处理
        //    {
        //        switch (ex.Number)
        //        {
        //            case 0:
        //                MessageBox.Show("连接数据库失败，请联系管理员");
        //                break;
        //            default:
        //                MessageBox.Show("无效用户名或密码，请重试");
        //                break;
        //        }
        //    }
        //    return cp_name;
        //}


        private int searchMysql(string sql, string para)
        {
            int count = 0;
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
        } // 统计信息函数的sql工具

        // 读取企业统计信息
        private void company_info()

        {
            
            string sql = "";

            // 查询并设置企业设备数
            if (cp_name != "平台所有设备总览" && cp_name != null) sql = "select * from cp_eqp where CP_ID = (select CP_ID from company where CP_name = ('{0}'))";
            else sql = "select * from equipment";
            eqp_num.Text = searchMysql(sql, cp_name).ToString();

            // 查询并设置企业订单数
            if (cp_name != "平台所有设备总览" && cp_name != null) sql = "select * from orders where CP_ID = (select CP_ID from company where CP_name = ('{0}'))";
            else sql = "select * from orders";
            ord_num.Text = searchMysql(sql, cp_name).ToString();

            // 查询并设置企业运行设备数
            if (cp_name != "平台所有设备总览" && cp_name != null) sql = "select * from equipment where EQP_state = '运行中' and EQP_ID in (select EQP_ID from cp_eqp where CP_ID = (select CP_ID from  company where CP_name = ('{0}')))";
            else sql = "select * from equipment where EQP_state = '运行中'";
            excute_ord_num.Text = searchMysql(sql, cp_name).ToString();

        }

        // 读取显示设备
        private void readLoadEQP(string id, string brand, string cat, string origin, string offon, string model, string process, string time, string state, string img)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/equipment/" + img + ".png")); //"pack://application:,,,/image/equipment/" + img + ".png"

            Grid grid = new Grid();
            grid.Margin = new Thickness(40, 30, 20, 50);
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(510) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(170) });
            grid.Background = brush;
            equipments.Children.Add(grid);
            grid.MouseLeftButtonDown += new MouseButtonEventHandler(mouseDown); // 设备绑定点击事件，显示详情页面

            // 设备详情点击事件
            void mouseDown(object sender, RoutedEventArgs e)
            {
                company_EQP detatilPage = new company_EQP();
                // 设备详情界面传值
                detatilPage.cp_name = company_Name.Text;
                detatilPage.EQP_ID = id;
                if (state == "运行中") detatilPage.state = 0;
                else if (state == "待机中") detatilPage.state = 1;
                else if (state == "暂停中") detatilPage.state = 2;
                else if (state == "异常停止") detatilPage.state = 3;
                // 导航至详情界面
                this.NavigationService.Navigate(detatilPage);
            }

            // 品牌
            TextBlock tx1 = new TextBlock();
            tx1.Margin = new Thickness(15, 36, 0, 0);
            tx1.Text = brand;
            tx1.FontSize = 16;
            tx1.Foreground = Brushes.White;

            // 设备种类
            TextBlock tx2 = new TextBlock();
            tx2.Margin = new Thickness(18, 76, 0, 0);
            tx2.Text = cat;
            tx2.FontSize = 16;
            tx2.Foreground = Brushes.White;

            // 产地
            TextBlock tx3 = new TextBlock();
            tx3.Margin = new Thickness(24, 115, 0, 0);
            tx3.Text = origin;
            tx3.FontSize = 16;
            tx3.Foreground = Brushes.White;

            // 在线状态
            TextBlock tx4 = new TextBlock();
            tx4.Margin = new Thickness(270, 6, 0, 0);
            if (offon == "1") tx4.Text = "Online";
            else if(offon == "0") tx4.Text = "Offline";
            tx4.FontSize = 16;
            tx4.Foreground = Brushes.GreenYellow;

            // 型号
            TextBlock tx5 = new TextBlock();
            tx5.Margin = new Thickness(270, 37, 0, 0);
            tx5.Text = "型号：" + model;
            tx5.FontSize = 16;
            tx5.Foreground = Brushes.White;

            // 加工率
            TextBlock tx6 = new TextBlock();
            tx6.Margin = new Thickness(270, 75, 0, 0);
            tx6.Text = "加工率：" + process.ToString() + "%";
            tx6.FontSize = 16;
            tx6.Foreground = Brushes.White;

            // 加工时间
            TextBlock tx7 = new TextBlock();
            tx7.Margin = new Thickness(270, 114, 0, 0);
            tx7.Text = "加工时间：" + time;
            tx7.FontSize = 16;
            tx7.Foreground = Brushes.White;


            // 加工时间
            TextBlock tx8_title = new TextBlock();
            TextBlock tx8 = new TextBlock();
            tx8_title.Margin = new Thickness(270, 146, 0, 0);
            tx8_title.Text = "设备状态：";
            tx8_title.Foreground = Brushes.White;
            tx8_title.FontSize = 16;
            tx8.Margin = new Thickness(355, 146, 0, 0);
            tx8.Text = state;
            if(state == "运行中") tx8.Foreground = Brushes.GreenYellow;    // GreenYellow(运行) Gray(待机) Red(异常停止) Yellow(暂时停止)
            else if(state == "待机中") tx8.Foreground = Brushes.Gray;
            else if(state == "暂停中") tx8.Foreground = Brushes.Yellow;
            else if(state == "异常停止") tx8.Foreground = Brushes.Red; 
            tx8.FontSize = 16;

            grid.Children.Add(tx1);
            grid.Children.Add(tx2);
            grid.Children.Add(tx3);
            grid.Children.Add(tx4);
            grid.Children.Add(tx5);
            grid.Children.Add(tx6);
            grid.Children.Add(tx7);
            grid.Children.Add(tx8_title);
            grid.Children.Add(tx8);

        }

        // 读取并生成每个公司的所有设备(去除内嵌网页需修改)
        private void companyEqpuipments(string name)
        {
            if (name == "our") company_Name.Text = "";
            else {
                company_Name.Text = name;
                MySqlConnection conn = new MySqlConnection(connectStr);
                conn.Open();
                String sql = "select * from equipment where EQP_ID in (select EQP_ID from cp_eqp where CP_ID = (select CP_ID from company where CP_name = ('{0}')))";
                sql = string.Format(sql, name);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string img = "EDM_ON";
                    string id = reader["EQP_ID"].ToString();
                    string brand = reader["EQP_brand"].ToString();
                    string cat = reader["EQP_category"].ToString();
                    string origin = reader["EQP_orgin"].ToString();
                    string offon = reader["EQP_offon"].ToString();
                    string model = reader["EQP_model"].ToString();
                    string process = reader["EQP_process"].ToString();
                    string time = reader["EQP_time"].ToString();
                    string state = reader["EQP_state"].ToString();
                    // 判断设备状态，调用不同的背景图片
                    if (state == "运行中" && (cat == "EMD" || cat == "CNC")) img = "EDM_RUN";
                    else if (state == "暂停中" && (cat == "EMD" || cat == "CNC")) img = "EDM_ON";
                    else if (state == "待机中" || state == "异常停止" && (cat == "EMD" || cat == "CNC")) img = "EDM_OFF";

                    if (state == "运行中" && cat == "EDW") img = "EDW_RUN";
                    else if (state == "暂停中" && cat == "EDW") img = "EDW_ON";
                    else if ((state == "待机中" || state == "异常停止") && cat == "EDW") img = "EDW_OFF";

                    readLoadEQP(id, brand, cat, origin, offon, model, process, time, state, img);
                }

            }
        }

        // 读取生成所有设备
        private void loadAllEQP()
        {
            MySqlConnection conn = new MySqlConnection(connectStr);
            try
            {
                conn.Open();
                string sql = "select * from equipment";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string img = "EDM_ON";
                    string id = reader["EQP_ID"].ToString();
                    string brand = reader["EQP_brand"].ToString();
                    string cat = reader["EQP_category"].ToString();
                    string origin = reader["EQP_orgin"].ToString();
                    string offon = reader["EQP_offon"].ToString();
                    string model = reader["EQP_model"].ToString();
                    string process = reader["EQP_process"].ToString();
                    string time = reader["EQP_time"].ToString();
                    string state = reader["EQP_state"].ToString();
                    // 判断设备状态，调用不同的背景图片
                    if (state == "运行中" && (cat == "EMD" || cat == "CNC")) img = "EDM_RUN";
                    else if (state == "暂停中" && (cat == "EMD" || cat == "CNC")) img = "EDM_ON";
                    else if (state == "待机中" || state == "异常停止" && (cat == "EMD" || cat == "CNC")) img = "EDM_OFF";

                    if (state == "运行中" && cat == "EDW") img = "EDW_RUN";
                    else if (state == "暂停中" && cat == "EDW") img = "EDW_ON";
                    else if ((state == "待机中" || state == "异常停止") && cat == "EDW") img = "EDW_OFF";

                    readLoadEQP(id, brand, cat, origin, offon, model, process, time, state, img);
                }
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
        }

        // 依据公司名展示设备(去除内嵌网页需修改)
        private void showEQP(string companyName)
        {
            if (companyName == our)
            {
                // 若是则公司名设为空
                companyEqpuipments(companyName);

                WebBrowser web = new WebBrowser();
                pageGrid.Children.Add(web);
                //pageGrid.Margin = new Thickness(20, 0, 0, 0);

                string url = "https://www.bilibili.com/";
                //string url = "http://192.168.1.243/Miemmerce/MachineList.aspx?page=1";
                Uri uri = new Uri((String)url);
                web.Navigate(uri);
            }
            else
            {
                pageGrid.Children.Clear();      // 若不是协同中心则移除webBrowser
                equipments.Children.Clear();    // 点击其他公司时删除上一个公司的所有设备
                companyEqpuipments(companyName);
            }
        }

        // 动态添加所有公司列表
        private void addCompanyList()
        {
            MySqlConnection conn = new MySqlConnection(connectStr);
            //    // 数据库连接
            try
            {
                conn.Open();
                string sql = "select * from company";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TextBlock tx = new TextBlock();
                    tx.Text = reader["CP_name"].ToString();
                    tx.FontSize = 24;
                    tx.Margin = new Thickness(15, 11, 15, 11);

                    tx.MouseLeftButtonDown += new MouseButtonEventHandler(TextBlock_click);
                    tx.MouseEnter += new MouseEventHandler(enter);
                    tx.MouseLeave += new MouseEventHandler(leave);
                    companyList.Children.Add(tx);
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
            
        }
        
        // 公司列表按钮点击事件
        private void TextBlock_click(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            // 得到公司名
            string companyName = tx.Text;
            // 赋值给页面全局变量
            cp_name = companyName;
            // 展示公司设备
            showEQP(companyName);
            // 展示公司设备统计信息
            company_info();
        }
        // 鼠标进入文本框事件
        private void enter(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.LawnGreen);
        }
        // 鼠标离开文本框事件
        private void leave(object sender, RoutedEventArgs e)
        {
            TextBlock tx = sender as TextBlock;
            tx.Foreground = new SolidColorBrush(Colors.White);
        }

        // 加载页面
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            addCompanyList();
            company_info();                 // 显示每个公司的设备信息，点企业进去则显示平台所有的设备
            if (cp_name == null || cp_name == "平台所有设备总览")
            {
                pageGrid.Children.Clear();
                company_Name.Text = "平台所有设备总览";
                loadAllEQP();
                //cp_name = "苏州协同智能制造创新中心";  // 默认公司页面的主页
            }
            else
            {
                showEQP(cp_name);               // 显示搜索公司设备
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
