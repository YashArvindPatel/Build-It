using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{
    public Material material;
    public byte r = 255;
    public byte g = 0;
    public byte b = 0; 
    bool step1 = true, step2 = false, step3 = false, step4 = false, step5 = false, step6 = false;

    void Update()
    {
        material.color = new Color32(r, g, b, 255);

        if (step1)
        {
            if (g == 255)
            {
                step1 = false;
                step2 = true;
            }
            else
            {
                g += 1;
            }      
        }
        else if (step2)
        {
            if (r == 0)
            {
                step2 = false;
                step3 = true;
            }
            else
            {
                r -= 1;
            }
        }
        else if (step3)
        {
            if (b == 255)
            {
                step3 = false;
                step4 = true;
            }
            else
            {
                b += 1;
            }
        }
        else if (step4)
        {
            if (g == 0)
            {
                step4 = false;
                step5 = true;
            }
            else
            {
                g -= 1;
            }
        }
        else if (step5)
        {
            if (r == 255)
            {
                step5 = false;
                step6 = true;
            }
            {
                r += 1;
            }
        }
        else if (step6)
        {
            if (b == 0)
            {
                step6 = false;
            }
            else
            {
                b -= 1;
            }
        }
        else
        {
            step1 = true;
        }
        
    }
}
