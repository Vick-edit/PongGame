using System;
using Components.Helpers;
using GameEventParams;
using GamePlayScripts.UserPrefsController.DataModels;
using GamePlayScripts.UserPrefsController.Interfaces;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Dispatcher;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Основной компонент, отвечающий за управление состоянием игры
    ///     в нём нет сложной логики, в основном управление состояниям и сообщениями, поэтому у него нет отдельного контроллера
    ///     но, если логика будет усложняться, то можно будет отрефакторить класс
    /// </summary>
    [RequireComponent(typeof(PlayerBallLauncherMono))]
    public class GameManagerMono : MonoBehaviour
    {
        [Header("UI элементы отображения статуса игры")]
        [SerializeField] private ScoreToTextMono _scoreToText;

        [Header("Страницы окончания игры")]
        [SerializeField] private Transform _deathScreen;

        [Header("Вспомогательные компоненты")]
        [SerializeField] private PlayerBallLauncherMono _ballLauncher;
        [SerializeField] private int _onHitScore = 70;

        private IUserPrefsController _userPrefsController;
        private UserPrefs _userPrefs;
        private int _currentScore;

        private void Awake()
        {
            _currentScore = 0;
            _scoreToText.SetScore(_currentScore);

            _userPrefsController = DependencyResolver.GetCachedUserPrefsControllerl();
            _userPrefs = _userPrefsController.GetUserPrefs();

            this.WeakSubscribe<GameManagerMono, GameManagementEvent>(gm => gm.OnGameEvent);
        }

        private void Start()
        {
            //Запускаем шарик из старта, а не из Awake, чтобы к этому времени ужё всё было проинициализированно
            _ballLauncher.LaunchBallFromSpawnPosition();
        }

        private void OnDestroy()
        {
            UpdateHighScore();
        }



        #region OnGameEvent
        private void OnGameEvent(object source, GameManagementEvent gameManagementEvent)
        {
            switch (gameManagementEvent.GameEvent)
            {
                case GameManagementEvent.GameEvents.PaddleHit:
                    OnPaddleHit();
                    break;
                case GameManagementEvent.GameEvents.BallFlewAway:
                    OnBallFlew();
                    break;
            }
        }
        private void OnPaddleHit()
        {
            _currentScore += _onHitScore;
            _scoreToText.SetScore(_currentScore);
        }

        private void OnBallFlew()
        {
            _deathScreen.gameObject.SetActive(true);
        }

        private void UpdateHighScore()
        {
            if(_currentScore < _userPrefs.UserScore)
                return;

            _userPrefs.UserScore = _currentScore;
            _userPrefsController.Save(_userPrefs);
        }
        #endregion


        #region OnUiButtons
        public void ResumeGame()
        {
            _deathScreen.gameObject.SetActive(false);
            _ballLauncher.LaunchBallFromSpawnPosition();
        }

        public void ResetGame()
        {
            UpdateHighScore();
            _currentScore = 0;
            _scoreToText.SetScore(_currentScore);

            _deathScreen.gameObject.SetActive(false);
            _ballLauncher.LaunchBallFromSpawnPosition();
        }
        #endregion

    }
}