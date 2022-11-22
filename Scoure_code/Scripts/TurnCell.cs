using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cellStage
{
    Road,
    Fire,
    Water,
    Enemy,
    Team,
    obstrust
}


public class TurnCell : MonoBehaviour
{
    Material _selfMat;
    Color _originColor;
    [SerializeField]
    Color _highColor;
    public cellStage _currentStage;

    public int G;
    public int F;
    public TurnCell Parent;



    // Start is called before the first frame update
    void Start()
    {
        _selfMat = GetComponent<MeshRenderer>().material;
        _originColor = _selfMat.color;
    }

    public void Highlight()
    {
        _selfMat.color = _highColor;
    }

    public void normalLight()
    {
        _selfMat.color = _originColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
