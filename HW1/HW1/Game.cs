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

        static Game()
        {

        }

        public static void  Init (Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            //Выбрасываем исключение если размер окна задан меньше 0 или больше 1000. Если < 0, то задается разрешение 800х600
            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                if (Width > 1000 || Height >1000 || Width < 0 || Height < 0) { throw new ArgumentOutOfRangeException(); }
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
            form.KeyPress += new KeyPressEventHandler(Shoot);
        }
        
        //Действия таймера для обновления экрана
        private static void Timer_Tick (object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        //Выстре по SpaceBar
        private static void Shoot (object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Space) { _bullet.Draw(); System.Media.SystemSounds.Beep.Play(); Buffer.Render(); }
        }

        //Отрисовка
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach(BaseObject obj in _objs)
            {
                obj.Draw();
            }
            foreach(Asteroid obj in _asteroids)
            {
                obj.Draw();
            }
            _bullet.Draw();
            Buffer.Render();

        }

        //Обновление экрана
        public static void Update ()
        {
            foreach(BaseObject obj in _objs)
            {
                obj.Update();
            }
            foreach(Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    a.ResetPos();
                    _bullet.ResetPos();
                }
            }
            _bullet.Update();
        }

        public static BaseObject[] _objs;

        //Инициализация объектов
        public static void Load ()
        {
            _objs = new BaseObject[50]; //Звезды
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
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
                _objs[i] = new Star(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)), new Point(-dirStar, dirStar), new Size(3, 3));
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
                _asteroids[i] = new Asteroid(new Point(rnd.Next(0, Game.Width), rnd.Next(0, Game.Height)), new Point(dirAsteroid, dirAsteroid), new Size(aSize, aSize));
            }
        }
    }
}
