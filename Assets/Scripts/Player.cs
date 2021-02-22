using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Player
    {
        public List<int> Hand;

        public void GiveCard(int index)
        {
            Hand.Add(index);
        }
    }
}
