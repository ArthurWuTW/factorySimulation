using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project;

namespace Project
{
    public partial class Form1 : Form
    {

        List<Node> allNode = new List<Node>();
        

        public Node GetNodebyName(char name)
        {
            //return reference
            return allNode.FirstOrDefault(c => c.name == name);
        }

        public Node GetNodebyTime(double time)
        {
            //return reference
            return allNode.FirstOrDefault(c => c.eventTime == time);
        }

        public double getRandom()
        {
            double mu = 5;
            Random rnd = new Random();
            return -mu * Math.Log(1 - rnd.NextDouble());
        }

        public Form1()
        {
            InitializeComponent();
            //Node ff;

            double stoptime = 30;

            allNode.Add(new Node(2, 1, 'W'));
            allNode.Add(new Node(1, 1, 'P'));
            allNode.Add(new Node(1, 1, 'A'));
            double gen_time=0.0;

            //ff = GetNodebyName('W');
            //ff.name = 'F';
            while (allNode.Min(r => r.eventTime) != int.MaxValue || gen_time != int.MaxValue)
            {
                if (gen_time < stoptime)
                {
                    gen_time = gen_time + getRandom();
                    Client client_gen = new Client(gen_time);
                    GetNodebyName('W').AddClient(client_gen);
                }
                else
                {
                    gen_time = int.MaxValue;
                }

                //Node Event
                if(GetNodebyTime(allNode.Min(r => r.eventTime)) != null && allNode.Min(r => r.eventTime) < gen_time)
                {
                    char next_Node_name = GetNodebyTime(allNode.Min(r => r.eventTime)).getTempNextNodeName();
                    if (next_Node_name != 'E')  // 'E' = End
                    {
                        if (GetNodebyName(next_Node_name).isBlock() == false)
                        {
                            //no blocks
                            // GetNodeByTime == event Time -> current Node
                            // next_Node_name -> Next Node 

                            //remove node and create new complete time
                            Client client_move = GetNodebyTime(allNode.Min(r => r.eventTime)).getTemp();
                            GetNodebyTime(allNode.Min(r => r.eventTime)).tempLeaveNode();

                            //add client to next node
                            GetNodebyName(next_Node_name).AddClient(client_move);

                        }
                    }
                   else
                   {
                        //leave the system
                        GetNodebyTime(allNode.Min(r => r.eventTime)).tempLeaveNode();
                    }
                }

            }

            int b = 0;
            
            // init Node and name
            // all Node.time = int.max
            // init gen_time = 0

            // while all Node.time and gen_time != int.max
            //      if gen_time < T
            //          gen_time = gen_time + rdm
            //          client = new client(time)
            //          Node['W'].add( client )
            //      if NODE EVENT:
            //          go_i = client.flow[0]
            //          check Node[go_i].block?
            //              Yes, t[last]=int.max
            //                   t[last]= min(E)+ float.epsilon
            //              No, Node[go_i].add(new Client(last complete time, flow))
            //                  Q--, t_i <- new complete time
            //      
            


        }
    }
}
