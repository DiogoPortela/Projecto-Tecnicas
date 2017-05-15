using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projecto.AI
{
    /// <summary>
    /// This class is responsible for creating a thread from a ThreadPool 
    /// so the A* algorithm can run on and produce It's results.
    /// </summary>
    class AstarManager
    {
        /// <summary>
        /// A ConcurrentQueue where every AstarThreadWorker will store its results in.
        /// </summary>
        public static ConcurrentQueue<AstarThreadWorker> AstarThreadWorkerResults = new ConcurrentQueue<AstarThreadWorker>();
        /// <summary>
        /// This function will add a new thread worker for A* algorithm to run on.
        /// </summary>
        /// <param name="StartingTile">The starting position in (Array coordinates) of the search path.</param>
        /// <param name="TargetTile">The target or destination position in (Array coordinates) where the search for the path will end at.</param>
        /// <param name="map">Map class.</param>
        /// <param name="DisableDiagonalPathfinding">If true, the A* algorithm will not search the path in diagonal direction.</param>
        /// <param name="WorkerIDNumber">ID number for this worker thread so you can get the results back.</param>
        public static void AddNewThreadWorker(Tile StartingTile, Tile TargetTile, MapGenerator Map, bool DisableDiagonalPathfinding, int WorkerIDNumber)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                AstarThreadWorker astarWorker = new AstarThreadWorker(StartingTile, TargetTile, Map, DisableDiagonalPathfinding, WorkerIDNumber);
                AstarThreadWorkerResults.Enqueue(astarWorker);
            }));
        }
    }
}
