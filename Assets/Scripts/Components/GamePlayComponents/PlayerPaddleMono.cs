using Components.Helpers;
using GamePlayScripts.UserPaddleController;
using GamePlayScripts.UserPaddleController.DataModels;
using Tools;
using UnityEngine;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Компонент управлени пользовательской панелькой
    /// </summary>
    [RequireComponent(typeof(OnBallCollisionMono))]
    public class PlayerPaddleMono : MonoBehaviour
    {
        [SerializeField] 
        private PaddleControllerParameters _paddleControllerParameters = new PaddleControllerParameters();

        [SerializeField] 
        private UserInputToPaddlePositionMono _paddleInput;

        private IPaddleController _paddleController;

        public PaddleControllerParameters ControllerParameters => _paddleControllerParameters;


        private void Awake()
        {
            _paddleController = DependencyResolver.GetPaddleController(_paddleControllerParameters);
        }

        private void Update()
        {
            var xCoordinate = _paddleInput.UserInputForPaddle.GetInputPosition();
            if (float.IsNaN(xCoordinate))
                return;
            var yCoordinate = _paddleControllerParameters.PaddleTransform.position.y;
            _paddleController.MovePaddleHorizontal(new Vector2(xCoordinate, yCoordinate));
        }

        /// <summary> Используется для вызова в <see cref="OnBallCollisionMono.OnBallCollisionEvent"/> </summary>
        public void OnBallCollision(ContactPoint2D playerBallContactPoint)
        {
            _paddleController.OnPaddleHit(playerBallContactPoint);
        }

        public void CopyInput(PlayerPaddleMono otherPaddle)
        {
            _paddleInput = otherPaddle._paddleInput;
        }
    }
}