using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public static class Playable
    {
        public static bool isValidPlay(int cardValue, int lastValue)
        {
            return cardValue > lastValue;
        }
    }
}
