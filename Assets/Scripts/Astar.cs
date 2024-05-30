using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    /// <summary>斜め移動の可否</summary>
    bool _diagonal = false;

    /// <summary>オープンリスト</summary>
    List<Node> _openList = null;

    /// <summary>ゴール座標</summary>
    int _goal_x = 0;
    int _goal_y = 0;

    void Start()
    {
        
    }

    /// <summary>ノードのインスタンスを生成して推定コストを設定する</summary>
    /// <param name="x">X座標</param>
    /// <param name="y">y座標</param>
    Node GetNode(int x, int y)
    {
        // ノードをインスタンス化
        var node = new Node(x, y);

        // 推定コストを計算
        node.SetEstimatedScore(_diagonal, _goal_x, _goal_y);

        // ノードを返す
        return node;
    }

    /// <summary>座標を指定してノードを開いた後に返す</summary>
    Node OpenTargetNode(int x, int y, int actualCost, Node parent)
    {
        // 指定した座標にあるノードのインスタンスを生成
        var node = GetNode(x, y);

        // ノードを開けない場合は何も返さない
        if(node.IsNormal() == false)
        {
            return null;
        }

        // ノードを開く
        node.GetOpend(parent, actualCost);

        // オープンリストに追加する
        AddToOpenList(node);

        // ノードを返す
        return node;
    }

    /// <summary>周囲のノードを開く</summary>
    void OpenNodeAround(Node parent)
    {
        // 親ノードの座標を基準とする
        var standard_x = parent.X;
        var standard_y = parent.Y;

        // 親ノードの実コストを受け取る
        var actualCost = parent.ActualCost;
        actualCost++;

        // 斜め移動が可能な場合
        if(_diagonal)
        {
            // 親ノードを中心とした上下左右9マスを開く
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    var x = standard_x + j - 1;
                    var y = standard_y + i - 1;
                    OpenTargetNode(x, y, actualCost, parent);
                }
            }
        }
        else
        {
            // 親ノードを中心とした上下左右4マスを開く
            OpenTargetNode(standard_x, standard_y + 1, actualCost, parent);
            OpenTargetNode(standard_x, standard_y - 1, actualCost, parent);
            OpenTargetNode(standard_x - 1, standard_y, actualCost, parent); 
            OpenTargetNode(standard_x + 1, standard_y, actualCost, parent); 
        }
    }

    /// <summary>ノードをオープンリストに追加する</summary>
    void AddToOpenList(Node node)
    {
        _openList.Add(node);
    }

    /// <summary>ノードをオープンリストから削除する</summary>
    void RemoveFromOpenList(Node node)
    {
        _openList.Remove(node);
    }

    /// <summary>オープンリストから最小スコアのノードを返す</summary>
    Node GetMinScoreNode()
    {
        // 最小値
        int minScore = int.MaxValue;
        int minActualCost = int.MaxValue;

        // 最小スコアのノード
        Node minNode = null;

        foreach(Node currentNode in _openList)
        {
            // 現在のノードのスコア
            int score = currentNode.GetScore();

            // スコアが最少スコアより高い場合は抜ける
            if(score > minScore)
            {
                continue;
            }

            // スコアが最少スコアと同じ場合は、実コストも比較する（効率のため）
            if(score == minScore && currentNode.ActualCost >= minActualCost)
            {
                continue;
            }

            // 最小値とノードを更新
            minScore = score;
            minActualCost = currentNode.ActualCost;
            minNode = currentNode;
        }

        return minNode;
    }
}
