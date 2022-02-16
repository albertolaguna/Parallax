using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Prallax.Elements;

namespace Parallax
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;
        Airplane airplane;
        Song backgroundSong;
        GameController gameController;
        SpriteFont font;
        string punctuationStr = "";
        Vector2 punctuationPosition;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Scene.graphics = graphics;
            Airplane.graphics = graphics;
            Enemy.graphics = graphics;
            GameController.graphics = graphics;
            scene = new Scene();
            airplane = new Airplane();
            Enemy.DefineDelay();
            gameController = new GameController();
            punctuationPosition = new Vector2
            {
                Y = 5
            };
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create the content textures for scene components
            // Visual content
            if(gameController.GameStatus == GameController.INIT_STATUS)
            {
                scene.BackgroundTexture = Content.Load<Texture2D>("background");
                scene.CloudsTexture = Content.Load<Texture2D>("clouds");
                scene.FloorTexture = Content.Load<Texture2D>("floor");
                scene.CityTexture = Content.Load<Texture2D>("city");
                airplane.Texture2D = Content.Load<Texture2D>("plane");
                BulletHeart.texture2D = Content.Load<Texture2D>("heart");
                Enemy.texture2D = Content.Load<Texture2D>("enemy");
                font = Content.Load<SpriteFont>("Arial");
                GameController.instructionsTexture = Content.Load<Texture2D>("instructions");
                GameController.pauseIntructionsTexture = Content.Load<Texture2D>("pause");
                GameController.gameOverInstructionsTexture = Content.Load<Texture2D>("gameover");
            }

            // Sound content
            Airplane.shootSound = Content.Load<SoundEffect>("shoot");
            Enemy.destroySound = Content.Load<SoundEffect>("blop");
            backgroundSong = Content.Load<Song>("backgroundSong");
            MediaPlayer.Volume = (float)0.5;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundSong);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            punctuationPosition.X = graphics.PreferredBackBufferWidth - font.MeasureString(punctuationStr).X - 10;

            switch (gameController.GameStatus)
            {
                case GameController.INIT_STATUS:
                    scene.Move();
                    gameController.MoveAirplane(airplane);
                    gameController.AirplaneShoot(airplane);
                    gameController.MoveBullets(airplane.ArrayBullet);
                    //-------------------------------------------------------
                    gameController.ChangeToPlayStatus();
                    break;
                case GameController.PLAYING_STATUS:
                    scene.Move();
                    gameController.MoveAirplane(airplane);
                    gameController.AirplaneShoot(airplane);
                    gameController.MoveBullets(airplane.ArrayBullet);
                    gameController.EnemyAppear();
                    gameController.MoveEnemies();
                    gameController.VerifyBulletEnemyImpact(airplane.ArrayBullet);
                    gameController.VerifyAirplaneBulletCollision(airplane);
                    gameController.VerifyIfLose();
                    punctuationStr = "Puntuacion: " + gameController.Punctuation;
                    gameController.ChangeBulletSpeed();
                    gameController.ChangeBulletFrecuency(airplane);
                    gameController.ChangeEnemiesSpeed();
                    gameController.ChangeEnemiesFrecuency();
                    gameController.ChangeAirplaneSpeed();
                    //-------------------------------------------------------
                    gameController.ChangeToPauseStatus();
                    break;
                case GameController.LOSE_STATUS:
                    gameController.ChangeToInitStatus(backgroundSong, airplane.ArrayBullet);
                    break;
                case GameController.PAUSE_STATUS:
                    gameController.ChangeToPlayStatus();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            // Dibujar fondo
            gameController.DrawBackground(spriteBatch, scene, GameController.DRAW_INACTIVE);

            // Dibujar el resto
            switch (gameController.GameStatus)
            {
                case GameController.INIT_STATUS:
                    gameController.DrawAirplane(spriteBatch, airplane, GameController.DRAW_INACTIVE);
                    gameController.DrawBullets(spriteBatch, airplane.ArrayBullet, GameController.DRAW_INACTIVE);
                    gameController.DrawInstuctions(spriteBatch);
                    break;
                case GameController.PLAYING_STATUS:
                    gameController.DrawAirplane(spriteBatch, airplane, GameController.DRAW_ACTIVE);
                    gameController.DrawEnemies(spriteBatch, GameController.DRAW_ACTIVE);
                    gameController.DrawBullets(spriteBatch, airplane.ArrayBullet, GameController.DRAW_ACTIVE);
                    spriteBatch.DrawString(font, punctuationStr, punctuationPosition, Color.White);
                    break;
                case GameController.LOSE_STATUS:
                    gameController.DrawAirplane(spriteBatch, airplane, GameController.DRAW_INACTIVE);
                    gameController.DrawEnemies(spriteBatch, GameController.DRAW_INACTIVE);
                    gameController.DrawBullets(spriteBatch, airplane.ArrayBullet, GameController.DRAW_INACTIVE);
                    spriteBatch.DrawString(font, punctuationStr, punctuationPosition, Color.White);
                    gameController.DrawGameOverInstuctions(spriteBatch);
                    break;
                case GameController.PAUSE_STATUS:
                    gameController.DrawAirplane(spriteBatch, airplane, GameController.DRAW_INACTIVE);
                    gameController.DrawEnemies(spriteBatch, GameController.DRAW_INACTIVE);
                    gameController.DrawBullets(spriteBatch, airplane.ArrayBullet, GameController.DRAW_INACTIVE);
                    spriteBatch.DrawString(font, punctuationStr, punctuationPosition, Color.White);
                    gameController.DrawPauseInstuctions(spriteBatch);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
