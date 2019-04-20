using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void ReceiveDamage()
    {
        Die();
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }
}
