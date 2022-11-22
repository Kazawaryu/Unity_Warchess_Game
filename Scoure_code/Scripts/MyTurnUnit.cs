using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnUnit : MonoBehaviour
{
    enum StateEnum
    {
        free_state,
        preSelected_state,
        Selected_state
    }


    StateEnum _currentState;
    public TurnCell _currentCell;
    Outline _selfOut;

    // Start is called before the first frame update
    void Start()
    {
        _selfOut = GetComponent<Outline>();
    }


    public void PreSelect()
    {
        if (_currentState == StateEnum.Selected_state)
            return;
        _selfOut.enabled = true;
        _selfOut.OutlineColor = Color.white;
        _currentState = StateEnum.preSelected_state;
    }

    public void Selected()
    {
        

        _selfOut.OutlineColor = Color.green;
        _currentState = StateEnum.Selected_state;
    }

    public void DisSelect()
    {
        if (_currentState == StateEnum.Selected_state)
            return;
        _selfOut.enabled = false;
        _currentState = StateEnum.free_state;
    }

    public void DirDisSelect()
    {
        _selfOut.enabled = false;
        _currentState = StateEnum.free_state;
    }

    public void SetCell(TurnCell cell)
    {
        _currentCell = cell;
        transform.position = cell.transform.position;
    }

    public void smoothMove(List<TurnCell> path)
    {
        StartCoroutine(MoveCor(path));
    }


    IEnumerator MoveCor(List<TurnCell> path)
    {
        _currentCell.normalLight();
        while (path.Count>0)
        {
            float workTime = 0;
            Vector3 begin_pos = transform.position;
            Vector3 end_pos = path[0].transform.position;
            while (true)
            {
                workTime += Time.deltaTime * 2;
                transform.position = Vector3.Lerp(begin_pos, end_pos, workTime);


                if (workTime >= 1)
                {
                    _currentCell = path[0];
                    path[0].normalLight();

                    path.RemoveAt(0);
                    break;
                }
                yield return null;
            }

        }


    // Update is called once per frame
    void Update()
    {
        
    }
}
}
