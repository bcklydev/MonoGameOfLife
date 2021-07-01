using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class Cell
    {
        Random rnd = new Random();
        private int State;
        public Cell()
        {
            int r = rnd.Next(0, 10);
            if(r > 8)
            {
                State = 1;
            }
            else
            {
                State = 0;
            }
        }

        public int GetState()
        {
            return State;
        }

        public void MakeAlive()
        {
            State = 1;
        }

        public void MakeDead()
        {
            State = 0;
        }
    }
}