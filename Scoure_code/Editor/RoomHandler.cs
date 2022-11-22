using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RoomHandler : Editor
{

   



    [MenuItem("环境/初始化环境")]
    public static void CreateRoom()
    {
        GameObject[] _floorTiles = new GameObject[9];
        _floorTiles[0] = Resources.Load<GameObject>("Floor_Tile");
        _floorTiles[1] = Resources.Load<GameObject>("Floor_Tile_CornerDNLEFT");
        _floorTiles[2] = Resources.Load<GameObject>("Floor_Tile_CornerDNRIGHT");
        _floorTiles[3] = Resources.Load<GameObject>("Floor_Tile_CornerUPLEFT");
        _floorTiles[4] = Resources.Load<GameObject>("Floor_Tile_CornerUPRIGHT");
        _floorTiles[5] = Resources.Load<GameObject>("Floor_Tile_Straight_HorizDown");
        _floorTiles[6] = Resources.Load<GameObject>("Floor_Tile_Straight_HorizUp");
        _floorTiles[7] = Resources.Load<GameObject>("Floor_Tile_Straight_VerticLeft");
        _floorTiles[8] = Resources.Load<GameObject>("Floor_Tile_Straight_VerticRight");
        GameObject _wallTile = Resources.Load<GameObject>("Wall_Tile");
        GameObject _door = Resources.Load<GameObject>("Door");
        GameObject _Corner_Pillar = Resources.Load<GameObject>("Corner_Pillar");
        GameObject _Ceiling_Tile = Resources.Load<GameObject>("Ceiling_Tile");
        GameObject _Ceiling_Lamp = Resources.Load<GameObject>("Ceiling_Lamp");






        int col = 5;
        int row = 10;

        float _tileW = 3.3f;
        float _wallWidth = 0.2f;
        float _wallH = 3.4f;
        float _pillarW = 0.5f;
        float _ceilingW = 6;

        var room = GameObject.Find("Room");
        if (room!=null)
        {
            DestroyImmediate(room);
        }
        Transform _roomRoot = new GameObject("Room").transform;

       
        _roomRoot.position = Vector3.zero;

        for (int i = 1; i < col - 1; i++)
        {
            for (int j = 1; j < row - 1; j++)
            {
                GameObject cloneFloor = Instantiate(_floorTiles[0]);
                cloneFloor.transform.SetParent(_roomRoot);
                cloneFloor.transform.position = _roomRoot.position + Vector3.forward * _tileW / 2 + Vector3.right * _tileW / 2 + Vector3.forward * i * _tileW + Vector3.right * j * _tileW;
            }
        }


        for (int i = 0; i < row; i++)
        {
            GameObject cloneWall = Instantiate(_wallTile);
            cloneWall.transform.SetParent(_roomRoot);
            cloneWall.transform.position = _roomRoot.position + Vector3.right * i * _tileW + Vector3.right * _tileW / 2 + Vector3.forward * _wallWidth;


            GameObject cloneTile = null;



            if (i == 0)
            {
                cloneTile = Instantiate(_floorTiles[4]);
            }
            else if (i == row - 1)
            {
                cloneTile = Instantiate(_floorTiles[3]);
            }
            else
            {
                cloneTile = Instantiate(_floorTiles[6]);

            }

            cloneTile.transform.SetParent(_roomRoot);
            cloneTile.transform.position = _roomRoot.position + Vector3.forward * _tileW / 2 + Vector3.right * _tileW / 2 + Vector3.right * i * _tileW;


        }

        for (int i = 0; i < row; i++)
        {
            GameObject cloneWall = Instantiate(_wallTile);
            cloneWall.transform.SetParent(_roomRoot);
            cloneWall.transform.position = _roomRoot.position + Vector3.forward * col * _tileW + Vector3.right * i * _tileW + Vector3.right * _tileW / 2 - Vector3.forward * _wallWidth;
            cloneWall.transform.RotateAround(cloneWall.transform.position, Vector3.up, 180);

            GameObject cloneTile = null;

            if (i == 0)
            {
                cloneTile = Instantiate(_floorTiles[2]);
            }
            else if (i == row - 1)
            {
                cloneTile = Instantiate(_floorTiles[1]);
            }
            else
            {
                cloneTile = Instantiate(_floorTiles[5]);

            }

            cloneTile.transform.SetParent(_roomRoot);
            cloneTile.transform.position = _roomRoot.position + Vector3.forward * col * _tileW + Vector3.right * i * _tileW + Vector3.right * _tileW / 2 - Vector3.forward * _tileW / 2;


        }



        for (int i = 0; i < col; i++)
        {
            GameObject cloneWall = Instantiate(_wallTile);
            cloneWall.transform.SetParent(_roomRoot);
            cloneWall.transform.position = _roomRoot.position + Vector3.forward * i * _tileW + Vector3.forward * _tileW / 2 + Vector3.right * _wallWidth;
            cloneWall.transform.RotateAround(cloneWall.transform.position, Vector3.up, 90);

            if (i > 0 && i < col - 1)
            {
                GameObject cloneTile = Instantiate(_floorTiles[8]);
                cloneTile.transform.SetParent(_roomRoot);
                cloneTile.transform.position = _roomRoot.position + Vector3.forward * i * _tileW + Vector3.forward * _tileW / 2 + Vector3.right * _tileW / 2;

            }

        }

        for (int i = 0; i < col; i++)
        {
            if (i == col / 2)
            {
                GameObject cloneDoor = Instantiate(_door);
                cloneDoor.transform.SetParent(_roomRoot);
                cloneDoor.transform.position = _roomRoot.position + Vector3.forward * i * _tileW + Vector3.right * row * _tileW + Vector3.forward * _tileW / 2 - Vector3.right * _wallWidth;
                cloneDoor.transform.RotateAround(cloneDoor.transform.position, Vector3.up, -90);
            }
            else
            {

                GameObject cloneWall = Instantiate(_wallTile);
                cloneWall.transform.SetParent(_roomRoot);
                cloneWall.transform.position = _roomRoot.position + Vector3.forward * i * _tileW + Vector3.right * row * _tileW + Vector3.forward * _tileW / 2 - Vector3.right * _wallWidth;
                cloneWall.transform.RotateAround(cloneWall.transform.position, Vector3.up, -90);
            }


            if (i > 0 && i < col - 1)
            {
                GameObject cloneTile = Instantiate(_floorTiles[7]);
                cloneTile.transform.SetParent(_roomRoot);
                cloneTile.transform.position = _roomRoot.position + Vector3.forward * i * _tileW + Vector3.right * row * _tileW + Vector3.forward * _tileW / 2 - Vector3.right * _tileW / 2;
            }





        }




        GameObject clone = Instantiate(_Corner_Pillar);
        clone.transform.SetParent(_roomRoot);
        clone.transform.position = _roomRoot.position + Vector3.right * _pillarW + Vector3.forward * _pillarW;
        clone.transform.RotateAround(clone.transform.position, Vector3.up, 90);
        clone = Instantiate(_Corner_Pillar);
        clone.transform.SetParent(_roomRoot);
        clone.transform.position = _roomRoot.position + Vector3.right * _pillarW + Vector3.forward * col * _tileW - Vector3.forward * _pillarW;
        clone.transform.RotateAround(clone.transform.position, Vector3.up, 180);
        clone = Instantiate(_Corner_Pillar);
        clone.transform.SetParent(_roomRoot);
        clone.transform.position = _roomRoot.position - Vector3.right * _pillarW + Vector3.right * row * _tileW + Vector3.forward * _pillarW;
        clone = Instantiate(_Corner_Pillar);
        clone.transform.SetParent(_roomRoot);
        clone.transform.position = _roomRoot.position - Vector3.right * _pillarW - Vector3.forward * _pillarW + Vector3.right * row * _tileW + Vector3.forward * col * _tileW;
        clone.transform.RotateAround(clone.transform.position, Vector3.up, -90);


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
            {


                if (i==1&&j==3)
                {
                    GameObject cloneCeiling = Instantiate(_Ceiling_Lamp);
                    cloneCeiling.transform.SetParent(_roomRoot);
                    cloneCeiling.transform.position = _roomRoot.position + Vector3.forward * _ceilingW / 2 + Vector3.right * _ceilingW / 2 + Vector3.up * 2.8f + Vector3.forward * i * _ceilingW + Vector3.right * j * _ceilingW;

                }
                else
                {
                    GameObject cloneCeiling = Instantiate(_Ceiling_Tile);
                    cloneCeiling.transform.SetParent(_roomRoot);
                    cloneCeiling.transform.position = _roomRoot.position + Vector3.forward * _ceilingW / 2 + Vector3.right * _ceilingW / 2 + Vector3.up * _wallH + Vector3.forward * i * _ceilingW + Vector3.right * j * _ceilingW;

                }




            }
        }






    }

}


   
