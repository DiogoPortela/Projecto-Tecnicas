using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using static Projecto.MapGenerator;

namespace Projecto.AI
{
    class Astar
    {
        MapGenerator Map;
        Tile[,] TileMap; // array para os tiles
        Tile CurrentTile; // Tile currently selected
        List<Tile> OpenList; // List for open Tiles
        List<Tile> ClosedList; // List for closed Tiles
        List<Tile> FinalPath; // The path to take

        Tile StartingTile, TargetTile;

        public bool ReachedTarget;
        //horizontal and vertical G cost
        const int HV_G_Cost = 10;
        const int Diagonal_G_cost = 14;

        public bool DisableDiagonalPathfinding; // Para movimentos diagonais ou não

        // recebe uma posicao inicial e final, o mapa e um boleano para as activar movimento diagonais
        public Astar(Tile startingTile, Tile targetTile, MapGenerator map, bool disableDiagonalPathfinding)
        {
            this.StartingTile = startingTile;
            this.TargetTile = targetTile;
            this.Map = map;
            this.DisableDiagonalPathfinding = disableDiagonalPathfinding;
        }
        void CreateTileList()
        {
            TileMap = new Tile[(int)Map.ArraySize.X, (int)Map.ArraySize.Y];

            for (int y = 0; y < Map.ArraySize.Y; y++)
                for (int x = 0; x < Map.ArraySize.X; x++)
                    TileMap[x, y] = new Tile(0, new Vector2(x, y), 5);
        }
        /// <summary>
        /// Create an array of Tiles for the collision detection in pathfinding.
        /// </summary>
        void GenerateCollisionTiles()
        {
            for (int i = 0; i < Map.AStarCollisionObjects.Count; i++)
                TileMap[(int)Map.AStarCollisionObjects[i].X, (int)Map.AStarCollisionObjects[i].Y].isWalkable = false;
        }
        /// <summary>
        /// Add Neighbors Tiles to the OpenList and the Current Tile to the Closed list
        /// </summary>
        void FindNeighbourTiles()
        {
            Tile NeighborTile;
            bool Add = false;

            OpenList.Remove(CurrentTile); // Remove da openlist o current Tile
            if (!ClosedList.Contains(CurrentTile)) // Adiciona o current Tile à closed list se não estiver já la
                ClosedList.Add(CurrentTile);

            // Check if we are in the bounds
            #region Top Tile
            if (CurrentTile.Position.Y - 1 >= 0)
            {
                NeighborTile = TileMap[(int)CurrentTile.Position.X, (int)CurrentTile.Position.Y - 1];
                if (!OpenList.Contains(NeighborTile))
                {
                    if (NeighborTile.isWalkable)
                    {
                        for (int i = 0; i < ClosedList.Count; i++)
                        {
                            if (NeighborTile.Position == ClosedList[i].Position)
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
                        NeighborTile.Parent = CurrentTile;
                        NeighborTile.Diagonal = false;
                        OpenList.Add(NeighborTile);
                        Add = false;
                    }
                }
            }
            #endregion
            #region Bottom Tile
            if (CurrentTile.Position.Y + 1 < TileMap.GetLength(1))
            {
                NeighborTile = TileMap[(int)CurrentTile.Position.X, (int)CurrentTile.Position.Y + 1];

                if (!OpenList.Contains(NeighborTile))
                {
                    if (NeighborTile.isWalkable)
                    {
                        for (int i = 0; i < ClosedList.Count; i++)
                        {
                            if (NeighborTile.Position == ClosedList[i].Position)
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
                        NeighborTile.Parent = CurrentTile;
                        NeighborTile.Diagonal = false;
                        OpenList.Add(NeighborTile);
                        Add = false;
                    }
                }
            }
            #endregion
            #region RightTile
            if (CurrentTile.Position.X + 1 < TileMap.GetLength(0))
            {
                NeighborTile = TileMap[(int)CurrentTile.Position.X + 1, (int)CurrentTile.Position.Y];

                if (!OpenList.Contains(NeighborTile))
                {
                    if (NeighborTile.isWalkable)
                    {
                        for (int i = 0; i < ClosedList.Count; i++)
                        {
                            if (NeighborTile.Position == ClosedList[i].Position)
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
                        NeighborTile.Parent = CurrentTile;
                        NeighborTile.Diagonal = false;
                        OpenList.Add(NeighborTile);
                        Add = false;
                    }
                }
            }
            #endregion
            #region Left Tile
            if (CurrentTile.Position.X - 1 >= 0)
            {
                NeighborTile = TileMap[(int)CurrentTile.Position.X - 1, (int)CurrentTile.Position.Y];

                if (!OpenList.Contains(NeighborTile))
                {
                    if (NeighborTile.isWalkable)
                    {
                        foreach (Tile closedTile in ClosedList)  // Melhor?
                        {
                            if (NeighborTile.Position == closedTile.Position)
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
                        NeighborTile.Parent = CurrentTile;
                        NeighborTile.Diagonal = false;
                        OpenList.Add(NeighborTile);
                    }
                }
            }
            #endregion
            // Pathfinding Diagonal
            if (!DisableDiagonalPathfinding)
            {
                #region Top-Right Tile
                if (CurrentTile.Position.X + 1 < TileMap.GetLength(0) && CurrentTile.Position.Y - 1 >= 0)
                {
                    NeighborTile = TileMap[(int)CurrentTile.Position.X + 1, (int)CurrentTile.Position.Y - 1];

                    if (!OpenList.Contains(NeighborTile))
                    {
                        if (NeighborTile.isWalkable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborTile.Position == ClosedList[i].Position)
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
                            NeighborTile.Parent = CurrentTile;
                            NeighborTile.Diagonal = true;
                            OpenList.Add(NeighborTile);
                            Add = false;
                        }
                    }
                }
                #endregion
                #region Top-Left Tile
                if (CurrentTile.Position.X - 1 >= 0 && CurrentTile.Position.Y - 1 >= 0)
                {
                    NeighborTile = TileMap[(int)CurrentTile.Position.X - 1, (int)CurrentTile.Position.Y - 1];

                    if (!OpenList.Contains(NeighborTile))
                    {
                        if (NeighborTile.isWalkable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborTile.Position == ClosedList[i].Position)
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
                            NeighborTile.Parent = CurrentTile;
                            NeighborTile.Diagonal = true;
                            OpenList.Add(NeighborTile);
                            Add = false;
                        }
                    }
                }
                #endregion
                #region Bottom-Right Tile
                if (CurrentTile.Position.X + 1 < TileMap.GetLength(0) && CurrentTile.Position.Y + 1 < TileMap.GetLength(1))
                {
                    NeighborTile = TileMap[(int)CurrentTile.Position.X + 1, (int)CurrentTile.Position.Y + 1];

                    if (!OpenList.Contains(NeighborTile))
                    {
                        if (NeighborTile.isWalkable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborTile.Position == ClosedList[i].Position)
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
                            NeighborTile.Parent = CurrentTile;
                            NeighborTile.Diagonal = true;
                            OpenList.Add(NeighborTile);
                            Add = false;
                        }
                    }
                }
                #endregion
                #region Bottom-Left Tile
                if (CurrentTile.Position.X - 1 >= 0 && CurrentTile.Position.Y + 1 < TileMap.GetLength(1))
                {
                    NeighborTile = TileMap[(int)CurrentTile.Position.X - 1, (int)CurrentTile.Position.Y + 1];

                    if (!OpenList.Contains(NeighborTile))
                    {
                        if (NeighborTile.isWalkable)
                        {
                            for (int i = 0; i < ClosedList.Count; i++)
                            {
                                if (NeighborTile.Position == ClosedList[i].Position)
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
                            NeighborTile.Parent = CurrentTile;
                            NeighborTile.Diagonal = true;
                            OpenList.Add(NeighborTile);
                            Add = false;
                        }
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// Distancia do CurrentTile até ao TargetTile
        /// </summary>
        void Calculate_Heuristic_Value()
        {
            // distância do startingTile ao targetTile
            Vector2 Distance = Vector2.Zero;

            for (int x = 0; x < TileMap.GetLength(0); x++)
            {
                for (int y = 0; y < TileMap.GetLength(1); y++)
                {
                    Vector2 CurrentTilePosition = new Vector2(x, y);

                    // Tile at right
                    if (CurrentTilePosition.X <= TargetTile.Position.X)
                        Distance.X = TargetTile.Position.X - CurrentTilePosition.X;
                    // Tile at left
                    if (CurrentTilePosition.X >= TargetTile.Position.X)
                        Distance.X = CurrentTilePosition.X - TargetTile.Position.X;
                    // Tile at Up
                    if (CurrentTilePosition.Y <= TargetTile.Position.Y)
                        Distance.Y = TargetTile.Position.Y - CurrentTilePosition.Y;
                    // Tile at Down
                    if (CurrentTilePosition.Y >= TargetTile.Position.Y - TargetTile.Position.Y)
                        Distance.Y = CurrentTilePosition.Y - TargetTile.Position.Y;

                    TileMap[x, y].H_Value = (int)(Distance.X + Distance.Y);
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
        // Devolve o Tile com o F value mais baixo
        Tile GetMin_F_Value()
        {
            List<int> F_ValuesList = new List<int>();
            int Lowest_F_Value = 0;

            for (int i = 0; i < OpenList.Count; i++)
                F_ValuesList.Add(OpenList[i].F_Value);
            if(F_ValuesList.Count > 0)
            {
                Lowest_F_Value = F_ValuesList.Min();
                foreach(Tile openTile in OpenList)
                {
                    if (openTile.F_Value == Lowest_F_Value)
                        return openTile;
                }
            }

            return CurrentTile;
        }
        // Organiza a lista de Tiles com o caminho final (FinalPath)
        void CalculateFinalPath()
        {
            List<Tile> FinalPathTemp = new List<Tile>();

            FinalPathTemp.Add(CurrentTile);
            FinalPathTemp.Add(CurrentTile.Parent);

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
            // Com a lista de Tiles(FinalPath) cria-se uma lista de vetores
            for (int i = 1; i < FinalPath.Count; i++)
                FinalPatchVector2.Add(FinalPath[i].Position);

            return FinalPatchVector2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartingTile">The starting position of the search path in map coordinate</param>
        /// <param name="EndingTile">The Goal or the ending position of the search path in map coordinate</param>
        public void FindPath()
        {
            CreateTileList();
            GenerateCollisionTiles();

            CurrentTile = this.StartingTile; // 

            OpenList = new List<Tile>();
            ClosedList = new List<Tile>();
            FinalPath = new List<Tile>();

            Calculate_Heuristic_Value();

            while (true)
            {
                CurrentTile = GetMin_F_Value(); // 

                FindNeighbourTiles();
                Calculate_G_Value();
                Calculate_F_Value();

                foreach(Tile openTile in OpenList)
                {
                    if(openTile.Position == TargetTile.Position)
                    {
                        CurrentTile = openTile;
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

