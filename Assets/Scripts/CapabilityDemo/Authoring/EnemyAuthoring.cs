using Unity.Entities;
using UnityEngine;

namespace TMG.GDG23
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddBuffer<DamageBufferElement>(entity);
            }
        }
    }
}