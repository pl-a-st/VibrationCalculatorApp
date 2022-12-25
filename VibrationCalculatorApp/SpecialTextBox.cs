using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections;
using System.ComponentModel;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows;

namespace VibrationCalculatorApp {
    public enum Access {
        ForUser,
        Blocked
    }
    [ContentProperty("Text")]
    [Localizability(LocalizationCategory.Text)]
    public class SpecialTextBox : TextBox {
        private string LastText;
        public Access Access = Access.ForUser;
        protected override void OnTextChanged(TextChangedEventArgs e) {
            if (Access == Access.ForUser) {
                if (!double.TryParse(Text, out double result) && Text != "") {
                    Access = Access.Blocked;
                    Text = LastText;
                    Access = Access.ForUser;
                    this.SelectionStart = Text.Length;
                    return;
                }
                LastText = Text;
                base.OnTextChanged(e);
                Access = Access.Blocked;
                Text = (double.Parse(Text) / 2).ToString();
                Access = Access.ForUser;
                return;
            }
            base.OnTextChanged(e);
        }

    }
}

