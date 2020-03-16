using System;
using Utils.Dispatcher.EventParameters;

namespace GameEventParams
{
    public class GameManagementEvent : BaseDispatcherEventParams
    {
        [Serializable]
        public enum GameEvents
        {
            BallSpawned,
            BallDestroyed,
            BallFlewAway,
            PaddleHit,
            LevelReset,
        }

        public GameEvents GameEvent { get; }

        public GameManagementEvent(GameEvents gameEvent)
        {
            GameEvent = gameEvent;
        }

        public static GameManagementEvent OnBallSpawned()
        {
            return new GameManagementEvent(GameEvents.BallSpawned);
        }

        public static GameManagementEvent OnBallFlewAway()
        {
            return new GameManagementEvent(GameEvents.BallFlewAway);
        }

        public static GameManagementEvent OnBallDestroyed()
        {
            return new GameManagementEvent(GameEvents.BallDestroyed);
        }

        public static GameManagementEvent OnPaddleHit()
        {
            return new GameManagementEvent(GameEvents.PaddleHit);
        }
        
        public static GameManagementEvent OnLevelReset()
        {
            return new GameManagementEvent(GameEvents.LevelReset);
        }
    }
}