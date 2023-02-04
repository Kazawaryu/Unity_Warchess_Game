using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WetlandBeginner : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        GameObject _gridPoint = GameObject.Find("Grids");
        _gridPoint.AddComponent<MyTurn>();
        MyTurn turn = _gridPoint.GetComponent<MyTurn>();
        turn.WetlandBegin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
