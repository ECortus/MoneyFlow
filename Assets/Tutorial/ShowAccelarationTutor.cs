using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAccelarationTutor : MonoBehaviour
{
    public void Do()
    {
        Tutorial.Instance.SetState(TutorialState.ACCELERATION);
    }
}
