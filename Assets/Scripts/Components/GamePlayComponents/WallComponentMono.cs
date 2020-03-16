using GamePlayScripts.WallController;
using GamePlayScripts.WallController.DataModels;
using Tools;
using UnityEditor;
using UnityEngine;

namespace Components.GamePlayComponents
{
    /// <summary>
    ///     Компонент для кирпичика, отвечает за задание параметров через эдитор и прокидывании
    ///     сообщений Unity контроллеру кирпичика
    /// </summary>
    [RequireComponent(typeof(OnBallCollisionMono))]
    public class WallComponentMono : MonoBehaviour
    {
        [SerializeField]
        private bool _isLeftWall;

        [SerializeField] 
        private WallControllerParameters _wallParameters;

        private IWallController _wallController;


        private void Awake()
        {
            _wallController = DependencyResolver.GetBrickController(_wallParameters);
        }

        /// <summary> Используется для вызова в <see cref="OnBallCollisionMono.OnBallCollisionEvent"/> </summary>
        public void OnBallCollision(ContactPoint2D playerBallContactPoint)
        {
            _wallController.OnWallHit(playerBallContactPoint);
        }


        #region DrawGizmos
#if UNITY_EDITOR
        private readonly Color _notAllowedConeColor = new Color(0.823f, 0.215f, 0.215f, 0.5f);
        private readonly Color _reflectConeColor = new Color(0.215f, 0.450f, 0.823f, 0.5f);

        private Transform _cachedTransform;
        private Transform CachedTransform
        {
            get
            {
                _cachedTransform = _cachedTransform ? _cachedTransform : transform;
                return _cachedTransform;
            }
        }


        private void OnDrawGizmos()
        {
            var position = CachedTransform.position;
            var direction = _isLeftWall ? Vector3.right : Vector3.left;

            var radius = 2;
            var notAllowedCone = _wallParameters.MinAllowedReflectionToNormal;
            var reflectionCone = _wallParameters.AdditionalAngel + notAllowedCone;

            var defaultColor = Handles.color;
            Handles.color = _notAllowedConeColor;
            Handles.DrawSolidArc(position, Vector3.forward, direction, notAllowedCone, radius);
            Handles.DrawSolidArc(position, Vector3.forward, direction, -notAllowedCone, radius);
            Handles.color = _reflectConeColor;
            Handles.DrawSolidArc(position, Vector3.forward, direction, reflectionCone, radius);
            Handles.DrawSolidArc(position, Vector3.forward, direction, -reflectionCone, radius);
            Handles.color = defaultColor;
        }
#endif
        #endregion
    }
}