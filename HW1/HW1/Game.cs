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

        static Game()
        {

        }

        public static void  Init (Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick (object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach(BaseObject obj in _objs)
            {
                obj.Draw();
            }
            Buffer.Render();

        }

        public static void Update ()
        {
            foreach(BaseObject obj in _objs)
            {
                obj.Update();
            }
        }

        public static BaseObject[] _objs;

        public static void Load ()
        {
            _objs = new BaseObject[60];
            for (int i = 0; i <= _objs.Length/10; i++)
            {
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(10-i*4, 10-i*4), new Size(50, 50));
            }
            for (int i = _objs.Length/10; i < _objs.Length; i++)
            {
                _objs[i] = new Star(new Point(600, i * 15), new Point(10 -i, 10 -i), new Size(5, 5));
            }
        }
    }
}
