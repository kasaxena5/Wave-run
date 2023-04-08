using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int pos = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            pos = 1 - pos;
        }
    }
}
