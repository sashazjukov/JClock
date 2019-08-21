using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace JClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer _timer;
        public static readonly DependencyProperty TimeNowProperty = DependencyProperty.Register("TimeNow", typeof(object), typeof(MainWindow), new PropertyMetadata(default(object)));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _timer = new Timer(Callback, null,0,1000);
            this.MouseDown +=Window_MouseDown;
            TimeNow = DateTime.Now.ToString("T");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public object TimeNow
        {
            get { return (object) GetValue(TimeNowProperty); }
            set { SetValue(TimeNowProperty, value); }
        }

        private void Callback(object state)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { TimeNow = DateTime.Now.ToString("T"); }));
            
        }
    }
}
