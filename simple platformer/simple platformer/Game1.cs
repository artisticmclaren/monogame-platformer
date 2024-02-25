using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using simple_platformer.UI;

namespace simple_platformer
{

    public class Game1 : Game
    {

        // default variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        // new cool variables
        int wWidth=1280;
        int wHeight=720;

        Entity player = new Entity("block",new Vector2(1280/2,720/2),0.5f,Color.White,1);
        Entity sky = new Entity("sky",new Vector2(1280/2,720/2),0f,Color.White,0);

        ToggleElement pause = new ToggleElement(new Vector2(25, 25), "pause", "unpause");

        float gravity;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // window resizing
            _graphics.PreferredBackBufferWidth = wWidth;
            _graphics.PreferredBackBufferHeight = wHeight;
            _graphics.ApplyChanges();

            gravity = -0.2f;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            player.texture = Content.Load<Texture2D>(player.textureName);
            sky.texture = Content.Load<Texture2D>(sky.textureName);


            pause.toggleOn = Content.Load<Texture2D>(pause.toggleOnName);
            pause.toggleOff = Content.Load<Texture2D>(pause.toggleOffName);
        }

        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))  Exit();

            var kstate = Keyboard.GetState();

            // user input
            if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
            {
                player.velocity.X -= .7f;
            }
            if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
            {
                player.velocity.X += .7f;
            }
            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
            {
                player.velocity.Y -= .7f;
            }
            
            player.velocity.Y -= gravity;

            player.Update();
            pause.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _spriteBatch.Draw(sky.texture, sky.position, null, sky.color, 0f, sky.GetOrigin(), new Vector2(1.2f, 1), SpriteEffects.None, sky.depth);
            _spriteBatch.Draw(player.texture,player.position,null, player.color,0f,player.GetOrigin(),Vector2.One,SpriteEffects.None,player.depth);

            _spriteBatch.Draw(pause.GetTexture(), pause.position, null, Color.White, 0f, pause.GetOrigin(), pause.scale, SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}