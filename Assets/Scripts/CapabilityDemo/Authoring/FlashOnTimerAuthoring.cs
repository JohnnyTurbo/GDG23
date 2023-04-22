using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(DestroyOnTimerAuthoring))]
    [RequireComponent(typeof(CapabilityTagAuthoring))]
    public class FlashOnTimerAuthoring : MonoBehaviour
    {
        public float RegularToggleInterval;
        public float RapidToggleInterval;
        public float RapidToggleThreshold;
        
        public class FlashOnTimerBaker : Baker<FlashOnTimerAuthoring>
        {
            public override void Bake(FlashOnTimerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new FlashOnTimer
                {
                    RegularToggleInterval = authoring.RegularToggleInterval,
                    RapidToggleInterval = authoring.RapidToggleInterval,
                    RapidToggleThreshold = authoring.RapidToggleThreshold,
                    NextToggleTime = float.MaxValue, 
                    IsRed = false
                });
                AddComponent<URPMaterialPropertyBaseColor>(entity);
            }
        }
    }
}