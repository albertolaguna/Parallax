using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Prallax.Elements
{
    public class Enemy
    {
        private Rectangle rectangle;
        private readonly int initY;
        private static Random random = new Random();
        public static int delay;
        public static Texture2D texture2D;
        public static GraphicsDeviceManager graphics;
        public static int size;
        public static int speed = 5;
        public static int inferiorLimitDelay = 50;
        public static SoundEffect destroySound;

        public Enemy()
        {
            size = 50;
            initY = DefineInitY();
            rectangle = new Rectangle(graphics.PreferredBackBufferWidth, initY, size, size);
        }

        public void Move()
        {
            rectangle.X -= speed;
        }

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                this.rectangle = value;
            }
        }

        private int DefineInitY()
        {
            return random.Next(50, graphics.PreferredBackBufferHeight- size);
        }

        public static void DefineDelay()
        {
            delay = random.Next(inferiorLimitDelay, 100);
        }

        public static int Delay => delay;
    }
}
