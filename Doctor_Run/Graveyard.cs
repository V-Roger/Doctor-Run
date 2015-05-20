using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Graveyard : Level
    {
        public Graveyard(Game game) : base(game) 
        {
            name = "graveyard";
            lvlLength = 1f;
            this.bg = new Background(game, name, 3);
            this.Game.Components.Add(this);
        }
    }
}
