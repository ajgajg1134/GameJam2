using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GAMEJAM2
{
    class Wall
    {
        Texture2D texture;
        Vector2 position;
        Rectangle hitBox;

        public Wall(Texture2D tex, Vector2 pos)
        {
            this.texture = tex;
            this.position = pos;
            hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); 
        }
        public Rectangle getHitBox()
        {
            return hitBox;
        }
        public void draw(SpriteBatch sb)
        {
            sb.Draw(texture, hitBox, Color.White);
        }
    }
}
