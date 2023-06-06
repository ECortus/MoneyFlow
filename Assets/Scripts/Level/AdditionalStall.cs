using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdditionalStall : MonoBehaviour
{
    [SerializeField] private string PropsName = "";

    private Queue<Chelick> chelicks = new Queue<Chelick>();
    private Queue<Vector3> mainTargets = new Queue<Vector3>();

    [SerializeField] private int Max = 4;
    [SerializeField] private float Delay = 3f;
    [SerializeField] private int defReward = 100;
    private int Reward => defReward * (Road.Instance.Size + 1);
    
    [Space]
    [SerializeField] private Transform CenterTransform;

    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private List<Animation> anims;

    void Start()
    {
        Activate();
    }

    public void Activate()
    {
        foreach(Animation anim in anims)
        {
            anim.transform.localScale = Vector3.zero;
        }
    }

    public void AnimIncome()
    {
        int i = Random.Range(0, anims.Count);

        texts[i].text = $"{MoneyAmountConvertator.IntoText(Reward)}$";
        anims[i].Play();
    }

    public void Call(Chelick chel)
    {
        if(chelicks.Count >= Max) return;

        chelicks.Enqueue(chel);
        mainTargets.Enqueue(chel.target);

        /* RefreshTargets(); */
        if(chel != chelicks.Peek()) chel.SetTarget(target);
        else chel.SetTarget(CenterTransform.position);

        StartCoroutine(Visit(chel));
    }

    public void Decall()
    {
        Chelick chel = chelicks.Dequeue();

        chel.target = mainTargets.Dequeue();
        chel.ResetRotateTarget();

        if(chelicks.Count > 0)
        {
            chel = chelicks.Peek();
            chel.SetTarget(CenterTransform.position);
        }

        /* RefreshTargets(); */
    }

    IEnumerator Visit(Chelick chelick)
    {
        Chelick chel = chelick;

        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => chel.Agent.velocity.magnitude < 0.05f);
        chel.SetRotateTarget(CenterTransform.position);

        yield return new WaitUntil(() => ConditionToVisit(chel));

        yield return new WaitForSeconds(Delay);
        Buy();
        chel.Props(PropsName);

        Decall();
    }

    void Buy()
    {
        Money.Plus(Reward);
        AnimIncome();
    }

    bool ConditionToVisit(Chelick chel)
    {
        return Vector3.Distance(chel.transform.position, CenterTransform.position) < 0.1f && chel.Agent.velocity.magnitude < 0.05f
            && chel == chelicks.Peek();
    }

    Vector3 target 
    {
        get
        {
            float mod = chelicks.Count / 4f;
            float value = 0.75f;

            return CenterTransform.position + new Vector3(
                Random.Range(-value, value),
                0f,
                Random.Range(0f, value)
            ) * mod;
        }
    }

    void RefreshTargets()
    {
        Vector3 center = CenterTransform.position;
        Vector3 split;
        Vector3 dest;

        int i = 0;
        float mod = 2f;

        foreach(Chelick chel in chelicks)
        {
            split = new Vector3(
                Random.Range(-0.5f, 0.5f),
                0f,
                Random.Range(-0.25f, 0.5f)
            ) * mod;

            if(i > 0) dest = center + split;
            else dest = center;

            chel.SetTarget(dest);

            i++;
        }
    }
}
