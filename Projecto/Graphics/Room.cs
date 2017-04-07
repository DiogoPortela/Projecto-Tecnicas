using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projecto.MapGenerator;

namespace Projecto
{
    class Room : IComparable<Room>
    {
        public List<Coordinate> tiles;
        public List<Coordinate> edgeTiles;
        public List<Room> connectedRooms;
        public int roomSize;
        public bool isAccessibleFromMainRoom;
        public bool isMainRoom;

        public Room(){}

        public Room(List<Coordinate> roomTiles, int[,] map)
        {
            tiles = roomTiles;
            roomSize = tiles.Count;

            connectedRooms = new List<Room>();
            edgeTiles = new List<Coordinate>();

            foreach(Coordinate tile in tiles)
            {
                for(int x = tile.tileX -1; x <= tile.tileX + 1; x++)
                {
                    for (int y = tile.tileY -1; y <= tile.tileY + 1; y++)
                    {
                        if(x == tile.tileX || y == tile.tileY)
                        {
                           // try
                            {
                                if (map[x, y] == 1)
                                {
                                    edgeTiles.Add(tile);
                                }
                            }
                            //catch { }
                        }
                    }
                }
            }
        }

        public void SetAccessibleFromMainRoom()
        {
            if(!isAccessibleFromMainRoom)
            {
                isAccessibleFromMainRoom = true;
                foreach(Room connectedRoom in connectedRooms)
                {
                    connectedRoom.SetAccessibleFromMainRoom();
                }
            }
        }

        public static void ConnectRooms(Room roomA, Room roomB)
        {
            if(roomA.isAccessibleFromMainRoom)
            {
                roomB.SetAccessibleFromMainRoom();
            }
            else if(roomB.isAccessibleFromMainRoom)
            {
                roomA.SetAccessibleFromMainRoom();
            }
            roomA.connectedRooms.Add(roomB);
            roomB.connectedRooms.Add(roomA);
        }

        public bool IsConnected(Room otherRoom)
        {
            return connectedRooms.Contains(otherRoom);
        }

        public int CompareTo(Room otherRoom)
        {
            return otherRoom.roomSize.CompareTo(roomSize);
        }
    }
}
