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
            Loaded += async (sender, args) => await ViewModel.Load();
        }

        private void Hue_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            ViewModel.UpdateColor();
        }
    }
}
