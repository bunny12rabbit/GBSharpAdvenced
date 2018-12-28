﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HW1
{
    class Star : BaseObject
    {
        public Star (Point pos, Point dir, Size size) : base (pos, dir, size)
        {

        }

        public static BaseObject[] _obj;


        //Отрисовка
        public override void Draw()
        {
            
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X+Size.Width, Pos.Y, Pos.X, Pos.Y+Size.Height);
            //base.Draw();
        }

        //Обновление позиции
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X < 0 || Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0 || Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
