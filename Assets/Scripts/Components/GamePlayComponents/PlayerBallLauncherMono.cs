using Extensions;
using GameEventParams;
using Tools;
using UnityEditor;
using UnityEngine;
using Utils.Dispatcher;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Компонент запускающий шарик в начале игры
    /// </summary>
    public class PlayerBallLauncherMono : MonoBehaviour
    {
        [SerializeField, Range(0f, 90f)] 
        private float _startAngelsRange;

        [SerializeField] private Transform _startingBallPosition;
        [SerializeField] private PlayerBallMono _playerBall;

        [SerializeField] private Transform _startingPadPosition;
        [SerializeField] private PlayerPaddleMono _playerPadPrefab;
        [SerializeField] private PlayerPaddleMono _playerMainPad;
        private PlayerPaddleMono _playerSecondPad;

        private Vector3 _mainPadStartPosition;
        private Vector3 _secondPadStartPosition;

        private IDispatcher _dispatcher;


        private void Awake()
        {
            _dispatcher = DependencyResolver.GetCachedDispatcher();

            _mainPadStartPosition = _startingPadPosition.position;
            _secondPadStartPosition = _mainPadStartPosition;
            _secondPadStartPosition.y *= -1f;

            _playerSecondPad = Instantiate(_playerPadPrefab, _startingPadPosition);
            _playerSecondPad.transform.Rotate(0, 0, 180);
            _playerSecondPad.ControllerParameters.CopyPrivateParams(_playerMainPad.ControllerParameters);
            _playerSecondPad.CopyInput(_playerMainPad);
        }

        
        public void LaunchBallFromSpawnPosition()
        {
            _playerBall.transform.position = _startingBallPosition.position;
            var ballMovementVector = GetRandomLaunchVector();
            _playerBall.PlayerBallController.StartMovementInDirection(ballMovementVector);

            var randomSpeed = Random.Range(_playerBall.PlayerBallParameters.MinForcePower, _playerBall.PlayerBallParameters.MaxForcePower);
            var randomSize = Random.Range(_playerBall.PlayerBallParameters.MinBallRadius, _playerBall.PlayerBallParameters.MaxBallRadius);
            var randomColor = Random.ColorHSV();
            randomColor.a = 1;
            _playerBall.PlayerBallController.ChangeSpeed(randomSpeed);
            _playerBall.PlayerBallController.ChangeBallSize(randomSize);
            _playerBall.PlayerBallController.ChangeBallColor(randomColor);

            _playerMainPad.transform.position = _mainPadStartPosition;
            _playerSecondPad.transform.position = _secondPadStartPosition;
            _dispatcher.Rise(_playerMainPad, new OnPaddleMovedEventParams(_playerMainPad.transform.position.x));
        }

        private Vector2 GetRandomLaunchVector()
        {
            var randomSign = Mathf.Sign(Random.Range(-1, 1));
            var launchSide = Vector2.up * randomSign;

            return launchSide.RotateRandomByAngle(_startAngelsRange);
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var radius = 2;
            var halfRange = Mathf.Abs(_startAngelsRange / 2);

            var launchSide = Vector2.up;
            var startingPosition = _startingBallPosition.position;
            Handles.DrawSolidArc(startingPosition, Vector3.forward, launchSide, halfRange, radius);
            Handles.DrawSolidArc(startingPosition, Vector3.forward, launchSide, -halfRange, radius);

            launchSide *= -1;
            Handles.DrawSolidArc(startingPosition, Vector3.forward, launchSide, halfRange, radius);
            Handles.DrawSolidArc(startingPosition, Vector3.forward, launchSide, -halfRange, radius);
        }
#endif
    }
}