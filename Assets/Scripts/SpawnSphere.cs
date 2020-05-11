using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphere : MonoBehaviour
{
    public GameObject sphere;
    public GameObject PPV;
    public List<float> yPos;

    void Start()
    {
        foreach (var item in yPos)
        {
            Instantiate(sphere, new Vector3(Random.Range(-4, 5), item, Random.Range(-4, 5)),Quaternion.identity);
        }
    }

    public void PostProcess()
    {
        if (PPV.activeSelf)
        {
            PPV.SetActive(false);
        }
        else
        {
            PPV.SetActive(true);
        }
    }

}
