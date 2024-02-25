using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace simple_platformer
{

    namespace UI
    {

        public class ToggleElement
        {
            public Vector2 position;
            public Vector2 scale=Vector2.One;
            MouseState mstate;

            // variables (duh)
            public string toggleOnName;
            public string toggleOffName;
            public bool toggled=false;

            public Texture2D toggleOn;
            public Texture2D toggleOff;

            public ToggleElement(Vector2 _position, string _toggleOnName, string _toggleOffName)
            {
                position = _position;
                toggleOnName = _toggleOnName;
                toggleOffName = _toggleOffName;
            }

            public Texture2D GetTexture()
            {
                if (toggled) return toggleOn;
                return toggleOff;
            }

            public Vector2 GetOrigin()
            {
                Vector2 output = position;

                Texture2D tex = GetTexture();

                output = new Vector2(tex.Width/2,tex.Height/2);

                return output;
            }

            public void Update()
            {
                // check if mouse is hovering over button
                Vector2 mousePosition = new Vector2(mstate.Position.X,mstate.Position.Y);
                Texture2D tex = GetTexture();
                Rectangle bounds = tex.Bounds; // very cool monogame :)
                
                if (bounds.Contains(mousePosition)) 
                {
                    scale = new Vector2(1.2f);

                    if (mstate.LeftButton==ButtonState.Pressed)
                    {
                        Console.WriteLine("pressed");
                    }
                }
                else
                {
                    scale = Vector2.One;
                    Console.WriteLine("ok");
                }
            }
        }
    }
}
