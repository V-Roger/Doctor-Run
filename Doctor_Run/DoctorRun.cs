using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Doctor_run;

namespace Doctor_Run
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DoctorRun : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Level lvl;
        private Doctor who;
        private Tardis tardis;
        private List<Foe> baddies;
        Random rnd;
          

        private float timer;
        private float spawnTimer;

        private bool lvlOver;

        private string lvlType = "ruins";

        private enum gameState
        {
            Start,
            InGame,
            GameOver
        }

        public DoctorRun()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            switch (this.lvlType)
            {
                case "city":
                    lvl = new City(this);
                    break;
                case "graveyard":
                    lvl = new Graveyard(this);
                    break;
                case "ruins":
                    lvl = new Ruins(this);
                    break;
                default:
                    lvl = new City(this);
                    break;
            }
            who = new Doctor(this);
            tardis = new Tardis(this);
            baddies = new List<Foe>();
            timer = -1f;
            lvlOver = false;
            rnd = new Random();
            spawnTimer = 0f;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            collide(gameTime);

            checkLvlEnd();

            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds; 

            if (timer != -1f)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
            if (lvlOver && timer > 2.0f)
            {
                switch (this.lvlType)
                {
                    case "city":
                        this.lvlType = "graveyard";
                        break;
                    case "graveyard":
                        this.lvlType = "ruins";
                        break;
                    default:
                        break;
                }
                this.Initialize();
            }

            if(this.lvlType == "city")
            {
                CybermenInvasion(gameTime);
            }
            else if (this.lvlType == "graveyard")
            {
                WeepingAngelsAttack(gameTime);
            }
            else
            {
                DalekApocalypse(gameTime);
            }



            if (who.State == Status.DEAD)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void collide(GameTime gameTime)
        {

            ennemiesCollision();

            if(!lvlOver && Engine2D.testCollision(who, tardis.tardisBbox))
            {
                who.enterTardis();
                tardis.dematerialise(gameTime);                
                this.timer = 0f;
                this.lvlOver = true;
            }
        }

        private void checkLvlEnd()
        {
            if (this.lvl.LvlLength * GraphicsDevice.Viewport.Width + this.lvl.Bg_Position.X <= 250)
            {
                this.tardis.tardisPosition -= new Vector2(1.5f, 0);
            }
        }

        private void ennemiesCollision()
        {
            foreach (Foe baddy in baddies)
            {
                if (Engine2D.testCollision(who, baddy.Bbox))
                {
                    Exit();
                }
                if (baddy is Cyberman)
                {
                    if (Engine2D.testSonicAttack(who.SonicBbox, baddy.Bbox))
                    {
                        baddy.State = Status.DEAD;
                    }
                }
                else if (baddy is Dalek)
                {
                    if (Engine2D.testLaserAttack(who.Bbox,(Dalek) baddy))
                    {
                        who.State = Status.DEAD;
                    }
                }
            }
        }

        private void CybermenInvasion(GameTime gameTime)
        {
                if (rnd.Next(1, 200) == 1)
                {
                    baddies.Add(new Cyberman(this, new Vector2(0, 950), Orientation.DROITE));
                }
                else if (rnd.Next(1,200) == 2)
                {
                    baddies.Add(new Cyberman(this, new Vector2(1920, 950), Orientation.GAUCHE));
                }
        }

        private void WeepingAngelsAttack(GameTime gameTime)
        {
            if (rnd.Next(1, 600) == 1)
            {
                baddies.Add(new WeepingAngel(this, new Vector2(0, 950), Orientation.DROITE, who));
            }
        }

        private void DalekApocalypse(GameTime gameTime)
        {
            if (rnd.Next(1, 350) == 1)
            {
                baddies.Add(new Dalek(this, new Vector2(0, 950), Orientation.DROITE, who));
            }
        }
    }
}
