using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Texture2D paddleTexture;
        Texture2D netTexture;

        Texture2D playerWon;
        Texture2D playerLost;

        Texture2D zeroTexture;
        Texture2D oneTexture;
        Texture2D twoTexture;
        Texture2D threeTexture;
        Texture2D fourTexture;
        Texture2D fiveTexture;
        Texture2D sixTexture;
        Texture2D sevenTexture;
        Texture2D eightTexture;
        Texture2D nineTexture;

        Vector2 ballPosition;
        Vector2 playerPosition;
        Vector2 aiPosition;

        float ballSpeedY;
        float ballSpeedX;
        float paddleSpeed;
        float playerScore;
        float aiScore;

        bool spinningClockwise;
        bool running;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            running = true;

            playerScore = 0;
            aiScore = 0;

            ballSpeedX = -5f;
            ballSpeedY = 0;
            paddleSpeed = 10;

            ballPosition.X = _graphics.PreferredBackBufferWidth / 2;
            ballPosition.Y = _graphics.PreferredBackBufferHeight / 2;

            playerPosition.X = 100;
            playerPosition.Y = 100;

            aiPosition.X = 1100;
            aiPosition.Y = 100;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            ballTexture = Content.Load<Texture2D>("ball");
            paddleTexture = Content.Load<Texture2D>("paddle");
            netTexture = Content.Load<Texture2D>("net");

            zeroTexture = Content.Load<Texture2D>("Number0 7x10");
            oneTexture = Content.Load<Texture2D>("Number1 7x10");
            twoTexture = Content.Load<Texture2D>("Number2 7x10");
            threeTexture = Content.Load<Texture2D>("Number3 7x10");
            fourTexture = Content.Load<Texture2D>("Number4 7x10");
            fiveTexture = Content.Load<Texture2D>("Number5 7x10");
            sixTexture = Content.Load<Texture2D>("Number6 7x10");
            sevenTexture = Content.Load<Texture2D>("Number7 7x10");
            eightTexture = Content.Load<Texture2D>("Number8 7x10");
            nineTexture = Content.Load<Texture2D>("Number9 7x10");

            playerWon = Content.Load<Texture2D>("playerWon");
            playerLost = Content.Load<Texture2D>("playerLost");
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            
            

            ballPosition.X += ballSpeedX;
            ballPosition.Y += ballSpeedY / 15;

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) && playerPosition.Y - (paddleTexture.Height / 2) > 0)
            {
                playerPosition.Y -= 5;
            }else if (keyboardState.IsKeyDown(Keys.S) && playerPosition.Y + (paddleTexture.Height / 2) < 800)
            {
                playerPosition.Y -= -5;
            }

            if (running == false && keyboardState.IsKeyDown(Keys.Space)){
                Initialize();
            }

                if (ballPosition.Y > aiPosition.Y && aiPosition.Y + (paddleTexture.Height / 2) < 800)
            {
                aiPosition.Y += paddleSpeed;
            }

            if (ballPosition.Y < aiPosition.Y && aiPosition.Y - (paddleTexture.Height / 2) > 0)
            {
                aiPosition.Y -= paddleSpeed;
            }
            
            if(ballPosition.X < 0)
            {
                aiScore++;
                ballPosition.X = _graphics.PreferredBackBufferWidth / 2;
                ballPosition.Y = _graphics.PreferredBackBufferHeight / 2;
                ballSpeedX = -5;
                ballSpeedY = 0;
            }
            else if(ballPosition.X > 1200)
            {
                playerScore++;
                ballPosition.X = _graphics.PreferredBackBufferWidth / 2;
                ballPosition.Y = _graphics.PreferredBackBufferHeight / 2;
                ballSpeedX = -5;
                ballSpeedY = 0;
            }


            if (ballPosition.X - (ballTexture.Width / 2) <= playerPosition.X + (paddleTexture.Width / 2) && ballPosition.Y + (ballTexture.Height / 2) >= playerPosition.Y - (paddleTexture.Height / 2) && ballPosition.Y - (ballTexture.Height / 2) <= playerPosition.Y + (paddleTexture.Height / 2))
            {
                ballSpeedX *= -1.1f;

                ballSpeedY = ballPosition.Y - playerPosition.Y;
            }

            if (ballPosition.X + (ballTexture.Width / 2) >= aiPosition.X - (paddleTexture.Width / 2) && ballPosition.Y + (ballTexture.Height / 2) >= aiPosition.Y - (paddleTexture.Height / 2) && ballPosition.Y - (ballTexture.Height / 2) <= aiPosition.Y + (paddleTexture.Height / 2))
            {
                ballSpeedX *= -1.1f;

                ballSpeedY = ballPosition.Y - aiPosition.Y;
            }

            if (ballPosition.Y <= 0 || ballPosition.Y >= 800)
            {
                ballSpeedY *= -1;
            }

            if(playerScore == 10 || aiScore == 10)
            {
                running = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if (running == true)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(netTexture, new Vector2(600, 25), null, Color.White, 0f, new Vector2(netTexture.Width / 2, 0), Vector2.One, SpriteEffects.None, 0f);

                switch (playerScore)
                {
                    case 0f:
                        _spriteBatch.Draw(zeroTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 1f:
                        _spriteBatch.Draw(oneTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 2f:
                        _spriteBatch.Draw(twoTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 3f:
                        _spriteBatch.Draw(threeTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 4f:
                        _spriteBatch.Draw(fourTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 5f:
                        _spriteBatch.Draw(fiveTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 6f:
                        _spriteBatch.Draw(sixTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 7f:
                        _spriteBatch.Draw(sevenTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 8f:
                        _spriteBatch.Draw(eightTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                    case 9f:
                        _spriteBatch.Draw(nineTexture, new Rectangle(450, 50, 70, 100), Color.White);
                        break;
                }

                switch (aiScore)
                {
                    case 0f:
                        _spriteBatch.Draw(zeroTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 1f:
                        _spriteBatch.Draw(oneTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 2f:
                        _spriteBatch.Draw(twoTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 3f:
                        _spriteBatch.Draw(threeTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 4f:
                        _spriteBatch.Draw(fourTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 5f:
                        _spriteBatch.Draw(fiveTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 6f:
                        _spriteBatch.Draw(sixTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 7f:
                        _spriteBatch.Draw(sevenTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 8f:
                        _spriteBatch.Draw(eightTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                    case 9f:
                        _spriteBatch.Draw(nineTexture, new Rectangle(680, 50, 70, 100), Color.White);
                        break;
                }

                _spriteBatch.Draw(netTexture, new Vector2(600, 450), null, Color.White, 0f, new Vector2(netTexture.Width / 2, 0), Vector2.One, SpriteEffects.None, 0f);
                _spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
                _spriteBatch.Draw(paddleTexture, playerPosition, null, Color.White, 0f, new Vector2(paddleTexture.Width / 2, paddleTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
                _spriteBatch.Draw(paddleTexture, aiPosition, null, Color.White, 0f, new Vector2(paddleTexture.Width / 2, paddleTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }
            else if(playerScore == 10)
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(playerWon, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), null, Color.White, 0f, new Vector2(playerWon.Width / 2, playerWon.Height / 2), Vector2.One, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }
            else
            {
                _spriteBatch.Begin();
                _spriteBatch.Draw(playerLost, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), null, Color.White, 0f, new Vector2(playerLost.Width / 2, playerLost.Height / 2), Vector2.One, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }
            

            base.Draw(gameTime);
        }
    }
}