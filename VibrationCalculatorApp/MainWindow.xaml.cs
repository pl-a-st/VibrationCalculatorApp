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
using System.Windows.Media.Animation;
using VibroMath;
using System.Windows.Documents;
using Metrology;


namespace VibrationCalculatorApp
{
    public enum Pocess
    {
        InPocess,
        Finish
    }
    public enum Parametr
    {
        Acceleration,
        Acceleration_dB,
        VeloCity,
        VeloCity_dB,
        Displacment,
        Displacment_dB,
        Voltage,
        Voltage_dB,
        Sensitivity,
        Frequency
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Pocess InitializeComponentStatus = Pocess.InPocess;
        public Pocess ChangeTextbox = Pocess.Finish;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponentStatus = Pocess.Finish;
            PushAllTexbox();
        }
        private Freeze TakeFreeze()
        {
            VibroMath.Freeze freeze = VibroMath.Freeze.Acceleration;
            if (RFreezAcc.IsChecked ?? false)
            {
                freeze = VibroMath.Freeze.Acceleration;
            }
            if (RFreezVel.IsChecked ?? false)
            {
                freeze = VibroMath.Freeze.Velocity;
            }
            if (RFreezDis.IsChecked ?? false)
            {
                freeze = VibroMath.Freeze.Displacement;
            }
            if (RFreezVolt.IsChecked ?? false)
            {
                freeze = VibroMath.Freeze.Voltage;
            }

            return freeze;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnMinimizate_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.Top = 0;
            }
            DragMove();
        }

        private void Rectangle_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void Grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            ThicknessAnimation themeAnimation = new ThicknessAnimation();

            themeAnimation.From = TeamButt.Margin;
            double newMarginLeft = 0;
            string themeName = "Dark.xaml";
            if (AppViewModel.Themes == Themes.Black)
            {
                newMarginLeft = FonTemButt.Margin.Left + FonTemButt.ActualWidth - FonTemButt.ActualHeight + TeamButt.ActualHeight - TeamButt.ActualWidth;
                themeName = "Light.xaml";
            }
            if (AppViewModel.Themes == Themes.Light)
            {
                newMarginLeft = FonTemButt.Margin.Left + FonTemButt.ActualHeight - TeamButt.ActualHeight;
                themeName = "Dark.xaml";
            }
            themeAnimation.To = new Thickness(
                newMarginLeft,
                TeamButt.Margin.Top,
                TeamButt.Margin.Right,
                TeamButt.Margin.Bottom);
            themeAnimation.Duration = TimeSpan.FromMilliseconds(500);
            TeamButt.BeginAnimation(Rectangle.MarginProperty, themeAnimation);
            AppViewModel.ChangeTheme();
            var uri = new Uri(themeName, UriKind.Relative);
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void RAccRMS_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TAcc.MagnitudeType = MagnitudeType.RMS;
                AppViewModel.SetTAcc();
                PushTexboxNotRecalc(AppViewModel.TAcc, TAcceleration);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }
        private void TSensitivity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TSensitivity.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                SensitivityType sensitivityType = SensitivityType.mV_g;
                if (RSens_mV_g.IsChecked ?? false)
                {
                    sensitivityType = SensitivityType.mV_g;
                }
                if (RSens_mV_m_s2.IsChecked ?? false)
                {
                    sensitivityType = SensitivityType.mV_mS2;
                }
                if (RSens_mV_mm_s.IsChecked ?? false)
                {
                    sensitivityType = SensitivityType.mV_mS2;
                }
                if (RSens_mV_mkm.IsChecked ?? false)
                {
                    sensitivityType = SensitivityType.mV_mS2;
                }
                AppViewModel.SetTSensAndALL(TSensitivity.Text, sensitivityType, TakeFreeze());
                AppViewModel.TSens.Access = Access.Blocked;
                PushAllTexbox(Parametr.Sensitivity);
                AppViewModel.TSens.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TSens.LastTry == LastTry.Unsuccessful)
                    {
                        TSensitivity.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TSensitivity.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TSensitivity.SelectionStart = TSensitivity.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }
        private void TAcceleration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TAcceleration.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                MagnitudeType magnitudeType = MagnitudeType.RMS;
                magnitudeType = CheckMagnitudeTypeAcc(magnitudeType, RAccRMS, RAccPik, RAccPikPik);
                AppViewModel.SetTAccAndALL(TAcceleration.Text, magnitudeType);
                AppViewModel.TAcc.Access = Access.Blocked;
                PushAllTexbox(Parametr.Acceleration);
                AppViewModel.TAcc.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TAcc.LastTry == LastTry.Unsuccessful)
                    {
                        TAcceleration.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TAcceleration.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TAcceleration.SelectionStart = TAcceleration.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }
        private void TAcceleration_dB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TAcceleration_dB.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                AppViewModel.SetTAcc_dBAndALL(TAcceleration_dB.Text);
                AppViewModel.TAcc_dB.Access = Access.Blocked;
                PushAllTexbox(Parametr.Acceleration_dB);
                AppViewModel.TAcc_dB.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TAcc_dB.LastTry == LastTry.Unsuccessful)
                    {
                        TAcceleration_dB.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TAcceleration_dB.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TAcceleration_dB.SelectionStart = TAcceleration_dB.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }
        private MagnitudeType CheckMagnitudeTypeAcc(MagnitudeType magnitudeType, RadioButton RMS, RadioButton Pik, RadioButton PikPik)
        {

            if (RMS.IsChecked ?? false)
            {
                magnitudeType = MagnitudeType.RMS;
                return magnitudeType;
            }
            if (Pik.IsChecked ?? false)
            {
                magnitudeType = MagnitudeType.Pik;
                return magnitudeType;
            }
            if (PikPik.IsChecked ?? false)
            {
                magnitudeType = MagnitudeType.PikPik;
                return magnitudeType;
            }
            return magnitudeType;
        }
        private void RAccPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TAcc.MagnitudeType = MagnitudeType.Pik;
                AppViewModel.SetTAcc();
                PushTexboxNotRecalc(AppViewModel.TAcc, TAcceleration);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }
        private void GetRoundParamPushTexbox(DoubleForTextBox parameter, TextBox textBox)
        {
            textBox.Text = MetrologyRound.GetRounded(double.Parse(parameter.Text), countMainChars: 4).ToString();
        }

        private void TVelocity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TVelocity.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                MagnitudeType magnitudeType = MagnitudeType.RMS;
                magnitudeType = CheckMagnitudeTypeAcc(magnitudeType, RVelRMS, RVelPik, RVelPikPik);
                AppViewModel.SetTVelAndALL(TVelocity.Text, magnitudeType);
                AppViewModel.TVel.Access = Access.Blocked;
                PushAllTexbox(Parametr.VeloCity);
                AppViewModel.TVel.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TVel.LastTry == LastTry.Unsuccessful)
                    {
                        TVelocity.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TVelocity.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TVelocity.SelectionStart = TVelocity.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }

        private void TDisplasment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TDisplasment.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                MagnitudeType magnitudeType = MagnitudeType.RMS;
                magnitudeType = CheckMagnitudeTypeAcc(magnitudeType, RDisRMS, RDisPik, RDisPikPik);
                AppViewModel.SetTDisAndALL(TDisplasment.Text, magnitudeType);
                AppViewModel.TDis.Access = Access.Blocked;
                PushAllTexbox(Parametr.Displacment);
                AppViewModel.TDis.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TDis.LastTry == LastTry.Unsuccessful)
                    {
                        TDisplasment.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TDisplasment.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TDisplasment.SelectionStart = TDisplasment.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }
        private void TVelocity_dB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TVelocity_dB.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                AppViewModel.SetTVel_dBAndALL(TVelocity_dB.Text);
                AppViewModel.TVel_dB.Access = Access.Blocked;
                PushAllTexbox(Parametr.VeloCity_dB);
                AppViewModel.TVel_dB.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TVel_dB.LastTry == LastTry.Unsuccessful)
                    {
                        TVelocity_dB.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TVelocity_dB.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TVelocity_dB.SelectionStart = TVelocity_dB.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }

        private void TDisplasment_dB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TDisplasment_dB.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                AppViewModel.SetTDis_dBAndALL(TDisplasment_dB.Text);
                AppViewModel.TDis_dB.Access = Access.Blocked;
                PushAllTexbox(Parametr.Displacment_dB);
                AppViewModel.TDis_dB.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TDis_dB.LastTry == LastTry.Unsuccessful)
                    {
                        TDisplasment_dB.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TDisplasment_dB.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TDisplasment_dB.SelectionStart = TVelocity_dB.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }

        private void TVoltage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TVoltage.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                MagnitudeType magnitudeType = MagnitudeType.RMS;
                magnitudeType = CheckMagnitudeTypeAcc(magnitudeType, RVoltRMS, RVoltPik, RVoltPikPik);
                AppViewModel.SetTVoltAndALL(TVoltage.Text, magnitudeType);
                AppViewModel.TVolt.Access = Access.Blocked;
                PushAllTexbox(Parametr.Voltage);
                AppViewModel.TVolt.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TVolt.LastTry == LastTry.Unsuccessful)
                    {
                        TVoltage.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TVoltage.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TVoltage.SelectionStart = TVoltage.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }

        private void TVoltage_dB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TVoltage_dB.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                AppViewModel.SetTVolt_dBAndALL(TVoltage_dB.Text);
                AppViewModel.TVolt_dB.Access = Access.Blocked;
                PushAllTexbox(Parametr.Voltage_dB);
                AppViewModel.TVolt_dB.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TVolt_dB.LastTry == LastTry.Unsuccessful)
                    {
                        TVoltage_dB.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TVoltage.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TVoltage_dB.SelectionStart = TVoltage_dB.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }

        private void RAccPikPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TAcc.MagnitudeType = MagnitudeType.PikPik;
                AppViewModel.SetTAcc();
                PushTexboxNotRecalc(AppViewModel.TAcc, TAcceleration);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RVelRMS_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TVel.MagnitudeType = MagnitudeType.RMS;
                AppViewModel.SetTVel();
                PushTexboxNotRecalc(AppViewModel.TVel, TVelocity);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RVelPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TVel.MagnitudeType = MagnitudeType.Pik;
                AppViewModel.SetTVel();
                PushTexboxNotRecalc(AppViewModel.TVel, TVelocity);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RVelPikPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TVel.MagnitudeType = MagnitudeType.PikPik;
                AppViewModel.SetTVel();
                PushTexboxNotRecalc(AppViewModel.TVel, TVelocity);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RDisRMS_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TDis.MagnitudeType = MagnitudeType.RMS;
                AppViewModel.SetTDis();
                PushTexboxNotRecalc(AppViewModel.TDis, TDisplasment);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RDisPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TDis.MagnitudeType = MagnitudeType.Pik;
                AppViewModel.SetTDis();
                PushTexboxNotRecalc(AppViewModel.TDis, TDisplasment);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RDisPikPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TDis.MagnitudeType = MagnitudeType.PikPik;
                AppViewModel.SetTDis();
                PushTexboxNotRecalc(AppViewModel.TDis, TDisplasment);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RVoltRMS_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TVolt.MagnitudeType = MagnitudeType.RMS;
                AppViewModel.SetTVolt();
                PushTexboxNotRecalc(AppViewModel.TVolt, TVoltage);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RVoltPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TVolt.MagnitudeType = MagnitudeType.Pik;
                AppViewModel.SetTVolt();
                PushTexboxNotRecalc(AppViewModel.TVolt, TVoltage);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RVoltPikPik_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TVolt.MagnitudeType = MagnitudeType.PikPik;
                AppViewModel.SetTVolt();
                PushTexboxNotRecalc(AppViewModel.TVolt, TVoltage);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }
        private void RSens_mV_g_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                GBDis.Visibility = Visibility.Visible;
                GBVel.Visibility = Visibility.Visible;
                LVel.Content = "Скорость";
                LAcc.Content = "Ускорение";
                LAccDim.Content = "м/с­­²";
                LVelDim.Content = "мм/с";
                AppViewModel.TSens.SensitivityType = SensitivityType.mV_g;
                AppViewModel.SetTSens();
                PushTexboxNotRecalc(AppViewModel.TSens, TSensitivity);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }
        private void RSens_mV_m_s2_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                GBDis.Visibility = Visibility.Visible;
                GBVel.Visibility = Visibility.Visible;
                LVel.Content = "Скорость";
                LAcc.Content = "Ускорение";
                LAccDim.Content = "м/с­­²";
                LVelDim.Content = "мм/с";
                AppViewModel.TSens.SensitivityType = SensitivityType.mV_mS2;
                AppViewModel.SetTSens();
                PushTexboxNotRecalc(AppViewModel.TSens, TSensitivity);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void TFrequency_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TFrequency.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                FrequencyType frequencyType = FrequencyType.Hz;
                if (RFreqHz.IsChecked ?? false)
                {
                    frequencyType = FrequencyType.Hz;
                }
                if (RFreqRPM.IsChecked ?? false)
                {
                    frequencyType = FrequencyType.RPM;
                }
                AppViewModel.SetTFreqAndALL(TFrequency.Text, frequencyType, TakeFreeze());
                AppViewModel.TFreq.Access = Access.Blocked;
                PushAllTexbox(Parametr.Frequency);
                AppViewModel.TFreq.Access = Access.ForUser;
                try
                {
                    if (AppViewModel.TFreq.LastTry == LastTry.Unsuccessful)
                    {
                        TFrequency.SelectionStart = selectionStart - 1;
                    }
                    else
                    {
                        TFrequency.SelectionStart = selectionStart;
                    }
                }
                catch
                {
                    TFrequency.SelectionStart = TFrequency.Text.Length;
                }
                ChangeTextbox = Pocess.Finish;
            }
        }
        private void RFreqHz_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TFreq.FrequencyType = FrequencyType.Hz;
                AppViewModel.SetTFreq();
                PushTexboxNotRecalc(AppViewModel.TFreq, TFrequency);
            }
        }
        private void RFreqRPM_Checked(object sender, RoutedEventArgs e)
        {

            if (InitializeComponentStatus == Pocess.Finish)
            {
                AppViewModel.TFreq.FrequencyType = FrequencyType.RPM;
                AppViewModel.SetTFreq();
                PushTexboxNotRecalc(AppViewModel.TFreq, TFrequency);
            }
        }
        private void PushTexboxNotRecalc(DoubleForTextBox Param, TextBox textBox)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                ChangeTextbox = Pocess.InPocess;
                GetRoundParamPushTexbox(Param, textBox);
                ChangeTextbox = Pocess.Finish;
            }
        }
        private void PushAllTexbox()
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                GetRoundParamPushTexbox(AppViewModel.TAcc, TAcceleration);
                GetRoundParamPushTexbox(AppViewModel.TSens, TSensitivity);
                GetRoundParamPushTexbox(AppViewModel.TFreq, TFrequency);
                GetRoundParamPushTexbox(AppViewModel.TAcc_dB, TAcceleration_dB);
                GetRoundParamPushTexbox(AppViewModel.TVel, TVelocity);
                GetRoundParamPushTexbox(AppViewModel.TVel_dB, TVelocity_dB);
                GetRoundParamPushTexbox(AppViewModel.TDis, TDisplasment);
                GetRoundParamPushTexbox(AppViewModel.TDis_dB, TDisplasment_dB);
                GetRoundParamPushTexbox(AppViewModel.TVolt, TVoltage);
                GetRoundParamPushTexbox(AppViewModel.TVolt_dB, TVoltage_dB);
                SetSpectrumParameters();
            }
        }
        private void PushAllTexbox(Parametr param)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                if (param == Parametr.Acceleration)
                {
                    TAcceleration.Text = AppViewModel.TAcc.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TAcc, TAcceleration);
                }
                if (param == Parametr.Acceleration_dB)
                {
                    TAcceleration_dB.Text = AppViewModel.TAcc_dB.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TAcc_dB, TAcceleration_dB);
                }
                if (param == Parametr.Acceleration_dB)
                {
                    TAcceleration_dB.Text = AppViewModel.TAcc_dB.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TAcc_dB, TAcceleration_dB);
                }
                if (param == Parametr.Sensitivity)
                {
                    TSensitivity.Text = AppViewModel.TSens.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TSens, TSensitivity);
                }
                if (param == Parametr.Frequency)
                {
                    TFrequency.Text = AppViewModel.TFreq.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TFreq, TFrequency);
                }
                if (param == Parametr.VeloCity)
                {
                    TVelocity.Text = AppViewModel.TVel.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TVel, TVelocity);
                }
                if (param == Parametr.VeloCity_dB)
                {
                    TVelocity_dB.Text = AppViewModel.TVel_dB.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TVel_dB, TVelocity_dB);
                }
                if (param == Parametr.Displacment)
                {
                    TDisplasment.Text = AppViewModel.TDis.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TDis, TDisplasment);
                }
                if (param == Parametr.Displacment_dB)
                {
                    TDisplasment_dB.Text = AppViewModel.TDis_dB.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TDis_dB, TDisplasment_dB);
                }
                if (param == Parametr.Voltage)
                {
                    TVoltage.Text = AppViewModel.TVolt.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TVolt, TVoltage);
                }
                if (param == Parametr.Voltage_dB)
                {
                    TVoltage_dB.Text = AppViewModel.TVolt_dB.Text;
                }
                else
                {
                    GetRoundParamPushTexbox(AppViewModel.TVolt_dB, TVoltage_dB);
                }
                SetSpectrumParameters();
            }
        }

        private void SetSpectrumParameters()
        {
            _ = double.TryParse(TBoundaryFreq.Text, out double boundaryFreq);
            _ = double.TryParse(TLineCount.Text, out double lineCount);
            double divisionFreq = boundaryFreq / lineCount;
            TDivisionFreq.Text = (divisionFreq).ToString();
            double lineNum = VibroCalc.Frequency.Get_Hz() / (boundaryFreq / lineCount);
            int minLineNum = (int)Math.Floor(lineNum);
            int maxLineNum = minLineNum + 1;
            TFerstFreqCenterChannel.Text = (minLineNum * divisionFreq).ToString();
            TSecondFreqCenterChannel.Text = (maxLineNum * divisionFreq).ToString();
        }

        private async void TSensitivity_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }
        private async void TAcceleration_GotFocus(object s, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((s as TextBox).SelectAll);
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void RSens_mV_mm_s_Checked(object sender, RoutedEventArgs e)
        {

            if (InitializeComponentStatus == Pocess.Finish)
            {
                GBDis.Visibility = Visibility.Hidden;
                GBVel.Visibility = Visibility.Visible;
                LVel.Content = "Перемещение";
                LAcc.Content = "Скорость";
                LAccDim.Content = "мм/с";
                LVelDim.Content = "мкм";
                AppViewModel.TSens.SensitivityType = SensitivityType.mV_mS2;
                AppViewModel.SetTSens();
                PushTexboxNotRecalc(AppViewModel.TSens, TSensitivity);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void RSens_mV_mkm_Checked(object sender, RoutedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish)
            {
                GBDis.Visibility = Visibility.Hidden;
                GBVel.Visibility = Visibility.Hidden;
                LAcc.Content = "Перемещение";
                LAccDim.Content = "мкм";
                AppViewModel.TSens.SensitivityType = SensitivityType.mV_mS2;
                AppViewModel.SetTSens();
                PushTexboxNotRecalc(AppViewModel.TSens, TSensitivity);
                //AppViewModel.SetAll();
                //PushAllTexbox();
            }
        }

        private void TBoundaryFreq_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TFrequency.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                PushAllTexbox();
                ChangeTextbox = Pocess.Finish;
            }
        }

        private void TLineCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InitializeComponentStatus == Pocess.Finish && ChangeTextbox == Pocess.Finish)
            {
                int selectionStart = TFrequency.SelectionStart;
                ChangeTextbox = Pocess.InPocess;
                PushAllTexbox();
                ChangeTextbox = Pocess.Finish;
            }
        }

        private async void TFrequency_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TVoltage_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TVoltage_dB_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TAcceleration_dB_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TBoundaryFreq_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TVelocity_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TVelocity_dB_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TDivisionFreq_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TDisplasment_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TDisplasment_dB_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TFerstFreqCenterChannel_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TSecondFreqCenterChannel_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }

        private async void TLineCount_GotFocus(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync((sender as TextBox).SelectAll);
        }
    }
}
