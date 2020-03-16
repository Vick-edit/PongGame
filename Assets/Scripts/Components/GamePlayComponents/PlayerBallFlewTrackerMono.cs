using GameEventParams;
using UnityEngine;
using Utils.Dispatcher;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Компонент, останавливающий мяч если он улетел за края карты
    /// </summary>
    [RequireComponent(typeof(OnBallCollisionMono))]
    public class PlayerBallFlewTrackerMono : MonoBehaviour
    {
        /// <summary> Используется для вызова в <see cref="OnBallCollisionMono.OnBallCollisionEvent"/> </summary>
        public void OnBallCollision(ContactPoint2D playerBallContactPoint)
        {
            //GetComponent это медлено, но ситуация, когда мяч улетел за край карты, возникает редко
            var playerBallComponent = playerBallContactPoint.collider.transform.GetComponent<PlayerBallMono>();
            playerBallComponent.PlayerBallController.PauseBall();
            this.Rise(GameManagementEvent.OnBallFlewAway());
        }
    }
}