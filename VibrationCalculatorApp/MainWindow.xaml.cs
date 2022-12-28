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
using System.Windows.Media.Animation;

namespace VibrationCalculatorApp {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void TSensitivity_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void BtnMinimizate_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {

        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {

        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {

        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) {

        }

        private void Grid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) {
            if (this.WindowState == WindowState.Maximized) {
                this.WindowState = WindowState.Normal;
                this.Top = 0;
            }
            DragMove();
        }

        private void Rectangle_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {


        }

        private void Grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e) {

        }

        private void Grid_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e) {
            ThicknessAnimation teamAnimation = new ThicknessAnimation();
            
            teamAnimation.From = TeamButt.Margin;
            double newMarginLeft = 0;
            string themeName = "Dark.xaml";
            if (AppViewModel.Themes == Themes.Black) {
                newMarginLeft = TeamButt.Margin.Left + FonTemButt.ActualWidth - (FonTemButt.ActualHeight - TeamButt.ActualHeight) - TeamButt.ActualWidth;
                themeName = "Light.xaml";
            }
            if (AppViewModel.Themes == Themes.Light) {
                newMarginLeft = TeamButt.Margin.Left - FonTemButt.ActualWidth + (FonTemButt.ActualHeight - TeamButt.ActualHeight) + TeamButt.ActualWidth;
                themeName = "Dark.xaml";
            }
            teamAnimation.To = new Thickness(
                newMarginLeft,
                TeamButt.Margin.Top,
                TeamButt.Margin.Right,
                TeamButt.Margin.Bottom);
            teamAnimation.Duration = TimeSpan.FromMilliseconds(500);
            TeamButt.BeginAnimation(Rectangle.MarginProperty, teamAnimation);
            AppViewModel.ChangeTheme();
            var uri = new Uri(themeName, UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
