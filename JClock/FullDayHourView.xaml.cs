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
    /// Interaction logic for FullDayHourView.xaml
    /// </summary>
    public partial class FullDayHourView : UserControl
    {
        private IList<HourElement> houerElements = new List<HourElement>();

        public void UpdateHourProgress(DateTime dateTime)
        {
            int hour = dateTime.Hour-6;
            if (hour > 12)
            {
                hour = 12;

            }
            int i;
            for (i = 0; i < hour; i++)
            {
                houerElements[i].HourProgressElement.Value = 60;
                houerElements[i].hourText.FontWeight = FontWeights.Light;
            }
            for (i = hour; i < 12; i++)
            {
                houerElements[i].HourProgressElement.Value = 0;
                houerElements[i].hourText.FontWeight = FontWeights.Light;
            }
            // curent hour
            if (hour < 12)
            {
                houerElements[hour].HourProgressElement.Value = dateTime.Minute;
                houerElements[hour].hourText.FontWeight = FontWeights.Bold;
            }
            else
            {
                houerElements[hour-1].HourProgressElement.Value = 60;
                houerElements[hour-1].hourText.FontWeight = FontWeights.Light;
            }
        }

        public FullDayHourView()
        {
            InitializeComponent();            
            int p = 0;
            for (int i = 0; i < 12; i++)
            {
                HourElement hourElement = new HourElement();
                houerElements.Add(hourElement);
                ParentGrid.Children.Add(hourElement);

                //rectangle.hourText.Text = (i+6).ToString();
                hourElement.hoourValue = (i+6);
                Grid.SetColumn(hourElement, p);
                if ((i + 1) % 3 == 0) p++;
                p++;                
            } 
            UpdateHourProgress(DateTime.Now);
        }
    }
}
