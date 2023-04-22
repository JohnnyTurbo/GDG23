using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    public class CapabilityTagAuthoring : MonoBehaviour
    {
        public class CapabilityTagBaker : Baker<CapabilityTagAuthoring>
        {
            public override void Bake(CapabilityTagAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<CapabilityTag>(entity);
            }
        }
    }
}