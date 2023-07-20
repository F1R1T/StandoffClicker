using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickerSystem : MonoBehaviour 
{
    // DATA HOLDER DEFINITION
    // public DataHolder DH;

    public void ButtonClick()
    {

        //TODO Buffs that makes sense on the userClickPower, ALWAYS call it through the DataHolder.userClickPower
        DataHolder.userMoney += DataHolder.userClickPower;
        Debug.Log(DataHolder.userMoney);
    }

}