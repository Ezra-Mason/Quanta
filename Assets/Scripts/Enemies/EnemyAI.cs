using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 GetTargetPosition()
    {

        return new Vector2((int)Random.Range(-1, 1), (int)Random.Range(-1, 1));
    }
}
