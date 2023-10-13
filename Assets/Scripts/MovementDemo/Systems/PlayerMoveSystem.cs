using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.GDG23
{
    [UpdateInGroup(typeof(MovementSystemGroup), OrderFirst = true)]
    public partial struct PlayerMoveSystem : ISystem, ISystemStartStop
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var moveInput = SystemAPI.GetSingleton<PlayerMoveInput>().Current;
            
            /*
            var playerMoveInput = new float2(moveInput.x, moveInput.z);
            var inputVector = new float3(playerMoveInput.x, 0f, playerMoveInput.y);
            
            foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, MoveSpeed>())
            {
                transform.ValueRW.Position += inputVector * moveSpeed.Value * deltaTime;
            }
            */
            
            new PlayerMoveJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
            
            /*
            new NewPlayerMoveJob
            {
                DeltaTime = deltaTime,
                PlayerInputVector = moveInput
            }.ScheduleParallel();
            */
        }

        #region UI

        public void OnStartRunning(ref SystemState state)
        {
            if (MovementOptionsUIController.Instance == null) return;
            MovementOptionsUIController.Instance.SetMoveButtonColor(true);
        }

        public void OnStopRunning(ref SystemState state)
        {
            if (MovementOptionsUIController.Instance == null) return;
            MovementOptionsUIController.Instance.SetMoveButtonColor(false);
        }

        #endregion
    }

    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, in PlayerMoveInput input, in MoveSpeed moveSpeed)
        {
            transform.Position += input.Current * moveSpeed.Value * DeltaTime;
            transform.Rotation = quaternion.LookRotation(input.LastTrueInput, math.up());
        }
    }
    
    [BurstCompile]
    public partial struct NewPlayerMoveJob : IJobEntity
    {
        public float DeltaTime;
        public float3 PlayerInputVector;
        
        [BurstCompile]
        private void Execute(ref LocalTransform transform, in MoveSpeed moveSpeed)
        {
            transform.Position += PlayerInputVector * moveSpeed.Value * DeltaTime;
        }
    }
}