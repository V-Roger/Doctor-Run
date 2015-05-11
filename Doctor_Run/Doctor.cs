﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Doctor_Run
{
    public class Doctor : DrawableGameComponent
    {
        private Vector2 position;
        private int orientation;
        SpriteBatch spriteBatch;
        private Texture2D still;
        private Texture2D run_l;
        private Texture2D run_m;
        private Texture2D run_r;
        Texture2D spriteSheet;
        Point FrameSize = new Point(30, 30);//this is the size of your frame.  This is an example.
        //It should be the Width, Height of each of your frames.  
        //It's important that each frame is the same size.

        Point SheetSize = new Point(2, 2);//this is how many frames of animation
        //you have.  The first number is the number of frames in a row.  The second is the
        //number of rows you have.  E.g, for 8 frames that are in one row, it would be (8,1).

        Point CurrentFrame = new Point(0, 0); //You'll use this later to keep track
        //of what frame you're on.

        int AnimationDelay = 0;//You'll use this later to slow down the animation
        //if it plays too quickly.

        KeyboardState currentKBState;
        KeyboardState oldKBState;

        private TimeSpan lastTimeSlideOrJump;
        private static readonly TimeSpan JumpAnimation = TimeSpan.FromMilliseconds(500);
        private static readonly TimeSpan SlideAnimation = TimeSpan.FromMilliseconds(2500);
    

        public Doctor(Game game)
            : base(game)
        {
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.position.X = 100;
            this.position.Y = 1000;
            this.orientation = 1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            still = Game.Content.Load<Texture2D>(@"img\doctor_still");
            run_l = Game.Content.Load<Texture2D>(@"img\doctor_run_left");
            run_m = Game.Content.Load<Texture2D>(@"img\doctor_run_mid");
            run_r = Game.Content.Load<Texture2D>(@"img\doctor_run_right");
            spriteSheet = Game.Content.Load<Texture2D>(@"img\doctor_spritesheet");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            float scale = 2.0f; //200% size
            spriteBatch.Draw(this.spriteSheet, this.position, new Rectangle(CurrentFrame.X * FrameSize.X, CurrentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            run(gameTime);
            this.position.X -= 1.5f;
            base.Update(gameTime);
        }

        public void run(GameTime gameTime)
        {
            currentKBState = Keyboard.GetState();

            if (currentKBState.IsKeyDown(Keys.Right) == true)
            {
                this.orientation = 1;
                this.position.X += 5f;
            } else if (currentKBState.IsKeyDown(Keys.Left) == true) {
                this.orientation = -1;
                this.position.X -= 3.5f;
            }
            else if (currentKBState.IsKeyDown(Keys.Down) == true && lastTimeSlideOrJump + SlideAnimation < gameTime.TotalGameTime)
            {             
                this.orientation = 2;
                this.position.X += 6.5f;
                lastTimeSlideOrJump = gameTime.TotalGameTime;
            }
            else if (currentKBState.IsKeyDown(Keys.Up) == true)
            {

                if (!oldKBState.IsKeyDown(Keys.Up))
                {
                    lastTimeSlideOrJump = gameTime.TotalGameTime;
                }

                jumpDoctor(gameTime);

            }
            else if (oldKBState.IsKeyDown(Keys.Up))
            {
                this.position.Y = 1000;
            }
            else
            {
                this.orientation = 0;
            }
            runDoctor();
            oldKBState = currentKBState;
        }

        public void jumpDoctor(GameTime gameTime)
        {
            if (lastTimeSlideOrJump + JumpAnimation > gameTime.TotalGameTime)
            {
                this.orientation = 4;
                this.position.Y = 950;
            }
            else
            {
                this.orientation = 0;
                this.position.Y = 1000;
            }
        }

        public void runDoctor()
        {
            if (AnimationDelay == 4)// delay frame update if it's too fast
            {
                if (this.orientation == -1)
                {
                    CurrentFrame.Y = 1;
                    if (CurrentFrame.X < SheetSize.X)
                    {
                        ++CurrentFrame.X;// Move to a new frame
                    }
                    else
                    {
                        CurrentFrame.X = 1;//set the X to 1, so we start fresh
                    }
                }
                else if(this.orientation == 1)
                {
                    CurrentFrame.Y = 2;
                    if (CurrentFrame.X < SheetSize.X)
                    {
                        ++CurrentFrame.X;// Move to a new frame
                    }
                    else
                    {
                        CurrentFrame.X = 1;//set the X to 1, so we start fresh
                    }
                }
                else if (this.orientation == 2)
                {
                    CurrentFrame.Y = 0;
                    CurrentFrame.X = 1;
                }
                else if (this.orientation == 4)
                {
                    CurrentFrame.Y = 3;
                    CurrentFrame.X = 0;
                }
                else
                {
                    CurrentFrame.Y = 0;
                    CurrentFrame.X = 0;
                }

                AnimationDelay = 0;//Set this to 0, so we delay it again
            }
            else
            {
                AnimationDelay += 1;// add one, so we can continue when we are ready
            }
        }

    }
}