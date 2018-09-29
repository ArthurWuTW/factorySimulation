using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{

    public class Client : Type
    {
        Queue<char> flow = new Queue<char>();
        double time = 0.0;

        public Client(double time)
        {
            this.time = time;

            Random rnd = new Random();
            if (rnd.NextDouble() > (double)1 / 3)
            {
                type = 'A';
                flow.Enqueue('W');
                flow.Enqueue('P');
                flow.Enqueue('A');
            }
            else
            {
                type = 'B';
                flow.Enqueue('W');
                flow.Enqueue('P');
                flow.Enqueue('W');
                flow.Enqueue('A');
            }
        }

        public double getTime()
        {
            return time;
        }

        public void removeFlowFirst()
        {
            flow.Dequeue();
        }

        public char showNextFlowName()
        {
            if (flow.Count != 0)
                return flow.ElementAt(0);
            else
                return 'E';
        }

        public void upDateTimeByNode(double time)
        {
            this.time = time;
        }
     }
}
    

