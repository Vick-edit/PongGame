using GamePlayScripts.WallController;
using GamePlayScripts.WallController.DataModels;
using GamePlayScripts.PlayerBallController;
using GamePlayScripts.PlayerBallController.DataModels;
using GamePlayScripts.UserInput;
using GamePlayScripts.UserInput.Interfaces;
using GamePlayScripts.UserPaddleController;
using GamePlayScripts.UserPaddleController.DataModels;
using GamePlayScripts.UserPrefsController;
using GamePlayScripts.UserPrefsController.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utils.Dispatcher;
using Utils.EffectPool;
using Utils.Logging;
using Utils.PreprocessorDirectives;
using ILogger = Utils.Logging.ILogger;

namespace Tools
{
    /// <summary>
    ///     Класс, который знает, какому интерфейсу соответствует какой класс и как его строить,
    ///     по сути замена конфигу/фабрике/DI есть недостатки, такие, как большая кодогенерация и быстрое разрастание класса,
    ///     но он даёт гибкость - заменить реализацию интерфейса в проекте можно в одном месте, при этом
    ///     не используется рефлекшен, как стандартном DI, а значит можно без опаски использовать в апдейт и прочих часто вызываемых местах
    /// </summary>
    public static class DependencyResolver
    {
        /// <summary> Получить логгер </summary>
        public static ILogger GetLogger()
        {
            if(IsIt.Editor)
                return new UnityLogger();
            else
                return new MockLogger();
        }

        /// <summary> Получить инстанс диспатчера </summary>
        public static IDispatcher GetCachedDispatcher()
        {
            return DispatcherWrapper.Instance;
        }

        /// <summary> Получить инстанс пула эффектов </summary>
        public static IEffectPool GetCachedEffectPool()
        {
            return EffectPoolWrapper.Instance;
        }

        /// <summary> Получить инстанс настроек пользователя </summary>
        public static IUserPrefsController GetCachedUserPrefsControllerl()
        {
            return UserPrefsController.Instance;
        }

        /// <summary> Получить контроллер стены игрового поля </summary>
        public static IWallController GetBrickController(WallControllerParameters wallParameters)
        {
            var effectPool = GetCachedEffectPool();
            return new WallController(wallParameters, effectPool);
        }

        /// <summary> Получить контроллер шарика </summary>
        public static IPlayerBallController GetPlayerBallController(PlayerBallParameters ballParameters)
        {
            return new PlayerBallController(ballParameters);
        }

        /// <summary> Получить контроллер панельки, которой управляет пользователь </summary>
        public static IPaddleController GetPaddleController(PaddleControllerParameters paddleParameters)
        {
            var dispatcher = GetCachedDispatcher();
            var effectPool = GetCachedEffectPool();
            return new PaddleController(paddleParameters, dispatcher, effectPool);
        }

        /// <summary> Получить класс инпута для компьютера </summary>
        public static IUserInputForPaddle GetUserInput(Camera mainCamera)
        { 
            return new ComputerInputForPaddle(mainCamera);
        }

        /// <summary> Получить класс инпута для телефона </summary>
        public static IUserInputForPaddle GetUserInput(Slider slider)
        {
            return new MobileInputForPaddle(slider);
        }
    }
}