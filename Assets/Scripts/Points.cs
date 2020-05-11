using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<ClickToSpawn>().UpdateScore();
        Destroy(gameObject);
    }
}
