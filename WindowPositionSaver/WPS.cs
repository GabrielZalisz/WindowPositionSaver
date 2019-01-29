using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WindowPositionSaver
{
    public static class WPS
    {
        public static void WPS_Window_Constructor(Window w)
        {
            //tohle může být v loadu nebo konstruktoru
            try
            {
                Nastaveni.Nacist();
                Nastaveni.SetCurrentNastaveni(SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenLeft);

                if (Nastaveni.CurrentNastaveni.LastUse.Year <= 2000)
                {
                    w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    Nastaveni.CurrentNastaveni.LastUse = DateTime.Now;
                }
                else
                {
                    //musí být
                    w.WindowStartupLocation = WindowStartupLocation.Manual;

                    w.Left = Nastaveni.CurrentNastaveni.Left;
                    w.Top = Nastaveni.CurrentNastaveni.Top;
                    w.Width = Nastaveni.CurrentNastaveni.Width;
                    w.Height = Nastaveni.CurrentNastaveni.Height;

                    Nastaveni.CurrentNastaveni.LastUse = DateTime.Now;
                }
                Nastaveni.Ulozit();
            }
            catch { }
        }

        public static void WPS_Window_Loaded(Window w)
        {
            //tohle musí být až v loadu okna
            try
            {
                w.WindowState = Nastaveni.CurrentNastaveni.State;

                if (w.WindowState == WindowState.Minimized)
                    w.WindowState = WindowState.Normal;
            }
            catch { }
        }

        public static void WPS_Window_Closing(Window w)
        {
            try
            {
                w.Hide();

                WindowState st = w.WindowState;

                Nastaveni.CurrentNastaveni.State = w.WindowState;

                w.WindowState = WindowState.Normal;

                Nastaveni.CurrentNastaveni.Left = w.Left;
                Nastaveni.CurrentNastaveni.Top = w.Top;

                Nastaveni.CurrentNastaveni.AllWidth = SystemParameters.VirtualScreenWidth;
                Nastaveni.CurrentNastaveni.AllMinimum = SystemParameters.VirtualScreenLeft;

                if (st != WindowState.Maximized)
                {
                    Nastaveni.CurrentNastaveni.Width = w.Width;
                    Nastaveni.CurrentNastaveni.Height = w.Height;
                }

                Nastaveni.Ulozit();
            }
            catch
            {
                try
                {
                    w.Show();
                }
                catch { }
            }
        }
    }

    public class Nastaveni
    {
        static Nastaveni Nastaveni1;
        static Nastaveni Nastaveni2;
        static Nastaveni Nastaveni3;

        public static Nastaveni CurrentNastaveni;

        public static void SetCurrentNastaveni(double current_all_width, double current_all_minimum)
        {
            if (Nastaveni1.AllMinimum == current_all_minimum && Nastaveni1.AllWidth == current_all_width)
            {
                CurrentNastaveni = Nastaveni1;
            }
            else if (Nastaveni2.AllMinimum == current_all_minimum && Nastaveni2.AllWidth == current_all_width)
            {
                CurrentNastaveni = Nastaveni2;
            }
            else if (Nastaveni3.AllMinimum == current_all_minimum && Nastaveni3.AllWidth == current_all_width)
            {
                CurrentNastaveni = Nastaveni3;
            }
            else
            {
                List<Nastaveni> l = new List<Nastaveni>() { Nastaveni1, Nastaveni2, Nastaveni3 };
                l = l.OrderBy(q => q.LastUse).ToList();
                CurrentNastaveni = l.First();
                CurrentNastaveni.LastUse = new DateTime(2000, 1, 1);
                CurrentNastaveni.State = WindowState.Normal;
            }
        }

        public double Left;
        public double Top;
        public double Width;
        public double Height;
        public WindowState State;
        public double AllWidth;
        public double AllMinimum;
        public DateTime LastUse;

        public static void Nacist()
        {
            Nastaveni1 = new Nastaveni()
            {
                Left = Properties.Settings.Default.wps1_left,
                Top = Properties.Settings.Default.wps1_top,
                Width = Properties.Settings.Default.wps1_width,
                Height = Properties.Settings.Default.wps1_height,
                State = Properties.Settings.Default.wps1_window_state,
                AllWidth = Properties.Settings.Default.wps1_all_screens_width,
                AllMinimum = Properties.Settings.Default.wps1_all_screens_minimum,
                LastUse = Properties.Settings.Default.wps1_last_use
            };
            Nastaveni2 = new Nastaveni()
            {
                Left = Properties.Settings.Default.wps2_left,
                Top = Properties.Settings.Default.wps2_top,
                Width = Properties.Settings.Default.wps2_width,
                Height = Properties.Settings.Default.wps2_height,
                State = Properties.Settings.Default.wps2_window_state,
                AllWidth = Properties.Settings.Default.wps2_all_screens_width,
                AllMinimum = Properties.Settings.Default.wps2_all_screens_minimum,
                LastUse = Properties.Settings.Default.wps2_last_use
            };
            Nastaveni3 = new Nastaveni()
            {
                Left = Properties.Settings.Default.wps3_left,
                Top = Properties.Settings.Default.wps3_top,
                Width = Properties.Settings.Default.wps3_width,
                Height = Properties.Settings.Default.wps3_height,
                State = Properties.Settings.Default.wps3_window_state,
                AllWidth = Properties.Settings.Default.wps3_all_screens_width,
                AllMinimum = Properties.Settings.Default.wps3_all_screens_minimum,
                LastUse = Properties.Settings.Default.wps3_last_use
            };
        }

        public static void Ulozit()
        {
            Properties.Settings.Default.wps1_left = Nastaveni1.Left;
            Properties.Settings.Default.wps1_top = Nastaveni1.Top;
            Properties.Settings.Default.wps1_width = Nastaveni1.Width;
            Properties.Settings.Default.wps1_height = Nastaveni1.Height;
            Properties.Settings.Default.wps1_window_state = Nastaveni1.State;
            Properties.Settings.Default.wps1_all_screens_width = Nastaveni1.AllWidth;
            Properties.Settings.Default.wps1_all_screens_minimum = Nastaveni1.AllMinimum;
            Properties.Settings.Default.wps1_last_use = Nastaveni1.LastUse;

            Properties.Settings.Default.wps2_left = Nastaveni2.Left;
            Properties.Settings.Default.wps2_top = Nastaveni2.Top;
            Properties.Settings.Default.wps2_width = Nastaveni2.Width;
            Properties.Settings.Default.wps2_height = Nastaveni2.Height;
            Properties.Settings.Default.wps2_window_state = Nastaveni2.State;
            Properties.Settings.Default.wps2_all_screens_width = Nastaveni2.AllWidth;
            Properties.Settings.Default.wps2_all_screens_minimum = Nastaveni2.AllMinimum;
            Properties.Settings.Default.wps2_last_use = Nastaveni2.LastUse;

            Properties.Settings.Default.wps3_left = Nastaveni3.Left;
            Properties.Settings.Default.wps3_top = Nastaveni3.Top;
            Properties.Settings.Default.wps3_width = Nastaveni3.Width;
            Properties.Settings.Default.wps3_height = Nastaveni3.Height;
            Properties.Settings.Default.wps3_window_state = Nastaveni3.State;
            Properties.Settings.Default.wps3_all_screens_width = Nastaveni3.AllWidth;
            Properties.Settings.Default.wps3_all_screens_minimum = Nastaveni3.AllMinimum;
            Properties.Settings.Default.wps3_last_use = Nastaveni3.LastUse;

            Properties.Settings.Default.Save();
        }

    }
}
