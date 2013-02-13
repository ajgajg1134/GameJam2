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
    class Enemy : Moveable
    {
        float speed = 200f;
        bool isAlive = true;
        int hp = 5;
        public Enemy(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {

        }
        new public void update(Vector2 dest, float dt)
        {
            position.X += speed * dest.X * (dt / 1000);
            position.Y += speed * dest.Y * (dt / 1000);
            hitBox = new Rectangle((int)position.X - (getTexture().Width / 2), (int)position.Y, getTexture().Width-5, getTexture().Height-5); 
        }
        public void update(float dt)
        {
            Vector2 dest = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

            position.X += speed * dest.X * (dt / 1000);
            position.Y += speed * dest.Y * (dt / 1000);
            hitBox = new Rectangle((int)position.X - (getTexture().Width / 2), (int)position.Y, getTexture().Width-5, getTexture().Height-5); 
        }
        public void setIsAlive(bool alive)
        {
            isAlive = alive;
        }
        public bool getIsAlive()
        {
            return isAlive;
        }
        public int getHP()
        {
            return hp;
        }
        public void setHP(int health)
        {
            hp = health;
            if (hp <= 0)
            {
                isAlive = false;
            }
            else
            {
                isAlive = true;
            }
        }
        public void changeHP(int change)
        {
            hp += change;
            if (hp <= 0)
            {
                isAlive = false;
            }
            else
            {
                isAlive = true;
            }
        }
        public void draw(SpriteBatch sb)
        {
            if(isAlive)
                sb.Draw(getTexture(), getPosition(), null, Color.White, getAngle(), getOrigin(), 1, SpriteEffects.None, 0f);
        }
    }

}
