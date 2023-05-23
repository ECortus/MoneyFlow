using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class PlusMoneyTextUI : MonoBehaviour
{
    private Construction construction;
    private float income => construction.IncomePerSecond;
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private List<Animation> anims;

    Coroutine coroutine;

    public/*  async */ void Activate(Construction con)
    {
        foreach(Animation anim in anims)
        {
            anim.transform.localScale = Vector3.zero;
        }

        construction = con;
        
        /* await UniTask.WaitUntil(() => construction.buyed);
        if(coroutine == null) coroutine = StartCoroutine(Anim()); */
    }

    public void AnimIncome()
    {
        int i = Random.Range(0, anims.Count);

        texts[i].text = $"{MoneyAmountConvertator.IntoText(income)}$";
        anims[i].Play();
    }

    public void Disable()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;

        foreach(Animation anim in anims)
        {
            anim.transform.localScale = Vector3.zero;
        }
    }

    IEnumerator Anim()
    {
        WaitForSeconds wait = new WaitForSeconds(0.33f);
        int i;
        while(true)
        {
            if(!GameManager.Instance.isActive) break;

            i = Random.Range(0, anims.Count);

            texts[i].text = $"{MoneyAmountConvertator.IntoText(income)}$";
            anims[i].Play();

            yield return wait;
        }
    }
}
