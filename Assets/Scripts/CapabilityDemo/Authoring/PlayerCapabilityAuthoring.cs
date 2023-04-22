using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    public class PlayerCapabilityAuthoring : MonoBehaviour
    {
        public GameObject CapabilityPrefab;

        public class CapabilityPrefabBaker : Baker<PlayerCapabilityAuthoring>
        {
            public override void Bake(PlayerCapabilityAuthoring authoring)
            {
                var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(playerEntity, new CapabilityPrefab
                {
                    Value = GetEntity(authoring.CapabilityPrefab, TransformUsageFlags.Dynamic)
                });
                AddComponent<PlayerCapabilityInput>(playerEntity);
            }
        }
    }
}