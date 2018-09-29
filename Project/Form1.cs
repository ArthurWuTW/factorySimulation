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

        List<Node> System = new List<Node>();
        

        public Node GetNodebyName(char name)
        {
            //return reference object Node
            return System.FirstOrDefault(c => c.name == name);
        }

        public Node GetNodebyTime(double time)
        {
            //return reference object Node
            return System.FirstOrDefault(c => c.eventTime == time);
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

            double stoptime = 30; //release module: stop relaease time 
            System.Add(new Node(2, 1, 'W')); // washing machine W
            System.Add(new Node(1, 1, 'P')); // press machines P
            System.Add(new Node(1, 1, 'A')); // assembly machines Y
            double gen_time=0.0; // release time

            while (System.Min(r => r.eventTime) != int.MaxValue || gen_time != int.MaxValue)
            {
                if (gen_time < stoptime)
                {
                    gen_time = gen_time + getRandom();
                    Client client_gen = new Client(gen_time);
                    GetNodebyName('W').AddClient(client_gen); //W Node Add client_gen
                }
                else
                {
                    gen_time = int.MaxValue;
                }

                //Node Event

                // if the Node whose time equals to the eventTime && the time earlier than release time
                if (GetNodebyTime(System.Min(r => r.eventTime)) != null && System.Min(r => r.eventTime) < gen_time)
                {
                    // earn the materials next path
                    char next_Node_name = GetNodebyTime(System.Min(r => r.eventTime)).getTempNextNodeName();
                    // if not an End
                    if (next_Node_name != 'E')  // 'E' = End
                    {
                        
                        if (GetNodebyName(next_Node_name).isBlock() == false)
                        {
                            //no blocks
                            // GetNodeByTime == event Time -> current Node
                            // next_Node_name -> Next Node 

                            //client(temp) leave current Node and new a new client to next node
                            //and create new complete time in current Node
                            Client client_move = GetNodebyTime(System.Min(r => r.eventTime)).getTemp();
                            GetNodebyTime(System.Min(r => r.eventTime)).tempLeaveNode();

                            //add client to next node
                            GetNodebyName(next_Node_name).AddClient(client_move);

                        }
                    }
                   else
                   {
                        //leave the system
                        GetNodebyTime(System.Min(r => r.eventTime)).tempLeaveNode();
                    }
                }

            }

            
            
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
