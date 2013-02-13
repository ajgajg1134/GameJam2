using System;
using System.Collections.Generic;
using System.Collections;
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
    class Player : Moveable
    {
        Texture2D laser;
        Texture2D flash;
        bool hasFired = false;
        int hp = 100;
        float speed = 160f;
        int speedUpCounter = 10000;//Milliseconds
        bool speedUp = false;
        bool isAlive = true;
        public bool hasShotgun = false;

        public Player(Texture2D tex, Vector2 pos, Texture2D las, Texture2D flash)
            : base(tex, pos)
        {
            this.laser = las;
            this.flash = flash;
        }

        new public void update(Vector2 dest, float dt)
        {
            position.X += speed * dest.X * (dt / 1000);
            position.Y += speed * dest.Y * (dt / 1000);
            hitBox = new Rectangle((int)position.X - getTexture().Width / 2, (int)position.Y, getTexture().Width, getTexture().Height);
            if (speedUp)
            {
                speedUpCounter -= (int)dt;
                if (speedUpCounter < 0)
                {
                    speedUp = false;
                    speed = 150f;
                }
            }
            
        }
        public void setIsAlive(bool alive)
        {
            isAlive = alive;
        }
        public void fire()
        {
            hasFired = true;
        }
        public void speedUpPowerup()
        {
            speed = 240f;
            speedUpCounter = 10000;//milliseconds
            speedUp = true;
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
            if (isAlive)
            {

                sb.Draw(laser, getPosition(), null, Color.White, getAngle(), new Vector2(0, laser.Height / 2), 1, SpriteEffects.None, 0f);

                if (hasFired)
                    sb.Draw(flash, getPosition(), null, Color.White, getAngle(), new Vector2(0, flash.Height / 2), 1, SpriteEffects.None, 0f); 

                sb.Draw(getTexture(), getPosition(), null, Color.White, getAngle(), getOrigin(), 1, SpriteEffects.None, 0f);
            }
            hasFired = false;
        }
        
    }
}
