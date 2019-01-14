using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    /// <summary>
    /// Аптечка
    /// </summary>
    class RepairKit : BaseObject, ICollision
    {
        public int Power => 15;
        Bitmap bmp = new Bitmap(Properties.Resources.recovery);

        public RepairKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        { }
        /// <summary>
        /// отрисовка
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(
                bmp,
                Pos.X, Pos.Y,
                Size.Width, Size.Height
                );
        }
        /// <summary>
        /// обновление состояния
        /// </summary>
        public override void Update()
        {
            Pos.X -= Dir.X;
            if (Pos.X < 0) Resp();
        }
        /// <summary>
        /// Respawn объекта
        /// </summary>
        public void Resp()
        {
            Random rnd = new Random();
            Pos.X = Game.Width;
            Pos.Y = rnd.Next(10, Game.Height-10);
        }
        //public new Rectangle Rect => new Rectangle(Pos, Size);
       // public new bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
    }
}
