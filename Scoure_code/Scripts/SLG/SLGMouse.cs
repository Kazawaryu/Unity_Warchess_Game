using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateSortEnum
{
    放置角色,
    选取角色,
    移动角色,

}




public class SLGMouse : MonoBehaviour
{
    Transform _currentTarget;
    StateSortEnum _currentState;
    SLGUnit _currentSelectUnit;
    SLGUnit _currentReadyUnit;
    GameObject _originUnit;
    public static SLGMouse Instance;
    GameObject _originGirl;


    List<GameObject> _unitList;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _unitList = new List<GameObject>();
        _originUnit = Resources.Load<GameObject>("Unit");
        _originGirl = Resources.Load<GameObject>("Girl");
       
        _unitList.Add(_originUnit);
        _unitList.Add(_originGirl);

        Debug.Log(_unitList[0]);
        Debug.Log(_unitList[1]);

    }

    // Update is called once per frame
    void Update()
    {
        MouseDetect();

        MouseInput();


    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentTarget)
            {
                switch (_currentState)
                {
                    case StateSortEnum.放置角色:
                        {
                            SLGCell cell = _currentTarget.GetComponent<SLGCell>();
                            if (cell )
                            {

                                if (_unitList.Count>0)
                                {
                                    var clone = Instantiate(_unitList[0]);
                                    clone.GetComponent<SLGUnit>().SetCell(cell);
                                    _unitList.RemoveAt(0);
                                    if (_unitList.Count==0)
                                    {
                                        _currentState = StateSortEnum.选取角色;
                                    }

                                }

                            }
                        }
                        break;
                    case StateSortEnum.选取角色:
                        {
                            SLGCell cell = _currentTarget.GetComponent<SLGCell>();
                            if (cell && _currentSelectUnit)
                            {
                                var path = SLGMap.Instance.GeneratePath(_currentSelectUnit.StandingCell, cell);
                                _currentSelectUnit.MoveTo(path);
                                _currentSelectUnit.Idle(true);
                                _currentSelectUnit = null;
                                _currentState = StateSortEnum.移动角色;
                               
                            }
                            else
                            {
                                SLGUnit unit = _currentTarget.GetComponent<SLGUnit>();
                                if (unit)
                                {
                                    _currentSelectUnit = unit;
                                    _currentSelectUnit.Select();
                                }
                            }
                        }
                        break;
                    case StateSortEnum.移动角色:
                        break;

                }



              

            }
        }

    }



    public void SwitchState(StateSortEnum targetState)
    {
        _currentState = targetState;
    }




    private void MouseDetect()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            _currentTarget = hitInfo.transform;

            SLGCell cell = hitInfo.transform.GetComponent<SLGCell>();
            if (cell!=null)
            {
                if (_currentSelectUnit!=null&&_currentState!= StateSortEnum.移动角色)
                {
                    SLGMap.Instance.GeneratePath(_currentSelectUnit.StandingCell, cell);
                }
                else
                {
                    if (_currentState == StateSortEnum.放置角色)
                    {
                        SLGMap.Instance.Highlight(cell);
                    }
                }

            }

            SLGUnit unit = hitInfo.transform.GetComponent<SLGUnit>();
            if (unit!=null)
            {
                _currentReadyUnit = unit;
                unit.PreSelect();
            }
            else
            {
                if (_currentReadyUnit!=null)
                {
                    _currentReadyUnit.Idle();
                    _currentReadyUnit = null;
                }
            }
        }
        else
        {
            if (_currentReadyUnit != null)
            {
                _currentReadyUnit.Idle();
                _currentReadyUnit = null;
            }
        }
    }
}
