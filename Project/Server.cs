using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Server
    {
        Random rdn = new Random();
        public bool idle;
        public double completeTime = int.MaxValue;

        public Client temp;

        private double getRandom()
        {
            double mu = 10;
            Random rnd = new Random();
            return mu * rnd.NextDouble();
        }

        public Server()
        {
            idle = true;
        }

        public void GiveCompleteTime(double time)
        {
            completeTime = time + getRandom();
        } 
    }
}
