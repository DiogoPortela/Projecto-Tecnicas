using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto.Characters
{
    class EnemyManager
    {
        public List<Enemy> Units;

        public EnemyManager()
        {
            Units = new List<Enemy>();
        }
        public void Initialize(GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < Units.Count; i++)
                Units[i].Initialize(graphics);
        }
        public void LoadContent()
        {
            for (int i = 0; i < Units.Count; i++)
                Units[i].LoadContent();
        }
        public void Update(GameTime gameTime, MapGenerator map)
        {
            for (int i = 0; i < Units.Count; i++)
                Units[i].Update(gameTime, map);
        }
    }
}
