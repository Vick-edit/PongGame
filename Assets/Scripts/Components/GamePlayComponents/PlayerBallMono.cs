using System;
using GameEventParams;
using GamePlayScripts.PlayerBallController;
using GamePlayScripts.PlayerBallController.DataModels;
using Tools;
using UnityEngine;
using Utils.Dispatcher;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Компонент снаряда
    /// </summary>
    [RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
    public class PlayerBallMono : MonoBehaviour
    {
        [SerializeField] 
        private PlayerBallParameters _ballParameters = new PlayerBallParameters();

        public IPlayerBallController PlayerBallController { get; private set; }
        public PlayerBallParameters PlayerBallParameters => _ballParameters;


        private void Awake()
        {
            if (_ballParameters.Transform == null)
                _ballParameters.Transform = transform;
            
            PlayerBallController = DependencyResolver.GetPlayerBallController(_ballParameters);
            this.Rise(GameManagementEvent.OnBallSpawned());
        }

        private void OnDestroy()
        {
            this.Rise(GameManagementEvent.OnBallDestroyed());
        }
    }
}