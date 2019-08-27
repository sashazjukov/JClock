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

namespace JClock
{
    /// <summary>
    /// Interaction logic for HourElement.xaml
    /// </summary>
    public partial class HourElement : UserControl
    {
        public static readonly DependencyProperty hoourValueProperty = DependencyProperty.Register("hoourValue", typeof(int), typeof(HourElement), new PropertyMetadata(default(int), new PropertyChangedCallback(HourValueChanged)));

        private static void HourValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            (dependencyObject as HourElement).hourText.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
        }

        public HourElement()
        {
            InitializeComponent();
            hoourValue = 2;
        }

        public int hoourValue
        {
            get { return (int) GetValue(hoourValueProperty); }
            set { SetValue(hoourValueProperty, value); }
        }
    }
}
