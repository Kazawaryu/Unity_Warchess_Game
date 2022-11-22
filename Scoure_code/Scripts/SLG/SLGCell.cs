using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLGCell : MonoBehaviour
{

    [SerializeField]
    Color _targetColor;
    Color _originColor;

    public bool IsObstacle
    {
        get
        {

            if (_currentUnit != null || _currentItem != null)
            {
                return true;
            }

            return false;

        }
    }
    Material _selfMat;
    SLGUnit _currentUnit;
    SLGItem _currentItem;
    public SLGCell Parent;
    public int F;
    public int G;




    // Start is called before the first frame update
    void Start()
    {
        _selfMat = GetComponent<MeshRenderer>().material;
        _originColor = _selfMat.color;
    }

    public void SetUnit(SLGUnit unit)
    {
        _currentUnit = unit;
        _currentUnit.transform.position = transform.position;
    }

    public void SetItem(SLGItem item)
    {
        _currentItem = item;
        _currentItem.transform.position = transform.position;
    }

    public void Clean()
    {
        _currentItem = null;
        _currentUnit = null;
    }


    public void HighLight()
    {
        _selfMat.color = _targetColor;
    }

    public void BackToNormal()
    {
        _selfMat.color = _originColor;
    }


    public void Hide()
    {
        _selfMat.color = Color.clear;
    }




    // Update is called once per frame
    void Update()
    {

    }
}
