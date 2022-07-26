using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static Dictionary<string, GameObject> itemsCollected = new Dictionary<string, GameObject>();
    private bool isAlive = true;
    public bool IsAlive
    {
        get { return isAlive; }

        set { isAlive = value; }
    }
    private int lifePoints = 3;
    public int LifePoints
    {
        get { return lifePoints; }
        set { LifePointsSetTreatment(value); }
    }

    private void LifePointsSetTreatment(int value)
    {
        if(value <= 0)
        {
            lifePoints = 0;
        }
        else
        {
            lifePoints = value;
        }
    }

   
}
