using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HW1
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        public static int Width { get; set; }
        public static int Height { get; set; }

        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(150, 150));

        private static Font Font { get; } = new Font("Vivaldi", 32, FontStyle.Regular); //Шрифт GUI
        private static Font FontTitle { get; } = new Font("Vivaldi", 60, FontStyle.Underline); //Шрифт Title
        private static Timer timer = new Timer { Interval = 50 };
        public static Random rnd = new Random();

        static Game()
        {

        }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;

            //Выбрасываем исключение если размер окна задан меньше 0 или больше 1000. Если < 0, то задается разрешение 800х600
            try
            {
                Width = Screen.PrimaryScreen.Bounds.Width;//form.ClientSize.Width;
                Height = Screen.PrimaryScreen.Bounds.Height;//form.ClientSize.Height;
                if (Width > 1000 || Height > 1000 || Width < 0 || Height < 0) { throw new ArgumentOutOfRangeException(); }
            }
            catch (ArgumentOutOfRangeException e)
            {

                //MessageBox.Show("Размер экрана больше 1000 или меньше 0", "Не знаю зачем, но сказали вывести! :)");
                if (Width < 0 || Height < 0) { Width = 800; Height = 600; }
            }

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            
            timer.Start();
            timer.Tick += Timer_Tick;
            //form.KeyPress += new KeyPressEventHandler(KeyPressed);
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) _bullet = new Bullet(new Point(_ship.Rect.X + 150, _ship.Rect.Y + 75), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        //Действия таймера для обновления экрана
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        //Обработка нажатия кнопок (Выстрел и ESC для выхода)
        //private static void KeyPressed (object sender, KeyPressEventArgs e)
        //{

        //    if (e.KeyChar == (char)Keys.Space) { _bullet.Draw(); System.Media.SystemSounds.Beep.Play(); Buffer.Render(); }
        //    if (e.KeyChar == (char)Keys.Escape)
        //    {
        //        if (MessageBox.Show("Уходите? Так быстро? :(", "Asteroid Game", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
        //            Application.Exit();
        //    }
        //}

        //Отрисовка
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
            {
                obj.Draw();
            }
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy: " + _ship.Energy, Font, Brushes.White, Width / 2, 10);
            Buffer.Render();

        }

        //Обновление экрана
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                obj.Update();
            }
            //foreach(Asteroid a in _asteroids)
            //{
            //    a.Update();
            //    if (a.Collision(_bullet))
            //    {
            //        System.Media.SystemSounds.Hand.Play();
            //        a.ResetPos();
            //        _bullet.ResetPos();
            //    }
            //}
            _bullet?.Update();
            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();

                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = null;
                    _bullet = null;
                    continue;
                }

                if (_ship.Collision(_asteroids[i])) continue;
                _ship?.EnergyDown(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        public static BaseObject[] _objs;

        //Инициализация объектов
        public static void Load()
        {
            _objs = new BaseObject[50]; //Звезды
            try
            {
                _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            }
            catch (GameObjectException e)
            {
                _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            }
            _asteroids = new Asteroid[3];
            
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

        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", FontTitle, Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}
