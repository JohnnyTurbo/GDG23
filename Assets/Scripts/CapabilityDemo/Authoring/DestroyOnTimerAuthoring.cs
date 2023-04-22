using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(CapabilityTagAuthoring))]
    public class DestroyOnTimerAuthoring : MonoBehaviour
    {
        public float Timer;

        public class DestroyTimerBaker : Baker<DestroyOnTimerAuthoring>
        {
            public override void Bake(DestroyOnTimerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new DestroyOnTimer { Value = authoring.Timer });
            }
        }
    }
}