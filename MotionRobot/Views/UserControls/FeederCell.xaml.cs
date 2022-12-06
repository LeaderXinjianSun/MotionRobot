
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MotionRobot.Views.UserControls
{
    /// <summary>
    /// FeederCell.xaml 的交互逻辑
    /// </summary>
    public partial class FeederCell : UserControl
    {
        public FeederCell()
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
            DependencyProperty.Register("MName", typeof(string), typeof(FeederCell));






        public ICommand PropertyChangedCommand
        {
            get { return (ICommand)GetValue(PropertyChangedCommandProperty); }
            set { SetValue(PropertyChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PropertyChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyChangedCommandProperty =
            DependencyProperty.Register("PropertyChangedCommand", typeof(ICommand), typeof(FeederCell));





        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(FeederCell));



        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(FeederCell));







        public double DeltaX
        {
            get { return (double)GetValue(DeltaXProperty); }
            set { SetValue(DeltaXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeltaX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeltaXProperty =
            DependencyProperty.Register("DeltaX", typeof(double), typeof(FeederCell));



        public double DeltaY
        {
            get { return (double)GetValue(DeltaYProperty); }
            set { SetValue(DeltaYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeltaY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeltaYProperty =
            DependencyProperty.Register("DeltaY", typeof(double), typeof(FeederCell));



        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(FeederCell));



    }
}
