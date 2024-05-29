using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Node : MonoBehaviour
{
    /// <summary>�m�[�h�̏��</summary>
    enum eStatus
    {
        Normal,
        Open,
        Closed
    }

    // �X�e�[�^�X
    eStatus _status = eStatus.Normal;

    // �e�m�[�h
    Node _parent = null;

    // ����R�X�g
    int _estimatedCost = 0;

    // ���R�X�g
    int _actualCost = 0;

    public int ActualCost
    {
        get => _estimatedCost;
        set => _estimatedCost = value;
    }

    // X���W
    int _x = 0;
    public int X => _x;

    // Y���W
    int _y = 0;
    public int Y => _y;

    // �R���X�g���N�^
    public Node(int x, int y)
    {
        _x = x;
        _y = y;
    }

    /// <summary>����R�X�g���Z�o���郁�\�b�h</summary>
    /// <param name="diagonal">�΂߈ړ��̉�</param>
    /// <param name="goal_x">�S�[����X���W</param>
    /// <param name="goal_y">�S�[����Y���W</param>
    /// <returns>����R�X�g</returns>
    public void GetEstimatedScore(bool diagonal, int goal_x, int goal_y)
    {
        // �΂߈ړ����\�ȏꍇ
        if (diagonal)
        {
            // X���W��Y���W�̍������߂�
            int deltaX = Mathf.Abs(goal_x - _x);
            int deltaY = Mathf.Abs(goal_y - _y);

            // ���߂����̑傫�����𐄒�R�X�g�ɂ���
            _estimatedCost = deltaX > deltaY ? deltaX : deltaY;
        }
        // �c���ړ��݂̂̏ꍇ
        else
        {
            // X���W��Y���W�̍������߂�
            int deltaX = Mathf.Abs(goal_x - _x);
            int deltaY = Mathf.Abs(goal_y - _y);

            // ���߂����̍��v�𐄒�R�X�g�ɂ���
            _estimatedCost = deltaX + deltaY;
        }
    }

    /// <summary>�X�R�A���Z�o���郁�\�b�h</summary>
    /// <returns>����R�X�g�Ǝ��R�X�g�̍��v</returns>
    public int GetScore()
    {
        return _estimatedCost + _actualCost;
    }
}
