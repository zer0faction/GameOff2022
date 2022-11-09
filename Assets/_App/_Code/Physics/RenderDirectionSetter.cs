using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDirectionSetter : MonoBehaviour
{
    public void SetDir(int dir)
    {
        transform.localScale = new Vector3(dir, 1, 1);
    }
}
