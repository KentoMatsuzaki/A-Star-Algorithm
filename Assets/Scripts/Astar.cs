using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    /// <summary>斜め移動の可否</summary>
    bool _diagonal = false;

    /// <summary>オープンリスト</summary>
    List<Node> _openList = null;

    void Start()
    {
        
    }
}
