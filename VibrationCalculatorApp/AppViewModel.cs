using VibroMath;



namespace VibrationCalculatorApp {
    public enum Themes {
        Light,
        Black
    }
    public enum MagnitudeType {
        RMS,
        Pik,
        PikPik,
    }
    public enum SensitivityType {
        mV_g,
        mV_mS2
    }
    public enum FrequencyType {
        Hz,
        RPM
    }
    public enum LastTry {
        Successfully,
        Unsuccessful
    }
    public class DoubleForTextBox {
        public string Text {
            get;
            private set;
        }
        public Access Access = Access.ForUser;
        public LastTry LastTry {
            get;
            private set;
        } = LastTry.Successfully;
        public void SetText(string newText) {
            if (Access == Access.ForUser) {
                newText = newText.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                newText = newText.Replace(",", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                if (double.TryParse(newText, out double result) || newText == "") {
                    Text = newText;
                    LastTry = LastTry.Successfully;
                    return;
                }
                LastTry = LastTry.Unsuccessful;
            }
        }
        public void SetText(double value) {
            if (Access != Access.Blocked) {
                Text = value.ToString();
            }
        }
    }
    public class Parameters : DoubleForTextBox {
        public MagnitudeType MagnitudeType = MagnitudeType.RMS;
    }
    public class Sensitivity : DoubleForTextBox {
        public SensitivityType SensitivityType = SensitivityType.mV_g;
    }
    public class Frequency : DoubleForTextBox {
        public FrequencyType FrequencyType = FrequencyType.Hz;
    }
    public static class AppViewModel {
        public static Themes Themes {
            get;
            private set;
        } = Themes.Black;
        public static Parameters TAcc {
            get;
            private set;
        } = new Parameters();
        public static Parameters TVel {
            get;
            private set;
        } = new Parameters();
        public static Parameters TDis {
            get;
            private set;
        } = new Parameters();
        public static Parameters TAcc_dB {
            get;
            private set;
        } = new Parameters();
        public static Parameters TVel_dB {
            get;
            private set;
        } = new Parameters();
        public static Parameters TDis_dB {
            get;
            private set;
        } = new Parameters();
        public static Sensitivity TSens {
            get;
            private set;
        } = new Sensitivity();
        public static Frequency TFreq {
            get;
            private set;
        } = new Frequency();
        public static Parameters TVolt {
            get;
            private set;
        } = new Parameters();
        public static Parameters TVolt_dB {
            get;
            private set;
        } = new Parameters();
        public static DoubleForTextBox TBoundaryFrequency = new DoubleForTextBox();
        public static DoubleForTextBox TLineCount = new DoubleForTextBox();


        static AppViewModel() {
            SetAll();
        }
        public static void SetTVoltAndALL(string volt, MagnitudeType magnitudeType) {
            TVolt.MagnitudeType = magnitudeType;
            TVolt.SetText(volt);
            if (TVolt.Text == "") {
                return;
            }
            Voltage voltage = new Voltage();
            if (magnitudeType == MagnitudeType.RMS) {
                voltage.SetRMS(double.Parse(TVolt.Text));
            }
            if (magnitudeType == MagnitudeType.Pik) {
                voltage.SetPIK(double.Parse(TVolt.Text));
            }
            if (magnitudeType == MagnitudeType.PikPik) {
                voltage.SetPIK_PIK(double.Parse(TVolt.Text));
            }
            VibroCalc.CalcAll(voltage);
            TVolt.Access = Access.Blocked;
            SetAll();
            TVolt.Access = Access.ForUser;
        }
        public static void SetTAccAndALL(string acc, MagnitudeType magnitudeType) {
            TAcc.MagnitudeType = magnitudeType;
            TAcc.SetText(acc);
            if (TAcc.Text == "") {
                return;
            }
            Acceleration acceleration = new Acceleration();
            if (magnitudeType == MagnitudeType.RMS) {
                acceleration.SetRMS(double.Parse(TAcc.Text));
            }
            if (magnitudeType == MagnitudeType.Pik) {
                acceleration.SetPIK(double.Parse(TAcc.Text));
            }
            if (magnitudeType == MagnitudeType.PikPik) {
                acceleration.SetPIK_PIK(double.Parse(TAcc.Text));
            }
            VibroCalc.CalcAll(acceleration);
            TAcc.Access = Access.Blocked;
            SetAll();
            TAcc.Access = Access.ForUser;
        }
        public static void SetTVelAndALL(string vel, MagnitudeType magnitudeType) {
            TVel.MagnitudeType = magnitudeType;
            TVel.SetText(vel);
            if (vel == "") {
                return;
            }
            Velocity velocity = new Velocity();
            if (magnitudeType == MagnitudeType.RMS) {
                velocity.SetRMS(double.Parse(TVel.Text));
            }
            if (magnitudeType == MagnitudeType.Pik) {
                velocity.SetPIK(double.Parse(TVel.Text));
            }
            if (magnitudeType == MagnitudeType.PikPik) {
                velocity.SetPIK_PIK(double.Parse(TVel.Text));
            }
            VibroCalc.CalcAll(velocity);
            TVel.Access = Access.Blocked;
            SetAll();
            TVel.Access = Access.ForUser;
        }
        public static void SetTDisAndALL(string dis, MagnitudeType magnitudeType) {
            TDis.MagnitudeType = magnitudeType;
            TDis.SetText(dis);
            if (dis == "") {
                return;
            }
            Displacement displacement = new Displacement();
            if (magnitudeType == MagnitudeType.RMS) {
                displacement.SetRMS(double.Parse(TDis.Text));
            }
            if (magnitudeType == MagnitudeType.Pik) {
                displacement.SetPIK(double.Parse(TDis.Text));
            }
            if (magnitudeType == MagnitudeType.PikPik) {
                displacement.SetPIK_PIK(double.Parse(TDis.Text));
            }
            VibroCalc.CalcAll(displacement);
            TDis.Access = Access.Blocked;
            SetAll();
            TDis.Access = Access.ForUser;
        }
        public static void SetTVolt_dBAndALL(string volt_dB) {
            TVolt_dB.SetText(volt_dB);
            if (TVolt_dB.Text == "") {
                return;
            }
            Voltage voltage = new Voltage();
            voltage.Set_dB(double.Parse(TVolt_dB.Text));
            VibroCalc.CalcAll(voltage);
            TVolt_dB.Access = Access.Blocked;
            SetAll();
            TVolt_dB.Access = Access.ForUser;
        }
        public static void SetTAcc_dBAndALL(string acc) {
            TAcc_dB.SetText(acc);
            if (acc == "") {
                return;
            }
            Acceleration acceleration = new Acceleration();
            acceleration.Set_dB(double.Parse(TAcc_dB.Text));
            VibroCalc.CalcAll(acceleration);
            TAcc_dB.Access = Access.Blocked;
            SetAll();
            TAcc_dB.Access = Access.ForUser;
        }
        public static void SetTVel_dBAndALL(string vel) {
            TVel_dB.SetText(vel);
            if (vel == "") {
                return;
            }
            Velocity velocity = new Velocity();
            velocity.Set_dB(double.Parse(TVel_dB.Text));
            VibroCalc.CalcAll(velocity);
            TVel_dB.Access = Access.Blocked;
            SetAll();
            TVel_dB.Access = Access.ForUser;
        }
        public static void SetTDis_dBAndALL(string dis) {
            TDis_dB.SetText(dis);
            if (dis == "") {
                return;
            }
            Displacement displacement = new Displacement();
            displacement.Set_dB(double.Parse(TDis_dB.Text));
            VibroCalc.CalcAll(displacement);
            TDis_dB.Access = Access.Blocked;
            SetAll();
            TDis_dB.Access = Access.ForUser;
        }
        public static void SetTSensAndALL(string sens, SensitivityType sensitivityType, Freeze freeze) {
            TSens.SensitivityType = sensitivityType;
            TSens.SetText(sens);
            if (sens == "") {
                return;
            }
            VibroMath.Sensitivity sensitivity = new VibroMath.Sensitivity();
            if (sensitivityType == SensitivityType.mV_g) {
                sensitivity.Set_mV_G(double.Parse(TSens.Text));
            }
            if (sensitivityType == SensitivityType.mV_mS2) {
                sensitivity.Set_mV_MS2(double.Parse(TSens.Text));
            }
            VibroCalc.CalcAll(sensitivity, freeze);
            TSens.Access = Access.Blocked;
            SetAll();
            TSens.Access = Access.ForUser;
        }
        public static void SetTFreqAndALL(string freq, FrequencyType frequencyType, Freeze freeze) {
            TFreq.FrequencyType = frequencyType;
            TFreq.SetText(freq);
            if (freq == "") {
                return;
            }
            VibroMath.Frequency frequency = new VibroMath.Frequency();
            if (frequencyType == FrequencyType.Hz) {
                frequency.Set_Hz(double.Parse(TFreq.Text));
            }
            if (frequencyType == FrequencyType.RPM) {
                frequency.Set_RPM(double.Parse(TFreq.Text));
            }
            VibroCalc.CalcAll(frequency, freeze);
            TFreq.Access = Access.Blocked;
            SetAll();
            TFreq.Access = Access.ForUser;
        }
        public static void SetAll() {
            SetTAcc();
            SetTVel();
            SetTDis();
            SetTSens();
            SetTFreq();
            SetTVolt();
            TVolt_dB.SetText(VibroCalc.Voltage.Get_dB());
            TAcc_dB.SetText(VibroCalc.Acceleration.Get_dB());
            TVel_dB.SetText(VibroCalc.Velocity.Get_dB());
            TDis_dB.SetText(VibroCalc.Displacement.Get_dB());
        }
        private static void SetTVolt() {
            if (TVolt.MagnitudeType == MagnitudeType.RMS) {
                TVolt.SetText(VibroCalc.Voltage.GetRMS());
            }
            if (TVolt.MagnitudeType == MagnitudeType.Pik) {
                TVolt.SetText(VibroCalc.Voltage.GetPIK());
            }
            if (TVolt.MagnitudeType == MagnitudeType.PikPik) {
                TVolt.SetText(VibroCalc.Voltage.GetPIK_PIK());
            }
        }
        private static void SetTAcc() {
            if (TAcc.MagnitudeType == MagnitudeType.RMS) {
                TAcc.SetText(VibroCalc.Acceleration.GetRMS());
            }
            if (TAcc.MagnitudeType == MagnitudeType.Pik) {
                TAcc.SetText(VibroCalc.Acceleration.GetPIK());
            }
            if (TAcc.MagnitudeType == MagnitudeType.PikPik) {
                TAcc.SetText(VibroCalc.Acceleration.GetPIK_PIK());
            }
        }
        private static void SetTVel() {
            if (TVel.MagnitudeType == MagnitudeType.RMS) {
                TVel.SetText(VibroCalc.Velocity.GetRMS());
            }
            if (TVel.MagnitudeType == MagnitudeType.Pik) {
                TVel.SetText(VibroCalc.Velocity.GetPIK());
            }
            if (TVel.MagnitudeType == MagnitudeType.PikPik) {
                TVel.SetText(VibroCalc.Velocity.GetPIK_PIK());
            }
        }
        private static void SetTDis() {
            if (TDis.MagnitudeType == MagnitudeType.RMS) {
                TDis.SetText(VibroCalc.Displacement.GetRMS());
            }
            if (TDis.MagnitudeType == MagnitudeType.Pik) {
                TDis.SetText(VibroCalc.Displacement.GetPIK());
            }
            if (TDis.MagnitudeType == MagnitudeType.PikPik) {
                TDis.SetText(VibroCalc.Displacement.GetPIK_PIK());
            }
        }
        private static void SetTSens() {
            if (TSens.SensitivityType == SensitivityType.mV_g) {
                TSens.SetText(VibroCalc.Sensitivity.Get_mV_G());
            }
            if (TSens.SensitivityType == SensitivityType.mV_mS2) {
                TSens.SetText(VibroCalc.Sensitivity.Get_mV_MS2());
            }
        }
        private static void SetTFreq() {
            if (TFreq.FrequencyType == FrequencyType.Hz) {
                TFreq.SetText(VibroCalc.Frequency.Get_Hz());
            }
            if (TFreq.FrequencyType == FrequencyType.RPM) {
                TFreq.SetText(VibroCalc.Frequency.Get_RPM());
            }
        }
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
