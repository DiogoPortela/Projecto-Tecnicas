using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Projecto.AI
{
    class Node
    {
        #region Node class variables
        /// <summary>
        /// Node Parent.
        /// </summary>
        internal Node Parent;

        /// <summary>
        /// Node position.
        /// </summary>
        internal Vector2 Position;

        /// <summary>
        /// Is this node position is diagonal?
        /// </summary>
        internal bool Diagonal;

        /// <summary>
        /// Can you move through this node?
        /// </summary>
        internal bool Passable = true;

        /// <summary>
        /// Node H cost.
        /// </summary>
        internal int H_Value;

        /// <summary>
        /// Node G cost.
        /// </summary>
        internal int G_Value;

        /// <summary>
        /// Node F cost.
        /// </summary>
        internal int F_Value;
        #endregion

        public Node(Vector2 position)
        {
            this.Position = position;
        }
    }

}
