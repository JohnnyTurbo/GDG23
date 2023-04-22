using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(CapabilityTagAuthoring))]
    public class ProjectileSpeedAuthoring : MonoBehaviour
    {
        public float Value;

        public class ProjectileSpeedBaker : Baker<ProjectileSpeedAuthoring>
        {
            public override void Bake(ProjectileSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ProjectileSpeed { Value = authoring.Value });
            }
        }
    }
}