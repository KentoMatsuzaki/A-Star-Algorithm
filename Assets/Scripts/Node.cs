using UnityEngine;

public class Node : MonoBehaviour
{
    /// <summary>ノードの状態</summary>
    enum eStatus
    {
        Normal,
        Open,
        Closed
    }

    /// <summary>ステータス</summary>
    eStatus _status = eStatus.Normal;

    /// <summary>親ノード</summary>
    Node _parent = null;

    /// <summary>推定コスト</summary>
    int _estimatedCost = 0;

    /// <summary>実コスト</summary>
    int _actualCost = 0;

    public int ActualCost
    {
        get => _estimatedCost;
        set => _estimatedCost = value;
    }

    /// <summary>X座標</summary>
    int _x = 0;
    public int X => _x;

    /// <summary>Y座標</summary>
    int _y = 0;
    public int Y => _y;

    // コンストラクタ
    public Node(int x, int y)
    {
        _x = x;
        _y = y;
    }

    /// <summary>推定コストを算出する</summary>
    /// <param name="diagonal">斜め移動の可否</param>
    /// <param name="goal_x">ゴールのX座標</param>
    /// <param name="goal_y">ゴールのY座標</param>
    /// <returns>推定コスト</returns>
    public void GetEstimatedScore(bool diagonal, int goal_x, int goal_y)
    {
        // 斜め移動が可能な場合
        if (diagonal)
        {
            // X座標とY座標の差を求める
            int deltaX = Mathf.Abs(goal_x - _x);
            int deltaY = Mathf.Abs(goal_y - _y);

            // 求めた差の大きい方を推定コストにする
            _estimatedCost = deltaX > deltaY ? deltaX : deltaY;
        }
        // 縦横移動のみの場合
        else
        {
            // X座標とY座標の差を求める
            int deltaX = Mathf.Abs(goal_x - _x);
            int deltaY = Mathf.Abs(goal_y - _y);

            // 求めた差の合計を推定コストにする
            _estimatedCost = deltaX + deltaY;
        }
    }

    /// <summary>スコアを算出する</summary>
    /// <returns>推定コストと実コストの合計</returns>
    public int GetScore()
    {
        return _estimatedCost + _actualCost;
    }

    /// <summary>ノードの状態をOpenにし、各種パラメーターを更新する</summary>
    public void OpenNode(Node parent, int actualCost)
    {
        // ステータスの更新
        _status = eStatus.Normal;

        // 親ノードを更新
        _parent = parent;

        // 実コストを更新
        _actualCost = actualCost;
    }

    /// <summary>ノードの状態をClosedにし、各種パラメーターを更新する</summary>
    public void CloseNode()
    {
        // ステータスの更新
        _status = eStatus.Closed;
    }
}
