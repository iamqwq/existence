using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 辅助类，用于实现Dijkstra算法
public class PathNode : IComparable<PathNode>
{
    public string Node;
    public float Distance;
    public List<string> Path;

    public PathNode(string node, float distance, List<string> path)
    {
        Node = node;
        Distance = distance;
        Path = new List<string>(path);
        Path.Add(node);
    }

    public int CompareTo(PathNode other)
    {
        return Distance.CompareTo(other.Distance);
    }
}

public class Graph : MonoBehaviour
{
    private Dictionary<string, List<string>> adjacencyList;
    private List<Room> rooms;

    public Graph(List<Room> rooms)
    {
        adjacencyList = new Dictionary<string, List<string>>();
        this.rooms = rooms;

        foreach (var room in rooms)
        {
            if (!adjacencyList.ContainsKey(room.Index))
            {
                adjacencyList[room.Index] = new List<string>();
            }

            foreach (var connectedIndex in room.Connected)
            {
                if (!adjacencyList[room.Index].Contains(connectedIndex))
                {
                    adjacencyList[room.Index].Add(connectedIndex);
                }

                if (!adjacencyList.ContainsKey(connectedIndex))
                {
                    adjacencyList[connectedIndex] = new List<string>();
                }
                if (!adjacencyList[connectedIndex].Contains(room.Index))
                {
                    adjacencyList[connectedIndex].Add(room.Index);
                }
            }
        }
    }

    public List<Room> Method(Room source, List<Room> targets)
    {
        var distances = new Dictionary<string, float>();
        var previousNodes = new Dictionary<string, string>();
        var pq = new SortedSet<PathNode>();  // 路径长度的最小堆

        foreach (var node in adjacencyList.Keys)
        {
            distances[node] = float.PositiveInfinity;
        }
        distances[source.Index] = 0;

        pq.Add(new PathNode(source.Index, 0, new List<string>()));

        while (pq.Count > 0)
        {
            var currentNode = pq.Min;
            pq.Remove(currentNode);

            foreach (var neighbor in adjacencyList[currentNode.Node])
            {
                float alt = currentNode.Distance + 1;
                if (alt < distances[neighbor])
                {
                    distances[neighbor] = alt;
                    previousNodes[neighbor] = currentNode.Node;
                    pq.Add(new PathNode(neighbor, alt, currentNode.Path));
                }
            }
        }

        // 寻找路径上最接近起始点的节点
        string closestTarget = null;
        float minDistance = float.PositiveInfinity;
        foreach (var target in targets)
        {
            if (distances.TryGetValue(target.Index, out float distance) && distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target.Index;
            }
        }

        if (closestTarget != null)
        {
            var pathIndices = ReconstructPath(previousNodes, source.Index, closestTarget);
            return pathIndices.Select(index => FindRoomByIndex(index, rooms)).ToList();
        }

        return new List<Room>();  // 若没有路径则返回空列表
    }

    // 辅助函数
    private Room FindRoomByIndex(string index, List<Room> rooms)
    {
        return rooms.Find(room => room.Index == index);
    }

    // 辅助函数
    private List<string> ReconstructPath(Dictionary<string, string> previousNodes, string start, string end)
    {
        var path = new List<string>();
        var current = end;
        while (current != start)
        {
            path.Add(current);
            current = previousNodes[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}