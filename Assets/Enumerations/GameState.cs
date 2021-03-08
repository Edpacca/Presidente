using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public enum GameState
    {
        inMenu = 0,
        newGame = 1,
        boardLoaded = 2,
        newRound = 3,
        inPlay = 4,
        roundOver = 5,
        gameOver = 6
    }
}
