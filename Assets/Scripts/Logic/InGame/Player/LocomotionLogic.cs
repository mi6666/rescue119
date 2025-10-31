using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.Global;
using Structure.InGame;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Logic.InGame.Player
{
    public class LocomotionLogic : ILocomotionLogic
    {
        public LocomotionLogic
        (
            ILocomotionModel locomotionModel,
            ILocomotionSetting locomotionSetting
        )
        {
            LocomotionModel = locomotionModel;
            LocomotionSetting = locomotionSetting;
        }

        public Vector2 CalcVelocity(LocomotionArgument argument)
        {
            var moveTo = math.normalizesafe(argument.CurrentVelocity);
            var acceleration = DoAccel(argument);
            var directionChangeSpeed = LocomotionSetting.DirectionChangeSpeed;

            if (acceleration)
            {
                var targetDirection = math.normalizesafe(argument.MoveInput);
                moveTo = math.lerp(moveTo, targetDirection, directionChangeSpeed * argument.DeltaTime);
                moveTo = math.normalizesafe(moveTo);
            }

            var speed = CalcSpeed(acceleration, argument);

            var midSpeed = moveTo * speed;
            var result = PostProcess(midSpeed, LocomotionSetting.WallFriction, argument);

            DebugLogger.Log("move to", moveTo.ToString());
            DebugLogger.Log("speed", speed.ToString("F1"));
            DebugLogger.Log("mid speed", midSpeed.ToString());
            DebugLogger.Log("result speed", result.ToString());

            return result;
        }

        private bool DoAccel(LocomotionArgument argument)
        {
            var moveInput = argument.MoveInput;
            var currentVelocity = argument.CurrentVelocity;
            var angle = GetAngle(moveInput, currentVelocity);

            var hasInput = math.lengthsq(moveInput) > Constants.Threshold; // 入力はあるか
            var inputIsReverse = angle > LocomotionSetting.ReverseAngleThreshold; // 入力は反転か
            var acceleration = !(!hasInput | inputIsReverse);

            return acceleration;
        }

        private float CalcSpeed(bool acceleration, LocomotionArgument argument)
        {
            var deltaTime = argument.DeltaTime;

            if (acceleration)
            {
                LocomotionModel.IncreaseTime(deltaTime);
                return GetSpeed();
            }

            LocomotionModel.DecreaseTime(deltaTime);
            var result = GetSpeed();
            return result;
        }


        [BurstCompile]
        private static float2 PostProcess(float2 moveTo, float wallFriction, LocomotionArgument argument)
        {
            if (argument.FrontObjects.Length == 0)
            {
                return moveTo;
            }

            var frontObject = argument.FrontObjects[0];
            float2 normal = frontObject.normal;

            // 壁に向かっている場合のみ、壁に沿って滑らせる
            var dotProduct = math.dot(moveTo, normal);
            if (dotProduct >= 0)
            {
                return moveTo;
            }

            // 壁方向の速度成分を打ち消す
            var result = moveTo - dotProduct * normal;
            return result * wallFriction;
        }

        private float GetSpeed()
        {
            var accelTime = LocomotionModel.AccelerationTime;
            var duration = LocomotionSetting.AccelerationDuration;
            var maxSpeed = LocomotionSetting.MaxSpeed;

            var currentSpeed = LocomotionSetting.GetSpeedCurve(accelTime / duration);
            DebugLogger.Log("accel time", accelTime.ToString("F1"));

            return maxSpeed * currentSpeed;
        }

        [BurstCompile]
        private static float GetAngle(float2 moveInput, float2 currentVelocity)
        {
            return math.degrees(math.acos(math.dot(math.normalize(moveInput), math.normalize(currentVelocity))));
        }

        private ILocomotionModel LocomotionModel { get; }
        private ILocomotionSetting LocomotionSetting { get; }
    }
}