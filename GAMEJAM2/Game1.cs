using System;
using System.Collections.Generic;
using System.Linq;
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

    //NO WAY OUT
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Moveable room0;
        Moveable bloodScreen;
        Moveable menu;

        Moveable shotgunPowerup;
        Moveable speedUpPowerup;
        Moveable healthPowerup;
        Moveable bombPowerup;


        Moveable crosshair;


        Explosion exp;

        bool shotgunDropped = false;
        bool shotgunPickedUp = false;
        bool speedUpDropped = false;
        bool speedUpPickedUp = false;
        bool healthDropped = false;
        bool healthPickedUp = false;
        bool bombDropped = false;
        bool bombPickedUp = false;

        bool gettingHurt = false;
        bool wasGettingHurt = false;

        bool atMenu = true;

        int timeMSec = 0;

        int kills = 0;

        SoundEffect shotgun;
        SoundEffect pistol;
        SoundEffect theme;
        SoundEffect end;
        SoundEffect ahh;
        SoundEffect healSound;
        SoundEffect speedSound;
        SoundEffect explosionSound;

        Player p0;

        SpriteFont font1;
        SpriteFont font2;

        MouseState mouseStatePrevious;
        KeyboardState keyStatePrevious;

        Projectile[] projectiles = new Projectile[20];
        Projectile[] shotgunShells = new Projectile[3];

        Corpse[] corpses = new Corpse[5000];

        Enemy[] enemiesWest = new Enemy[3];
        Enemy[] enemiesNorth = new Enemy[2];
        Enemy[] enemiesSouth = new Enemy[3];

        Wall w1;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.IsFullScreen = true;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Random rand = new Random();


            exp = new Explosion(Content.Load<Texture2D>("explosion"), new Vector2(0, 0));

            Texture2D player = Content.Load<Texture2D>("player0");

            shotgun = Content.Load<SoundEffect>("shotgun1");
            pistol = Content.Load<SoundEffect>("pistol1");
            theme = Content.Load<SoundEffect>("pushLine");
            end = Content.Load<SoundEffect>("TheEnd");
            ahh = Content.Load<SoundEffect>("ahh");
            healSound = Content.Load<SoundEffect>("healSound");
            speedSound = Content.Load<SoundEffect>("speedSound");
            explosionSound = Content.Load<SoundEffect>("GrenadeSound");

            p0 = new Player(player, new Vector2(300, 250), Content.Load<Texture2D>("laser"), Content.Load<Texture2D>("flash"));

            room0 = new Moveable(Content.Load<Texture2D>("room1"), new Vector2(0, 0));
            bloodScreen = new Moveable(Content.Load<Texture2D>("bloodyScreen"), new Vector2(0, 0));
            menu = new Moveable(Content.Load<Texture2D>("menu"), new Vector2(0, 0));


            crosshair = new Moveable(Content.Load<Texture2D>("crosshair"), new Vector2(0, 0));

            w1 = new Wall(Content.Load<Texture2D>("wall0"), new Vector2(750, 0));

            shotgunPowerup = new Moveable(Content.Load<Texture2D>("shotgun"), new Vector2(rand.Next(50, 700), rand.Next(50, 500)));
            speedUpPowerup = new Moveable(Content.Load<Texture2D>("speedUp"), new Vector2(-50, -50));
            healthPowerup = new Moveable(Content.Load<Texture2D>("health"), new Vector2(-50, -50));
            bombPowerup = new Moveable(Content.Load<Texture2D>("bombPowerup"), new Vector2(-50, -50));

            font1 = Content.Load<SpriteFont>("font1");
            font2 = Content.Load<SpriteFont>("EndTitle");

            for (int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i] = new Projectile(new Vector2(-10, -10), 0);
                projectiles[i].setTexture(Content.Load<Texture2D>("projectile"));
            }
            for (int i = 0; i < shotgunShells.Length; i++)
            {
                shotgunShells[i] = new Projectile(new Vector2(-10, -10), 0);
                shotgunShells[i].setTexture(Content.Load<Texture2D>("projectile"));
            }
            for (int i = 0; i < corpses.Length; i++)
            {
                corpses[i] = new Corpse(Content.Load<Texture2D>("body0"), new Vector2(-10, -10), Content.Load<Texture2D>("body1"), rand.Next(0, 2));
            }
            for (int i = 0; i < enemiesWest.Length; i++)
            {
                enemiesWest[i] = new Enemy(Content.Load<Texture2D>("enemy0"), new Vector2(-10, rand.Next(600)));
            }
            for (int i = 0; i < enemiesNorth.Length; i++)
            {
                enemiesNorth[i] = new Enemy(Content.Load<Texture2D>("enemy0"), new Vector2(rand.Next(-50, 750), rand.Next(-300, -40)));
            }
            for (int i = 0; i < enemiesSouth.Length; i++)
            {
                enemiesSouth[i] = new Enemy(Content.Load<Texture2D>("enemy0"), new Vector2(rand.Next(-50, 750), rand.Next(650, 900)));
            }
            theme.Play();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (atMenu)
            {
                KeyboardState key = Keyboard.GetState();
                if(key.IsKeyDown(Keys.Space))
                    atMenu = false;
            }
            else
            {
                exp.update(gameTime);

                timeMSec += gameTime.ElapsedGameTime.Milliseconds;

                Random rand = new Random();

                if ((timeMSec / 1000) > 30 && !shotgunDropped && !shotgunPickedUp)
                {
                    shotgunDropped = true;
                }
                if ((((timeMSec / 1000) % 20 == 0) || ((timeMSec / 1000) == 10))  && !speedUpPickedUp && (timeMSec / 1000) >= 10 && !speedUpDropped)
                {
                    speedUpPowerup.setPosition(new Vector2(rand.Next(50, 700), rand.Next(50, 450)));
                    speedUpDropped = true;
                }
                if ((timeMSec / 1000) % 25 == 0 && !healthPickedUp && (timeMSec / 1000) > 10 && !healthDropped)
                {
                    healthPowerup.setPosition(new Vector2(rand.Next(50, 700), rand.Next(50, 450)));
                    healthDropped = true;
                }
                if ((timeMSec / 1000) % 25 == 0 && !bombPickedUp && (timeMSec / 1000) > 10 && !bombDropped)
                {
                    bombPowerup.setPosition(new Vector2(rand.Next(50, 700), rand.Next(50, 450)));
                    bombDropped = true;
                }


                float dt = gameTime.ElapsedGameTime.Milliseconds;
                

                if (p0.getIsAlive())
                {
                    updatePlayer(gameTime);
                    updateEnemies(dt);
                }
                updateProjectiles(dt);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void updateEnemies(float dt)
        {
            Random rand = new Random();
            foreach (Enemy e in enemiesWest)
            {
                if (e.getIsAlive())
                {
                    float XDistance = e.getPosition().X - p0.getPosition().X;
                    float YDistance = e.getPosition().Y - p0.getPosition().Y;

                    if (e.getHitBox().Intersects(p0.getHitBox()))
                    {
                        p0.changeHP(-1);
                        if (!wasGettingHurt)
                            ahh.Play();
                        gettingHurt = true;
                    }

                    e.setAngle(((float)Math.Atan2(YDistance, XDistance) + (float)(Math.PI)));
                    e.update(dt);
                }
                else
                {
                    foreach (Corpse c in corpses)
                    {
                        if (!c.drawMe)
                        {
                            c.setPosition(e.getPosition());
                            c.setAngle(e.getAngle());
                            c.drawMe = true;
                            break;
                        }
                    }
                    kills++;
                    e.setPosition(new Vector2(rand.Next(-100, -50), rand.Next(700)));
                    e.setHP(5);
                    e.setIsAlive(true);
                }
            }
            if ((timeMSec / 1000) > 15)
            {
                foreach (Enemy e in enemiesNorth)
                {
                    if (e.getIsAlive())
                    {
                        float XDistance = e.getPosition().X - p0.getPosition().X;
                        float YDistance = e.getPosition().Y - p0.getPosition().Y;

                        if (e.getHitBox().Intersects(p0.getHitBox()))
                        {
                            p0.changeHP(-1);
                            if (!wasGettingHurt)
                                ahh.Play();
                            gettingHurt = true;
                        }

                        e.setAngle(((float)Math.Atan2(YDistance, XDistance) + (float)(Math.PI)));
                        e.update(dt);
                    }
                    else
                    {
                        foreach (Corpse c in corpses)
                        {
                            if (!c.drawMe)
                            {
                                c.setPosition(e.getPosition());
                                c.setAngle(e.getAngle());
                                c.drawMe = true;
                                break;
                            }
                        }
                        kills++;
                        e.setPosition(new Vector2(rand.Next(-50, 750), rand.Next(-200, -10)));
                        e.setHP(5);
                        e.setIsAlive(true);
                    }
                }
            }
            if ((timeMSec / 1000) > 60)
            {
                foreach (Enemy e in enemiesSouth)
                {
                    if (e.getIsAlive())
                    {
                        float XDistance = e.getPosition().X - p0.getPosition().X;
                        float YDistance = e.getPosition().Y - p0.getPosition().Y;

                        if (e.getHitBox().Intersects(p0.getHitBox()))
                        {
                            p0.changeHP(-1);
                            if (!wasGettingHurt)
                                ahh.Play();
                            gettingHurt = true;
                        }

                        e.setAngle(((float)Math.Atan2(YDistance, XDistance) + (float)(Math.PI)));
                        e.update(dt);
                    }
                    else
                    {
                        foreach (Corpse c in corpses)
                        {
                            if (!c.drawMe)
                            {
                                c.setPosition(e.getPosition());
                                c.setAngle(e.getAngle());
                                c.drawMe = true;
                                break;
                            }
                        }
                        kills++;
                        e.setPosition(new Vector2(rand.Next(-50, 750), rand.Next(650, 800)));
                        e.setHP(5);
                        e.setIsAlive(true);
                    }
                }
            }
        }

        public void updateProjectiles(float dt)
        {
            foreach (Projectile p in projectiles)
            {
                p.update(dt);

                if (p.getDrawMe() && (p.getPosition().X > 800 || p.getPosition().Y > 600 || p.getPosition().X < 0 || p.getPosition().Y < 0))
                {
                    p.toggleDraw();
                }
                else if(p.getDrawMe())
                {
                    foreach (Enemy e in enemiesWest)
                    {
                        if (e.getIsAlive())
                        {
                            Rectangle hitBox = e.getHitBox();
                            Rectangle hitBoxShot = new Rectangle((int)p.getPosition().X, (int)p.getPosition().Y, p.getTexture().Width, p.getTexture().Height);
                            if (hitBox.Intersects(hitBoxShot))
                            {
                                e.setHP(0);
                                p.setDrawMe(false);
                            }
                        }
                    }
                    foreach (Enemy e in enemiesNorth)
                    {
                        if (e.getIsAlive())
                        {
                            Rectangle hitBox = e.getHitBox();
                            Rectangle hitBoxShot = new Rectangle((int)p.getPosition().X, (int)p.getPosition().Y, p.getTexture().Width, p.getTexture().Height);
                            if (hitBox.Intersects(hitBoxShot))
                            {
                                e.setHP(0);
                                p.setDrawMe(false);
                            }
                        }
                    }
                    foreach (Enemy e in enemiesSouth)
                    {
                        if (e.getIsAlive())
                        {
                            Rectangle hitBox = e.getHitBox();
                            Rectangle hitBoxShot = new Rectangle((int)p.getPosition().X, (int)p.getPosition().Y, p.getTexture().Width, p.getTexture().Height);
                            if (hitBox.Intersects(hitBoxShot))
                            {
                                e.setHP(0);
                                p.setDrawMe(false);
                            }
                        }
                    }
                }
            }

            if (exp.getIsGoingOff())
            {
                foreach (Enemy e in enemiesWest)
                {
                    if (e.getIsAlive())
                    {
                        if (exp.getHitBox().Intersects(e.getHitBox()))
                        {
                            e.setHP(0);
                        }
                    }
                }
                foreach (Enemy e in enemiesNorth)
                {
                    if (e.getIsAlive())
                    {
                        if (exp.getHitBox().Intersects(e.getHitBox()))
                        {
                            e.setHP(0);
                        }
                    }
                }
                foreach (Enemy e in enemiesSouth)
                {
                    if (e.getIsAlive())
                    {
                        if (exp.getHitBox().Intersects(e.getHitBox()))
                        {
                            e.setHP(0);
                        }
                    }
                }
            }


            foreach (Projectile p in shotgunShells)
            {
                p.update(dt);

                if (p.getDrawMe() && (p.getPosition().X > 800 || p.getPosition().Y > 600 || p.getPosition().X < 0 || p.getPosition().Y < 0))
                {
                    p.toggleDraw();
                }
                else if (p.getDrawMe())
                {
                    foreach (Enemy e in enemiesWest)
                    {
                        if (e.getIsAlive())
                        {
                            Rectangle hitBox = e.getHitBox();
                            Rectangle hitBoxShot = new Rectangle((int)p.getPosition().X, (int)p.getPosition().Y, p.getTexture().Width, p.getTexture().Height);
                            if (hitBox.Intersects(hitBoxShot))
                            {
                                e.setHP(0);
                                p.setDrawMe(false);
                            }
                        }
                    }
                    foreach (Enemy e in enemiesNorth)
                    {
                        if (e.getIsAlive())
                        {
                            Rectangle hitBox = e.getHitBox();
                            Rectangle hitBoxShot = new Rectangle((int)p.getPosition().X, (int)p.getPosition().Y, p.getTexture().Width, p.getTexture().Height);
                            if (hitBox.Intersects(hitBoxShot))
                            {
                                e.setHP(0);
                                p.setDrawMe(false);
                            }
                        }
                    }
                    foreach (Enemy e in enemiesSouth)
                    {
                        if (e.getIsAlive())
                        {
                            Rectangle hitBox = e.getHitBox();
                            Rectangle hitBoxShot = new Rectangle((int)p.getPosition().X, (int)p.getPosition().Y, p.getTexture().Width, p.getTexture().Height);
                            if (hitBox.Intersects(hitBoxShot))
                            {
                                e.setHP(0);
                                p.setDrawMe(false);
                            }
                        }
                    }
                }
            }
        }

        public void updatePlayer(GameTime gt)
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState keys = Keyboard.GetState();

            crosshair.setPosition(new Vector2(mouse.X-13, mouse.Y-13));



            if (mouse.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                p0.fire();
                if (p0.hasShotgun)
                    shotgun.Play();
                else
                    pistol.Play();
                for (int i = 0; i < projectiles.Length; i++)
                {
                    if (!projectiles[i].getDrawMe()) 
                    {
                        if (!p0.hasShotgun)
                        {
                            projectiles[i].toggleDraw();
                            projectiles[i].setPosition(p0.getPosition());
                            projectiles[i].setAngle(p0.getAngle());
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < shotgunShells.Length; j++)
                            {
                                if (!shotgunShells[j].getDrawMe())
                                {
                                    shotgunShells[j].toggleDraw();
                                    shotgunShells[j].setPosition(p0.getPosition());
                                    switch (j)
                                    {
                                        case 0: shotgunShells[j].setAngle(p0.getAngle());
                                            break;
                                        case 1: shotgunShells[j].setAngle(p0.getAngle() + .1f);
                                            break;
                                        case 2: shotgunShells[j].setAngle(p0.getAngle() - .1f);
                                            break;
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }

            float XDistance = p0.getPosition().X - mouse.X;
            float YDistance = p0.getPosition().Y - mouse.Y;

            p0.setAngle(((float)Math.Atan2(YDistance, XDistance) + (float)(Math.PI)));

            Vector2 dest = new Vector2(0,0);

            if (keys.IsKeyDown(Keys.A))
            {
                if (!w1.getHitBox().Contains(p0.getHitBox().Left, w1.getHitBox().Y))
                {
                    dest.X = -1;
                }
            }
            if (keys.IsKeyDown(Keys.D))
            {
                if (!w1.getHitBox().Contains(p0.getHitBox().Right, w1.getHitBox().Y))
                {
                    dest.X = 1;
                }
            }

            if (keys.IsKeyDown(Keys.W))
            {
                if (!w1.getHitBox().Contains(p0.getHitBox().X, p0.getHitBox().Top))
                {
                    dest.Y = -1;
                }
            }
            if (keys.IsKeyDown(Keys.S))
            {
                if (!w1.getHitBox().Contains(p0.getHitBox().X, p0.getHitBox().Bottom))
                {
                    dest.Y = 1;
                }
            }
            if (dest.X != 0 && dest.Y != 0)
            {
                dest.Normalize();
            }


            if (p0.getHitBox().Intersects(shotgunPowerup.getHitBox()) && shotgunDropped)
            {
                shotgunPickedUp = true;
                p0.hasShotgun = true;
            }

            if (p0.getHitBox().Intersects(speedUpPowerup.getHitBox()))
            {
                speedUpPowerup.setPosition(new Vector2(-50, -50));
                speedUpDropped = false;
                p0.speedUpPowerup();
                speedSound.Play();
            }
            if (p0.getHitBox().Intersects(healthPowerup.getHitBox()))
            {
                healthPowerup.setPosition(new Vector2(-50, -50));
                healthDropped = false;
                p0.changeHP(50);
                healSound.Play();
            }
            if (p0.getHitBox().Intersects(bombPowerup.getHitBox()))
            {
                bombPowerup.setPosition(new Vector2(755, 0));
                bombDropped = false;
                bombPickedUp = true;
            }

            if (bombPickedUp && keys.IsKeyDown(Keys.Space) && keyStatePrevious.IsKeyUp(Keys.Space))
            {
                exp.setPosition(new Vector2(mouse.X, mouse.Y));
                bombPickedUp = false;
                bombPowerup.setPosition(new Vector2(-50, -50));
                explosionSound.Play();
                exp.fire();
            }

            

            p0.update(dest, gt.ElapsedGameTime.Milliseconds);

            mouseStatePrevious = mouse;
            keyStatePrevious = keys;

        }




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            if (atMenu)
            {
                menu.draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(room0.getTexture(), room0.getPosition(), Color.White);

                foreach (Corpse c in corpses)
                {
                    c.draw(spriteBatch);
                }

                spriteBatch.DrawString(font1, "HP: " + p0.getHP() + "     " + (timeMSec / 1000), new Vector2(50, 0), Color.Red);//FIX BEFORE SUBMISSION

                w1.draw(spriteBatch);

                if (shotgunDropped && !shotgunPickedUp)
                {
                    shotgunPowerup.draw(spriteBatch);
                }

                speedUpPowerup.draw(spriteBatch);

                healthPowerup.draw(spriteBatch);

                bombPowerup.draw(spriteBatch);


                crosshair.draw(spriteBatch);


                foreach (Enemy e in enemiesWest)
                {
                    e.draw(spriteBatch);
                }
                foreach (Enemy e in enemiesNorth)
                {
                    e.draw(spriteBatch);
                }
                foreach (Enemy e in enemiesSouth)
                {
                    e.draw(spriteBatch);
                }

                foreach (Projectile shot in projectiles)
                {
                    shot.draw(spriteBatch);
                }
                foreach (Projectile p in shotgunShells)
                {
                    p.draw(spriteBatch);
                }

                p0.draw(spriteBatch);


                wasGettingHurt = gettingHurt;
                if (gettingHurt)
                {
                    bloodScreen.draw(spriteBatch);
                    gettingHurt = false;
                }

                exp.draw(spriteBatch);

                if (!p0.getIsAlive())
                {
                    spriteBatch.DrawString(font1, "Press ESC to quit.", new Vector2(150, 100), Color.White);
                    spriteBatch.DrawString(font2, "You have died.", new Vector2(250, 300), Color.White);
                    spriteBatch.DrawString(font2, "You took " + kills + " aliens with you.", new Vector2(115, 340), Color.White);
                }
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
