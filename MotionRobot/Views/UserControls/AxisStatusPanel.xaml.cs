
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// AxisStatusPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AxisStatusPanel : UserControl
    {
        public AxisStatusPanel()
        {
            InitializeComponent();
        }


        public bool HomeState
        {
            get { return (bool)GetValue(HomeStateProperty); }
            set { SetValue(HomeStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HomeState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HomeStateProperty =
            DependencyProperty.Register("HomeState", typeof(bool), typeof(AxisStatusPanel));



        public bool PosLimitState
        {
            get { return (bool)GetValue(PosLimitStateProperty); }
            set { SetValue(PosLimitStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PosLimitState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PosLimitStateProperty =
            DependencyProperty.Register("PosLimitState", typeof(bool), typeof(AxisStatusPanel));



        public bool NeglimitState
        {
            get { return (bool)GetValue(NeglimitStateProperty); }
            set { SetValue(NeglimitStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NeglimitState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NeglimitStateProperty =
            DependencyProperty.Register("NeglimitState", typeof(bool), typeof(AxisStatusPanel));



        public bool AlmState
        {
            get { return (bool)GetValue(AlmStateProperty); }
            set { SetValue(AlmStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlmState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlmStateProperty =
            DependencyProperty.Register("AlmState", typeof(bool), typeof(AxisStatusPanel));



        public bool ServoOnState
        {
            get { return (bool)GetValue(ServoOnStateProperty); }
            set { SetValue(ServoOnStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ServoOnState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServoOnStateProperty =
            DependencyProperty.Register("ServoOnState", typeof(bool), typeof(AxisStatusPanel));



        public bool MoveState
        {
            get { return (bool)GetValue(MoveStateProperty); }
            set { SetValue(MoveStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MoveState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoveStateProperty =
            DependencyProperty.Register("MoveState", typeof(bool), typeof(AxisStatusPanel));



        public ICommand ClearAlarmCommand
        {
            get { return (ICommand)GetValue(ClearAlarmCommandProperty); }
            set { SetValue(ClearAlarmCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClearAlarmCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClearAlarmCommandProperty =
            DependencyProperty.Register("ClearAlarmCommand", typeof(ICommand), typeof(AxisStatusPanel));



        public object ClearAlarmCommandParameter
        {
            get { return (object)GetValue(ClearAlarmCommandParameterProperty); }
            set { SetValue(ClearAlarmCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClearAlarmCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClearAlarmCommandParameterProperty =
            DependencyProperty.Register("ClearAlarmCommandParameter", typeof(object), typeof(AxisStatusPanel));



        public ICommand ServoOnCommand
        {
            get { return (ICommand)GetValue(ServoOnCommandProperty); }
            set { SetValue(ServoOnCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ServoOnCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServoOnCommandProperty =
            DependencyProperty.Register("ServoOnCommand", typeof(ICommand), typeof(AxisStatusPanel));



        public object ServoOnCommandParameter
        {
            get { return (object)GetValue(ServoOnCommandParameterProperty); }
            set { SetValue(ServoOnCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ServoOnCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServoOnCommandParameterProperty =
            DependencyProperty.Register("ServoOnCommandParameter", typeof(object), typeof(AxisStatusPanel));


    }
}
