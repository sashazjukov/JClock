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
        private int _hourShift;
        private bool _inititlized = false;

        public void UpdateHourProgress(DateTime dateTime)
        {
            
            if (!(dateTime.Second == 0 || _inititlized == false)) return;
            _inititlized = true;
            int hour = dateTime.Hour- _hourShift;
            if (hour > 24)
            {
                hour = 24;

            }
            if (hour < 0)
            {
                hour = 24 + hour;
            }
            hour = dateTime.Hour;

            int i;
            //for (i = 0; i < hour; i++)
            //{
            //    houerElements[i].HourProgressElement.Value = 60;
            //    houerElements[i].hourText.FontWeight = FontWeights.Light;
            //}
            //for (i = hour; i < 24; i++)
            //{
            //    houerElements[i].HourProgressElement.Value = 0;
            //    houerElements[i].hourText.FontWeight = FontWeights.Light;
            //}
            //// curent hour
            //if (hour < 24)
            //{
            //    houerElements[hour].HourProgressElement.Value = dateTime.Minute;
            //    houerElements[hour].hourText.FontWeight = FontWeights.Bold;
            //}
            //else
            //{
            //    houerElements[hour-1].HourProgressElement.Value = 60;
            //    houerElements[hour-1].hourText.FontWeight = FontWeights.Light;

            for (i = 0; i < 24; i++)
            {
                if (houerElements[i].hoourValue < hour)
                {
                    houerElements[i].HourProgressElement.Value = 60;
                    houerElements[i].hourText.FontWeight = FontWeights.Light;
                }
                if (houerElements[i].hoourValue > hour)
                {
                    houerElements[i].HourProgressElement.Value = 0;
                    houerElements[i].hourText.FontWeight = FontWeights.Light;
                }
                if (houerElements[i].hoourValue == hour)
                {
                    houerElements[i].HourProgressElement.Value = dateTime.Minute;
                    houerElements[i].hourText.FontWeight = FontWeights.Bold;  
                }

            }
        }

        public FullDayHourView()
        {
            InitializeComponent();
            _hourShift = 8;
            int col = 0;
            int row = 0;
            int counter = 0;
            int hourElementHoourValue = _hourShift;
            for (int i = 0; i < 24; i++)
            {
                HourElement hourElement = new HourElement();
                houerElements.Add(hourElement);
                ParentGrid.Children.Add(hourElement);

                //rectangle.hourText.Text = (i+6).ToString();

                
                hourElement.hoourValue = hourElementHoourValue;
                Grid.SetColumn(hourElement, col);
                Grid.SetRow(hourElement, row);
                if ((counter + 1) % 3 == 0) col++;
                counter++;
                col++;
                if (i == 11) {
                    row += 2;
                    col = 0;
                    counter = 0;
                }
                hourElementHoourValue++;
                if (hourElementHoourValue == 24)
                {
                    hourElementHoourValue = 0;                    
                }

            } 
            UpdateHourProgress(DateTime.Now);
        }
    }
}
