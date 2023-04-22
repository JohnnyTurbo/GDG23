using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(CapabilityTagAuthoring))]
    [RequireComponent(typeof(CollisionDetectorAuthoring))]
    public class DestroyOnCollisionAuthoring : MonoBehaviour
    {
        public class DestroyOnCollisionBaker : Baker<DestroyOnCollisionAuthoring>
        {
            public override void Bake(DestroyOnCollisionAuthoring authoring)
            {
                var capabilityEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<DestroyOnCollisionTag>(capabilityEntity);
            }
        }
    }
    
}