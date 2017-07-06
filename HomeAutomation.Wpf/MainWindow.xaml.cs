using System.Windows;
using System.Windows.Controls.Primitives;
using HomeAutomation.Wpf.Model;

namespace HomeAutomation.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public HomeModel ViewModel => DataContext as HomeModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hue_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            ViewModel.UpdateColor();
        }

        private void Hue_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ViewModel.UpdateColor();
        }
    }


}
