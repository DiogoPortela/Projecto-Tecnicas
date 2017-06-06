using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SettlersEngine;
using System.IO;

namespace Projecto
{
    class MapGenerator
    {
        static public int Width;
        static public int Height;
        static public int TileSize;

        public string Seed;
        public bool UseRandomSeed;

        public int RandomFillPercent;

        static public Tile[,] TilesMap;
        static public List<Room> MapRooms;
        static public Room Spawn;
        static public int[,] infoMap;
        static private int WalkableTiles;

        //------------->FUNCTIONS && METHODS<-------------//

        public void GenerateMap(int tileSize)
        {
            TileSize = tileSize;
            TilesMap = new Tile[Width, Height];
            MapRooms = new List<Room>();
            infoMap = new int[Width, Height];
            RandomFillMap();
            for (int i = 0; i < 10; i++)
            {
                SmoothMap();
            }
            ProcessMap();
            FillCorners();
            FillTileMap();
            FindPlayerSpawn();
            FindMapWalkableTiles();
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

            foreach (List<Coordinate> wallRegion in wallRegions)
            {
                if (wallRegion.Count < wallThreshholdSize)
                {
                    foreach (Coordinate tile in wallRegion)
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

        //----------------------------Map Generation-----------------------------//
        /// <summary>
        /// Transforms a number (0,1) into a Tile.
        /// </summary>
        private void FillTileMap()
        {
            if (infoMap != null)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        int aux = 0;
                        if(infoMap[x, y] == 1)
                        {
                            aux = Game1.random.Next(1, 4);
                        }
                        else
                        {
                            aux = Game1.random.Next(10, 13);
                        }
                        Tile tile = new Tile(aux, new Vector2(x, y), TileSize);
                        TilesMap[x, y] = tile;
                    }
                }
            }
        }
        /// <summary>
        /// Fills the corners of the map.
        /// </summary>
        private void FillCorners()
        {
            for(int y = 0; y < Height; y++)
            {
                infoMap[0, y] = 1;
                infoMap[Width - 1, y] = 1;
            }
            for (int x = 0; x < Width; x++)
            {
                infoMap[x, 0] = 1;
                infoMap[x, Height - 1] = 1;
            }

        }
        /// <summary>
        /// Given two Coordinates of the map, gets the count of walls around it.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns>The count of walls</returns>
        static public int GetSurroundingWallCount(int gridX, int gridY)
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
                        wallCount++;
                }
            }
            return wallCount;
        }
        /// <summary>
        /// Given the List of all rooms in our map, connects each one with the closest.
        /// </summary>
        /// <param name="allRooms"></param>
        /// <param name="forceAccessibilityFromMainRoom"></param>
        private void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
        {
            List<Room> roomListA = new List<Room>();
            List<Room> roomListB = new List<Room>();

            if (forceAccessibilityFromMainRoom)
            {
                foreach (Room room in allRooms)
                {
                    if (room.isAccessibleFromMainRoom)
                        roomListB.Add(room);
                    else
                        roomListA.Add(room);
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

            foreach (Room roomA in roomListA)
            {
                if (!forceAccessibilityFromMainRoom)
                {
                    possibleConnectionFound = false;
                    if (roomA.connectedRooms.Count > 0)
                        continue;
                }
                foreach (Room roomB in roomListB)
                {
                    if (roomA == roomB || roomA.IsConnected(roomB))
                        continue;

                    for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
                    {
                        for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
                        {
                            Coordinate tileA = roomA.edgeTiles[tileIndexA];
                            Coordinate tileB = roomB.edgeTiles[tileIndexB];
                            int distanceBetweenRooms = (int)(Math.Pow(tileA.tileX - tileB.tileX, 2) + Math.Pow(tileA.tileY - tileB.tileY, 2));

                            if (distanceBetweenRooms < bestDistance || !possibleConnectionFound)
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

                if (possibleConnectionFound && !forceAccessibilityFromMainRoom)
                    CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            }
            if (possibleConnectionFound && forceAccessibilityFromMainRoom)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
                ConnectClosestRooms(allRooms, true);
            }


            if (!forceAccessibilityFromMainRoom)
                ConnectClosestRooms(allRooms, true);

        }
        /// <summary>
        /// Given two Rooms and two Coordinates, calls the GetLine function to get a line between the given rooms and then calls DrawCircle for each of the Coordinates in the Line.
        /// This creates a passage with the DrawCircle radius as width.
        /// </summary>
        /// <param name="roomA"></param>
        /// <param name="roomB"></param>
        /// <param name="tileA"></param>
        /// <param name="tileB"></param>
        private void CreatePassage(Room roomA, Room roomB, Coordinate tileA, Coordinate tileB)
        {
            Room.ConnectRooms(roomA, roomB);

            List<Coordinate> line = GetLine(tileA, tileB);
            foreach (Coordinate c in line)
                DrawCircle(c, 4);
        }
        /// <summary>
        /// Given a Coordinate and a radius, this function draws a circle around the Coordinate.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        private void DrawCircle(Coordinate c, int r)
        {
            for (int x = -r; x <= r; x++)
            {
                for (int y = -r; y <= r; y++)
                {
                    if (x * x + y * y <= r * r)
                    {
                        int drawX = c.tileX + x;
                        int drawY = c.tileY + y;

                        if (IsInMapRange(drawX, drawY))
                            infoMap[drawX, drawY] = 0;
                    }
                }
            }
        }
        /// <summary>
        /// This function creates a line between two Coordinates.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>A list of Coordinates (representing a line).</returns>
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

            if (longest < shortest)
            {
                inverted = true;
                longest = Math.Abs(dy);
                shortest = Math.Abs(dx);

                step = Math.Sign(dy);
                gradientStep = Math.Sign(dx);
            }

            int gradientAccumulation = longest / 2;

            for (int i = 0; i < longest; i++)
            {
                line.Add(new Coordinate(x, y));

                if (inverted)
                    y += step;
                else
                    x += step;

                gradientAccumulation += shortest;

                if (gradientAccumulation >= longest)
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
        /// Transforms a Coordinate into a Vector2.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns>A Vector2 with the given Coordinates</returns>
        static private Vector2 CoordinateToWorldPoint(Coordinate tile)
        {
            return new Vector2(tile.tileX * TileSize, tile.tileY * TileSize);
        }
        /// <summary>
        /// Given the tileType, this function gets all the regions in the map. 
        /// Uses GetRegionTiles function for each new possible region it finds.
        /// </summary>
        /// <param name="tileType"></param>
        /// <returns>A List of Lists of Coordinates. These represent a Region of our map.</returns>
        private List<List<Coordinate>> GetRegions(int tileType)
        {
            List<List<Coordinate>> regions = new List<List<Coordinate>>();
            int[,] mapFlags = new int[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (mapFlags[x, y] == 0 && infoMap[x, y] == tileType)
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
        /// <summary>
        /// Gets a list of tiles given a starting position. Checks for the tileType of that given position and looks for similar tiles in the same area.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <returns>A list of Coordinates that forms a region </returns>
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

                //identifica tile relacionado com startx e starty
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
        /// <summary>
        /// Checks if a given Coordinate is inside map boundaris.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns a bool that confirms or denies the condition</returns>
        static private bool IsInMapRange(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
        /// <summary>
        /// Finds the rooms in which monsters can spawn.
        /// </summary>
        static private void FindMapWalkableTiles()
        {
            foreach (Room r in MapRooms)
            {
                if (!r.isSpawn)
                {
                    WalkableTiles += r.roomSize;
                }
            }
        }
        /// <summary>
        /// Finds the room in which the Player(s) can spawn.
        /// </summary>
        static private void FindPlayerSpawn()
        {
            int minX = Width;
            foreach (Room r in MapRooms)
            {
                if (!r.isMainRoom)
                    foreach (Coordinate c in r.tiles)
                    {
                        if (c.tileX < minX)
                        {
                            minX = c.tileX;
                            Spawn = r;
                        }
                    }
            }
            Spawn.isSpawn = true;
        }
        static private Coordinate RandomCoord(Room r)
        {
            return r.tiles[Game1.random.Next(r.tiles.Count())];
        }
        //---------------------------Player & Enemy Position--------------------------------//
        /// <summary>
        /// Randomizes the starting position of the player(s). This Coordinate will be a part of the Player spawn room
        /// </summary>
        /// <returns>A vector2 in which the player will spawn</returns>
        static public List<Vector2> FindEnemySpawns(int maxMobs)
        {
            List<Vector2> enemySpawns = new List<Vector2>();
            float aux;
            int[,] mapFlags = new int[Width, Height];
            Coordinate c;
            foreach (Room r in MapRooms)
            {
                if (!r.isSpawn)
                {
                    aux = (float)r.roomSize / (float)WalkableTiles;

                    if (r.isMainRoom)
                    {
                        for (int i = 0; i < maxMobs;)
                        {
                            c = RandomCoord(r);

                            if (GetSurroundingWallCount(c.tileX, c.tileY) == 0 && mapFlags[c.tileX,c.tileY] == 0 &&
                                TilesMap[c.tileX, c.tileY].isWall == false)
                            if (TilesMap[c.tileX, c.tileY].isWall == false)
                            {
                                enemySpawns.Add(CoordinateToWorldPoint(c));
                                mapFlags[c.tileX, c.tileY] = 1;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < aux * maxMobs;)
                        {
                            c = RandomCoord(r);

                            if (GetSurroundingWallCount(c.tileX, c.tileY) == 0 && mapFlags[c.tileX, c.tileY] == 0 &&
                                TilesMap[c.tileX,c.tileY].isWall == false)
                            if (TilesMap[c.tileX, c.tileY].isWall == false)
                            {
                                enemySpawns.Add(CoordinateToWorldPoint(c));
                                mapFlags[c.tileX, c.tileY] = 1;
                                i++;
                            }
                        }
                    }
                }
            }
            return enemySpawns;
        }
        /// <summary>
        /// This function calculates a position in the spawn for the player.
        /// </summary>
        /// <returns></returns>
        static public Vector2 GetPlayerStartingPosition()
        {
            Coordinate aux = Spawn.tiles[Game1.random.Next(Spawn.tiles.Count())];
            return new Vector2(aux.tileX * TileSize, aux.tileY * TileSize);
        }

        static public Vector2 GetPortalSpawn()
        {
            Coordinate aux = MapRooms[0].tiles[Game1.random.Next(MapRooms[0].tiles.Count())];
            return new Vector2(aux.tileX * TileSize, aux.tileY * TileSize);
        } 

        /// <summary>
        /// Draws the finished map on the given camera.
        /// </summary>
        /// <param name="camera"></param>
        public void Draw(Camera camera)
        {
            foreach (Tile tile in TilesMap)
            {
                tile.DrawTile(camera);
            }
        }
    }
}