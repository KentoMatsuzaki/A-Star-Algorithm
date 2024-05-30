using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    /// <summary>�΂߈ړ��̉�</summary>
    bool _diagonal = false;

    /// <summary>�I�[�v�����X�g</summary>
    List<Node> _openList = null;

    /// <summary>�S�[�����W</summary>
    int _goal_x = 0;
    int _goal_y = 0;

    void Start()
    {
        
    }

    /// <summary>�m�[�h�̃C���X�^���X�𐶐����Đ���R�X�g��ݒ肷��</summary>
    /// <param name="x">X���W</param>
    /// <param name="y">y���W</param>
    Node GetNode(int x, int y)
    {
        // �m�[�h���C���X�^���X��
        var node = new Node(x, y);

        // ����R�X�g���v�Z
        node.SetEstimatedScore(_diagonal, _goal_x, _goal_y);

        // �m�[�h��Ԃ�
        return node;
    }

    /// <summary>���W���w�肵�ăm�[�h���J������ɕԂ�</summary>
    Node OpenTargetNode(int x, int y, int actualCost, Node parent)
    {
        // �w�肵�����W�ɂ���m�[�h�̃C���X�^���X�𐶐�
        var node = GetNode(x, y);

        // �m�[�h���J���Ȃ��ꍇ�͉����Ԃ��Ȃ�
        if(node.IsNormal() == false)
        {
            return null;
        }

        // �m�[�h���J��
        node.GetOpend(parent, actualCost);

        // �I�[�v�����X�g�ɒǉ�����
        AddToOpenList(node);

        // �m�[�h��Ԃ�
        return node;
    }

    /// <summary>���͂̃m�[�h���J��</summary>
    void OpenNodeAround(Node parent)
    {
        // �e�m�[�h�̍��W����Ƃ���
        var standard_x = parent.X;
        var standard_y = parent.Y;

        // �e�m�[�h�̎��R�X�g���󂯎��
        var actualCost = parent.ActualCost;
        actualCost++;

        // �΂߈ړ����\�ȏꍇ
        if(_diagonal)
        {
            // �e�m�[�h�𒆐S�Ƃ����㉺���E9�}�X���J��
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
            // �e�m�[�h�𒆐S�Ƃ����㉺���E4�}�X���J��
            OpenTargetNode(standard_x, standard_y + 1, actualCost, parent);
            OpenTargetNode(standard_x, standard_y - 1, actualCost, parent);
            OpenTargetNode(standard_x - 1, standard_y, actualCost, parent); 
            OpenTargetNode(standard_x + 1, standard_y, actualCost, parent); 
        }
    }

    /// <summary>�m�[�h���I�[�v�����X�g�ɒǉ�����</summary>
    void AddToOpenList(Node node)
    {
        _openList.Add(node);
    }

    /// <summary>�m�[�h���I�[�v�����X�g����폜����</summary>
    void RemoveFromOpenList(Node node)
    {
        _openList.Remove(node);
    }

    /// <summary>�I�[�v�����X�g����ŏ��X�R�A�̃m�[�h��Ԃ�</summary>
    Node GetMinScoreNode()
    {
        // �ŏ��l
        int minScore = int.MaxValue;
        int minActualCost = int.MaxValue;

        // �ŏ��X�R�A�̃m�[�h
        Node minNode = null;

        foreach(Node currentNode in _openList)
        {
            // ���݂̃m�[�h�̃X�R�A
            int score = currentNode.GetScore();

            // �X�R�A���ŏ��X�R�A��荂���ꍇ�͔�����
            if(score > minScore)
            {
                continue;
            }

            // �X�R�A���ŏ��X�R�A�Ɠ����ꍇ�́A���R�X�g����r����i�����̂��߁j
            if(score == minScore && currentNode.ActualCost >= minActualCost)
            {
                continue;
            }

            // �ŏ��l�ƃm�[�h���X�V
            minScore = score;
            minActualCost = currentNode.ActualCost;
            minNode = currentNode;
        }

        return minNode;
    }
}
