using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Node : MonoBehaviour
{
    /// <summary>ノードの状態</summary>
    enum eStatus
    {
        Normal,
        Open,
        Closed
    }

    // ステータス
    eStatus _status = eStatus.Normal;

    // 親ノード
    Node _parent = null;

    // 推定コスト
    int _estimatedCost = 0;

    // 実コスト
    int _actualCost = 0;

    public int ActualCost
    {
        get => _estimatedCost;
        set => _estimatedCost = value;
    }

    // X座標
    int _x = 0;
    public int X => _x;

    // Y座標
    int _y = 0;
    public int Y => _y;

    // コンストラクタ
    public Node(int x, int y)
    {
        _x = x;
        _y = y;
    }

    /// <summary>推定コストを算出するメソッド</summary>
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

    /// <summary>スコアを算出するメソッド</summary>
    /// <returns>推定コストと実コストの合計</returns>
    public int GetScore()
    {
        return _estimatedCost + _actualCost;
    }
}
