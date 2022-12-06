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

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// CurrentPositionCell.xaml 的交互逻辑
    /// </summary>
    public partial class CurrentPositionCell : UserControl
    {
        public CurrentPositionCell()
        {
            InitializeComponent();
        }
        public string AxisName
        {
            get { return (string)GetValue(AxisNameProperty); }
            set { SetValue(AxisNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisNameProperty =
            DependencyProperty.Register("AxisName", typeof(string), typeof(CurrentPositionCell));

        public double PosValue
        {
            get { return (double)GetValue(PosValueProperty); }
            set { SetValue(PosValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosValueProperty =
            DependencyProperty.Register("PosValue", typeof(double), typeof(CurrentPositionCell));


    }
}
