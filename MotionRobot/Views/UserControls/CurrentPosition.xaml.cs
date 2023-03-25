
using System.Windows;
using System.Windows.Controls;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// CurrentPosition.xaml 的交互逻辑
    /// </summary>
    public partial class CurrentPosition : UserControl
    {
        public CurrentPosition()
        {
            InitializeComponent();
        }


        public double XPos
        {
            get { return (double)GetValue(XPosProperty); }
            set { SetValue(XPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XPosProperty =
            DependencyProperty.Register("XPos", typeof(double), typeof(CurrentPosition));

        public double YPos
        {
            get { return (double)GetValue(YPosProperty); }
            set { SetValue(YPosProperty, value); }
        }
        
        public static readonly DependencyProperty YPosProperty =
            DependencyProperty.Register("YPos", typeof(double), typeof(CurrentPosition));

        public double ZPos
        {
            get { return (double)GetValue(ZPosProperty); }
            set { SetValue(ZPosProperty, value); }
        }

        public static readonly DependencyProperty ZPosProperty =
            DependencyProperty.Register("ZPos", typeof(double), typeof(CurrentPosition));

    }
}
