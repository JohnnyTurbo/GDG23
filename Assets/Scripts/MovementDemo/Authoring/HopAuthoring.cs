using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    public class HopAuthoring : MonoBehaviour
    {
        public float HopHeight;
        public float JumpMultiplier;
        public float ChargeTime;
    }

    public class HopAuthoringBaker : Baker<HopAuthoring>
    {
        public override void Bake(HopAuthoring authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<ShouldHopTag>(playerEntity);
            SetComponentEnabled<ShouldHopTag>(playerEntity, false);
            AddComponent<HopTimer>(playerEntity);
            AddComponent(playerEntity, new HopProperties
            {
                ChargeTime = authoring.ChargeTime,
                MaxHeight = authoring.HopHeight,
                JumpMultiplier = authoring.JumpMultiplier
            });
        }
    }
}