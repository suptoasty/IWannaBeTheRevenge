using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonkHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void HandleBonk(float x, float y);

    // Update is called once per frame
    void Update()
    {
        
    }
}
