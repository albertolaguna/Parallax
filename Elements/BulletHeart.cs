using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Prallax.Elements
{
    public class BulletHeart
    {
        private Rectangle rectangle;
        public static Texture2D texture2D;
        public static int size;
        public static int speed = 10;

        public BulletHeart(int initX, int initY)
        {
            size = 20;
            rectangle = new Rectangle(initX, initY, size, size);
        }

        public void Move()
        {
            rectangle.X+=speed;
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
    }
}
