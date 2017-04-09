using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    class MapGenerator
    {
        static public int Width;
        static public int Height;

        public string Seed;
        public bool UseRandomSeed;

        public int RandomFillPercent;

        //public Tile tile;
        static public Tile[,] TilesMap;
        static public List<Room> MapRooms;
        static int[,] infoMap;
        public Vector2 PlayerStart;

        //------------->FUNCTIONS && METHODS<-------------//

        public void GenerateMap(int tileSize)
        {
            TilesMap = new Tile[Width, Height];
            MapRooms = new List<Room>();
            infoMap = new int[Width, Height];
            RandomFillMap();
            for (int i = 0; i < 5; i++)
            {
                SmoothMap();
            }
            ProcessMap();
            FillTileMap(tileSize);
            //GetPlayerStartingPosition();
        }

        private void RandomFillMap()
        {
            if (UseRandomSeed)
            {
                Seed = DateTime.Now.ToString();
            }

            System.Random pseudoRandom = new System.Random(Seed.GetHashCode());

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    {
                        infoMap[x, y] = 1;
                    }
                    else
                    {
                        infoMap[x, y] = (pseudoRandom.Next(0, 100) < RandomFillPercent) ? 1 : 0;
                    }
                }
            }
        }
        private void SmoothMap()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);

                    if (neighbourWallTiles > 4)
                        infoMap[x, y] = 1;
                    else if (neighbourWallTiles < 4)
                        infoMap[x, y] = 0;

                }
            }
        }
        private void ProcessMap()
        {
            List<List<Coordinate>> wallRegions = GetRegions(1);
            int wallThreshholdSize = 20;

            foreach(List<Coordinate> wallRegion in wallRegions)
            {
                if(wallRegion.Count < wallThreshholdSize)
                {
                    foreach(Coordinate tile in wallRegion)
                    {
                        infoMap[tile.tileX, tile.tileY] = 0;
                    }
                }
            }

            List<List<Coordinate>> roomRegions = GetRegions(0);
            int roomThreshholdSize = 20;

            foreach (List<Coordinate> roomRegion in roomRegions)
            {
                if (roomRegion.Count < roomThreshholdSize)
                {
                    foreach (Coordinate tile in roomRegion)
                    {
                        infoMap[tile.tileX, tile.tileY] = 1;
                    }
                }
                else
                {
                    MapRooms.Add(new Room(roomRegion, infoMap));
                }
            }
            MapRooms.Sort();
            MapRooms[0].isMainRoom = true;
            MapRooms[0].isAccessibleFromMainRoom = true;

            ConnectClosestRooms(MapRooms);
        }
        private void FillTileMap(int tileSize)
        {
            if (infoMap != null)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        int number = (infoMap[x, y] == 1) ? 1 : 0;
                        Vector2 pos = new Vector2(x, y);
                        Tile tile = new Tile(number, pos, tileSize);
                        TilesMap[x,y] = tile;
                    }
                }
            }
        }

        static private int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    if (IsInMapRange(neighbourX, neighbourY))
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                        {
                            wallCount += infoMap[neighbourX, neighbourY];
                        }
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }
            return wallCount;
        }
        private void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
        {
            List<Room> roomListA = new List<Room>();
            List<Room> roomListB = new List<Room>();

            if(forceAccessibilityFromMainRoom)
            {
                foreach(Room room in allRooms)
                {
                    if(room.isAccessibleFromMainRoom)
                    {
                        roomListB.Add(room);
                    }
                    else
                    {
                        roomListA.Add(room);
                    }
                }
            }
            else
            {
                roomListA = allRooms;
                roomListB = allRooms;
            }
                

            int bestDistance = 0;
            Coordinate bestTileA = new Coordinate();
            Coordinate bestTileB = new Coordinate();
            Room bestRoomA = new Room();
            Room bestRoomB = new Room();
            bool possibleConnectionFound = false;

            foreach(Room roomA in roomListA)
            {
                if(!forceAccessibilityFromMainRoom)
                {
                    possibleConnectionFound = false;
                    if(roomA.connectedRooms.Count > 0)
                        continue;
                }
                foreach (Room roomB in roomListB)
                {
                    if (roomA == roomB || roomA.IsConnected(roomB))
                        continue;

                    for(int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
                    {
                        for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
                        {
                            Coordinate tileA = roomA.edgeTiles[tileIndexA];
                            Coordinate tileB = roomB.edgeTiles[tileIndexB];
                            int distanceBetweenRooms = (int)(Math.Pow(tileA.tileX - tileB.tileX, 2) + Math.Pow(tileA.tileY - tileB.tileY, 2));

                            if(distanceBetweenRooms < bestDistance || !possibleConnectionFound)
                            {
                                bestDistance = distanceBetweenRooms;
                                possibleConnectionFound = true;
                                bestTileA = tileA;
                                bestTileB = tileB;
                                bestRoomA = roomA;
                                bestRoomB = roomB;
                            }
                        }
                    }
                }

                if(possibleConnectionFound && !forceAccessibilityFromMainRoom)
                {
                    CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
                }
            }
            if (possibleConnectionFound && forceAccessibilityFromMainRoom)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
                ConnectClosestRooms(allRooms, true);
            }


            if (!forceAccessibilityFromMainRoom)
            {
                ConnectClosestRooms(allRooms, true);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomA"></param>
        /// <param name="roomB"></param>
        /// <param name="tileA"></param>
        /// <param name="tileB"></param>
        private void CreatePassage(Room roomA, Room roomB, Coordinate tileA, Coordinate tileB)
        {
            Room.ConnectRooms(roomA, roomB);

            List<Coordinate> line = GetLine(tileA, tileB);
            foreach(Coordinate c in line)
            {
                DrawCircle(c, 2);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        private void DrawCircle(Coordinate c, int r)
        {
            for(int x = -r; x <= r; x++)
            {
                for (int y = -r; y <= r; y++)
                {
                    if(x*x + y*y <= r*r)
                    {
                        int drawX = c.tileX + x;
                        int drawY = c.tileY + y;

                        if(IsInMapRange(drawX,drawY))
                            infoMap[drawX, drawY] = 0;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private List<Coordinate> GetLine(Coordinate from, Coordinate to)
        {
            List<Coordinate> line = new List<Coordinate>();

            int x = from.tileX;
            int y = from.tileY;

            int dx = to.tileX - from.tileX;
            int dy = to.tileY - from.tileY;

            bool inverted = false;
            int step = Math.Sign(dx);
            int gradientStep = Math.Sign(dy);

            int longest = Math.Abs(dx);
            int shortest = Math.Abs(dy);

            if(longest < shortest)
            {
                inverted = true;
                longest = Math.Abs(dy);
                shortest = Math.Abs(dx);

                step = Math.Sign(dy);
                gradientStep = Math.Sign(dx);
            }

            int gradientAccumulation = longest / 2;

            for(int i = 0; i < longest; i++)
            {
                line.Add(new Coordinate(x, y));

                if (inverted)
                    y += step;
                else
                    x += step;

                gradientAccumulation += shortest;

                if(gradientAccumulation >= longest)
                {
                    if (inverted)
                        x += gradientStep;
                    else
                        y += gradientStep;

                    gradientAccumulation -= longest;
                }
            }

            return line;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private Vector2 CoordinateToWorldPoint(Coordinate tile)
        {
            return new Vector2(-Width / 2 + 0.5f + tile.tileX, -Height / 2 + 0.5f + tile.tileY);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileType"></param>
        /// <returns></returns>
        private List<List<Coordinate>> GetRegions(int tileType)
        {
            List<List<Coordinate>> regions = new List<List<Coordinate>>();
            int[,] mapFlags = new int[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if(mapFlags[x,y] == 0 && infoMap[x,y] == tileType)
                    {
                        List<Coordinate> newRegion = GetRegionTiles(x, y);
                        //List<Coordinate> newRegion = GetRegionTiles(new Coordinate(x, y), mapFlags, tileType);
                        regions.Add(newRegion);

                        foreach (Coordinate tile in newRegion)
                        {
                            mapFlags[tile.tileX, tile.tileY] = 1;
                        }
                    }
                }
            }
            return regions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <returns></returns>
        private List<Coordinate> GetRegionTiles(int startX, int startY)
        {
            List<Coordinate> tiles = new List<Coordinate>();
            int[,] mapFlags = new int[Width, Height];
            int tileType = infoMap[startX, startY];

            Queue<Coordinate> queue = new Queue<Coordinate>();
            queue.Enqueue(new Coordinate(startX, startY));
            mapFlags[startX, startY] = 1;

            while (queue.Count > 0)
            {
                Coordinate tile = queue.Dequeue();
                tiles.Add(tile);

                //identiifca tile relacionado com startx e starty
                for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
                {
                    for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                    {
                        if (IsInMapRange(x, y) && (x == tile.tileX || y == tile.tileY))
                        {
                            if (mapFlags[x, y] == 0 && infoMap[x, y] == tileType)
                            {
                                mapFlags[x, y] = 1;
                                queue.Enqueue(new Coordinate(x, y));
                            }
                        }
                    }
                }
            }
            //Analizar tiles...
            return tiles;
        }
        static private bool IsInMapRange(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
        static public Vector2 GetPlayerStartingPosition()
        {
            Vector2 result = Vector2.Zero;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    foreach (Room r in MapRooms)
                    {
                        if (infoMap[x, y] == 0 && GetSurroundingWallCount(x, y) == 0 && r.tiles.Contains(new Coordinate(x, y)))
                        {
                            result = new Vector2(x, y);
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public void Draw(Camera camera)
        {
            foreach(Tile tile in TilesMap)
            {
                tile.DrawTile(camera);
            }
        }
    }
}
