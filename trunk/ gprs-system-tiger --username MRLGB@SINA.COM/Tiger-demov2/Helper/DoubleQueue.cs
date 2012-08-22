using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace Tiger
{
    class DoubleQueue
    {
        public Queue<GPRS_DATA_RECORD> m_Q1;
        public Queue<GPRS_DATA_RECORD> m_Q2;
        public volatile Queue<GPRS_DATA_RECORD> m_CurrentWriteQueue;

        //public Thread m_ProducerThread;
        public Thread m_ConsumerThread;

        public ManualResetEvent m_HandlerFinishedEvent;
        public ManualResetEvent m_UnblockHandlerEvent;
        public AutoResetEvent m_DataAvailableEvent;
        public Random m_Random;

        //public StreamWriter m_ProducerData;
        //public StreamWriter m_ConsumerData;
        //public StreamWriter m_Log;

        // ------------------------------------------------------------------------
        /// <summary>Response data event. This event is called when have data item in double queue</summary>
        public delegate void ResponseData(ushort id, GPRS_DATA_RECORD data);
        /// <summary>Response data event. This event is called when have data item in double queue</summary>
        public event ResponseData OnResponseData;

        public DoubleQueue()
        {
            m_Q1 = new Queue<GPRS_DATA_RECORD>();
            m_Q2 = new Queue<GPRS_DATA_RECORD>();

            m_CurrentWriteQueue = m_Q1;

            //m_ProducerThread = new Thread(new ThreadStart(ProducerFunc));
            m_ConsumerThread = new Thread(new ThreadStart(ConsumerFunc));

            m_HandlerFinishedEvent = new ManualResetEvent(true);
            m_UnblockHandlerEvent = new ManualResetEvent(true);
            m_DataAvailableEvent = new AutoResetEvent(false);
            m_Random = new Random((int)DateTime.Now.Ticks);

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
            m_ConsumerThread.Start();
        }

        public void ConsumerFunc()
        {
            int count;
            GPRS_DATA_RECORD data;
            Queue<GPRS_DATA_RECORD> readQueue;

            while (true)
            {
                m_DataAvailableEvent.WaitOne();

                m_UnblockHandlerEvent.Reset(); // block the producer
                m_HandlerFinishedEvent.WaitOne(); // wait for the producer to finish
                readQueue = m_CurrentWriteQueue;
                m_CurrentWriteQueue = (m_CurrentWriteQueue == m_Q1) ? m_Q2 : m_Q1; // switch the write queue
                m_UnblockHandlerEvent.Set(); // unblock the producer

                count = 0;
                while (readQueue.Count > 0)
                {
                    count += 1;

                    data = readQueue.Dequeue();
                    if (global.attached)
                    {
                        OnResponseData(0, data);
                        //m_ConsumerData.WriteLine(data); // logging 
                        //MessageBox.Show(data.ToString());
                    }
                                
                    //Thread.Sleep(m_Random.Next(0, 2));
                }
                //MessageBox.Show("Removed items from queue: "+ "["+count+"]"+readQueue.GetHashCode());
            }
        }

        public void EnQueueItem(GPRS_DATA_RECORD datain)
        {
            m_UnblockHandlerEvent.WaitOne();
            m_HandlerFinishedEvent.Reset();

            m_CurrentWriteQueue.Enqueue(datain);
            //m_ProducerData.WriteLine(datain); // logging 

            m_DataAvailableEvent.Set();
            m_HandlerFinishedEvent.Set();
        }

    }
}
