using System.Drawing;

namespace HW1
{
    interface ICollision
    {
        //Интерфейс обнаружения столкновений
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
