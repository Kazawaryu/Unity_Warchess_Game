using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cellStage
{
    Road,
    Enemy,
    Player,
    obstrust
}

public enum cellPawnStage
{
    
}


public class TurnCell : MonoBehaviour
{
    public Material _selfMat { get; set; }
    public Color _originColor;
    [SerializeField]
    public Color _highColor;
    public cellStage _currentStage { get;  set; }

    public int G;
    public int F;
    public TurnCell Parent;

    public GameObject _cellObj = null;
    public bool ifVisable = false;


    // Start is called before the first frame update
    void Start()
    {
        _selfMat = GetComponent<MeshRenderer>().material;
        _originColor = _selfMat.color;
    }

    public void Highlight()
    {
        if (ifVisable)
            _selfMat.color = _highColor;
    }

    public void normalLight()
    {
        if (ifVisable)
            _selfMat.color = _originColor;
    }

    public void enemyLight()
    {
        if (ifVisable)
            _selfMat.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transStateToPlayer()
    {
        _currentStage = cellStage.Player;
    }

    public void transStateToEnemy()
    {
        _currentStage = cellStage.Enemy;
    }

    public void transStateToObstrust()
    {
        _currentStage = cellStage.obstrust;
    }

    public void transStateToRoad()
    {
        _currentStage = cellStage.Road;
    }
}
