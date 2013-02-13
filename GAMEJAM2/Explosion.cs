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
    class Explosion
    {
        Texture2D explosionSheet;

        Rectangle hitBox;

        Vector2 position;

        bool isGoingOff = false;

        //A Timer variable
        float timer = 0f;
        //The interval (100 milliseconds)
        float interval = 50f;
        //Current frame holder (start at 1)
        int currentFrame = 1;
        //Width of a single sprite image, not the whole Sprite Sheet
        int spriteWidth = 100;
        //Height of a single sprite image, not the whole Sprite Sheet
        int spriteHeight = 80;
        //A rectangle to store which 'frame' is currently being shown
        Rectangle sourceRect;
        //The centre of the current 'frame'
        Vector2 origin;

        public Explosion(Texture2D tex, Vector2 pos)
        {
            this.explosionSheet = tex;
            this.position = pos;
            this.hitBox = new Rectangle((int)position.X-50, (int)position.Y-50, spriteWidth + 50, spriteHeight + 50);
        }
        public void update(GameTime gt)
        {
            if (isGoingOff)
            {
                //Increase the timer by the number of milliseconds since update was last called
                timer += (float)gt.ElapsedGameTime.TotalMilliseconds;

                //Check the timer is more than the chosen interval
                if (timer > interval)
                {
                    //Show the next frame
                    currentFrame++;
                    //Reset the timer
                    timer = 0f;
                }
                //If we are on the last frame, reset back to the one before the first frame (because currentframe++ is called next so the next frame will be 1!)
                if (currentFrame == 12)
                {
                    currentFrame = 0;
                    isGoingOff = false;
                }
                this.hitBox = new Rectangle((int)position.X - 50, (int)position.Y - 50, spriteWidth + 50, spriteHeight + 50);
                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            }
        }
        public Rectangle getHitBox()
        {
            return hitBox;
        }
        public bool getIsGoingOff()
        {
            return isGoingOff;
        }
        public void fire()
        {
            isGoingOff = true;
        }
        public void draw(SpriteBatch sb)
        {
            if(isGoingOff)
                sb.Draw(explosionSheet, position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }
        public void setPosition(Vector2 pos)
        {
            this.position = pos;
        }


    }
}
