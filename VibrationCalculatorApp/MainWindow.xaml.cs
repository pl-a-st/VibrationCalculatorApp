﻿using System;
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
            DragMove();
        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
           
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
           
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
           
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) {

        }

        private void Grid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
