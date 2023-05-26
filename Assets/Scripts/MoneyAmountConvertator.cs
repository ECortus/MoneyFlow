using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MoneyAmountConvertator
{
    public static int GetThousandPower(float value)
    {
        float money = value;
        int power = 0;

        while (money >= 1000)
        {
            money /= 1000;
            power++;
        }

        return power;
    }

    public static string IntoText(float value)
    {
        float money = value;
        int power = GetThousandPower(money) * 3;
        string text = "";

        if(((Mathf.Round(money)).ToString().Length) < 4) return (Mathf.Round(money)).ToString();

        string whole = $"{Mathf.Round(money / Mathf.Pow(10, power))}";
        string left = $"{Mathf.Round(money % Mathf.Pow(10, power))}";

        if(left.Length < 3)
        {
            left = "0000000" + left;
        }

        if(whole.Length < 3) text = whole + "." + left[0..(3 - whole.Length)];
        else text = whole;

        if(!Char.IsDigit(text[text.Length - 1])) text = text[0..(text.Length - 1)];

        text += $"{GetPreName(power)}";

        return text;
    }

    static string GetPreName(int power)
    {
        if(power < 3) return "";
        else if(power >= 3 && power < 6) return "K";
        else if(power >= 6 && power < 9) return "M";
        else if(power >= 9 && power < 12) return "B";
        else if(power >= 12 && power < 15) return "T";
        else if(power >= 15 && power < 18) return "QA";
        else if(power >= 18 && power < 21) return "QI";
        else if(power >= 21 && power < 24) return "SX";
        else if(power >= 24 && power < 27) return "SP";
        else if(power >= 27 && power < 30) return "O";
        else if(power >= 30 && power < 33) return "N";
        else if(power >= 33 && power < 36) return "D";
        else if(power >= 36 && power < 39) return "UD";
        else if(power >= 39 && power < 42) return "DO";
        else if(power >= 42 && power < 45) return "TD";
        else if(power >= 45 && power < 48) return "QAD";
        else if(power >= 48 && power < 51) return "QID";
        else if(power >= 51 && power < 54) return "SXD";
        else if(power >= 54 && power < 57) return "SPD";
        else if(power >= 57 && power < 60) return "OD";
        else if(power >= 60 && power < 63) return "ND";
        else if(power >= 63) return "V";
        else return "...";
    }
}
