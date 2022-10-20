using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public void Spawn(Vector3 Pos)
    {
        transform.position = Pos;
    }
}
