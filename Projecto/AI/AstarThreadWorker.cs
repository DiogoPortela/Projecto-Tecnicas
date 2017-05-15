using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    /// <summary>
    /// This class is responsible for running the A* algorithm in a new thread.
    /// This class will be called by AstarManager where AstarManager will create a new thread in a ThreadPool which this class will run on.
    /// After this class finds the path the thread will be destroyed and the results will be stored in a ConcurrentQueue in the AstartManager.
    /// </summary>
    class AstarThreadWorker
    {
        public Astar astar;
        /// <summary>
        /// ID number for this worker thread so you can get the results back.
        /// </summary>
        public int WorkerIDNumber;

        /// <summary>
        /// This function will run the astar algorithem and tries to find the shortest path.
        /// </summary>
        /// <param name="StartingTile">The starting position in (Array coordinates) of the search path.</param>
        /// <param name="TargetTile">The target or destination position in (Array coordinates) where the search for the path will end at.</param>
        /// <param name="map">Map class.</param>
        /// <param name="DisableDiagonalPathfinding">If true, the A* algorithm will not search the path in diagonal direction.</param>
        /// <param name="WorkerIDNumber">ID number for this worker thread so you can get the results back.</param>
        public AstarThreadWorker(Tile StartingTile, Tile TargetTile, MapGenerator map, bool DisableDiagonalPathfinding, int WorkerIDNumber)
        {
            if (StartingTile.Position.X > map.ArraySize.X || StartingTile.Position.Y > map.ArraySize.Y)
                throw new Exception("StartingTile size cannot be bigger than map array size. Please make sure the StartingTile position is in array coordinates not pixel coordinates.");

            if (TargetTile.Position.X > map.ArraySize.X || TargetTile.Position.Y > map.ArraySize.Y)
                throw new Exception("TargetTile size cannot be bigger than map array size. Please make sure the TargetTile position is in array coordinates not pixel coordinates.");

            this.WorkerIDNumber = WorkerIDNumber;
            astar = new Astar(StartingTile, TargetTile, map, DisableDiagonalPathfinding);
            astar.FindPath();
        }
    }
}
