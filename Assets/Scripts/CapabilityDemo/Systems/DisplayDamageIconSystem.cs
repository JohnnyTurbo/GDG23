using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class DisplayDamageIconSystem : SystemBase
    {
        public Action<int, float, float3> OnDamage;
        
        protected override void OnUpdate()
        {
            foreach (var (damageBuffer, transform, entity) in SystemAPI.Query<DynamicBuffer<DamageBufferElement>, LocalTransform>().WithEntityAccess())
            {
                foreach (var damage in damageBuffer)
                {
                    OnDamage?.Invoke(entity.Index, damage.Value, transform.Position);
                }
                damageBuffer.Clear();
            }
        }
    }
}