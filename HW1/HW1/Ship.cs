using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        private int _score = 0;
        public int Energy => _energy;
        public int Score => _score;
        Bitmap bmp =  new Bitmap(Properties.Resources.ship);

        public static event Message MessageDie;
        public static LogData logdata = new LogData();

        public void EnergyDown (int n)
        {
            _energy -= n;
            Log log = new Log(logdata.LogConsoleWrite);
            log($"Получено {n} урона");
        }

        internal void EnergyUp(int n)
        {
            _energy += n;
            Log log = new Log(logdata.LogConsoleWrite);
            log($"Восстановлено {n} здоровья");
        }
        public void AddScore (int n)
        {
            _score += n;
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(bmp, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {   
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }

        
    }
}
