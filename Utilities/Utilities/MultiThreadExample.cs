using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities
{
    //MultiThreadExample with race condition managed using monitors
    public class SharedDataProd
    {
        SharedData myData;         // Field to hold cell object to be used
        int quantity = 1;  // Field for how many items to produce in cell

        public SharedDataProd(SharedData data, int request)
        {
            myData = data;          // Pass in what cell object to be used
            quantity = request;  // Pass in how many items to produce in cell
        }
        public void ThreadRun()
        {
            for (int looper = 1; looper <= quantity; looper++)
                myData.WriteToSharedData(looper);
        }
    }

    public class SharedDataCons
    {
        SharedData myData;
        int quantity = 1;

        public SharedDataCons(SharedData data, int request)
        {
            myData = data;          // Pass in what cell object to be used
            quantity = request;  // Pass in how many items to consume from cell
        }

        public void ThreadRun()
        {
            int valReturned;

            for (int i = 1; i <= quantity; i++)
                // Consume the result by placing it in valReturned.
                valReturned = myData.ReadFromSharedData();
        }
    }

    public class SharedData
    {
        int contents; 
        bool readerFlag = false;

        public int ReadFromSharedData()
        {
            lock (this)
            {
                if (!readerFlag) // Wait until Cell.WriteToCell is done producing
                {            
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                Console.WriteLine("Consume: {0}", contents);
                readerFlag = false;    

                Monitor.Pulse(this);
            }
            return contents;
        }

        public void WriteToSharedData(int n)
        {
            lock (this)
            {
                if (readerFlag) // Wait until Cell.ReadFromCell is done consuming.
                {      
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (SynchronizationLockException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                contents = n;
                Console.WriteLine("Produce: {0}", contents);
                readerFlag = true;

                Monitor.Pulse(this);
            }
        }
    }
}
