using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBackdrop : MonoBehaviour
{
    public string food1;
    public string food2;
    public Sprite image1;
    public Sprite image2;
    short foodcount = 0;

    public Image first;
    public Image second;

    public void AddFood(string name, Sprite image)
    {
        if (foodcount < 2)
        {
            if (foodcount == 0)
            {
                food1 = name;
                image1 = image;
                first.sprite = image1;
            }
            else
            {
                food2 = name;
                image2 = image;
                second.sprite = image2;
            }
        }
    }

    public void RemoveFood(string name)
    {
        if (food1 == name)
        {
            food1 = "";
            image2 = null;
            first.sprite = null;
            foodcount--;
        }
        if (food2 == name)
        {
            food2 = "";
            image2 = null;
            second.sprite = null;
            foodcount--;
        }
    }
}
