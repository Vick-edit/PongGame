using System;
using GamePlayScripts.UserPrefsController.DataModels;
using GamePlayScripts.UserPrefsController.Interfaces;
using UnityEngine;
using Utils.EffectPool;

namespace GamePlayScripts.UserPrefsController
{
    public class UserPrefsController: IUserPrefsController
    {
        #region Singelton
        private static readonly Lazy<IUserPrefsController> LazyInstance = new Lazy<IUserPrefsController>(() => new UserPrefsController());

        /// <summary> Singleton точка доступа к реализации <see cref="IUserPrefsController"/> </summary>
        public static IUserPrefsController Instance => LazyInstance.Value;

        private UserPrefsController()
        {
            if (!PlayerPrefs.HasKey(USER_PREFS_KEY))
                Save(new UserPrefs());
        }
        #endregion

        private const string USER_PREFS_KEY = "UserPrefferences";
        private UserPrefs _paddleParameters;
        

        /// <inheritdoc />
        public UserPrefs GetUserPrefs()
        {
            if (_paddleParameters == null)
            {
                var serializedSettings = PlayerPrefs.GetString(USER_PREFS_KEY);
                _paddleParameters = JsonUtility.FromJson<UserPrefs>(serializedSettings);
            }
                
            return _paddleParameters;
        }

        /// <inheritdoc />
        public void Save(UserPrefs newUserPrefs)
        {
            var serializedSettings = JsonUtility.ToJson(newUserPrefs);
            PlayerPrefs.SetString(USER_PREFS_KEY, serializedSettings);
        }
    }
}