using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
            _timer = new Timer(Callback, null, 0, 1000);
            this.MouseDown += Window_MouseDown;
            TimeNow = DateTime.Now;
            this.MouseMove += OnMouseMove;
            this.MouseLeave += OnMouseLeave;

            BClose.Visibility = Visibility.Collapsed;
            BTitle.Visibility = Visibility.Collapsed;
            BTitle.Content = "JClock 1.0";
            this.Loaded += Window_Loaded;
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
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                TimeNow = DateTime.Now;
                FullDayHourViewElement.UpdateHourProgress(TimeNow);
            }));

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


        internal enum AccentState
        {
            ACCENT_DISABLED = 1,
            ACCENT_ENABLE_GRADIENT = 0,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19

            // ...
        }

        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }

        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }


    }


}
