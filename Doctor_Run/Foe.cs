using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Foe : DrawableGameComponent
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected int orientation;
        protected int mouvement;
        protected BoundingBox bbox;
        protected SpriteBatch spriteBatch;
        protected Texture2D spriteSheet;
        protected Point frameSize = new Point(60, 60);//this is the size of your frame.  This is an example.
        //It should be the Width, Height of each of your frames.  
        //It's important that each frame is the same size.

        protected Point SheetSize = new Point(3,1);//this is how many frames of animation
        //you have.  The first number is the number of frames in a row.  The second is the
        //number of rows you have.  E.g, for 8 frames that are in one row, it would be (8,1).

        protected Point CurrentFrame = new Point(0, 0); //You'll use this later to keep track
        //of what frame you're on.

        protected int AnimationDelay = 0;

        protected int state;

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

        public Foe(Game game, Vector2 pos) : base(game)
        {
            Position = pos;
        }

        public override void Initialize()
        {
            this.velocity = Vector2.Zero;
            this.orientation = Orientation.DROITE;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.position += this.velocity;
            this.bbox = new BoundingBox(new Vector3(this.Position.X, this.Position.Y, 0),
                                        new Vector3(this.Position.X + this.FrameSize.X, this.Position.Y + this.FrameSize.Y, 0));
            base.Update(gameTime);
        }

        public virtual void animate()
        {
            
        }
    }
}
