﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HW1
{
    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        protected BaseObject (Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        //Обнаруживаем столкновение
        public Rectangle Rect => new Rectangle(Pos, Size);

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public abstract void Draw();

        public abstract void Update();

        //Сбрасываем позицию
        public virtual void ResetPos()
        {
            Random rnd = new Random();
            Pos.X = rnd.Next(0, Game.Width);
            Pos.Y = rnd.Next(0, Game.Height);
        }


    }
}
