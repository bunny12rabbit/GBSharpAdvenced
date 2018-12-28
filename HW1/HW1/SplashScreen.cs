using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HW1
{
    class SplashScreen
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Asteroid[] _asteroids;
        public static BaseObject[] _objs;
        public static bool splash = false;

        public SplashScreen(Form form)
        {
            form = new Form();
        }

        public static void Init(Form form)
        { 
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            splash = true;

            //Выбрасываем исключение если размер окна задан меньше 0 или больше 1000. Если < 0, то задается разрешение 800х600
            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                if (Width > 1000 || Height > 1000 || Width < 0 || Height < 0) { throw new ArgumentOutOfRangeException(); }
            }
            catch (ArgumentOutOfRangeException e)
            {

                MessageBox.Show("Размер экрана больше 1000 или меньше 0", "Не знаю зачем, но сказали вывести! :)");
                if (Width < 0 || Height < 0) { Width = 800; Height = 600; }
            }

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;

            #region Buttons
            //Описание кнопок

            //Кнопка Новая игра
            Button BtNewGame = new Button();
            BtNewGame.Location = new Point(Width / 2 - 80, Height / 2 - 120);
            BtNewGame.Size = new Size(200, 50);
            BtNewGame.Text = "New Game";
            BtNewGame.Font = new Font("Vivaldi", 22, FontStyle.Regular);
            BtNewGame.UseVisualStyleBackColor = true;
            BtNewGame.BackColor = Color.Black;
            BtNewGame.ForeColor = Color.White;
            BtNewGame.Click += new EventHandler(BtNewGameClick);
            form.Controls.Add(BtNewGame);

            //Кнопка Рекорды
            Button BtScore = new Button();
            BtScore.Location = new Point(Width / 2 - 80, Height / 2 - 60);
            BtScore.Size = new Size(200, 50);
            BtScore.Text = "High Score";
            BtScore.Font = new Font("Vivaldi", 22, FontStyle.Regular);
            BtScore.UseVisualStyleBackColor = true;
            BtScore.BackColor = Color.Black;
            BtScore.ForeColor = Color.White;
            form.Controls.Add(BtScore);

            //Кнопка выход
            Button BtExit = new Button();
            BtExit.Location = new Point(Width / 2 - 80, Height / 2);
            BtExit.Size = new Size(200, 50);
            BtExit.Text = "Exit";
            BtExit.Font = new Font("Vivaldi", 30, FontStyle.Regular);
            BtExit.UseVisualStyleBackColor = true;
            BtExit.BackColor = Color.Black;
            BtExit.ForeColor = Color.White;
            BtExit.Click += new EventHandler(BtExitClick);
            form.Controls.Add(BtExit);

            //Label Credits
            Label name = new Label();
            name.Text = "Andrew Romanenko ©2018";
            name.Location = new Point(Width / 2 - 150, Height - 100);
            name.AutoSize = true;
            name.BackColor = Color.Black;
            name.ForeColor = Color.White;
            name.Font = new Font("Vivaldi", 22, FontStyle.Regular);
            form.Controls.Add(name);
            #endregion
        }

        public static void Load()
        {
            _objs = new BaseObject[50]; //Звезды
            _asteroids = new Asteroid[3];
            Random rnd = new Random();
            //Инициализация звезд
            for (int i = 0; i < _objs.Length; i++)
            {
                //Направление звезды
                int dirStar;
                do
                {
                    dirStar = rnd.Next(-10, 10);
                }
                while (dirStar == 0);
                _objs[i] = new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-dirStar, dirStar), new Size(3, 3));
            }
            //Инициализация астероидов
            for (int i = 0; i < _asteroids.Length; i++)
            {
                //Размер астероида
                int aSize = rnd.Next(20, 80);
                //Направление астероида
                int dirAsteroid;
                do
                {
                    dirAsteroid = rnd.Next(-5, 5);
                }
                while (dirAsteroid == 0);
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(dirAsteroid, dirAsteroid), new Size(aSize, aSize));
            }
        }

        //Действия таймера для обновления экрана
        private static void Timer_Tick(object sender, EventArgs e)
        {
            DrawSplash();
            Update();
        }

        //Кнопка Новая игра
        private static void BtNewGameClick(object sender, EventArgs e)
        {
            Form form2 = new Form();
            form2.Width = Width;
            form2.Height = Height;
            Game.Init(form2);
            form2.Show();
            form2.WindowState = FormWindowState.Maximized;
            form2.Text = "Asteroid Game";
            form2.ShowIcon = false;
            //form.Icon = SystemIcons.Application;  Почему показывает пустой значок, хотя он у программы есть?
            Program.form1.Hide();
            splash = false;
            form2.FormClosing += Form2_Closing;
        }

        //Кнопка Рекорды
        private static void BtScoreClick(object sender, EventArgs e)
        {

        }

        //Кнопка выхода
        private static void BtExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Отрисовка меню
        public static void DrawSplash()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }
            foreach (Asteroid obj in _asteroids)
            {
                obj.Draw();
            }
            Buffer.Render();
        }

        //Обновление экрана
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
            }
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
            }
        }


        //Запрашиваем подтверждение о выходе.
        static void Form2_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Уходите? Так быстро? :(", "Asteroid Game",
                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) //Почему при нажатии на Да диалог вызывается еще раз?
            {
                e.Cancel = true;
            }
            else
            {
                //e.Cancel = false;
                Application.Exit();
            }
        }

    }
}
