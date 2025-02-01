using System;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Data;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Common {
    // 辅助类，用于实现Dijkstra算法
    public class PathNode : IComparable<PathNode> {
        public string Node;
        public float Distance;
        public List<string> Path;

        public PathNode(string node, float distance, List<string> path) {
            Node = node;
            Distance = distance;
            Path = new List<string>(path);
            Path.Add(node);
        }

        public int CompareTo(PathNode other) {
            return Distance.CompareTo(other.Distance);
        }
    }

    public class Graph {
        
        private readonly Dictionary<string, List<string>> _adjacencyList = new();
        private readonly List<RoomData> _rooms = new();

        public Graph() {
            
        }

        public void Add(RoomData roomData) {
            if (!_adjacencyList.ContainsKey(roomData.index)) {
                _adjacencyList[roomData.index] = new List<string>();
            }
            foreach (var connectedIndex in roomData.connected) {
                if (!_adjacencyList[roomData.index].Contains(connectedIndex)) {
                    _adjacencyList[roomData.index].Add(connectedIndex);
                }

                if (!_adjacencyList.ContainsKey(connectedIndex)) {
                    _adjacencyList[connectedIndex] = new List<string>();
                }

                if (!_adjacencyList[connectedIndex].Contains(roomData.index)) {
                    _adjacencyList[connectedIndex].Add(roomData.index);
                }
            }
            _rooms.Add(roomData);
        }

        public List<RoomData> Method(RoomData source, List<RoomData> targets) {
            var distances = new Dictionary<string, float>();
            var previousNodes = new Dictionary<string, string>();
            var pq = new SortedSet<PathNode>(); // 路径长度的最小堆

            foreach (var node in _adjacencyList.Keys) {
                distances[node] = float.PositiveInfinity;
            }

            distances[source.index] = 0;

            pq.Add(new PathNode(source.index, 0, new List<string>()));

            while (pq.Count > 0) {
                var currentNode = pq.Min;
                pq.Remove(currentNode);

                foreach (var neighbor in _adjacencyList[currentNode.Node]) {
                    float alt = currentNode.Distance + 1;
                    if (alt < distances[neighbor]) {
                        distances[neighbor] = alt;
                        previousNodes[neighbor] = currentNode.Node;
                        pq.Add(new PathNode(neighbor, alt, currentNode.Path));
                    }
                }
            }

            // 寻找路径上最接近起始点的节点
            string closestTarget = null;
            float minDistance = float.PositiveInfinity;
            foreach (var target in targets) {
                if (distances.TryGetValue(target.index, out float distance) && distance < minDistance) {
                    minDistance = distance;
                    closestTarget = target.index;
                }
            }

            if (closestTarget != null) {
                var pathIndices = ReconstructPath(previousNodes, source.index, closestTarget);
                return pathIndices.Select(index => FindRoomByIndex(index, _rooms)).ToList();
            }

            return new List<RoomData>(); // 若没有路径则返回空列表
        }

        // 辅助函数
        private RoomData FindRoomByIndex(string index, List<RoomData> rooms) {
            return rooms.Find(room => room.index == index);
        }

        // 辅助函数
        private List<string> ReconstructPath(Dictionary<string, string> previousNodes, string start, string end) {
            var path = new List<string>();
            var current = end;
            while (current != start) {
                path.Add(current);
                current = previousNodes[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }

        // 检测两节点是否直接连通
        public bool CheckConnectivity(RoomData source, RoomData target)
        {
            if (_adjacencyList[source.index].Contains(target.index) && _adjacencyList[target.index].Contains(source.index))
            {
                return true;
            }
            return false;
        }

        // 阻断两个连通的节点
        public void BlockConnectivity(RoomData source, RoomData target)
        {
            if (!CheckConnectivity(source, target))
            {
                Debug.Log("Nodes are already disconnected.");
                return;
            }

            if (_adjacencyList[source.index].Contains(target.index))
            {
                _adjacencyList[source.index].Remove(target.index);
            }
            if (_adjacencyList[target.index].Contains(source.index))
            {
                _adjacencyList[target.index].Remove(source.index);
            }

            Debug.Log($"Blocked connectivity between {source.index} and {target.index}.");
        }

        // 连接两个节点
        public void OpenConnectivity(RoomData source, RoomData target)
        {
            if (CheckConnectivity(source, target))
            {
                Debug.Log("Nodes are already connected.");
                return;
            }

            if (!_adjacencyList[source.index].Contains(target.index))
            {
                _adjacencyList[source.index].Add(target.index);
            }
            if (!_adjacencyList[target.index].Contains(source.index))
            {
                _adjacencyList[target.index].Add(source.index);
            }

            Debug.Log($"Opened connectivity between {source.index} and {target.index}.");
        }
    }
}