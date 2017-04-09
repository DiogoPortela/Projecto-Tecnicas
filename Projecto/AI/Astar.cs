using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Projecto.AI
{
    class Astar
    {
        MapGenerator Map;

        Node[,] Nodes; // array para os tiles
        Node CurrentNode; // Node currently selected
        List<Node> OpenList; // List for open nodes
        List<Node> ClosedList; // List for closed nodes
        List<Node> FinalPath; // The path to take

        Node StartingNode, TargetNode;

        public bool ReachedTarget;
        //horizontal and vertical G cost
        const int HV_G_Cost = 10;
        const int Diagonal_G_cost = 14;

        public bool DisableDiagonalPathfinding; // Para movimentos diagonais ou não

        // recebe uma posicao inicial e final, o mapa e um boleano para as activar movimento diagonais
        public Astar(Node startingNode, Node targetNode, MapGenerator map, bool disableDiagonalPathfinding)
        {
            this.StartingNode = startingNode;
            this.TargetNode = targetNode;
            this.Map = map;
            this.DisableDiagonalPathfinding = disableDiagonalPathfinding;
        }
        void CreateNodeList()
        {
            Nodes = new Node[(int)Map.ArraySize.X, (int)Map.ArraySize.Y];

            for (int y = 0; y < Map.ArraySize.Y; y++)
                for (int x = 0; x < Map.ArraySize.X; x++)
                    Nodes[x, y] = new Node(new Vector2(x, y));
        }
        /// <summary>
        /// Create an array of nodes for the collision detection in pathfinding.
        /// </summary>
        void GenerateCollisionNodes()
        {
            for (int i = 0; i < Map.AStarCollisionObjects.Count; i++)
                Nodes[(int)Map.AStarCollisionObjects[i].X, (int)Map.AStarCollisionObjects[i].Y].Passable = false;
        }
        /// <summary>
        /// Add Neighbors Nodes to the OpenList and the Current node to the Closed list
        /// </summary>
        void FindNeighbourNodes()
        {
            Node NeighborNode;
            bool Add = false;

            OpenList.Remove(CurrentNode); // Remove da openlist o current node
            if (!ClosedList.Contains(CurrentNode)) // Adiciona o current node à closed list se não estiver já la
                ClosedList.Add(CurrentNode);

            // Check if we are in the bounds
            #region Top Node
            if (CurrentNode.Position.Y - 1 >= 0)
            {
                NeighborNode = Nodes[(int)CurrentNode.Position.X, (int)CurrentNode.Position.Y - 1];
                if (!OpenList.Contains(NeighborNode))
                {
                    if (NeighborNode.Passable)
                    {
                        for (int i = 0; i < ClosedList.Count; i++)
                        {
                            if (NeighborNode.Position == ClosedList[i].Position)
                            {
                                Add = false;
                                break;
                            }
                            else
                                Add = true;
                        }
                    }
                    if (Add)
                    {
                        NeighborNode.Parent = CurrentNode;
                        NeighborNode.Diagonal = false;
                        OpenList.Add(NeighborNode);
                        Add = false;
                    }
                }
            }
            #endregion
            #region Bottom Node
            if (CurrentNode.Position.Y + 1 < Nodes.GetLength(1))
            {
                NeighborNode = Nodes[(int)CurrentNode.Position.X, (int)CurrentNode.Position.Y + 1];

                if (!OpenList.Contains(NeighborNode))
                {
                    if (NeighborNode.Passable)
                    {
                        for (int i = 0; i < ClosedList.Count; i++)
                        {
                            if (NeighborNode.Position == ClosedList[i].Position)
                            {
                                Add = false;
                                break;
                            }
                            else
                            {
                                Add = true;
                            }
                        }
                    }
                    if (Add)
                    {
                        NeighborNode.Parent = CurrentNode;
                        NeighborNode.Diagonal = false;
                        OpenList.Add(NeighborNode);
                        Add = false;
                    }
                }
            }
            #endregion
            #region RightNode
            if (CurrentNode.Position.X + 1 < Nodes.GetLength(0))
            {
                NeighborNode = Nodes[(int)CurrentNode.Position.X + 1, (int)CurrentNode.Position.Y];

                if (!OpenList.Contains(NeighborNode))
                {
                    if (NeighborNode.Passable)
                    {
                        for (int i = 0; i < ClosedList.Count; i++)
                        {
                            if (NeighborNode.Position == ClosedList[i].Position)
                            {
                                Add = false;
                                break;
                            }
                            else
                                Add = true;
                        }
                    }
                    if (Add)
                    {
                        NeighborNode.Parent = CurrentNode;
                        NeighborNode.Diagonal = false;
                        OpenList.Add(NeighborNode);
                        Add = false;
                    }
                }
            }
            #endregion
            #region Left Node
            if (CurrentNode.Position.X - 1 >= 0)
            {
                NeighborNode = Nodes[(int)CurrentNode.Position.X - 1, (int)CurrentNode.Position.Y];

                if (!OpenList.Contains(NeighborNode))
                {
                    if (NeighborNode.Passable)
                    {
                        foreach (Node closedNode in ClosedList)  // Melhor?
                        {
                            if (NeighborNode.Position == closedNode.Position)
                            {
                                Add = false;
                                break;
                            }
                            else
                                Add = true;
                        }
                    }
                    if (Add)
                    {
                        NeighborNode.Parent = CurrentNode;
                        NeighborNode.Diagonal = false;
                        OpenList.Add(NeighborNode);
                    }
                }
            }
            #endregion
            // Pathfinding Diagonal
            if (!DisableDiagonalPathfinding)
            {
                #region Top-Right Node
                if (CurrentNode.Position.X + 1 < Nodes.GetLength(0) && CurrentNode.Position.Y - 1 >= 0)
                {
                    NeighborNode = Nodes[(int)CurrentNode.Position.X + 1, (int)CurrentNode.Position.Y - 1];

                    if (!OpenList.Contains(NeighborNode))
                    {
                        if (NeighborNode.Passable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborNode.Position == ClosedList[i].Position)
                                {
                                    Add = false;
                                    break;
                                }
                                else
                                    Add = true;
                            }
                        }

                        if (Add)
                        {
                            NeighborNode.Parent = CurrentNode;
                            NeighborNode.Diagonal = true;
                            OpenList.Add(NeighborNode);
                            Add = false;
                        }
                    }
                }
                #endregion
                #region Top-Left Node
                if (CurrentNode.Position.X - 1 >= 0 && CurrentNode.Position.Y - 1 >= 0)
                {
                    NeighborNode = Nodes[(int)CurrentNode.Position.X - 1, (int)CurrentNode.Position.Y - 1];

                    if (!OpenList.Contains(NeighborNode))
                    {
                        if (NeighborNode.Passable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborNode.Position == ClosedList[i].Position)
                                {
                                    Add = false;
                                    break;
                                }
                                else
                                    Add = true;
                            }
                        }

                        if (Add)
                        {
                            NeighborNode.Parent = CurrentNode;
                            NeighborNode.Diagonal = true;
                            OpenList.Add(NeighborNode);
                            Add = false;
                        }
                    }
                }
                #endregion
                #region Bottom-Right Node
                if (CurrentNode.Position.X + 1 < Nodes.GetLength(0) && CurrentNode.Position.Y + 1 < Nodes.GetLength(1))
                {
                    NeighborNode = Nodes[(int)CurrentNode.Position.X + 1, (int)CurrentNode.Position.Y + 1];

                    if (!OpenList.Contains(NeighborNode))
                    {
                        if (NeighborNode.Passable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborNode.Position == ClosedList[i].Position)
                                {
                                    Add = false;
                                    break;
                                }
                                else
                                    Add = true;
                            }
                        }

                        if (Add)
                        {
                            NeighborNode.Parent = CurrentNode;
                            NeighborNode.Diagonal = true;
                            OpenList.Add(NeighborNode);
                            Add = false;
                        }
                    }
                }
                #endregion
                #region Bottom-Left Node
                if (CurrentNode.Position.X - 1 >= 0 && CurrentNode.Position.Y + 1 < Nodes.GetLength(1))
                {
                    NeighborNode = Nodes[(int)CurrentNode.Position.X - 1, (int)CurrentNode.Position.Y + 1];

                    if (!OpenList.Contains(NeighborNode))
                    {
                        if (NeighborNode.Passable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborNode.Position == ClosedList[i].Position)
                                {
                                    Add = false;
                                    break;
                                }
                                else
                                    Add = true;
                            }
                        }

                        if (Add)
                        {
                            NeighborNode.Parent = CurrentNode;
                            NeighborNode.Diagonal = true;
                            OpenList.Add(NeighborNode);
                            Add = false;
                        }
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// Distancia do CurrentNode até ao TargetNode
        /// </summary>
        void Calculate_Heuristic_Value()
        {
            // distância do startingNode ao targetNode
            Vector2 Distance = Vector2.Zero;

            for (int x = 0; x < Nodes.GetLength(0); x++)
            {
                for (int y = 0; y < Nodes.GetLength(1); y++)
                {
                    Vector2 CurrentNodePosition = new Vector2(x, y);

                    // Node at right
                    if (CurrentNodePosition.X <= TargetNode.Position.X)
                        Distance.X = TargetNode.Position.X - CurrentNodePosition.X;
                    // Node at left
                    if (CurrentNodePosition.X >= TargetNode.Position.X)
                        Distance.X = CurrentNodePosition.X - TargetNode.Position.X;
                    // Node at Up
                    if (CurrentNodePosition.Y <= TargetNode.Position.Y)
                        Distance.Y = TargetNode.Position.Y - CurrentNodePosition.Y;
                    // Node at Down
                    if (CurrentNodePosition.Y >= TargetNode.Position.Y - TargetNode.Position.Y)
                        Distance.Y = CurrentNodePosition.Y - TargetNode.Position.Y;

                    Nodes[x, y].H_Value = (int)(Distance.X + Distance.Y);
                }
            }
        }
        /// <summary>
        /// Custo de movimento por tile( Horizontal, Vertical e Diagonal)
        /// </summary>
        void Calculate_G_Value()
        {
            for (int i = 0; i < OpenList.Count; i++)
            {
                if (OpenList[i].Diagonal && !DisableDiagonalPathfinding)
                    OpenList[i].G_Value = OpenList[i].Parent.G_Value + Diagonal_G_cost;
                else
                    OpenList[i].G_Value = OpenList[i].Parent.G_Value + HV_G_Cost;
            }
        }
        /// <summary>
        /// F cost = soma da heuristic e movement cost (H_Value e G_Value)
        /// </summary>
        void Calculate_F_Value()
        {
            for (int i = 0; i < OpenList.Count; i++)
            {
                OpenList[i].F_Value = OpenList[i].G_Value + OpenList[i].H_Value;
            }
        }
        // Devolve o Node com o F value mais baixo
        Node GetMin_F_Value()
        {
            List<int> F_ValuesList = new List<int>();
            int Lowest_F_Value = 0;

            for (int i = 0; i < OpenList.Count; i++)
                F_ValuesList.Add(OpenList[i].F_Value);
            if(F_ValuesList.Count > 0)
            {
                Lowest_F_Value = F_ValuesList.Min();
                foreach(Node openNode in OpenList)
                {
                    if (openNode.F_Value == Lowest_F_Value)
                        return openNode;
                }
            }

            return CurrentNode;
        }
        // Organiza a lista de nodes com o caminho final (FinalPath)
        void CalculateFinalPath()
        {
            List<Node> FinalPathTemp = new List<Node>();

            FinalPathTemp.Add(CurrentNode);
            FinalPathTemp.Add(CurrentNode.Parent);

            for(int i=1; ; i++)
            {
                if (FinalPathTemp[i].Parent != null)
                    FinalPathTemp.Add(FinalPathTemp[i].Parent);
                else
                    break;
            }

            // reverse the list elements order from last to first
            for (int i = FinalPathTemp.Count - 1; i >= 0; i--)
                FinalPath.Add(FinalPathTemp[i]);
        }
        /// <summary>
        /// Returns a set of Vector2 coordinates of the the shortest patch from the starting point to the goal or ending point( if found) in map coordenates
        /// </summary>
        /// <returns></returns>
        public List<Vector2> GetFinalPath()
        {
            List<Vector2> FinalPatchVector2 = new List<Vector2>();
            // Com a lista de nodes(FinalPath) cria-se uma lista de vetores
            for (int i = 1; i < FinalPath.Count; i++)
                FinalPatchVector2.Add(FinalPath[i].Position);

            return FinalPatchVector2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingNode">The starting position of the search path in map coordinate</param>
        /// <param name="EndingNode">The Goal or the ending position of the search path in map coordinate</param>
        public void FindPath()
        {
            CreateNodeList();
            GenerateCollisionNodes();

            CurrentNode = this.StartingNode; // 

            OpenList = new List<Node>();
            ClosedList = new List<Node>();
            FinalPath = new List<Node>();

            Calculate_Heuristic_Value();

            while (true)
            {
                CurrentNode = GetMin_F_Value(); // 

                FindNeighbourNodes();
                Calculate_G_Value();
                Calculate_F_Value();

                foreach(Node openNode in OpenList)
                {
                    if(openNode.Position == TargetNode.Position)
                    {
                        CurrentNode = openNode;
                        CalculateFinalPath();
                        ReachedTarget = true;
                        break;
                    }
                }
                if (ReachedTarget)
                    break;
                if(OpenList.Count == 0)
                {
                    ReachedTarget = false;
                    break;
                }
            }
        }
    }
}

