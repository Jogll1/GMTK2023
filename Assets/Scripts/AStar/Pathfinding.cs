using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

//Credit Sebastian Lague
public class Pathfinding : MonoBehaviour
{
    public Transform seeker;
    public Transform target;

    public Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        // seeker = transform;
    }

    private void Update()
    {
        // grid.path = FindPath(seeker.position, target.position);
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPos(startPos);
        Node targetNode = grid.NodeFromWorldPos(targetPos);

        startNode.gCost = 0;

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet.RemoveFirst();

            closedSet.Add(node);

            if (node == targetNode)
            {
                List<Node> path = RetracePath(startNode, targetNode);
                return path;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        //else return empty list
        return new List<Node>();
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        return path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return (distanceX > distanceY) ? 14 * distanceY + 10 * (distanceX - distanceY) : 14 * distanceX + 10 * (distanceY - distanceX);
    }
}