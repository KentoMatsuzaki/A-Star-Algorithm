using UnityEngine;

public class Node : MonoBehaviour
{
    /// <summary>�m�[�h�̏��</summary>
    enum eStatus
    {
        Normal,
        Open,
        Closed
    }

    /// <summary>�X�e�[�^�X</summary>
    eStatus _status = eStatus.Normal;

    /// <summary>�e�m�[�h</summary>
    Node _parent = null;

    /// <summary>����R�X�g</summary>
    int _estimatedCost = 0;

    /// <summary>���R�X�g</summary>
    int _actualCost = 0;

    public int ActualCost
    {
        get => _estimatedCost;
        set => _estimatedCost = value;
    }

    /// <summary>X���W</summary>
    int _x = 0;
    public int X => _x;

    /// <summary>Y���W</summary>
    int _y = 0;
    public int Y => _y;

    // �R���X�g���N�^
    public Node(int x, int y)
    {
        _x = x;
        _y = y;
    }

    /// <summary>����R�X�g���Z�o����</summary>
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

    /// <summary>�X�R�A���Z�o����</summary>
    /// <returns>����R�X�g�Ǝ��R�X�g�̍��v</returns>
    public int GetScore()
    {
        return _estimatedCost + _actualCost;
    }

    /// <summary>�m�[�h�̏�Ԃ�Open�ɂ��A�e��p�����[�^�[���X�V����</summary>
    public void OpenNode(Node parent, int actualCost)
    {
        // �X�e�[�^�X�̍X�V
        _status = eStatus.Normal;

        // �e�m�[�h���X�V
        _parent = parent;

        // ���R�X�g���X�V
        _actualCost = actualCost;
    }

    /// <summary>�m�[�h�̏�Ԃ�Closed�ɂ��A�e��p�����[�^�[���X�V����</summary>
    public void CloseNode()
    {
        // �X�e�[�^�X�̍X�V
        _status = eStatus.Closed;
    }
}
