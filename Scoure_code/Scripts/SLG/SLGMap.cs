using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HighlightSortEnum
{
    单个,
    十字,
    九宫格
}


public class SLGMap : MonoBehaviour
{
    GameObject _originBlue;
    List<SLGCell> _cellList;
    HighlightSortEnum _currentHighlightSort;
    public static SLGMap Instance;
    List<SLGCell> _openList;
    List<SLGCell> _closeList;
    List<SLGCell> _path;
    int col = 10;
    int row = 10;
    GameObject _originTree;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _originBlue = Resources.Load<GameObject>("Cell");
        _originTree = Resources.Load<GameObject>("Chair");
        _cellList = new List<SLGCell>();
        _openList = new List<SLGCell>();
        _closeList = new List<SLGCell>();
        _path = new List<SLGCell>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawMap();
        }
    }


    void DrawMap()
    {
        Vector3 startPos = transform.position - Vector3.right * col / 2 - Vector3.forward * row / 2 + Vector3.right * 0.5f + Vector3.forward * 0.5f;
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                GameObject cloneBlue = Instantiate(_originBlue);
                cloneBlue.transform.SetParent(transform);
                cloneBlue.transform.position = startPos + Vector3.right * i + Vector3.forward * j + Vector3.up * 0.01f + Vector3.right * i * 0.01f + Vector3.forward * j * 0.01f;
                var cell = cloneBlue.GetComponent<SLGCell>();
                _cellList.Add(cell);
                if (i == 3 && j == 4)
                {
                    GameObject cloneTree = Instantiate(_originTree);
                    cloneTree.transform.SetParent(transform);
                    cell.SetItem(cloneTree.GetComponent<SLGItem>());
                    
                }


            }
        }
    }


    public void Highlight(SLGCell cell)
    {
        switch (_currentHighlightSort)
        {
            case HighlightSortEnum.单个:
                {
                    ClearAll();
                    cell.HighLight();
                }
                break;
            case HighlightSortEnum.十字:
                break;
            case HighlightSortEnum.九宫格:
                break;
        }
    }

    void ClearAll()
    {
        for (int i = 0; i < _cellList.Count; i++)
        {
            _cellList[i].BackToNormal();
        }
    }



    public List<SLGCell> GeneratePath(SLGCell from, SLGCell to)
    {
        CleamPath();
        _openList.Add(from);
        while (_openList.Count > 0)
        {
            var workCell = _openList[0];
            if (workCell == to)
            {
                GetPath(_path, workCell);
                for (int i = 0; i < _path.Count; i++)
                {
                    _path[i].HighLight();
                }
                _path.Reverse();
                _path.RemoveAt(0);
                break;
            }
            else
            {
                _openList.RemoveAt(0);
                _closeList.Add(workCell);
                List<SLGCell> neighbour = FindNeighbour(workCell);
                for (int i = 0; i < neighbour.Count; i++)
                {
                    if (!_closeList.Contains(neighbour[i]) && !neighbour[i].IsObstacle)
                    {
                        if (!_openList.Contains(neighbour[i]))
                        {
                            neighbour[i].Parent = workCell;
                            int G = workCell.G + 1;
                            int H = CaculateH(neighbour[i], to);
                            int F = G + H;
                            neighbour[i].F = F;
                            neighbour[i].G = G;
                            neighbour[i].name = neighbour[i].F + ",G:" + G;
                            if (_openList.Count == 0)
                            {
                                _openList.Add(neighbour[i]);
                            }
                            else
                            {
                                if (neighbour[i].F < _openList[0].F)
                                {
                                    _openList.Insert(0, neighbour[i]);
                                }
                                else
                                {
                                    _openList.Add(neighbour[i]);
                                }

                            }
                        }
                        else
                        {
                            int G = workCell.G + 1;
                            int H = CaculateH(neighbour[i], to);
                            int F = G + H;
                            if (F < neighbour[i].F)
                            {
                                neighbour[i].Parent = workCell;
                                neighbour[i].F = F;
                                neighbour[i].G = G;
                                neighbour[i].name = neighbour[i].F + ",G:" + G;
                            }
                        }
                    }
                }

            }
        }
        return _path;
    }


    public void ShowCell(bool flag)
    {
        for (int i = 0; i < _cellList.Count; i++)
        {
            if (flag)
            {

                _cellList[i].Hide();
            }
            else
            {
                _cellList[i].BackToNormal();

            }
        }
    }




    void GetPath(List<SLGCell> path, SLGCell cell)
    {
        path.Add(cell);


        if (cell.Parent!=null)
        {
            GetPath(path, cell.Parent);
        }
    }



  public  void CleamPath()
    {
        for (int i = 0; i < _cellList.Count; i++)
        {
            _cellList[i].Parent = null;
            _cellList[i].F = 0;
            _cellList[i].G = 0;
            _cellList[i].BackToNormal();
        }
        _path.Clear();
        _openList.Clear();
        _closeList.Clear();
    }


   



    int CaculateH(SLGCell from,SLGCell to)
    {
        int fromIndex = _cellList.IndexOf(from);
        int toIndex = _cellList.IndexOf(to);
        int fromCol = fromIndex % col;
        int toCol = toIndex % col;
        int fromRow = fromIndex / row;
        int toRow = toIndex / row;
        int distance = Mathf.Abs(fromCol - toCol) + Mathf.Abs(fromRow - toRow);
        return distance;
    }


    List<SLGCell> FindNeighbour(SLGCell cell)
    {
        List<SLGCell> neighbourList = new List<SLGCell>();
        int cellIndex = _cellList.IndexOf(cell);
        if (cellIndex == 0)
        {
            if (!_cellList[cellIndex + 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + 1]);
            }
            if (!_cellList[cellIndex + col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + col]);
            }
        }
        else if (cellIndex == _cellList.Count - 1)
        {
            if (!_cellList[cellIndex - 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - 1]);
            }
            if (!_cellList[cellIndex - col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - col]);
            }
        }
        else if (cellIndex == col - 1)
        {
            if (!_cellList[cellIndex - 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - 1]);
            }
            if (!_cellList[cellIndex + col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + col]);
            }
        }
        else if (cellIndex == _cellList.Count - col)
        {
            if (!_cellList[cellIndex + 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + 1]);
            }
            if (!_cellList[cellIndex - col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - col]);
            }
        }
        else if (cellIndex / col == 0)
        {
            if (!_cellList[cellIndex - 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - 1]);
            }
            if (!_cellList[cellIndex + 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + 1]);
            }
            if (!_cellList[cellIndex + col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + col]);
            }
        }
        else if (cellIndex / col == row - 1)
        {
            if (!_cellList[cellIndex - 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - 1]);
            }
            if (!_cellList[cellIndex + 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + 1]);
            }
            if (!_cellList[cellIndex - col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - col]);
            }
        }
        else if (cellIndex % col == 0)
        {
            if (!_cellList[cellIndex + 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + 1]);
            }
            if (!_cellList[cellIndex + col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + col]);
            }
            if (!_cellList[cellIndex - col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - col]);
            }
        }
        else if (cellIndex % col == col - 1)
        {
            if (!_cellList[cellIndex - 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - 1]);
            }
            if (!_cellList[cellIndex + col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + col]);
            }
            if (!_cellList[cellIndex - col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - col]);
            }
        }
        else
        {
            if (!_cellList[cellIndex + 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + 1]);
            }
            if (!_cellList[cellIndex - 1].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - 1]);
            }
            if (!_cellList[cellIndex + col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex + col]);
            }
            if (!_cellList[cellIndex - col].IsObstacle)
            {
                neighbourList.Add(_cellList[cellIndex - col]);
            }
        }

        return neighbourList;
    }






}
