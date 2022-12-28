using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibrationCalculatorApp {
    public enum Themes {
        Light,
        Black
    }
    public static class AppViewModel {
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
    }
}
