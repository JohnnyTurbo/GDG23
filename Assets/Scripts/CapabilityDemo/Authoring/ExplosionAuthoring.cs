using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(CapabilityTagAuthoring))]
    public class ExplosionAuthoring : MonoBehaviour
    {
        public float Duration;
        public float MaxRadius;

        public class ExplosionBaker : Baker<ExplosionAuthoring>
        {
            public override void Bake(ExplosionAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ExplosionData
                {
                    Duration = authoring.Duration, 
                    MaxRadius = authoring.MaxRadius
                });
            }
        }
    }
}