using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Geared;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace suzhou.ViewModel
{
    class Alarm
    {
        private string _ID;
        private string _Date;
        private string _Type;

        public Alarm(string ID, string Date, string Type)
        {
            _ID = ID;
            _Date = Date;
            _Type = Type;
        }
    
        public string ID { get => _ID; set => _ID = value; }
        public string Date { get => _Date; set => _Date = value; }
        public string Type { get => _Type; set => _Type = value; }
    }

    class ScrollableViewModel : INotifyPropertyChanged
    {
        // 数据库连接
        String connectServer = "server=121.239.200.28; port=3306; user=root; password=Szxtzx06090512.@; database=mould;";

        // 滚动折线图属性
        private double _points;
        private List<string> _xStick;
        private SeriesCollection _seriesCollection;

        // 柱状图属性
        private List<string> _barStick;    // 柱状图X轴刻度
        private SeriesCollection _seriesCollection2;    // 柱状图数据

        public ScrollableViewModel()
        {
            _points = 2000;
            Quality = Quality.Medium;
            //From = "0";
            //To = "999";

            Go();

            // 给报警列表alarmList赋值
            findAlarm();
        }

        // 折线图
        public void Go()
        {
            Points = Math.Truncate(Points);
            xStick = new List<string>();

            var ar = new double[(int)Points];

            var r = new Random();
            var trend = 0d;

            for (var i = 0; i < Points; i++)
            {
                ar[i] = trend;

                if (r.NextDouble() > 0.5)
                {
                    trend += r.NextDouble() * 10;
                }
                else
                {
                    trend -= r.NextDouble() * 10;
                }

                // 生成坐标轴
                string temp = "第" + i.ToString() + "点";
                xStick.Add(temp);
            }

            var series = new GLineSeries()
            {
                Values = ar.AsGearedValues()
            };

            SeriesCollection = new SeriesCollection
            {
                series
            };

            //reset axis limits
            //From = "0";
            //To = "999";
        }

        public void updataScrollableVM(List<List<String>> allData)
        {
            Points = allData.Count;
            xStick = new List<string>();
            var ar1 = new double[(int)Points];  // 当前周期时间
            var ar2 = new double[(int)Points];  // 平均周期时间

            for (int i = 0; i < allData.Count; i++)
            {
                xStick.Add(allData[i][0]);  // 添加时间字符串作为X轴
                ar1[i] = System.Convert.ToDouble(allData[i][2]);    // 添加当前周期时间的数据
                ar2[i] = System.Convert.ToDouble(allData[i][3]);    // 添加平均周期时间的数据
            }

            var series1 = new GLineSeries() // 当前周期时间曲线
            {
                Title = "当前周期时间",
                Stroke = Brushes.Red,
                Values = ar1.AsGearedValues()
            };
            var series2 = new GLineSeries() // 平均周期时间曲线
            {
                Title = "平均周期时间",
                Values = ar2.AsGearedValues()
            };
            SeriesCollection = new SeriesCollection
            {
                series1, series2    // 添加两条曲线
            };
        }

        public Quality Quality { get; set; }

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public double Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged();
            }
        }

        public List<string> xStick
        {
            get { return _xStick; }
            set
            {
                _xStick = value;
                OnPropertyChanged();
            }
        }

        // 柱状图
        public void getBarStick(List<List<String>> allData)
        {
            if(allData.Count != 0)
            {

                barStick = new List<string>();
                var temp = Convert.ToDateTime(allData[0][0]).ToShortDateString();
                DateTime beginDate = Convert.ToDateTime(temp);
                barStick.Add(beginDate.ToShortDateString());
                for (int i = 1; i <= 6; i++)
                {
                    if (beginDate.AddDays(i) <= DateTime.Now)
                    {
                        string addDate = beginDate.AddDays(i).ToShortDateString();
                        barStick.Add(addDate);
                    }
                    else break;
                }

                var bar1 = new double[barStick.Count];  // 活动时间
                var bar2 = new double[barStick.Count];  // 怠速时间
                var bar3 = new double[barStick.Count];  // 睡眠时间

                int index = 0;  // 记录数据下标
                for (int i = 1; i <= 7; i++)
                {
                    if(i <= barStick.Count)
                    {
                        List<List<String>> dayData = new List<List<string>>();
                        for (int j = index; j < allData.Count; j++)
                        {
                            DateTime curDate = Convert.ToDateTime(allData[j][0]);
                            if (curDate <= beginDate.AddDays(i))
                            {
                                dayData.Add(allData[j]);
                                index++;
                            }
                            else {
                                Console.WriteLine(index.ToString() + " " + dayData.Count.ToString());
                                break;
                            }
                        }
                        //Console.WriteLine(index.ToString() + " " + dayData.Count.ToString());
                        // 统计一天时间内的活动时间，怠速时间和睡眠时间
                        double[] res = calculateTime(dayData);
                        bar1[i - 1] = res[0];
                        bar2[i - 1] = res[1];
                        bar3[i - 1] = res[2];
                    }
                }

                var series1 = new ColumnSeries()
                {
                    Title = "活动时间",
                    Fill = Brushes.ForestGreen,
                    Values = new ChartValues<double>(bar1)
                };
                var series2 = new ColumnSeries()
                {
                    Title = "怠速时间",
                    Values = new ChartValues<double>(bar2)
                };
                var series3 = new ColumnSeries()
                {
                    Title = "睡眠时间",
                    Values = new ChartValues<double>(bar3)
                };
                SeriesCollection2 = new SeriesCollection
                {
                    series1, series2, series3
                };
            }

        }

        public List<string> barStick
        {
            get { return _barStick; }
            set
            {
                _barStick = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection SeriesCollection2
        {
            get { return _seriesCollection2; }
            set
            {
                _seriesCollection2 = value;
                OnPropertyChanged();
            }
        }


        private double[] calculateTime(List<List<String>> dayData)
        {
            double[] res = new double[3];   // 最后返回的统计结果数组

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

            for (int i = 0; i < dayData.Count; i++)
            {
                // 计算移除时间
                if (!removedFlag && dayData[i][8].Equals("Removed"))
                {
                    removedFlag = true;
                    removedStart = dayData[i][0];
                }
                if (removedFlag && dayData[i][8].Equals("Unremove"))
                {
                    removedFlag = false;
                    removedEnd = dayData[i][0];
                    removedTime += diffTime(removedStart, removedEnd);
                }

                // 计算怠速时间
                if (!idlingFlag && double.Parse(dayData[i][2]) >= 600)
                {
                    idlingFlag = true;
                    idlingStart = dayData[i][0];
                }
                if (idlingFlag && double.Parse(dayData[i][2]) < 600)
                {
                    idlingFlag = false;
                    idlingEnd = dayData[i][0];
                    idlingTime += diffTime(idlingStart, idlingEnd);
                }
            }
            if (dayData.Count > 0)
            {
                workingTime = diffTime(dayData[0][0], dayData[dayData.Count - 1][0]) - removedTime - idlingTime;
            }
            res[0] = workingTime;
            res[1] = idlingTime;
            res[2] = removedTime;
            return res;
        }

        // 计算时间差
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


        // 统计报警信息
        ObservableCollection<Alarm> _alarmList = new ObservableCollection<Alarm>();
        public ObservableCollection<Alarm> alarmList
        {
            set { _alarmList = value; RaisePropertyChanged("alarmList"); }
            get { return _alarmList; }
        }

        // 找到报警信息
        public ObservableCollection<Alarm> findAlarm()
        {
            List<string> IDs = new List<string>();
            MySqlConnection conn = new MySqlConnection(connectServer);
            conn.Open();
            String sql = "show tables";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                IDs.Add(reader.GetString(0));
            }
            reader.Close();
            for(int i = 0; i < IDs.Count; i++)
            {
                sql = "select DtTm, Dis, ApChange from `" + IDs[i] + "` where Dis = 'Removed' or ApChange = 'Changed' order by DtTm desc";
                cmd = new MySqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(1).Equals("Removed"))
                    {
                        alarmList.Add(new Alarm(IDs[i], reader.GetString(0), "模具已移除"));
                    }
                    else
                    {
                        alarmList.Add(new Alarm(IDs[i], reader.GetString(0), "IP已改变"));
                    }
                }
                reader.Close();
            }
            conn.Close();
            return alarmList;
        }


        // 图的PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 报警信息的PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged_Alarm;
        public void RaisePropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                PropertyChanged_Alarm.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
