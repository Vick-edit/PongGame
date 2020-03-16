using GameEventParams;
using GamePlayScripts.UserInput.Interfaces;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Utils.Dispatcher;
using Utils.PreprocessorDirectives;

namespace Components.Helpers
{
    /// <summary>
    ///     Компонент конвертации инпута пользователя в позицию панельки
    /// </summary>
    public class UserInputToPaddlePositionMono : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Slider _mobileInput;
        [SerializeField] private Transform LeftBorder;
        [SerializeField] private Transform RightBorder;

        public IUserInputForPaddle UserInputForPaddle { get; private set; }


        private void Awake()
        {
            if (IsIt.Mobile)
            {
                _mobileInput.minValue = LeftBorder.position.x;
                _mobileInput.maxValue = RightBorder.position.x;
                UserInputForPaddle = DependencyResolver.GetUserInput(_mobileInput);
            }
            else
            {
                _mobileInput.gameObject.SetActive(false);
                UserInputForPaddle = DependencyResolver.GetUserInput(_mainCamera);
            }

            this.WeakSubscribe<UserInputToPaddlePositionMono, OnPaddleMovedEventParams>(x => x.OnPaddleMoved);
        }

        private void OnPaddleMoved(object source, OnPaddleMovedEventParams movementEvent)
        {
            UserInputForPaddle.SyncWithPaddlePosition(movementEvent.NewXCoordinate);
        }
    }
}