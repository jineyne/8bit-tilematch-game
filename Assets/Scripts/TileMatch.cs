using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public struct SwapData {
    public GameObject TargetObject;
    public Vector2 TargetVector;
}

public class Tile : MonoBehaviour {
    public int TypeId = -1;
}

public class TileMatch {
    public Tile[] _ArrOfTiles;
    public int _BoardHeight = 0;
    public int _BoardWidth = 0;
    public int _MinMatchLen = 3;

    private Tile[] ArrOfTilemaps;
    private List<SwapData> ArrOfSwaps;
    private void Awake() {
        ArrOfTilemaps = new Tile[_BoardHeight * _BoardWidth];
        ArrOfSwaps = new List<SwapData>();
    }

    private void Update() {
        for(int i = 0; i < ArrOfSwaps.Count; i++) {
            SwapData swapData = ArrOfSwaps[i];
            if(swapData.TargetObject == null) {
                ArrOfSwaps.Remove(swapData);
                i--;
            } else {
                if(Vector3.Distance(swapData.TargetObject.transform.position, swapData.TargetVector) <= 1) {
                    swapData.TargetObject.transform.position = swapData.TargetVector;
                    ArrOfSwaps.Remove(swapData);
                    i--;
                } else {
                    swapData.TargetObject.transform.position 
                        = Vector3.Lerp(swapData.TargetObject.transform.position, swapData.TargetVector, 0.5f);
                }
            }
        }
    }

    private List<Tile> FindMatch() {
        List<Tile> res = new List<Tile>();
        int additionalInt = ((int)(_MinMatchLen/2));
        for(int y= 0; y < _BoardHeight; y++) {
            for(int x = 0; x < _BoardWidth; x++) {
                Tile obj = GetAt(new Vector2(x, y));

                int i = 0;
                List<Tile> temp = new List<Tile>();
                // check 
                for(int yy = Mathf.Max(0, y-additionalInt); yy <= Mathf.Max(_BoardHeight, y+additionalInt); yy++) {
                    Tile tile = GetAt(new Vector2(x, yy));
                    if(tile.TypeId == obj.TypeId) {
                        i++;
                        temp.Add(tile);
                    }
                }
                if(i >= _MinMatchLen) {
                    foreach(Tile tile in temp) {
                        if(res.Find(t => t == tile) == null) {
                            res.Add(tile);
                        }
                    }
                }

                i = 0;
                for(int xx = Mathf.Max(0, x-additionalInt); xx <= Mathf.Max(_BoardWidth, x+additionalInt); xx++) {
                    Tile tile = GetAt(new Vector2(xx, y));
                    if(tile.TypeId == obj.TypeId) {
                        i++;
                        temp.Add(tile);
                    }
                }
                if(i >= _MinMatchLen) {
                    foreach(Tile tile in temp) {
                        if(res.Find(t => t == tile) == null) {
                            res.Add(tile);
                        }
                    }
                }
            }
        }
        return res;
    }

    private void Swap(GameObject o1, GameObject o2) {
        ArrOfSwaps.Add( new SwapData() {
            TargetObject = o1,
            TargetVector = o2.transform.position
        });

        ArrOfSwaps.Add( new SwapData() {
            TargetObject = o2,
            TargetVector = o1.transform.position
        });
    }

    private Tile GetAt(Vector2 pos) {
        return ArrOfTilemaps[(int)(pos.x * _BoardHeight + pos.y)];
    }

    private void SetAt(Vector2 pos, Tile obj) {
        ArrOfTilemaps[(int)(pos.x * _BoardHeight + pos.y)] = obj;
    }
}