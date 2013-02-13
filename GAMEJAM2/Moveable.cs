using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GAMEJAM2
{
    class Moveable
    {
        Texture2D texture;
        protected Vector2 position;
        protected float angle;
        Vector2 origin;
        protected Rectangle hitBox;
        public Moveable(Texture2D tex, Vector2 pos, Vector2 ori)
        {
            this.texture = tex;
            this.position = pos;
            this.origin = ori;
            this.angle = 0f;
            hitBox = new Rectangle((int)position.X - getTexture().Width / 2, (int)position.Y, texture.Width, texture.Height); 
        }
        public Moveable(Texture2D tex, Vector2 pos)
        {
            this.texture = tex;
            this.position = pos;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.angle = 0f;
            hitBox = new Rectangle((int)position.X - getTexture().Width / 2, (int)position.Y, texture.Width, texture.Height); 
        }
        public Rectangle getHitBox()
        {
            return hitBox;
        }
        public Texture2D getTexture()
        {
            return texture;
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public float getAngle()
        {
            return angle;
        }
        public void setPosition(Vector2 pos)
        {
            this.position = pos;
            hitBox = new Rectangle((int)position.X - getTexture().Width / 2, (int)position.Y, texture.Width, texture.Height); 
        }
        public Vector2 getOrigin()
        {
            return origin;
        }
        public void update(Vector2 dest, float dt)
        {
            position.X += dest.X * (dt / 1000);
            position.Y += dest.Y * (dt / 1000);
            hitBox = new Rectangle((int)position.X - getTexture().Width / 2, (int)position.Y, texture.Width, texture.Height); 
        }
        public void setAngle(float ang)
        {
            angle = ang;
        }
        public void draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }   
}
