using System.Collections;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Prallax.Elements
{
    public class Airplane
    {
        public static GraphicsDeviceManager graphics;
        private Texture2D texture2D;
        private Rectangle rectangle;
        private ArrayList arrayBullet;
        private int delayShoot;
        public static int speed = 6;
        public static SoundEffect shootSound;

        public Airplane()
        {
            rectangle = new Rectangle(graphics.PreferredBackBufferWidth / 2, 100, 100, 100);
            arrayBullet = new ArrayList();
            delayShoot = 0;
        }

        public void Shoot()
        {
            arrayBullet.Add(new BulletHeart(rectangle.Right, rectangle.Y + (2 * (rectangle.Width / 3)) - (BulletHeart.size / 2)));
            shootSound.Play();
        }

        public void MoveLeft()
        {
            if (rectangle.X > 0)
            {
                rectangle.X -= speed/2;
            }
        }

        public void MoveUp()
        {
            if (rectangle.Y > 0)
            {
                rectangle.Y -= speed;
            }
        }

        public void MoveDown()
        {
            if (rectangle.Bottom < graphics.PreferredBackBufferHeight)
            {
                rectangle.Y += speed;
            }
        }

        public void MoveRight()
        {
            if (rectangle.X <= (3 * (graphics.PreferredBackBufferWidth / 4)) - rectangle.Width)
            {
                rectangle.X += speed;
            }
        }

        public void MoveBack()
        {
            rectangle.X -= 3;
        }

        public Texture2D Texture2D
        {
            get
            {
                return texture2D;
            }
            set
            {
                this.texture2D = value;
            }
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

        public ArrayList ArrayBullet
        {
            get
            {
                return arrayBullet;
            }
        }

        public int DelayShoot
        {
            get
            {
                return delayShoot;
            }
            set
            {
                this.delayShoot = value;
            }
        }
    }
}
