using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Node
    {
        List<Server> Server_i = new List<Server>();
        List<Queue<Client>> Queue_i = new List<Queue<Client>>();
        public char name;
        public double eventTime;
        int queue_length = 10;
        bool block = false;
        

        private Server GetIdleServer()
        {
            //return reference
            return Server_i.FirstOrDefault(c => c.idle == true);
        }

        private Queue<Client> GetleastQueue()
        {
            //return reference
            if (Queue_i.Min(a => a.Count) < queue_length)
            {
                //return Queue_i.FirstOrDefault(c => c.Count < queue_length);
                return Queue_i.FirstOrDefault(c => c.Count == Queue_i.Min(a => a.Count));
            }
            else
                return null;
        }

        public char getTempNextNodeName()
        {
            return Server_i.FirstOrDefault(c => c.completeTime == eventTime).temp.showNextFlowName();
        }


        public Node(int ser_num, int que_num, char name)
        {
            this.name = name;
            for (int i = 0; i < ser_num; i++)
                Server_i.Add(new Server());
            for (int i = 0; i < que_num; i++)
                Queue_i.Add(new Queue<Client>());

            //event time is the min of Server.complete time
            eventTime = Server_i.Min(r => r.completeTime);
        }

        public void AddClient(Client newclient)
        {
            if (GetIdleServer()!=null)
            {
                Server IdleServer = GetIdleServer();
                IdleServer.GiveCompleteTime(newclient.getTime());
                IdleServer.idle = false;
                IdleServer.temp = newclient;
                IdleServer.temp.upDateTimeByNode(IdleServer.completeTime);
                IdleServer.temp.removeFlowFirst();
            }
            else
            {
                if (GetleastQueue() != null)
                {
                    GetleastQueue().Enqueue(newclient);
                }
                else
                {
                    block = true;
                }
             }

            eventTime = Server_i.Min(c => c.completeTime);
        }
        
        public bool isBlock()
        {
            return block;
        }

        public void tempLeaveNode()
        {
            // if queue > 0
            if (GetleastQueue() != null && GetleastQueue().Count > 0)
            {
                // q--, 
                //new complete time

                //first become idle
                Server_i.FirstOrDefault(c => c.completeTime == eventTime).idle = true;

                // then quickly become busy
                Server IdleServer = GetIdleServer();

                //dequeue
                Server_i.FirstOrDefault(c => c.completeTime == eventTime).temp = GetleastQueue().Dequeue();
                Server_i.FirstOrDefault(c => c.completeTime == eventTime).temp.upDateTimeByNode(IdleServer.completeTime);
                Server_i.FirstOrDefault(c => c.completeTime == eventTime).temp.removeFlowFirst();

                // then quickly become busy
                IdleServer.GiveCompleteTime(IdleServer.completeTime);
                IdleServer.idle = false;

                eventTime = Server_i.Min(c => c.completeTime);

            }

            else
            {
                //queue =0 become idle
                Server_i.FirstOrDefault(c => c.completeTime == eventTime).idle = true;
                Server_i.FirstOrDefault(c => c.completeTime == eventTime).temp = null;
                Server_i.FirstOrDefault(c => c.completeTime == eventTime).completeTime = int.MaxValue;
                
                //temp = null;

                eventTime = Server_i.Min(c => c.completeTime);
            }
        }

        public Client getTemp()
        {
            return Server_i.FirstOrDefault(c => c.completeTime == eventTime).temp;
        }

    }
}
