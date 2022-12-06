
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// AxisPointSingle.xaml 的交互逻辑
    /// </summary>
    public partial class AxisPointSingle : UserControl
    {
        public AxisPointSingle()
        {
            InitializeComponent();
        }


        public string PointName
        {
            get { return (string)GetValue(PointNameProperty); }
            set { SetValue(PointNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PointName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointNameProperty =
            DependencyProperty.Register("PointName", typeof(string), typeof(AxisPointSingle));



        public double PosValue
        {
            get { return (double)GetValue(PosValueProperty); }
            set { SetValue(PosValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosValueProperty =
            DependencyProperty.Register("PosValue", typeof(double), typeof(AxisPointSingle));



        public ICommand GetPositionCommand
        {
            get { return (ICommand)GetValue(GetPositionCommandProperty); }
            set { SetValue(GetPositionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetPositionCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetPositionCommandProperty =
            DependencyProperty.Register("GetPositionCommand", typeof(ICommand), typeof(AxisPointSingle));



        public object PositionCommandParameter
        {
            get { return (object)GetValue(PositionCommandParameterProperty); }
            set { SetValue(PositionCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetPositionCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionCommandParameterProperty =
            DependencyProperty.Register("PositionCommandParameter", typeof(object), typeof(AxisPointSingle));



        public ICommand GoPositionCommand
        {
            get { return (ICommand)GetValue(GoPositionCommandProperty); }
            set { SetValue(GoPositionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoPositionCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoPositionCommandProperty =
            DependencyProperty.Register("GoPositionCommand", typeof(ICommand), typeof(AxisPointSingle));




        public ICommand SavePositionCommand
        {
            get { return (ICommand)GetValue(SavePositionCommandProperty); }
            set { SetValue(SavePositionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SavePositionCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SavePositionCommandProperty =
            DependencyProperty.Register("SavePositionCommand", typeof(ICommand), typeof(AxisPointSingle));




    }
}
