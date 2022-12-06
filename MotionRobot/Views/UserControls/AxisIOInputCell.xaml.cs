
using System.Windows;
using System.Windows.Controls;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// AxisIOInputCell.xaml 的交互逻辑
    /// </summary>
    public partial class AxisIOInputCell : UserControl
    {
        public AxisIOInputCell()
        {
            InitializeComponent();
        }


        public bool State
        {
            get { return (bool)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(bool), typeof(AxisIOInputCell));


        public string StateName
        {
            get { return (string)GetValue(StateNameProperty); }
            set { SetValue(StateNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StateName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateNameProperty =
            DependencyProperty.Register("StateName", typeof(string), typeof(AxisIOInputCell));



    }
}
