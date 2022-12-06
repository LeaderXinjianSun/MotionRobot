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

        public double Z1Pos
        {
            get { return (double)GetValue(Z1PosProperty); }
            set { SetValue(Z1PosProperty, value); }
        }

        public static readonly DependencyProperty Z1PosProperty =
            DependencyProperty.Register("Z1Pos", typeof(double), typeof(CurrentPosition));

        public double Z2Pos
        {
            get { return (double)GetValue(Z2PosProperty); }
            set { SetValue(Z2PosProperty, value); }
        }

        public static readonly DependencyProperty Z2PosProperty =
            DependencyProperty.Register("Z2Pos", typeof(double), typeof(CurrentPosition));

        public double Z3Pos
        {
            get { return (double)GetValue(Z3PosProperty); }
            set { SetValue(Z3PosProperty, value); }
        }

        public static readonly DependencyProperty Z3PosProperty =
            DependencyProperty.Register("Z3Pos", typeof(double), typeof(CurrentPosition));

        public double Z4Pos
        {
            get { return (double)GetValue(Z4PosProperty); }
            set { SetValue(Z4PosProperty, value); }
        }

        public static readonly DependencyProperty Z4PosProperty =
            DependencyProperty.Register("Z4Pos", typeof(double), typeof(CurrentPosition));

        public double R1Pos
        {
            get { return (double)GetValue(R1PosProperty); }
            set { SetValue(R1PosProperty, value); }
        }

        public static readonly DependencyProperty R1PosProperty =
            DependencyProperty.Register("R1Pos", typeof(double), typeof(CurrentPosition));

        public double R2Pos
        {
            get { return (double)GetValue(R2PosProperty); }
            set { SetValue(R2PosProperty, value); }
        }

        public static readonly DependencyProperty R2PosProperty =
            DependencyProperty.Register("R2Pos", typeof(double), typeof(CurrentPosition));
    }
}
