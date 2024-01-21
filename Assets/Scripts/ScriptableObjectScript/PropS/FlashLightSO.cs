using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FlashLightSO")]
public class FlashLightSO : PropSO
{
    public override void AddProp()
    {
        PlayerController.Instance.GetFlashLight(this);
    }

    public override void RemoveProp()
    {
        PlayerController.Instance.RemoveFlashLight();
    }
}
