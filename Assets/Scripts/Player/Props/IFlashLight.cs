using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

namespace Player.Props
{
    public interface IFlashLight
    {
        
        public List<PropSO> props { get; set; }
        public Light2D Light { get; }
        public void GetFlashLight(PropSO prop)
        {
            props.Add(prop);
            Light.intensity = 4;
            Light.pointLightOuterRadius = 10;
        }

        public void RemoveFlashLight()
        {
            Light.intensity = 1;
            Light.pointLightOuterRadius = 4;
        }
    }
}