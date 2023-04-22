using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(CapabilityTagAuthoring))]
    public class SpawnOnDestroyAuthoring : MonoBehaviour
    {
        public GameObject PrefabToSpawn;

        public class SpawnOnDestroyBaker : Baker<SpawnOnDestroyAuthoring>
        {
            public override void Bake(SpawnOnDestroyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpawnOnDestroy
                {
                    Value = GetEntity(authoring.PrefabToSpawn, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}