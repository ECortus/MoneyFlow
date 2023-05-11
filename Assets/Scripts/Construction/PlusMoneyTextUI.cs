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
    int index => Random.Range(0, anims.Count);

    void Start()
    {
        foreach(Animation anim in anims)
        {
            anim.transform.localScale = Vector3.zero;
        }
    }

    public async void Activate(Construction con)
    {
        construction = con;
        
        await UniTask.WaitUntil(() => construction.buyed);
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        WaitForSeconds wait = new WaitForSeconds(0.33f);
        int i;
        while(true)
        {
            i = index;

            texts[i].text = $"+{MoneyAmountConvertator.IntoText(income)}$";
            anims[i].Play();

            yield return wait;
        }
    }
}
