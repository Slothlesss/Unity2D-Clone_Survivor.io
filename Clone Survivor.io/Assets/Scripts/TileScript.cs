using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public void SetUp(Vector3 worldPos, Transform parent)
    {
        transform.position = worldPos;
        transform.SetParent(parent);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
