using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    [SerializeField] StateHandler stateHandler;

    public void P_C2()
    {
        stateHandler.PlayC2();
    }
    public void RestAppearButton()
    {
        stateHandler.AppearButtonsForRest();
    }
    public void FirstAppearButton()
    {
        stateHandler.AppearButtonsForFirst();
    }
    public void PotTap()
    {
        stateHandler.TapWaterPot();
    }
}
