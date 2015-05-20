using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Ruins : Level
    {
        public Ruins(Game game) : base(game) 
        {
            name = "ruins";
            lvlLength = 1.5f;
            this.bg = new Background(game, name, 4);
            this.Game.Components.Add(this);
        }
    }
}
