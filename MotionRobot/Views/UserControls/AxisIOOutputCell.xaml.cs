
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// AxisIOOutputCell.xaml 的交互逻辑
    /// </summary>
    public partial class AxisIOOutputCell : UserControl
    {
        public AxisIOOutputCell()
        {
            InitializeComponent();
        }


        public string StateName
        {
            get { return (string)GetValue(StateNameProperty); }
            set { SetValue(StateNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StateName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateNameProperty =
            DependencyProperty.Register("StateName", typeof(string), typeof(AxisIOOutputCell));





        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(AxisIOOutputCell));

        public ICommand OutCommand
        {
            get { return (ICommand)GetValue(OutCommandProperty); }
            set { SetValue(OutCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutCommandProperty =
            DependencyProperty.Register("OutCommand", typeof(ICommand), typeof(AxisIOOutputCell));



        public int OutCommandParameter
        {
            get { return (int)GetValue(OutCommandParameterProperty); }
            set { SetValue(OutCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutCommandParameterProperty =
            DependencyProperty.Register("OutCommandParameter", typeof(int), typeof(AxisIOOutputCell));


    }
}
