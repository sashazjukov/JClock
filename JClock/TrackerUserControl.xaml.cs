using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using TimeTracker.ViewModels;

namespace TimeTracker
{
    public sealed partial class TrackerUserControl : UserControl
    {
        public TrackerUserControl()
        {
            this.InitializeComponent();
            this.Loaded+=OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            StoryboardBorder.Begin();
        }

        public static readonly DependencyProperty TrackerStateProperty =
            DependencyProperty.Register("TrackerState", typeof (TrackerStates), typeof (TrackerUserControl), new PropertyMetadata(default(TrackerStates),PropertyChanged));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = d as TrackerUserControl;
            
            TrackerStates trackerStates = e.NewValue is TrackerStates ? (TrackerStates) e.NewValue : TrackerStates.Normal;
            switch (trackerStates)
            {
                case TrackerStates.Normal:
                    VisualStateManager.GoToState(control, "Normal", true);
                    break;
                case TrackerStates.CoffeBracke:
                    VisualStateManager.GoToState(control, "CoffeBrake", true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public TrackerStates TrackerState
        {
            get { return (TrackerStates) GetValue(TrackerStateProperty); }
            set { SetValue(TrackerStateProperty, value); }
        }
    }
}
