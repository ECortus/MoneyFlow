using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalPropsController : MonoBehaviour
{
    [SerializeField] private Transform cloudParent, hotDogParent, iceCreamParent;
    private Vector3 cloudSize, hotDogSize, iceCreamSize;

    void Start()
    {
        cloudSize = cloudParent.localScale;
        hotDogSize = hotDogParent.localScale;
        iceCreamSize = iceCreamParent.localScale;
    }

    float sizeMod = 2.5f;

    public void Refresh(string Name = "")
    {
        cloudParent.gameObject.SetActive(false);
        hotDogParent.gameObject.SetActive(false);
        iceCreamParent.gameObject.SetActive(false);

        switch(Name)
        {
            case "HotDog":
                hotDogParent.gameObject.SetActive(true);
                hotDogParent.transform.localScale = hotDogSize * sizeMod;
                break;
            case "Cloud":
                cloudParent.gameObject.SetActive(true);
                cloudParent.transform.localScale = cloudSize * sizeMod;
                break;
            case "IceCream":
                iceCreamParent.gameObject.SetActive(true);
                iceCreamParent.transform.localScale = iceCreamSize * sizeMod;
                break;
            default:
                break;
        }
    }
}
