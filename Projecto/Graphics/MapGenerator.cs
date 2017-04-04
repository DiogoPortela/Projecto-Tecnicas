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

        public int width;
        public int height;

        public string seed;
        public bool useRandomSeed;

        public int randomFillPercent;

        public Tile tile;

        int[,] map;

        public void Start()
        {
            GenerateMap();
        }

        void GenerateMap()
        {
            map = new int[width, height];
            RandomFillMap();

            for (int i = 0; i < 5; i++)
            {
                SmoothMap();
            }

            ProcessMap();
        }

        void ProcessMap()
        {
            List<List<Coordinate>> wallRegions = GetRegions(1);
            int wallThreshholdSize = 50;

            foreach(List<Coordinate> wallRegion in wallRegions)
            {
                if(wallRegion.Count < wallThreshholdSize)
                {
                    foreach(Coordinate tile in wallRegion)
                    {
                        map[tile.tileX, tile.tileY] = 0;
                    }
                }
            }

            List<List<Coordinate>> roomRegions = GetRegions(0);
            int roomThreshholdSize = 50;
            List<Room> survivingRooms = new List<Room>();

            foreach (List<Coordinate> roomRegion in roomRegions)
            {
                if (roomRegion.Count < roomThreshholdSize)
                {
                    foreach (Coordinate tile in roomRegion)
                    {
                        map[tile.tileX, tile.tileY] = 1;
                    }
                }
                else
                {
                    survivingRooms.Add(new Room(roomRegion, map));
                }
            }

            survivingRooms.Sort();
            survivingRooms[0].isMainRoom = true;
            survivingRooms[0].isAccessibleFromMainRoom = true;

            ConnectClosestRooms(survivingRooms);
        }

        void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
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

        void CreatePassage(Room roomA, Room roomB, Coordinate tileA, Coordinate tileB)
        {
            Room.ConnectRooms(roomA, roomB);

            List<Coordinate> line = GetLine(tileA, tileB);
            foreach(Coordinate c in line)
            {
                DrawCircle(c, 2);
            }
        }

        void DrawCircle(Coordinate c, int r)
        {
            for(int x = -r; x <= r; x++)
            {
                for (int y = -r; y <= r; y++)
                {
                    if(x*x + y*y < r*r)
                    {
                        int realX = c.tileX + x;
                        int realY = c.tileY + y;

                        if(IsInMapRange(realX,realY))
                            map[realX, realY] = 0;
                    }
                }
            }
        }

        List<Coordinate> GetLine(Coordinate from, Coordinate to)
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

        Vector2 CoordinateToWorldPoint(Coordinate tile)
        {
            return new Vector2(-width / 2 + 0.5f + tile.tileX, -height / 2 + 0.5f + tile.tileY);
        }

        List<List<Coordinate>> GetRegions(int tileType)
        {
            List<List<Coordinate>> regions = new List<List<Coordinate>>();
            int[,] mapFlags = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(mapFlags[x,y] == 0 && map[x,y] == tileType)
                    {
                        List<Coordinate> newRegion = GetRegionTiles(x, y);
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

        List<Coordinate> GetRegionTiles(int startX, int startY)
        {
            List<Coordinate> tiles = new List<Coordinate>();
            int[,] mapFlags = new int[width, height];
            int tileType = map[startX, startY];

            Queue<Coordinate> queue = new Queue<Coordinate>();
            queue.Enqueue(new Coordinate(startX, startY));
            mapFlags[startX, startY] = 1;

            while(queue.Count > 0)
            {
                Coordinate tile = queue.Dequeue();
                tiles.Add(tile);

                for(int x = tile.tileX - 1; x <= tile.tileX; x++ )
                {
                    for(int y = tile.tileY - 1; y <= tile.tileY; y++)
                    {
                        if(IsInMapRange(x,y) && (x == tile.tileX || y == tile.tileY))
                        {
                            if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                            {
                                mapFlags[x, y] = 1;
                                queue.Enqueue(new Coordinate(x, y));
                            }
                        }
                    }
                }
            }
            return tiles;
        }

        bool IsInMapRange(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }


        void RandomFillMap()
        {
            if (useRandomSeed)
            {
                seed = DateTime.Now.ToString();
            }

            System.Random pseudoRandom = new System.Random(seed.GetHashCode());

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                    }
                }
            }
        }

        void SmoothMap()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);

                    if (neighbourWallTiles > 4)
                        map[x, y] = 1;
                    else if (neighbourWallTiles < 4)
                        map[x, y] = 0;

                }
            }
        }

        int GetSurroundingWallCount(int gridX, int gridY)
        {
            int wallCount = 0;
            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    if (IsInMapRange(neighbourX,neighbourY))
                    {
                        if (neighbourX != gridX || neighbourY != gridY)
                        {
                            wallCount += map[neighbourX, neighbourY];
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

        public struct Coordinate
        {
            public int tileX;
            public int tileY;

            public Coordinate(int x, int y)
            {
                tileX = x;
                tileY = y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (map != null)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int number = (map[x, y] == 1) ? 1 : 0;
                        Vector2 pos = new Vector2(x,y);
                        tile = new Tile(number, pos, 1);
                        tile.DrawTile(spriteBatch);
                    }
                }
            }
        }
    }
}
