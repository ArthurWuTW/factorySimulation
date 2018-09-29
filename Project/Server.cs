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

        public Server()
        {
            idle = true;
        }

        public void GiveCompleteTime(double time)
        {
            completeTime = time;
        } 
    }
}
