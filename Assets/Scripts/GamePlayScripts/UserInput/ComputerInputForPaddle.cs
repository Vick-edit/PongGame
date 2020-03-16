using GamePlayScripts.UserInput.Interfaces;
using UnityEngine;

namespace GamePlayScripts.UserInput
{
    /// <summary>
    ///     Реализация инпута для компьютера
    /// </summary>
    public class ComputerInputForPaddle : IUserInputForPaddle
    {
        private readonly Camera _mainCamera;
        private float _previousPaddlePosition = float.NaN;
        private float _previousMousePosition = float.NaN;

        /// <inheritdoc />
        public ComputerInputForPaddle(Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _previousMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        }


        /// <inheritdoc />
        public float GetInputPosition()
        {
            if (float.IsNaN(_previousPaddlePosition))
            {
                _previousMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                return float.NaN;
            }
                
            var currentMousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
            var movementOffset = currentMousePosition - _previousMousePosition;
            var newPaddlePosition = _previousPaddlePosition + movementOffset;

            _previousMousePosition = currentMousePosition;
            return newPaddlePosition;
        }

        /// <inheritdoc />
        public void SyncWithPaddlePosition(float currentPaddlePosition)
        {
            _previousPaddlePosition = currentPaddlePosition;
        }
    }
}