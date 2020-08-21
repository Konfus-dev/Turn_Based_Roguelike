using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePathfinding : MonoBehaviour
{
    public Node StartNode;
    public Node TargetNode;
    public GridNodes grid;
    public Transform targetPosition;
    public ActorMovement movement;
    public bool myTurn;



    public Node FindStep(Vector3 a_MyPos, Vector3 a_TargetPos)
    {
        StartNode = grid.NodeFromPosition(a_MyPos);
        TargetNode = grid.NodeFromPosition(a_TargetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);
            
            if (CurrentNode == TargetNode)
            {
                List<Node> bestPath = GetFinalPath(StartNode, TargetNode);
                if (bestPath.Count > 0)
                {
                    return bestPath[0];
                }
                else
                {
                    return null;
                }
            }
            foreach (Node NeighborNode in grid.GetNeighboringNodes(CurrentNode))
            {
                if ((NeighborNode.occupied && !NeighborNode == TargetNode) || NeighborNode.traverseCost >= 100 || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                float MoveCost = CurrentNode.gCost + NeighborNode.traverseCost + GetManhattenDistance(CurrentNode, NeighborNode);
                if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }
        }
        return null;
    }

    List<Node> GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while (CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        FinalPath.Reverse();
        return FinalPath;
    }

    float GetManhattenDistance(Node nodeA, Node nodeB)
    {
        Vector3 a = nodeA.transform.localPosition;
        Vector3 b = nodeB.transform.localPosition;
        float x = Mathf.Abs(a.x - b.x);
        float y = Mathf.Abs(a.y - b.y);
        return x + y;
    }

}
