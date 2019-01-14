using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

        public static Form form1 { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form form = new Form
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height

            };
            form.WindowState = FormWindowState.Maximized;
            form.Text = "Asteroid Game";
            form.ShowIcon = false;
            //form.Icon = SystemIcons.Application;  Почему показывает пустой значок, хотя он у программы есть?

            form1 = form;
            //Открываем SplashScreen
            SplashScreen.Init(form);
            form.Show();

            //Game.Load();
            //Game.Draw();
            Application.Run(form);
        }
    }
}
