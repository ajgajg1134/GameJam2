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
    class Corpse
    {
        Texture2D texture, texture2;
        int isFirst = 0;
        protected Vector2 position;
        protected float angle;
        Vector2 origin;
        public bool drawMe = false;
        public Corpse(Texture2D tex, Vector2 pos, Vector2 ori, Texture2D tex2)
        {
            this.texture = tex;
            this.texture2 = tex2;
            Random rand = new Random();
            isFirst = rand.Next(-1, 2);
            this.position = pos;
            this.origin = ori;
            this.angle = 0f;
        }
        public Corpse(Texture2D tex, Vector2 pos, Texture2D tex2, int first)
        {
            this.texture = tex;
            this.texture2 = tex2;
            this.isFirst = first;
            this.position = pos;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.angle = 0f;
        }
        public void setPosition(Vector2 pos)
        {
            this.position = pos;
        }
        public void setAngle(float ang)
        {
            this.angle = ang;
        }
        public void draw(SpriteBatch sb)
        {
            if (drawMe)
            {
                if (isFirst == 0)
                {
                    sb.Draw(texture, position, null, Color.White, angle, origin, 1, SpriteEffects.None, 0);
                    //Console.WriteLine("0");
                }
                else if (isFirst == 1)
                {
                    sb.Draw(texture2, position, null, Color.White, angle, origin, 1, SpriteEffects.None, 0);
                    //Console.WriteLine("1");
                }
            }
        }
    }
}
