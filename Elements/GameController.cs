using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Prallax.Elements
{
    public class GameController
    {
        public const int INIT_STATUS = 0;
        public const int PLAYING_STATUS = 1;
        public const int LOSE_STATUS = 2;
        public const int PAUSE_STATUS = 3;

        public const int DRAW_ACTIVE = 0;
        public const int DRAW_INACTIVE = 1;

        private KeyboardState keyboardState;
        public static GraphicsDeviceManager graphics;
        private int enemyDelay;
        private bool enemySpeedChanged = false;
        private bool enemyFrecuencyChanged = false;
        private bool bulletSpeedChanged = false;
        private bool bulletFrecuencyChanged = false;
        private bool airplaneSpeedChanged = false;
        private ArrayList arrayEnemies;
        private int gameStatus;
        private int punctuation;
        private Color drawColor;
        public static Texture2D instructionsTexture;
        public static Texture2D pauseIntructionsTexture;
        public static Texture2D gameOverInstructionsTexture;

        public GameController()
        {
            arrayEnemies = new ArrayList();
            gameStatus = INIT_STATUS;
            InstructionsRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public void MoveAirplane(Airplane airplane)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                airplane.MoveUp();
            if (keyboardState.IsKeyDown(Keys.Down))
                airplane.MoveDown();
            if (keyboardState.IsKeyDown(Keys.Left))
                airplane.MoveLeft();
            if (keyboardState.IsKeyDown(Keys.Right))
                airplane.MoveRight();
            if (keyboardState.IsKeyUp(Keys.Right) && airplane.Rectangle.X > 0)
                airplane.MoveBack();
        }

        public void AirplaneShoot(Airplane airplane)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && airplane.DelayShoot >= BulletHeart.size)
            {
                airplane.Shoot();
                airplane.DelayShoot = 0;
            }
            airplane.DelayShoot++;
        }

        public void MoveBullets(ArrayList arrayBullet)
        {
            foreach (BulletHeart bulletHeart in arrayBullet.ToArray())
            {
                bulletHeart.Move();
                if (bulletHeart.Rectangle.X >= graphics.PreferredBackBufferWidth)
                    arrayBullet.Remove(bulletHeart);
            }
        }

        public void EnemyAppear()
        {
            if (enemyDelay >= Enemy.Delay)
            {
                arrayEnemies.Add(new Enemy());
                enemyDelay = 0;
                Enemy.DefineDelay();
            }
            enemyDelay++;
        }

        public void MoveEnemies()
        {
            foreach (Enemy enemy in arrayEnemies.ToArray())
            {
                enemy.Move();
                //Eliminar al enemigo cuando salga de la pantalla
                if (enemy.Rectangle.Right <= 0)
                {
                    arrayEnemies.Remove(enemy);
                    punctuation -= 10;
                }
            }
        }

        public void ChangeBulletFrecuency(Airplane airplane)
        {
            if (punctuation != 0 && punctuation % 200 == 0 && airplane.DelayShoot >= 10 && !bulletFrecuencyChanged)
            {
                airplane.DelayShoot -= 1;
                bulletFrecuencyChanged = true;
            }
        }

        public void ChangeBulletSpeed()
        {
            if (punctuation != 0 && punctuation % 200 == 0 && BulletHeart.speed <= 15 && !bulletSpeedChanged)
            {
                BulletHeart.speed += 1;
                bulletSpeedChanged = true;
            }
        }
        public void ChangeAirplaneSpeed()
        {
            if (punctuation != 0 && punctuation % 200 == 0 && BulletHeart.speed <= 10 && !airplaneSpeedChanged)
            {
                Airplane.speed += 1;
                airplaneSpeedChanged = true;
            }
        }

        public void ChangeEnemiesFrecuency()
        {
            if (punctuation != 0 && punctuation % 100 == 0 && Enemy.inferiorLimitDelay >= 0 && !enemyFrecuencyChanged)
            {
                Enemy.inferiorLimitDelay -= 10;
                enemyFrecuencyChanged = true;
            }
        }

        public void ChangeEnemiesSpeed()
        {
            if (punctuation != 0 && punctuation % 100 == 0 && Enemy.speed <= 15 && !enemySpeedChanged)
            {
                Enemy.speed += 1;
                enemySpeedChanged = true;
            }
        }

        public void VerifyBulletEnemyImpact(ArrayList arrayBullet)
        {
            // Controlar los imactos de la bala con el enemigo
            foreach (Enemy enemy in arrayEnemies.ToArray())
            {
                foreach (BulletHeart bulletHeart in arrayBullet.ToArray())
                {
                    if (bulletHeart.Rectangle.Intersects(enemy.Rectangle))
                    {
                        arrayBullet.Remove(bulletHeart);
                        arrayEnemies.Remove(enemy);
                        Enemy.destroySound.Play();
                        punctuation += 10;
                        enemySpeedChanged = false;
                        enemyFrecuencyChanged = false;
                        bulletSpeedChanged = false;
                        bulletFrecuencyChanged = false;
                        airplaneSpeedChanged = false;
                    }
                 }
            }
        }

        public ArrayList ArrayEnemies
        {
            get
            {
                return arrayEnemies;
            }
            set
            {
                this.arrayEnemies = value;
            }
        }

        public int GameStatus
        {
            get
            {
                return gameStatus;
            }
            set
            {
                this.gameStatus = value;
            }
        }

        public void ChangeToPlayStatus()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
            {
                if(gameStatus == PAUSE_STATUS)
                {
                    MediaPlayer.Resume();
                }
                gameStatus = PLAYING_STATUS;
            }
        }

        public void ChangeToPauseStatus()
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.P))
            {
                MediaPlayer.Pause();
                gameStatus = PAUSE_STATUS;
            }
        }

        public int Punctuation
        {
            get
            {
                return punctuation;
            }
            set
            {
                this.punctuation = value;
            }
        }

        public void VerifyIfLose()
        {
            if(punctuation < 0)
            {
                gameStatus = LOSE_STATUS;
                MediaPlayer.Stop();
            }
        }

        public void ChangeToInitStatus(Song backgroundSong, ArrayList arrayBullet)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.C))
            {
                arrayEnemies.Clear();
                arrayBullet.Clear();
                punctuation = 0;
                MediaPlayer.Play(backgroundSong);
                gameStatus = INIT_STATUS;
            }
        }

        public void VerifyAirplaneBulletCollision(Airplane airplane)
        {
            foreach(Enemy enemy in arrayEnemies)
            {
                if (airplane.Rectangle.Intersects(enemy.Rectangle))
                {
                    gameStatus = LOSE_STATUS;
                    MediaPlayer.Stop();
                }
            }
        }

        private Color DefineColor(int drawState)
        {
            if (drawState == DRAW_ACTIVE)
            {
                return Color.White;
            }
            else
            {
                return Color.Gray;
            }
        }

        public void DrawBackground(SpriteBatch spriteBatch, Scene scene, int drawState)
        {
            drawColor = DefineColor(drawState);
            spriteBatch.Draw(scene.BackgroundTexture, scene.BackgroundRectangle, drawColor);
            spriteBatch.Draw(scene.CloudsTexture, scene.CloudsRectangle, drawColor);
            spriteBatch.Draw(scene.CloudsTexture, scene.CloudsRectangle2, drawColor);
            spriteBatch.Draw(scene.CityTexture, scene.CityRectangle, drawColor);
            spriteBatch.Draw(scene.CityTexture, scene.CityRectangle2, drawColor);
            spriteBatch.Draw(scene.FloorTexture, scene.FloorRectangle, drawColor);
            spriteBatch.Draw(scene.FloorTexture, scene.FloorRectangle2, drawColor);
        }

        public void DrawAirplane(SpriteBatch spriteBatch, Airplane airplane, int drawState)
        {
            drawColor = DefineColor(drawState);
            spriteBatch.Draw(airplane.Texture2D, airplane.Rectangle, drawColor);
        }

        public void DrawBullets(SpriteBatch spriteBatch, ArrayList arrayBullet, int drawState)
        {
            drawColor = DefineColor(drawState);
            foreach (BulletHeart bulletHeart in arrayBullet)
            {
                spriteBatch.Draw(BulletHeart.texture2D, bulletHeart.Rectangle, drawColor);
            }
        }

        public void DrawEnemies(SpriteBatch spriteBatch, int drawState)
        {
            drawColor = DefineColor(drawState);
            foreach (Enemy enemy in arrayEnemies)
            {
                spriteBatch.Draw(Enemy.texture2D, enemy.Rectangle, drawColor);
            }
        }

        public Rectangle InstructionsRectangle { get; set; }

        public void DrawInstuctions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(instructionsTexture, InstructionsRectangle, new Color(Color.DarkGray, (float)0.5));
        }

        public void DrawPauseInstuctions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pauseIntructionsTexture, InstructionsRectangle, new Color(Color.DarkGray, (float)0.5));
        }

        public void DrawGameOverInstuctions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameOverInstructionsTexture, InstructionsRectangle, new Color(Color.DarkGray, (float)0.5));
        }
    }
}
