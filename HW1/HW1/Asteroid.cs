using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>
    {
        public int Power { get; set; } = 3;
        Bitmap bmp = new Bitmap(Properties.Resources.asteroid);

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        //Отрисовка
        public override void Draw()
        {
            if (SplashScreen.splash)
            {
                SplashScreen.Buffer.Graphics.DrawImage(bmp, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
            else
            {
                Game.Buffer.Graphics.DrawImage(bmp, Pos.X, Pos.Y, Size.Width, Size.Height);
            }
        }

        //Обновление позиции
        public override void Update()
        {
            if (SplashScreen.splash)
            {
                Pos.X += Dir.X;
                Pos.Y += Dir.Y;
                if (Pos.X < 0 || Pos.X > SplashScreen.Width) Dir.X = -Dir.X;
                if (Pos.Y < 0 || Pos.Y > SplashScreen.Height) Dir.Y = -Dir.Y;
            } else
            {
                Pos.X += Dir.X;
                Pos.Y += Dir.Y;
                if (Pos.X < 0 || Pos.X > Game.Width) Dir.X = -Dir.X;
                if (Pos.Y < 0 || Pos.Y > Game.Height) Dir.Y = -Dir.Y;
            }
        }

        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(Pos.X, Pos.Y), new Point(Dir.X, Dir.Y), new Size(Size.Width, Size.Height)) { Power = Power };
            return asteroid;
        }

        public int CompareTo(Asteroid obj)
        {
                if (Power > obj.Power) return 1;
                if (Power < obj.Power) return -1;
                else
                    return 0;
        }

        //Сбрасываем позицию
        public virtual void ResetPos()
        {
            Random rnd = new Random();
            Pos.X = rnd.Next(0, Game.Width);
            Pos.Y = rnd.Next(0, Game.Height);
            Dir = new Point(rnd.Next(-10, 10), rnd.Next(-10, 10));
        }
    }
}
