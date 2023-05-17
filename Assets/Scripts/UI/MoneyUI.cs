using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public static MoneyUI Instance { get; set; }

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float counterPlusBySecond = 100f;
    private float counterPlusBySecondAdaptive => counterPlusBySecond * adaptiveMod;

    [SerializeField] private float bound = 3;
    private float boundAdaptive => bound * adaptiveMod;
    
    private float adaptiveMod
    {
        get
        {
            float power = $"{(int)currentMoneyCount}".Length - 3;
            return Mathf.Pow(10, (power < 0 ? 0 : power));
        }
    }

    private static float money { get { return Statistics.Money; } set { Statistics.Money = value; } }
    float currentMoneyCount = 0;

    Coroutine coroutine;
    int sign
    {
        get
        {
            if(currentMoneyCount > money) return -1;
            else if (currentMoneyCount < money) return 1;
            else return 0;
        }
    }

    void Awake() => Instance = this;

    void Start()
    {
        ResetMoney();
    }

    public void UpdateMoney()
    {
        if(currentMoneyCount == money) 
        {
            ResetMoney();
            return;
        }

        if(coroutine == null) coroutine = StartCoroutine(Coroutine());
    }

    public void ResetMoney()
    {
        currentMoneyCount = money;
        IntoText(currentMoneyCount);
    }

    IEnumerator Coroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.014f);

        while(currentMoneyCount != money)
        {
            currentMoneyCount = Mathf.Lerp(currentMoneyCount, money, counterPlusBySecond * Time.deltaTime);
            if (Mathf.Abs(currentMoneyCount - money) <= boundAdaptive) break;

            IntoText(currentMoneyCount);
            yield return wait;
        }

        currentMoneyCount = money;
        IntoText(currentMoneyCount);
        yield return null;

        StopCoroutine(coroutine);
        coroutine = null;
    }

    void IntoText(float value)
    {
        moneyText.text = MoneyAmountConvertator.IntoText(value);
    }
}
