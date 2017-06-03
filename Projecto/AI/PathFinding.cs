using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Projecto
{
    public enum NodeState
    {
        /// <summary>
        /// The node has not yet been considered in any possible paths
        /// </summary>
        Untested,
        /// <summary>
        /// The node has been identified as a possible step in a path
        /// </summary>
        Open,
        /// <summary>
        /// The node has already been included in a path and will not be considered again
        /// </summary>
        Closed
    }

    public class Node
    {
        private Node parentNode;

        /// <summary>
        /// The node's location in the grid
        /// </summary>
        public Vector2 Location { get; private set; }

        /// <summary>
        /// True when the node may be traversed, otherwise false
        /// </summary>
        public bool IsWalkable { get; set; }

        /// <summary>
        /// Cost from start to here
        /// </summary>
        public float G { get;  set; }

        /// <summary>
        /// Estimated cost from here to end
        /// </summary>
        public float H { get;  set; }

        /// <summary>
        /// Flags whether the node is open, closed or untested by the PathFinder
        /// </summary>
        public NodeState State { get; set; }

        /// <summary>
        /// Estimated total cost (F = G + H)
        /// </summary>
        public float F
        {
            get { return this.G + this.H; }
        }

        /// <summary>
        /// Gets or sets the parent node. The start node's parent is always null.
        /// </summary>
        public Node ParentNode
        {
            get { return this.parentNode; }
            set
            {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                this.parentNode = value;
                this.G = this.parentNode.G + GetTraversalCost(this.Location, this.parentNode.Location);
            }
        }

        /// <summary>
        /// Creates a new instance of Node.
        /// </summary>
        /// <param name="x">The node's location along the X axis</param>
        /// <param name="y">The node's location along the Y axis</param>
        /// <param name="isWalkable">True if the node can be traversed, false if the node is a wall</param>
        /// <param name="endLocation">The location of the destination node</param>
        public Node(int x, int y, bool isWalkable, Vector2 endLocation)
        {
            this.Location = new Vector2(x, y);
            this.State = NodeState.Untested;
            this.IsWalkable = isWalkable;
            this.H = GetTraversalCost(this.Location, endLocation);
            this.G = 0;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}: {2}", this.Location.X, this.Location.Y, this.State);
        }

        /// <summary>
        /// Gets the distance between two vectors
        /// </summary>
        public static float GetTraversalCost(Vector2 location, Vector2 otherLocation)
        {
            float deltaX = otherLocation.X - location.X;
            float deltaY = otherLocation.Y - location.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }

    public class PathFinder
    {
        private int width;
        private int height;
        private Node[,] nodes;
        private Node startNode;
        private Node endNode;

        /// <summary>
        /// Create a new instance of PathFinder
        /// </summary>
        /// <param name="searchParameters"></param>
        public PathFinder(Vector2 start, Vector2 end, ref int[,] map)
        {
            InitializeNodes(ref map, end);
            this.startNode = this.nodes[(int)start.X, (int)start.Y];
            this.startNode.State = NodeState.Open;
            this.endNode = this.nodes[(int)end.X, (int)end.Y];
        }

        /// <summary>
        /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
        /// </summary>
        /// <returns>A List of Vectors representing the path. If no path was found, the returned list is empty.</returns>
        public Stack<Vector2> FindPath()
        {
            // The start node is the first entry in the 'open' list
            Stack<Vector2> path = new Stack<Vector2>();
            bool success = Search();
            if (success)
            {
                // If a path was found, follow the parents from the end node to build a list of locations
                Node node = this.endNode;
                while (node.ParentNode != null)
                {
                    path.Push(node.Location);
                    node = node.ParentNode;
                }

                // Reverse the list so it's in the correct order when returned
                //path.Reverse();
            }

            return path;
        }

        /// <summary>
        /// Builds the node grid from a simple grid of booleans indicating areas which are and aren't walkable
        /// </summary>
        /// <param name="map">A boolean representation of a grid in which true = walkable and false = not walkable</param>
        private void InitializeNodes(ref int[,] map, Vector2 endPos)
        {
            this.width = map.GetLength(0);
            this.height = map.GetLength(1);
            this.nodes = new Node[this.width, this.height];
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    bool aux;
                    if (map[x, y] == 0)
                        aux = true;
                    else
                        aux = false;
                    this.nodes[x, y] = new Node(x, y, aux, endPos);
                }
            }
        }

        ///// <summary>
        ///// Attempts to find a path to the destination node using <paramref name="currentNode"/> as the starting location
        ///// </summary>
        ///// <param name="currentNode">The node from which to find a path</param>
        ///// <returns>True if a path to the destination has been found, otherwise false</returns>
        //private bool Search(Node currentNode)
        //{
        //    // Set the current node to Closed since it cannot be traversed more than once
        //    currentNode.State = NodeState.Closed;
        //    List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

        //    // Sort by F-value so that the shortest possible routes are considered first
        //    nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
        //    foreach (Node nextNode in nextNodes)
        //    {
        //        // Check whether the end node has been reached
        //        if (nextNode.Location == this.endNode.Location)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            // If not, check the next set of nodes
        //            if (Search(nextNode)) // Note: Recurses back into Search(Node)
        //                return true;
        //        }
        //    }

        //    // The method returns false if this path leads to be a dead end
        //    return false;
        //}

        ///// <summary>
        ///// Returns any nodes that are adjacent to <paramref name="fromNode"/> and may be considered to form the next step in the path
        ///// </summary>
        ///// <param name="fromNode">The node from which to return the next possible nodes in the path</param>
        ///// <returns>A list of next possible nodes in the path</returns>
        //private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        //{
        //    List<Node> walkableNodes = new List<Node>();
        //    IEnumerable<Vector2> nextLocations = GetAdjacentLocations(fromNode.Location);

        //    foreach (Vector2 location in nextLocations)
        //    {
        //        int x = (int)location.X;
        //        int y = (int)location.Y;

        //        // Stay within the grid's boundaries
        //        if (x < 0 || x >= this.width || y < 0 || y >= this.height)
        //            continue;

        //        Node node = this.nodes[x, y];
        //        // Ignore non-walkable nodes
        //        if (!node.IsWalkable)
        //            continue;

        //        // Ignore already-closed nodes
        //        if (node.State == NodeState.Closed)
        //            continue;

        //        // Already-open nodes are only added to the list if their G-value is lower going via this route.
        //        if (node.State == NodeState.Open)
        //        {
        //            float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
        //            float gTemp = fromNode.G + traversalCost;
        //            if (gTemp < node.G)
        //            {
        //                node.ParentNode = fromNode;
        //                walkableNodes.Add(node);
        //            }
        //        }
        //        else
        //        {
        //            // If it's untested, set the parent and flag it as 'Open' for consideration
        //            node.ParentNode = fromNode;
        //            node.State = NodeState.Open;
        //            walkableNodes.Add(node);
        //        }
        //    }

        //    return walkableNodes;
        //}

        ///// <summary>
        ///// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
        ///// </summary>
        ///// <param name="fromLocation">The location from which to return all adjacent vectors</param>
        ///// <returns>The locations as an IEnumerable of vectors</returns>
        //private static IEnumerable<Vector2> GetAdjacentLocations(Vector2 fromLocation)
        //{
        //    return new Vector2[]
        //    {
        //        new Vector2(fromLocation.X-1, fromLocation.Y-1),
        //        new Vector2(fromLocation.X-1, fromLocation.Y  ),
        //        new Vector2(fromLocation.X-1, fromLocation.Y+1),
        //        new Vector2(fromLocation.X,   fromLocation.Y+1),
        //        new Vector2(fromLocation.X+1, fromLocation.Y+1),
        //        new Vector2(fromLocation.X+1, fromLocation.Y  ),
        //        new Vector2(fromLocation.X+1, fromLocation.Y-1),
        //        new Vector2(fromLocation.X,   fromLocation.Y-1)
        //    };
        //}

        /// <summary>
        /// Attempts to find a path to the destination.
        /// </summary>
        /// <returns>True if a path to the destination has been found, otherwise false</returns>
        private bool Search()
        {
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].F < node.F || openSet[i].F == node.F)
                    {
                        if (openSet[i].H < node.H)
                            node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node.Location == endNode.Location)
                {                 
                    return true;
                }

                List<Node> adj = GetNeighbours(node);
                foreach (Node neighbour in adj)
                {
                    if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = (int)(node.G + Node.GetTraversalCost(node.Location, neighbour.Location));
                    if (newCostToNeighbour < neighbour.G || !openSet.Contains(neighbour))
                    {
                        neighbour.G = newCostToNeighbour;
                        neighbour.H = Node.GetTraversalCost(neighbour.Location, endNode.Location);
                        neighbour.ParentNode = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns the surrounding nodes.
        /// </summary>
        /// <param name="node">Node to search surronds.</param>
        /// <returns></returns>
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = (int)(node.Location.X + x);
                    int checkY = (int)(node.Location.Y + y);

                    if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
                    {
                        if (x == -1 && y == -1 && !nodes[checkX - 1, checkY].IsWalkable && !nodes[checkX, checkY - 1].IsWalkable)
                            continue;
                        else if (x == 1 && y == -1 && !nodes[checkX + 1, checkY].IsWalkable && !nodes[checkX, checkY - 1].IsWalkable)
                            continue;
                        else if (x == -1 && y == 1 && !nodes[checkX - 1, checkY].IsWalkable && !nodes[checkX, checkY + 1].IsWalkable)
                            continue;
                        else if (x == 1 && y == 1 && !nodes[checkX + 1, checkY].IsWalkable && !nodes[checkX, checkY + 1].IsWalkable)
                            continue;
                        neighbours.Add(nodes[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }
    }
}
