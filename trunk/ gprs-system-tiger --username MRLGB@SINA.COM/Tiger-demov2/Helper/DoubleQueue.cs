using System;
using System.Collections.Generic;
using System.Threading;
using Tiger.Gprs;

namespace Tiger.Helper
{
    class DoubleQueue
    {
        public Queue<GprsDataRecord> MQ1;
        public Queue<GprsDataRecord> MQ2;
        public volatile Queue<GprsDataRecord> MCurrentWriteQueue;

        //public Thread m_ProducerThread;
        public Thread MConsumerThread;

        public ManualResetEvent MHandlerFinishedEvent;
        public ManualResetEvent MUnblockHandlerEvent;
        public AutoResetEvent MDataAvailableEvent;
        public Random MRandom;

        //public StreamWriter m_ProducerData;
        //public StreamWriter m_ConsumerData;
        //public StreamWriter m_Log;

        // ------------------------------------------------------------------------
        /// <summary>Response data event. This event is called when have data item in double queue</summary>
        public delegate void ResponseData(ushort id, GprsDataRecord data);
        /// <summary>Response data event. This event is called when have data item in double queue</summary>
        public event ResponseData OnResponseData;

        public DoubleQueue()
        {
            MQ1 = new Queue<GprsDataRecord>();
            MQ2 = new Queue<GprsDataRecord>();

            MCurrentWriteQueue = MQ1;

            //m_ProducerThread = new Thread(new ThreadStart(ProducerFunc));
            MConsumerThread = new Thread(ConsumerFunc);

            MHandlerFinishedEvent = new ManualResetEvent(true);
            MUnblockHandlerEvent = new ManualResetEvent(true);
            MDataAvailableEvent = new AutoResetEvent(false);
            MRandom = new Random((int)DateTime.Now.Ticks);

            //m_ProducerData = new StreamWriter("Producer.txt");
            //m_ProducerData.AutoFlush = true;

            //m_ConsumerData = new StreamWriter("Consumer.txt");
            //m_ConsumerData.AutoFlush = true;

            //m_Log = new StreamWriter("Log.txt");
           // m_Log.AutoFlush = true;
        }

        public void Run()
        {
            //m_ProducerThread.Start();
            MConsumerThread.Start();
        }

        public void ConsumerFunc()
        {
            while (true)
            {
                MDataAvailableEvent.WaitOne();

                MUnblockHandlerEvent.Reset(); // block the producer
                MHandlerFinishedEvent.WaitOne(); // wait for the producer to finish
                Queue<GprsDataRecord> readQueue = MCurrentWriteQueue;
                MCurrentWriteQueue = (MCurrentWriteQueue == MQ1) ? MQ2 : MQ1; // switch the write queue
                MUnblockHandlerEvent.Set(); // unblock the producer

                while (readQueue.Count > 0)
                {
                    GprsDataRecord data = readQueue.Dequeue();
                    if (Global.Attached)
                    {
                        OnResponseData(0, data);
                        //m_ConsumerData.WriteLine(data); // logging 
                        //MessageBox.Show(data.ToString());
                    }
                                
                    //Thread.Sleep(m_Random.Next(0, 2));
                }
                //MessageBox.Show("Removed items from queue: "+ "["+count+"]"+readQueue.GetHashCode());
            }
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns

        public void EnQueueItem(GprsDataRecord datain)
        {
            MUnblockHandlerEvent.WaitOne();
            MHandlerFinishedEvent.Reset();

            MCurrentWriteQueue.Enqueue(datain);
            //m_ProducerData.WriteLine(datain); // logging 

            MDataAvailableEvent.Set();
            MHandlerFinishedEvent.Set();
        }

    }
}
