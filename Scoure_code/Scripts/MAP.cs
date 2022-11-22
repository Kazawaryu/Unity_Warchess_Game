using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAP : MonoBehaviour
{
    int row = 10;
    int col = 10;
    float _cellW = 1f;
    public static MAP Instance;
    List<TurnCell> _cellList;
    List<TurnCell> _openList;
    List<TurnCell> _closeList;
    public List<TurnCell> _path{ get; private set; }



    void Start()
    {
        Instance = this;
        _openList = new List<TurnCell>();
        _closeList = new List<TurnCell>();
        _path = new List<TurnCell>();
        _cellList = new List<TurnCell>();


        GameObject _originGridCell = Resources.Load<GameObject>("GridCell");
        GameObject _orginChair = Resources.Load<GameObject>("Chair");
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject cloneGridCell = Instantiate(_originGridCell);
                cloneGridCell.transform.SetParent(transform);
                cloneGridCell.transform.position = transform.position + 
                    Vector3.right * j * _cellW + Vector3.forward * i * _cellW 
                    + Vector3.right * _cellW/2 +Vector3.forward * _cellW/2
                    + Vector3.forward * 0.01f * i + Vector3.right * 0.01f * j;

                
                var cell = cloneGridCell.GetComponent<TurnCell>();
                cell._currentStage = cellStage.Road;
                if (i == 3 && j == 3)
                {
                    GameObject cloneChair = Instantiate(_orginChair);
                    cloneChair.transform.SetParent(transform);
                    cloneChair.transform.position = cell.transform.position;

                    cell._currentStage = cellStage.obstrust;
                }
                _cellList.Add(cell);
                
                

            }
        }

    }



    public void Astar(TurnCell from, TurnCell to)
    {
        Debug.Log("Now A star");
        clearPath();
        _openList.Add(from);
        
        while (_openList.Count > 0)
        {
            
            
            var workCell = _openList[0];
            if (workCell == to)
            {
                Debug.Log("Find the path!" + _path.Count);
                getPath(to);
                for (int i = 0; i < _path.Count; i++)
                {
                    _path[i].Highlight();
                }
                _path.Reverse();
                _path.RemoveAt(0);
                return;
            }
            else
            {
                _openList.RemoveAt(0);
                _closeList.Add(workCell);
                var neis = FindNei(workCell);

                for (int i = 0; i < neis.Count; i++)
                {
                    if (_closeList.Contains(neis[i]) || neis[i]._currentStage == cellStage.obstrust)
                    { 
                        continue;
                    }

                    if (!_openList.Contains(neis[i]))
                    {
                        int G = workCell.G + 1;
                        int H = calH(neis[i], to);
                        int F = G + H;
                        
                        neis[i].Parent = workCell;
                        if (_openList.Count == 0)
                        {
                            _openList.Add(neis[i]);
                        }
                        else {
                            if (F < _openList[0].F)
                            {
                                
                                _openList.Insert(0, neis[i]);
                            }
                            else
                            {
                                _openList.Add(neis[i]);
                            }
                        }
                    }
                    else
                    {
                        int G = workCell.G + 1;
                        int H = calH(neis[i], to);
                        int F = G + H;
                        if(F< neis[i].F)
                        {
                            neis[i].F = F;
                            neis[i].G = G;
                            neis[i].Parent = workCell;
                        }

                    }
                }

            }
        }
    }

    void getPath(TurnCell to)
    {
        _path.Add(to);
        if (to.Parent != null)
        {
            getPath(to.Parent);
        }
    }

    int calH(TurnCell from, TurnCell to)
    {
        int fidx = _cellList.IndexOf(from);
        int tidx = _cellList.IndexOf(to);
        Debug.Log(fidx + " " +tidx+" " +Mathf.Abs(fidx / col - tidx / col) + Mathf.Abs(fidx % col - tidx % col));
        return Mathf.Abs(fidx / col - tidx / col) + Mathf.Abs(fidx % col - tidx % col);
    }


    List<TurnCell> FindNei(TurnCell cell)
    {
        List<TurnCell> neis = new List<TurnCell>();
        var index = _cellList.IndexOf(cell);
        if (index == 0)
        {
            neis.Add(_cellList[index + 1]);
            neis.Add(_cellList[index + col]);
        }else if (index == col - 1)
        {
            neis.Add(_cellList[index - 1]);
            neis.Add(_cellList[index + col]);
        }else if(index == _cellList.Count - 1)
        {
            neis.Add(_cellList[index - 1]);
            neis.Add(_cellList[index - col]);
        }else if(index == _cellList.Count - col)
        {
            neis.Add(_cellList[index + 1]);
            neis.Add(_cellList[index - col]);
        }else if (index / col == 0)
        {
            neis.Add(_cellList[index + 1]);
            neis.Add(_cellList[index - 1]);
            neis.Add(_cellList[index + col]);
        }else if (index / col == row - 1)
        {
            neis.Add(_cellList[index + 1]);
            neis.Add(_cellList[index - 1]);
            neis.Add(_cellList[index - col]);
        }else if (index % col == 0)
        {
            neis.Add(_cellList[index + 1]);
            neis.Add(_cellList[index + col]);
            neis.Add(_cellList[index - col]);
        }else if (index % col == col - 1)
        {
            neis.Add(_cellList[index - 1]);
            neis.Add(_cellList[index + col]);
            neis.Add(_cellList[index - col]);
        }else
        {
            neis.Add(_cellList[index + 1]);
            neis.Add(_cellList[index - 1]);
            neis.Add(_cellList[index + col]);
            neis.Add(_cellList[index - col]);
        }

        return neis;
    }
    void clearPath()
    {

        for (int i = 0; i < _cellList.Count; i++)
        {
            _cellList[i].Parent = null;
            _cellList[i].F = 0;
            _cellList[i].G = 0;
            _cellList[i].normalLight();
        }

        _openList.Clear();
        _closeList.Clear();
        _path.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
