using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(MovementSystemGroup))]
    public partial struct PlayerHopSystem : ISystem, ISystemStartStop
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.Enabled = false;
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            
            new ChargePlayerHopJob
            {
                DeltaTime = deltaTime, 
                ECB = ecb
            }.Schedule();

            new PlayerHopJob
            {
                DeltaTime = deltaTime, 
                ECB = ecb
            }.Schedule();
        }

        #region UI

        public void OnStartRunning(ref SystemState state)
        {
            MovementOptionsUIController.Instance.SetHopButtonColor(true);
        }

        public void OnStopRunning(ref SystemState state)
        {
            MovementOptionsUIController.Instance.SetHopButtonColor(false);
        }

        #endregion
    }

    [BurstCompile]
    [WithNone(typeof(ShouldHopTag))]
    public partial struct ChargePlayerHopJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        
        [BurstCompile]
        private void Execute(Entity playerEntity, ref LocalTransform transform, ref HopTimer timer, 
            in PlayerMoveInput input, in HopProperties hop)
        {
            if (input.Begin)
            {
                transform.Rotation = quaternion.LookRotation(input.LastTrueInput, math.up());
            }
            
            if (input.Held)
            {
                timer.Value += DeltaTime;
                if (timer.Value >= hop.ChargeTime)
                {
                    timer.Value = 0f;
                    ECB.SetComponentEnabled<ShouldHopTag>(playerEntity, true);
                }
            }
            else if (input.End)
            {
                timer.Value = 0f;
            }
        }
    }
    
    [BurstCompile]
    [WithAll(typeof(ShouldHopTag))]
    public partial struct PlayerHopJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        
        [BurstCompile]
        private void Execute(Entity playerEntity, ref HopTimer timer, ref LocalTransform transform, 
            in MoveSpeed moveSpeed, in PlayerMoveInput input, in HopProperties hop)
        {
            timer.Value += DeltaTime;
            if (timer.Value < math.PI / hop.JumpMultiplier)
            {
                transform.Position.y = hop.MaxHeight * math.sin(timer.Value * hop.JumpMultiplier) + 0.765f;
                transform.Position.xz += input.Current.xz * moveSpeed.Value * DeltaTime;
                transform.Rotation = quaternion.LookRotation(input.LastTrueInput, math.up());
            }
            else
            {
                timer.Value = 0f;
                transform.Position.y = 0.765f;
                ECB.SetComponentEnabled<ShouldHopTag>(playerEntity, false);
            }
        }
    }
}