
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Bullet : BaseObject
    {
        public Bullet (Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        //Отрисовка
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        //Обновление позиции
        public override void Update()
        {
            Pos.X = Pos.X + 30;
            //Если пуля ушла за экран, возвращаем ее в начало (новый выстрел)
            if (Pos.X > Game.Width) ResetPos();
        }

        public override void ResetPos()
        {
            Pos.X = 0;
        }
    }
}
