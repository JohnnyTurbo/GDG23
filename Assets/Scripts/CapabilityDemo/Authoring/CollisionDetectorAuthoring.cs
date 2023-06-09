﻿using Unity.Entities;
using Unity.Physics.Authoring;
using UnityEngine;

namespace TMG.GDG23
{
    [RequireComponent(typeof(PhysicsShapeAuthoring))]
    [RequireComponent(typeof(PhysicsBodyAuthoring))]
    [RequireComponent(typeof(CapabilityTagAuthoring))]
    public class CollisionDetectorAuthoring : MonoBehaviour
    {
        public class CollisionDetectorBaker : Baker<CollisionDetectorAuthoring>
        {
            public override void Bake(CollisionDetectorAuthoring authoring)
            {
                var capabilityEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddBuffer<CapabilityHitBufferElement>(capabilityEntity);
            }
        }
    }
}