using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Asteroid : BaseObject
    {
        public int Power { get; set; }
        Bitmap bmp = new Bitmap("asteroid.png");

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

    }
}
