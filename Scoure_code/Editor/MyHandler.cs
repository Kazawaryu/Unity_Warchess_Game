using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyHandler : Editor
{
    [MenuItem("Room/CreateRoom")]
    public static void createRoom()
    {

        int col = 5;
        int row = 5;
        float _floorW = 3.3f;
        float _wallW = 3.3f;
        float _wallH = 3.3f;
        float _wallBias = 0.25f;
        float _pilliarW = 0.5f;
        float _ceilingW = 6f;
        float ceilingLampBias = 0.7f;

        GameObject room = GameObject.Find("Room");
        if (room != null)
        {
            DestroyImmediate(room);
        }

        Debug.Log("Now Create Room");
        Transform _roomRoot = new GameObject("Room").transform;
        _roomRoot.position = Vector3.zero;

        GameObject _originFloorTile = Resources.Load<GameObject>("Floor_Tile");

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                GameObject cloneFloorTile = Instantiate(_originFloorTile);
                cloneFloorTile.transform.SetParent(_roomRoot);
                cloneFloorTile.transform.position = _roomRoot.position + Vector3.right * _floorW / 2 + Vector3.forward * _floorW / 2 + Vector3.right * i * _floorW + Vector3.forward * j * _floorW;
            }
        }

        GameObject _originWall = Resources.Load<GameObject>("Wall_Tile");

        for (int i = 0; i < col; i++)
        {
            GameObject cloneWall = Instantiate(_originWall);
            cloneWall.transform.SetParent(_roomRoot);
            cloneWall.transform.position = _roomRoot.position + Vector3.right * _wallW / 2 + Vector3.forward * _wallBias + Vector3.right * i * _wallW;
        }

        GameObject _originDoor = Resources.Load<GameObject>("Door");
        for (int i = 0; i < col; i++)
        {
            if (i == col / 2)
            {
                GameObject cloneDoor = Instantiate(_originDoor);
                cloneDoor.transform.SetParent(_roomRoot);
                cloneDoor.transform.position = _roomRoot.position + Vector3.right * _wallW / 2 - Vector3.forward * _wallBias + Vector3.forward * 0.040f + Vector3.right * i * _wallW + Vector3.forward * row * _floorW;
                cloneDoor.transform.RotateAround(cloneDoor.transform.position, Vector3.up, 180);
            }
            else
            {
                GameObject cloneWall = Instantiate(_originWall);
                cloneWall.transform.SetParent(_roomRoot);
                cloneWall.transform.position = _roomRoot.position + Vector3.right * _wallW / 2 - Vector3.forward * _wallBias + Vector3.right * i * _wallW + Vector3.forward * row * _floorW;
                cloneWall.transform.RotateAround(cloneWall.transform.position, Vector3.up, 180);
            }
        }

        for (int i = 0; i < row; i++)
        {
            GameObject cloneWall = Instantiate(_originWall);
            cloneWall.transform.SetParent(_roomRoot);
            cloneWall.transform.position = _roomRoot.position + Vector3.right * _wallBias + Vector3.forward * i * _wallW + Vector3.forward * _wallW / 2;
            cloneWall.transform.RotateAround(cloneWall.transform.position, Vector3.up, 90);
        }

        for (int i = 0; i < row; i++)
        {
            GameObject cloneWall = Instantiate(_originWall);
            cloneWall.transform.SetParent(_roomRoot);
            cloneWall.transform.position = _roomRoot.position + Vector3.right * col * _wallW - Vector3.right * _wallBias + Vector3.forward * i * _wallW + Vector3.forward * _wallW / 2;
            cloneWall.transform.RotateAround(cloneWall.transform.position, Vector3.up, -90);
        }


        GameObject _originPilliar = Resources.Load<GameObject>("Corner_Pillar");
        GameObject clonePillar = Instantiate(_originPilliar);
        clonePillar.transform.SetParent(_roomRoot);
        clonePillar.transform.position = _roomRoot.position + Vector3.right * _pilliarW + Vector3.forward * _pilliarW;
        clonePillar.transform.RotateAround(clonePillar.transform.position, Vector3.up, 90);

        clonePillar = Instantiate(_originPilliar);
        clonePillar.transform.SetParent(_roomRoot);
        clonePillar.transform.position = _roomRoot.position + Vector3.right * _pilliarW - Vector3.forward * _pilliarW + Vector3.forward * row * _wallW;
        clonePillar.transform.RotateAround(clonePillar.transform.position, Vector3.up, 180);

        clonePillar = Instantiate(_originPilliar);
        clonePillar.transform.SetParent(_roomRoot);
        clonePillar.transform.position = _roomRoot.position - Vector3.right * _pilliarW + Vector3.forward * _pilliarW + Vector3.right * col * _wallW;

        clonePillar = Instantiate(_originPilliar);
        clonePillar.transform.SetParent(_roomRoot);
        clonePillar.transform.position = _roomRoot.position - Vector3.right * _pilliarW - Vector3.forward * _pilliarW + Vector3.forward * row * _wallW + Vector3.right * col * _wallW;
        clonePillar.transform.RotateAround(clonePillar.transform.position, Vector3.up, -90);

        GameObject _originCeiling = Resources.Load<GameObject>("Ceiling_Tile");
        GameObject _originCeilingLamp = Resources.Load<GameObject>("Ceiling_Lamp");
        for (int i = 0; i <= col /2; i++)
        {
            for (int j = 0; j <= row / 2; j++)
            {
                if (i == col / 4 && j == row / 4)
                {
                    GameObject cloneCeilingLamp = Instantiate(_originCeilingLamp);
                    cloneCeilingLamp.transform.SetParent(_roomRoot);
                    cloneCeilingLamp.transform.position = _roomRoot.position + Vector3.forward * j * _ceilingW + Vector3.right * i * _ceilingW + Vector3.up * _wallH + Vector3.forward * _ceilingW / 2 + Vector3.right * _ceilingW / 2 - Vector3.up * ceilingLampBias;
                }
                else
                {
                    GameObject cloneCeiling = Instantiate(_originCeiling);
                    cloneCeiling.transform.SetParent(_roomRoot);
                    cloneCeiling.transform.position = _roomRoot.position + Vector3.forward * j * _ceilingW + Vector3.right * i * _ceilingW + Vector3.up * _wallH + Vector3.forward * _ceilingW / 2 + Vector3.right * _ceilingW / 2;

                }
            }
        }



    }
}
