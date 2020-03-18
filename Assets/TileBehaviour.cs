using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBehaviour : BonkHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void HandleBonk(float x, float y) {
        Tilemap map = GetComponent<Tilemap>();
        Vector3Int gridCoordinates = map.layoutGrid.WorldToCell(new Vector3(x, y, 0));

        string name = map.GetTile(gridCoordinates).name;
        if(name == "colored_273") {
            map.SetTile(gridCoordinates, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
