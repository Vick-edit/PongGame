using System;
using GamePlayScripts.PlayerBallController.DataModels;
using UnityEngine;

namespace GamePlayScripts.PlayerBallController
{
    public class PlayerBallController : IPlayerBallController
    {
        private readonly PlayerBallParameters _monoBehaviourParameters;


        /// <inheritdoc />
        public PlayerBallController(PlayerBallParameters monoBehaviourParameters)
        {
            _monoBehaviourParameters = monoBehaviourParameters;
        }


        /// <inheritdoc />
        public void StartMovementInDirection(Vector2 movementDirection)
        {
            if (_monoBehaviourParameters.IsRunning)
                throw new Exception("Шарик уже запущен, невозможно запустить его по новой");

            _monoBehaviourParameters.IsRunning = true;
            _monoBehaviourParameters.Rigidbody.isKinematic = false;
            _monoBehaviourParameters.Rigidbody.velocity = movementDirection;
            ChangeSpeed(_monoBehaviourParameters.BallSpeedPower);
            ChangeBallSize(_monoBehaviourParameters.BallRadius);
        }

        /// <inheritdoc />
        public void PauseBall()
        {
            _monoBehaviourParameters.IsRunning = false;
            _monoBehaviourParameters.Rigidbody.velocity = Vector2.zero;
        }

        /// <inheritdoc />
        public void ChangeSpeed(float newSpeed)
        {
            newSpeed = Mathf.Clamp(newSpeed, _monoBehaviourParameters.MinForcePower, _monoBehaviourParameters.MaxForcePower);

            var ballVelocity = _monoBehaviourParameters.Rigidbody.velocity.normalized * newSpeed;
            _monoBehaviourParameters.Rigidbody.velocity = ballVelocity;
        }


        /// <inheritdoc />
        public void ChangeBallSize(float newScale)
        {
            newScale = Mathf.Clamp(newScale, _monoBehaviourParameters.MinBallRadius, _monoBehaviourParameters.MaxBallRadius);
            var newScaleVector = new Vector3(newScale, newScale, 1f);

            _monoBehaviourParameters.Transform.localScale = newScaleVector;
        }
    }
}