using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Axede.WPF.Softphone.Applications.UtilControls.Loading
{
    /// <summary>
    /// Interaction logic for RectangleWP.xaml
    /// </summary>
    public partial class RectangleWP : UserControl
    {
        public RectangleWP()
        {
            InitializeComponent();
        }


        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                KeyFrame2.Value = this.ActualWidth / 3;
                KeyFrame3.Value = KeyFrame2.Value * 2;
                KeyFrame4.Value = this.ActualWidth + 10;
                KeyFrame5.Value = this.ActualWidth + 10;
            }
        }

        private double OffsetSecondsValue;

        public double OffsetSeconds
        {

            get
            {
                return this.OffsetSecondsValue;
            }
            set
            {
                this.OffsetSecondsValue = value;

                this.OffsetKeyFrameKeyTimes();

            }

        }

        /// <summary>

        /// Offsets the four keyframes of the animation with the set offset value. This 
        /// allows rectangles to be visually staggered if more than one are being used together.

        /// </summary>

        private void OffsetKeyFrameKeyTimes()
        {
            TimeSpan offset = TimeSpan.FromSeconds(OffsetSeconds);
            KeyFrame1.KeyTime = KeyFrame1.KeyTime.TimeSpan.Add(offset);
            KeyFrame2.KeyTime = KeyFrame2.KeyTime.TimeSpan.Add(offset);
            KeyFrame3.KeyTime = KeyFrame3.KeyTime.TimeSpan.Add(offset);
            KeyFrame4.KeyTime = KeyFrame4.KeyTime.TimeSpan.Add(offset);
        }
    }
}
