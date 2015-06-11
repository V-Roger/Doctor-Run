using Microsoft.Xna.Framework;
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
        private Vector2 sonicPosition;
        private Vector2 velocity;
        private int orientation;
        private int mouvement;
        private BoundingBox bbox;
        private BoundingBox sonicBbox;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;
        Texture2D sonicSprite;
        Point frameSize = new Point(40, 40);//this is the size of your frame.  This is an example.
        //It should be the Width, Height of each of your frames.  
        //It's important that each frame is the same size.

        Point SheetSize = new Point(2, 5);//this is how many frames of animation
        //you have.  The first number is the number of frames in a row.  The second is the
        //number of rows you have.  E.g, for 8 frames that are in one row, it would be (8,1).

        Point CurrentFrame = new Point(0, 0); //You'll use this later to keep track
        //of what frame you're on.

        int AnimationDelay = 0;//You'll use this later to slow down the animation
        //if it plays too quickly.

        KeyboardState currentKBState;
        KeyboardState oldKBState;

        private TimeSpan lastTimeSlideOrJump;
        private static readonly TimeSpan JumpAnimation = TimeSpan.FromMilliseconds(300);
        private static readonly TimeSpan SlideAnimation = TimeSpan.FromMilliseconds(1000);

        private int state;

        private bool TardisEntered;
        private float alpha;
    
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        public BoundingBox Bbox
        {
            get
            {
                return bbox;
            }
        }

        public BoundingBox SonicBbox
        {
            get
            {
                return sonicBbox;
            }
        }

        public Point FrameSize
        {
            get
            {
                return frameSize;
            }
            set
            {
                frameSize = value;
            }
        }

        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public int DoctorOrientation
        {
            get
            {
                return orientation;
            }
        }

        public Doctor(Game game)
            : base(game)
        {
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.position.X = 450;
            this.position.Y = 1000;
            this.velocity = Vector2.Zero;
            this.orientation = Orientation.DROITE;
            this.mouvement = Mouvement.IMMOBILE;
            this.state = Status.ALIVE;
            this.alpha = 1f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Game.Content.Load<Texture2D>(@"img\spritesheet");
            sonicSprite = Game.Content.Load<Texture2D>(@"img\sonic_sprite");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            float scale = 2.0f; //200% size
            spriteBatch.Draw(this.spriteSheet, this.position, new Rectangle(CurrentFrame.X * frameSize.X, CurrentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White * this.alpha, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            if(this.mouvement == Mouvement.SONICDROITE) {
                this.sonicPosition = this.position + new Vector2(this.frameSize.X+38, 24);
                spriteBatch.Draw(this.sonicSprite, sonicPosition, new Rectangle(0, 0, 27, 14), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            else if (this.mouvement == Mouvement.SONICGAUCHE)
            {
                this.sonicPosition = this.position - new Vector2(30, -15);
                spriteBatch.Draw(this.sonicSprite, sonicPosition, new Rectangle(0, 14, 27, 14), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if(this.state != Status.BLOCKED) move(gameTime);
            collide();
            this.position.X -= 1.5f;
            this.position += this.velocity;
            base.Update(gameTime);
            if (this.TardisEntered)
            {
                if (this.alpha > 0)
                {
                    this.alpha -= 0.1f;
                }
            }
        }

        public void move(GameTime gameTime)
        {
            currentKBState = Keyboard.GetState();

            if (currentKBState.IsKeyDown(Keys.Right) == true)
            {
                this.orientation = Orientation.DROITE;
                if (this.mouvement == Mouvement.IMMOBILE)
                {
                    this.mouvement = Mouvement.COURS;
                }
                
                this.velocity.X = 5f;
            }
            else if (oldKBState.IsKeyDown(Keys.Right))
            {
                this.mouvement = Mouvement.IMMOBILE;
                //this.velocity.X = 0;
            }
            else if (currentKBState.IsKeyDown(Keys.Left) == true) {
                this.orientation = Orientation.GAUCHE;
                this.mouvement = (this.mouvement == Mouvement.IMMOBILE) ? Mouvement.COURS : this.mouvement;
                this.velocity.X = -5f;
            }
            else if (oldKBState.IsKeyDown(Keys.Left))
            {
                this.mouvement = Mouvement.IMMOBILE;
                //this.velocity.X = 0;
            }
            if (currentKBState.IsKeyDown(Keys.Down) == true)
            {

                if (!oldKBState.IsKeyDown(Keys.Down))
                {
                    lastTimeSlideOrJump = gameTime.TotalGameTime;
                }

                
                if (lastTimeSlideOrJump + JumpAnimation > gameTime.TotalGameTime)
                {
                    
                    this.mouvement = Mouvement.GLISSADE;
                    if (this.orientation == Orientation.DROITE) 
                    {
                        this.velocity.X = 6.5f;
                    }
                    else
                    {
                        this.velocity.X = -6.5f;
                    }
                }
                else
                {
                    this.mouvement = Mouvement.IMMOBILE;
                    //this.velocity.X = 0;
                }

            }
            else if (oldKBState.IsKeyDown(Keys.Down))
            {
                this.mouvement = Mouvement.IMMOBILE;
                //this.velocity.X = 0;
            }
            if (currentKBState.IsKeyDown(Keys.Up) == true)
            {

                if (!oldKBState.IsKeyDown(Keys.Up))
                {
                    lastTimeSlideOrJump = gameTime.TotalGameTime;
                }

                jumpDoctor(gameTime);
            }
            else if (oldKBState.IsKeyDown(Keys.Up))
            {
                //this.orientation[4] = false;
                this.mouvement = Mouvement.IMMOBILE;
            }
            else
            {
                //this.orientation[0] = true;
                if (this.velocity.Y != 0)
                {
                    this.velocity.Y = this.position.Y < 1000 ? 8f : 0;
                }
            }
            if (!currentKBState.IsKeyDown(Keys.Left) && !currentKBState.IsKeyDown(Keys.Right))
            {
                Boolean stop = false;
                if (this.velocity.X > 0)
                {
                    this.velocity.X -= 0.1f;
                    if ((this.velocity.X < 0))
                    {
                        stop = true;
                    }
                } else
                {
                    this.velocity.X += 0.1f;
                    if ((this.velocity.X > 0))
                    {
                        stop = true;
                    }
                }
                if (stop)
                {
                    this.velocity.X = 0;
                    this.mouvement = Mouvement.IMMOBILE;

                }
            }
            if (currentKBState.IsKeyDown(Keys.Space))
            {
                this.velocity.X = 0;
                if (this.orientation == Orientation.DROITE)
                {
                    this.mouvement = Mouvement.SONICDROITE;
                }
                else
                {
                    this.mouvement = Mouvement.SONICGAUCHE;
                }
                sonic();
            }
            {

            }
            runDoctor();
            this.bbox = new BoundingBox(new Vector3(this.Position.X, this.Position.Y, 0),
                                        new Vector3(this.Position.X + this.FrameSize.X, this.Position.Y + this.FrameSize.Y, 0));
            this.sonicBbox = new BoundingBox(new Vector3(this.sonicPosition.X, this.sonicPosition.Y, 0),
                                             new Vector3(this.sonicPosition.X + 14, this.sonicPosition.Y + 19, 0));
            oldKBState = currentKBState;
        }

        public void jumpDoctor(GameTime gameTime)
        {
            if (lastTimeSlideOrJump + JumpAnimation > gameTime.TotalGameTime)
            {
                this.mouvement = Mouvement.SAUT;
                this.velocity.Y = -8f;
            }
            else
            {
                this.mouvement = Mouvement.IMMOBILE;
                this.velocity.Y = this.position.Y < 1000 ? 5f : 0;
            }
        }

        public void runDoctor()
        {
            if (AnimationDelay == 4)// delay frame update if it's too fast
            {
                switch (this.mouvement)
                {
                    case Mouvement.COURS:
                        if (this.orientation == Orientation.GAUCHE)
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
                        else if (this.orientation == Orientation.DROITE)
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
                        break;
                    case Mouvement.GLISSADE:
                        if (this.orientation == Orientation.DROITE)
                        {
                            CurrentFrame.Y = 0;
                            CurrentFrame.X = 1;
                        }
                        else if (this.orientation == Orientation.GAUCHE)
                        {
                            CurrentFrame.Y = 4;
                            CurrentFrame.X = 1;
                        }
                        break;
                    case Mouvement.SAUT:
                        CurrentFrame.Y = 3;
                        CurrentFrame.X = 0;
                        break;
                    case Mouvement.SONICDROITE:
                        CurrentFrame.Y = 5;
                        CurrentFrame.X = 0;
                        break;
                    case Mouvement.SONICGAUCHE:
                        CurrentFrame.Y = 5;
                        CurrentFrame.X = 1;
                        break;
                    default:
                        if (this.orientation == Orientation.DROITE)
                        {
                            CurrentFrame.Y = 0;
                        }
                        else
                        {
                            CurrentFrame.Y = 4;
                        }
                        CurrentFrame.X = 0;
                        break;
                }
                AnimationDelay = 0;//Set this to 0, so we delay it again
            }
            else
            {
                AnimationDelay += 1;// add one, so we can continue when we are ready
            }
        }

        public void enterTardis()
        {
            this.TardisEntered = true;
        }

        private void collide()
        {
            Vector2 v;
            // Test de collision
            float[] docInfo = { Position.X, Position.Y, frameSize.X, frameSize.Y };

            if (this.Position.X <= Game.GraphicsDevice.Viewport.Bounds.Left)
            {
                this.state = Status.DEAD;
            }
            else if (this.Position.X + this.frameSize.X >= Game.GraphicsDevice.Viewport.Bounds.Right)
            {
                this.state = Status.BLOCKED;
                this.velocity.X = 0;
            }
            else
            {
                this.state = Status.FREE;
            }

        }

        private void sonic()
        {
            
        }
    }
}
