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
        public static readonly DependencyProperty TimeNowProperty = DependencyProperty.Register("TimeNow", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(default(DateTime)));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _timer = new Timer(Callback, null,0,1000);
            this.MouseDown +=Window_MouseDown;
            TimeNow = DateTime.Now;
            this.MouseMove+=OnMouseMove;
            this.MouseLeave+=OnMouseLeave;

            BClose.Visibility = Visibility.Collapsed;
            BTitle.Visibility = Visibility.Collapsed;
        }

        private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            BClose.Visibility = Visibility.Collapsed;
            BTitle.Visibility = Visibility.Collapsed;
        }

        private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            BClose.Visibility = Visibility.Visible;
            BTitle.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public DateTime TimeNow
        {
            get { return (DateTime) GetValue(TimeNowProperty); }
            set { SetValue(TimeNowProperty, value); }
        }

        private void Callback(object state)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { TimeNow = DateTime.Now; }));
            
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

        private void BClose_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BClose_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BClose.Background = Brushes.Transparent;
        }

        private void BClose_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BClose.Background = Brushes.Red;
            e.Handled = true;
        }
    }
}
