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
    class Projectile
    {
        Texture2D texture;
        protected Vector2 position;
        protected float angle;
        bool drawMe = false;
        float speed = 1500f;

        public Projectile(Vector2 pos, float angle)
        {
            this.position = pos;
            this.angle = angle;
        }
        public void setDrawMe(bool draw)
        {
            drawMe = draw;
        }
        public void toggleDraw()
        {
            drawMe = !drawMe;
        }
        public bool getDrawMe()
        {
            return drawMe;
        }
        public void setTexture(Texture2D tex)
        {
            this.texture = tex;
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
        public Vector2 getOrigin()
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }
        public void setAngle(float ang)
        {
            angle = ang;
        }
        public void setPosition(Vector2 pos)
        {
            this.position = pos;
        }
        public void update(float dt)
        {
            Vector2 dest = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

            position.X += speed * dest.X * (dt / 1000);
            position.Y += speed * dest.Y * (dt / 1000);
        }
        public void draw(SpriteBatch sb)
        {
            if(drawMe)
                sb.Draw(getTexture(), getPosition(), null, Color.White, getAngle(), getOrigin(), 1, SpriteEffects.None, 0f);
        }
    }
}
