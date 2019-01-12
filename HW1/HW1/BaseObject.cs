using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HW1
{
    public delegate void Message();

    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        protected BaseObject (Point pos, Point dir, Size size)
        {
            if (pos.X < 0 || pos.Y < 0) throw new GameObjectException("Отрицательные координаты для создания объекта запрещены!");
            Pos = pos;
            Dir = dir;
            if (size.Height < 0 || size.Width < 0) throw new GameObjectException("Отрицательный размер для создания объекта запрещены!");
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
