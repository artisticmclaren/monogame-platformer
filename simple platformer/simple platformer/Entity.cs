using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace simple_platformer
{
    public class Entity
    {
        public string textureName;
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D texture;
        public Color color;
        public float depth;
        public float friction;

        public Entity(string _textureName, Vector2 _position, float _friction, Color _color, float _depth)
        {
            textureName = _textureName;
            position = _position;
            color = _color;
            depth = _depth;
            friction = _friction;
        }

        public Vector2 GetOrigin()
        {
            Vector2 output = position;

            if (texture == null) return output;
            output = new Vector2(texture.Width / 2, texture.Height / 2);

            return output;
        }

        public void Update()
        {
            if (velocity.X > 0)
            {
                if (velocity.X - friction < 0) velocity.X = 0;
                velocity.X -= friction;
            }
            else if (velocity.X < 0)
            {
                if (velocity.X + friction > 0) velocity.X = 0;
                velocity.X += friction;
            }

            position.X += velocity.X;
            position.Y += velocity.Y;
        }
    }
}
