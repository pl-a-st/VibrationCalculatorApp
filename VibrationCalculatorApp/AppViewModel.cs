using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibroMath;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace VibrationCalculatorApp {
    public enum Themes {
        Light,
        Black
    }
    public class AppViewModel : INotifyPropertyChanged {
        public static VibrationCalculatorApp.MainWindow MainWindow;
        static AppViewModel() {
            
        }
        public static Themes Themes {
            get;
            private set;
        } = Themes.Black;
        public static void ChangeTheme() {
            if (Themes == Themes.Black) {
                Themes = Themes.Light;
                return;
            }
            if (Themes == Themes.Light) {
                Themes = Themes.Black;
                return;
            }
        }
        public static string TAcc {
            get {
                return VibroCalc.Acceleration.GetRMS().ToString();
                //if ((bool)MainWindow.RAccRMS.IsChecked) {
                //    return VibroCalc.Acceleration.GetRMS().ToString();
                //}
                //if ((bool)MainWindow.RAccPik.IsChecked) {
                //    return VibroCalc.Acceleration.GetPIK().ToString();
                //}
                //if ((bool)MainWindow.RAccPikPik.IsChecked) {
                //    return VibroCalc.Acceleration.GetPIK_PIK().ToString();
                //}
                //return string.Empty;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
