using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Prallax.Elements
{
    public class Scene
    {
        public static GraphicsDeviceManager graphics;
        private Texture2D backgroundTexture;
        private Texture2D floorTexture;
        private Texture2D cityTexture;
        private Texture2D cloudsTexture;
        private Rectangle backgroundRectangle;
        private Rectangle floorRectangle;
        private Rectangle cityRectangle;
        private Rectangle cloudsRectangle;
        private Rectangle floorRectangle2;
        private Rectangle cityRectangle2;
        private Rectangle cloudsRectangle2;

        public Scene()
        {
            backgroundRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            cloudsRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            floorRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            cityRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            cloudsRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            floorRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            cityRectangle2 = new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void Move()
        {
            MoveClouds();
            MoveCity();
            MoveFloor();
        }

        private void MoveClouds()
        {
            if (cloudsRectangle.X <= -graphics.PreferredBackBufferWidth)
            {
                cloudsRectangle.X = graphics.PreferredBackBufferWidth;
            }
            if (cloudsRectangle2.X <= -graphics.PreferredBackBufferWidth)
            {
                cloudsRectangle2.X = graphics.PreferredBackBufferWidth;
            }
            cloudsRectangle.X--;
            cloudsRectangle2.X--;
        }

        private void MoveCity()
        {
            if (cityRectangle.X <= -graphics.PreferredBackBufferWidth)
            {
                cityRectangle.X = graphics.PreferredBackBufferWidth;
            }
            if (cityRectangle2.X <= -graphics.PreferredBackBufferWidth)
            {
                cityRectangle2.X = graphics.PreferredBackBufferWidth;
            }
            cityRectangle.X-=2;
            cityRectangle2.X-=2;
        }

        private void MoveFloor()
        {
            if (floorRectangle.X <= -graphics.PreferredBackBufferWidth)
            {
                floorRectangle.X = graphics.PreferredBackBufferWidth;
            }
            if (floorRectangle2.X <= -graphics.PreferredBackBufferWidth)
            {
                floorRectangle2.X = graphics.PreferredBackBufferWidth;
            }
            floorRectangle.X-=4;
            floorRectangle2.X-=4;
        }

        public Texture2D BackgroundTexture
        {
            get
            {
                return backgroundTexture;
            }
            set
            {
                this.backgroundTexture = value;
            }
        }

        public Texture2D FloorTexture
        {
            get
            {
                return floorTexture;
            }
            set
            {
                this.floorTexture = value;
            }
        }

        public Texture2D CityTexture
        {
            get
            {
                return cityTexture;
            }
            set
            {
                this.cityTexture = value;
            }
        }

        public Texture2D CloudsTexture
        {
            get
            {
                return cloudsTexture;
            }
            set
            {
                this.cloudsTexture = value;
            }
        }

        public Rectangle BackgroundRectangle
        {
            get
            {
                return backgroundRectangle;
            }
            set
            {
                this.backgroundRectangle = value;
            }
        }

        public Rectangle FloorRectangle
        {
            get
            {
                return floorRectangle;
            }
            set
            {
                this.floorRectangle = value;
            }
        }

        public Rectangle CityRectangle
        {
            get
            {
                return cityRectangle;
            }
            set
            {
                this.cityRectangle = value;
            }
        }

        public Rectangle CloudsRectangle
        {
            get
            {
                return cloudsRectangle;
            }
            set
            {
                this.cloudsRectangle = value;
            }
        }

        public Rectangle FloorRectangle2
        {
            get
            {
                return floorRectangle2;
            }
            set
            {
                this.floorRectangle2 = value;
            }
        }

        public Rectangle CityRectangle2
        {
            get
            {
                return cityRectangle2;
            }
            set
            {
                this.cityRectangle2 = value;
            }
        }

        public Rectangle CloudsRectangle2
        {
            get
            {
                return cloudsRectangle2;
            }
            set
            {
                this.cloudsRectangle2 = value;
            }
        }
    }
}
