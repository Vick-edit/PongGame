using Components.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Компонент управления основным меню
    /// </summary>
    public class MainMenuMono : MonoBehaviour
    {
        [SerializeField] private string _mainScene;
        [SerializeField] private string _soundScene;
        private int currentSceneIndex;

        [SerializeField] private Transform _highScoreContainer;
        [SerializeField] private ScoreToTextMono _scoreToText;

        private void Awake()
        {
            currentSceneIndex = gameObject.scene.buildIndex;
            SceneManager.LoadSceneAsync(_soundScene, LoadSceneMode.Additive);
        }

        
        public void NewGame()
        {
            SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(currentSceneIndex);
        }

        public void ShowHighScore()
        {
            _highScoreContainer.gameObject.SetActive(true);
        }


        public void HideHighScore()
        {
            _highScoreContainer.gameObject.SetActive(false);
        }

        public void Quite()
        {
            Application.Quit(0);
        }
    }
}