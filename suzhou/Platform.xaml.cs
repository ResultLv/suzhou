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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace suzhou
{
    /// <summary>
    /// frame.xaml 的交互逻辑
    /// </summary>
    public partial class Platform : Window
    {
        public String username { get; set; }    //设置属性username
        public DispatcherTimer ShowTimer;
        public Platform()
        {
            InitializeComponent();

            ShowTime();    //在这里窗体加载的时候不执行文本框赋值，窗体上不会及时的把时间显示出来，而是等待了片刻才显示了出来
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler(ShowCurTimer);//起个Timer一直获取当前时间
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
        }
        // 显示当前时间
        private void ShowCurTimer(object sender, EventArgs e)
        {
            ShowTime();
        }

        private void ShowTime()
        {
            txt_Time.Text = DateTime.Now.ToString();
        }
        // 窗口加载时显示欢迎信息
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_User.Text = username + ",欢迎您!";

        }

        // 确认注销
        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            Logout logout = new Logout();
            logout.ShowDialog();
        }
        // 取消注销
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 菜单点击切换Page事件
        private void home_Click(object sender, RoutedEventArgs e)
        {
            this.framePage.Navigate(new Uri("./pages/home.xaml", UriKind.Relative));

        }
        public void home_enter(object sender, RoutedEventArgs e)
        {
            home.Foreground = new SolidColorBrush(Color.FromRgb(5, 199, 247));
            home.FontSize = 40;
        }
        public void home_leave(object sender, RoutedEventArgs e)
        {
            home.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            home.FontSize = 28;
        }

        private void firm_Click(object sender, RoutedEventArgs e)
        {
            this.framePage.Navigate(new Uri("./pages/company.xaml", UriKind.Relative));
        }
        public void firm_enter(object sender, RoutedEventArgs e)
        {
            firm.Foreground = new SolidColorBrush(Color.FromRgb(5, 199, 247));
            firm.FontSize = 40;
        }
        public void firm_leave(object sender, RoutedEventArgs e)
        {
            firm.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            firm.FontSize = 28;
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            this.framePage.Navigate(new Uri("./pages/order.xaml", UriKind.Relative));
        }
        public void order_enter(object sender, RoutedEventArgs e)
        {
            order.Foreground = new SolidColorBrush(Color.FromRgb(5, 199, 247));
            order.FontSize = 40;
        }
        public void order_leave(object sender, RoutedEventArgs e)
        {
            order.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            order.FontSize = 28;
        }

        private void mould_Click(object sender, RoutedEventArgs e)
        {
            this.framePage.Navigate(new Uri("./pages/mould.xaml", UriKind.Relative));
        }
        public void mould_enter(object sender, RoutedEventArgs e)
        {
            mould.Foreground = new SolidColorBrush(Color.FromRgb(5, 199, 247));
            mould.FontSize = 40;
        }
        public void mould_leave(object sender, RoutedEventArgs e)
        {
            mould.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            mould.FontSize = 28;
        }

        private void logistics_Click(object sender, RoutedEventArgs e)
        {
            this.framePage.Navigate(new Uri("./pages/logistics.xaml", UriKind.Relative));
        }
        public void logistics_enter(object sender, RoutedEventArgs e)
        {
            logistics.Foreground = new SolidColorBrush(Color.FromRgb(5, 199, 247));
            logistics.FontSize = 40;
        }
        public void logistics_leave(object sender, RoutedEventArgs e)
        {
            logistics.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            logistics.FontSize = 28;
        }

    }
}
