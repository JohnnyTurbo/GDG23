using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(CollisionDetectorAuthoring))]
    public class DamageOnCollisionAuthoring : MonoBehaviour
    {
        public float DamageValue;
        
        public class DamageOnCollisionBaker : Baker<DamageOnCollisionAuthoring>
        {
            public override void Bake(DamageOnCollisionAuthoring authoring)
            {
                var capabilityEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(capabilityEntity, new DamageOnCollision { Value = authoring.DamageValue });
            }
        }
    }
}