
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// SuckerCell.xaml 的交互逻辑
    /// </summary>
    public partial class SuckerCell : UserControl
    {
        public SuckerCell()
        {
            InitializeComponent();
        }
        public string MName
        {
            get { return (string)GetValue(MNameProperty); }
            set { SetValue(MNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MNameProperty =
            DependencyProperty.Register("MName", typeof(string), typeof(SuckerCell));



        public int MSelectedIndex
        {
            get { return (int)GetValue(MSelectedIndexProperty); }
            set { SetValue(MSelectedIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MSelectedIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MSelectedIndexProperty =
            DependencyProperty.Register("MSelectedIndex", typeof(int), typeof(SuckerCell));

        public ICommand PropertyChangedCommand
        {
            get { return (ICommand)GetValue(PropertyChangedCommandProperty); }
            set { SetValue(PropertyChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PropertyChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyChangedCommandProperty =
            DependencyProperty.Register("PropertyChangedCommand", typeof(ICommand), typeof(SuckerCell));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(SuckerCell));
    }
}
