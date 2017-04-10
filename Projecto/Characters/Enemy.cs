using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Projecto.AI;

namespace Projecto
{
    public class Enemy: GameObject
    {
        AstarThreadWorker astarThreadWorkerTemp, astarThreadWorker;
        List<Vector2> WayPointList;
        Waypoint waypoint;
        public float Speed { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public float MS { get; set; }
        public double PhysDmg { get; set; }
        public double MagicDmg { get; set; }

        public Enemy(Vector2 position,Vector2 size, int numID):base("chickenprofilewalkx2",position,size,0f)
        {
            #region Stats Initializer
            this.HP = 30;
            this.MP = 30;
            this.MS = 0.8f;
            this.PhysDmg = 10;
            this.MagicDmg = 5;
            #endregion
            Size = new Vector2(10, 10);
            Speed = 0.1f;
            WayPointList = new List<Vector2>();
            waypoint = new Waypoint();
        }
        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }
        public void LoadContent()
        {

        }
        void Astar(GameTime gameTime, int numID, List<Enemy> units, MapGenerator map)
        {
            if ()
            { // Inicializa a Astar numa thread
                AstarManager.AddNewThreadWorker();
            }
            // Guarda os resultados
            AstarManager.AstarThreadWorkerResults.TryPeek(out astarThreadWorkerTemp);

            if (astarThreadWorkerTemp != null)
            {
                if (astarThreadWorkerTemp.WorkerIDNumber == numID)
                {
                    AstarManager.AstarThreadWorkerResults.TryDequeue(out astarThreadWorker);
                }
                if (astarThreadWorker != null)
                {
                    waypoint = new Waypoint();

                    WayPointList = astarThreadWorker.astar.GetFinalPath();

                    for (int i = 0; i < WayPointList.Count; i++)
                    {
                        WayPointList[i] = new Vector2(WayPointList[i].X * 10, WayPointList[i].Y * 10);
                    }
                }

            }


        }
    }
}
