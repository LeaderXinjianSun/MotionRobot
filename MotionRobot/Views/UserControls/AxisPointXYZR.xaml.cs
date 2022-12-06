
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// AxisPointXYZR.xaml 的交互逻辑
    /// </summary>
    public partial class AxisPointXYZR : UserControl
    {
        public AxisPointXYZR()
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
            DependencyProperty.Register("PointName", typeof(string), typeof(AxisPointXYZR));



        public double XPosValue
        {
            get { return (double)GetValue(XPosValueProperty); }
            set { SetValue(XPosValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XPosValueProperty =
            DependencyProperty.Register("XPosValue", typeof(double), typeof(AxisPointXYZR));

        public double YPosValue
        {
            get { return (double)GetValue(YPosValueProperty); }
            set { SetValue(YPosValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YPosValueProperty =
            DependencyProperty.Register("YPosValue", typeof(double), typeof(AxisPointXYZR));
        public double ZPosValue
        {
            get { return (double)GetValue(ZPosValueProperty); }
            set { SetValue(ZPosValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZPosValueProperty =
            DependencyProperty.Register("ZPosValue", typeof(double), typeof(AxisPointXYZR)); 
        public double RPosValue
        {
            get { return (double)GetValue(RPosValueProperty); }
            set { SetValue(RPosValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RPosValueProperty =
            DependencyProperty.Register("RPosValue", typeof(double), typeof(AxisPointXYZR));

        public ICommand GetPositionCommand
        {
            get { return (ICommand)GetValue(GetPositionCommandProperty); }
            set { SetValue(GetPositionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetPositionCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetPositionCommandProperty =
            DependencyProperty.Register("GetPositionCommand", typeof(ICommand), typeof(AxisPointXYZR));



        public object GetPositionCommandParameter
        {
            get { return (object)GetValue(GetPositionCommandParameterProperty); }
            set { SetValue(GetPositionCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetPositionCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetPositionCommandParameterProperty =
            DependencyProperty.Register("GetPositionCommandParameter", typeof(object), typeof(AxisPointXYZR));



        public ICommand GoPositionCommand
        {
            get { return (ICommand)GetValue(GoPositionCommandProperty); }
            set { SetValue(GoPositionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoPositionCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoPositionCommandProperty =
            DependencyProperty.Register("GoPositionCommand", typeof(ICommand), typeof(AxisPointXYZR));



        public object GoPositionCommandParameter
        {
            get { return (object)GetValue(GoPositionCommandParameterProperty); }
            set { SetValue(GoPositionCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoPositionCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoPositionCommandParameterProperty =
            DependencyProperty.Register("GoPositionCommandParameter", typeof(object), typeof(AxisPointXYZR));



        public ICommand JumpPositionCommand
        {
            get { return (ICommand)GetValue(JumpPositionCommandProperty); }
            set { SetValue(JumpPositionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JumpPositionCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JumpPositionCommandProperty =
            DependencyProperty.Register("JumpPositionCommand", typeof(ICommand), typeof(AxisPointXYZR));



        public bool JumpPositionCommandParameter
        {
            get { return (bool)GetValue(JumpPositionCommandParameterProperty); }
            set { SetValue(JumpPositionCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for JumpPositionCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JumpPositionCommandParameterProperty =
            DependencyProperty.Register("JumpPositionCommandParameter", typeof(bool), typeof(AxisPointXYZR));


    }
}
