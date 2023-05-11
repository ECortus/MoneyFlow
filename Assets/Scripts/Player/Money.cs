using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Money
{
    private static string key = DataManager.MoneyKey;
    private static float money { get { return Statistics.Money; } set { Statistics.Money = value; } }

    public static void Plus(float count) 
    {
        money += count;

        MoneyUI.Instance.UpdateMoney();
    }
    
    public static void Minus(float count)
    { 
        money -= count;
        if(money < 0) money = 0;

        MoneyUI.Instance.UpdateMoney();
    }
}
