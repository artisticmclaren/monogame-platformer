using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace simple_platformer
{

    public class Game1 : Game
    {

        // default variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int wWidth=1280;
        int wHeight=720;

        Entity player = new Entity("block",new Vector2(1280/2,720/2),0.5f,Color.White,1);
        Entity sky = new Entity("sky",new Vector2(1280/2,720/2),0f,Color.White,0);

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

            if (player.position.Y+player.texture.Height < 720)
            {
                player.velocity.Y -= gravity;
            }

            player.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _spriteBatch.Draw(sky.texture, sky.position, null, sky.color, 0f, sky.GetOrigin(), new Vector2(1.2f, 1), SpriteEffects.None, sky.depth);
            _spriteBatch.Draw(player.texture,player.position,null, player.color,0f,player.GetOrigin(),Vector2.One,SpriteEffects.None,player.depth);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public struct Entity // basic enough entity system for now
    {
        public Entity(string _textureName,Vector2 _position,float _friction,Color _color,float _depth)
        {
            textureName = _textureName;
            position = _position;
            color = _color;
            depth = _depth;
            friction = _friction;
        }

        public string textureName;
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D texture;
        public Color color;
        public float depth;
        public float friction;

        public Vector2 GetOrigin()
        {
            Vector2 output=position;

            if (texture == null) return output;
            output = new Vector2(texture.Width/2,texture.Height/2);

            return output;
        }

        public void Update()
        {
            if (velocity.X > 0) 
            { 
                if (velocity.X-friction<0) velocity.X = 0;
                velocity.X -= friction; 
            }
            else if (velocity.X<0)
            {
                if (velocity.X + friction > 0) velocity.X = 0;
                velocity.X += friction;
            }

            position.X += velocity.X;
            position.Y += velocity.Y;
        }
    }
}