using System.Collections;
using _MobControl.Scripts.Controller.CanvasController;
using _MobControl.Scripts.Data;
using _MobControl.Scripts.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace _MobControl.Scripts.Controller
{
    public class CannonMachineController : BuildController
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [SerializeField] private float soldierAnimDuration, soldierSlideIncCount, moveSpeed;
        [SerializeField] private int minKey, maxKey;
        [SerializeField] private Vector2Int border;

        private bool _isMove, _isCreateBigSoldier;
        private float _bigSoldierSlideCount;

        public void Move(bool moveIsLeft)
        {
            var direction = moveIsLeft ? -1 : 1;
            var pos = transform.position;
            if (pos.x <= border.x || pos.x >= border.y)
                return;
            pos.x +=  moveSpeed * direction* Time.deltaTime;

            transform.position = pos;
            if (_isCreateBigSoldier)
                return;

            _bigSoldierSlideCount += soldierSlideIncCount;

            inGameCanvas.UpdateCannonSliderValue(_bigSoldierSlideCount);

            if (_bigSoldierSlideCount >= 1)
                _isCreateBigSoldier = true;
        }

        public void CreateBigSoldier()
        {
            if (!_isCreateBigSoldier) return;

            _bigSoldierSlideCount = 0;
            inGameCanvas.UpdateCannonSliderValue(0);

            _isCreateBigSoldier = false;

            CreateSoldier(SoldierType.PartisanSoldierBig, 1, false);
        }

        protected override IEnumerator CreateSoldierRoutine(SoldierType type, int soldierCount,
            float soldierCreateDelay = 0)
        {
            StartCoroutine(CreateSoldierAnimationRoutine());
            return base.CreateSoldierRoutine(type, soldierCount, soldierAnimDuration);
        }

        private IEnumerator CreateSoldierAnimationRoutine()
        {
            var newWeight = 0f;
            yield return LerpRoutine(true);
            yield return LerpRoutine(false);

            IEnumerator LerpRoutine(bool isAsc)
            {
                var blendShapeWeight = meshRenderer.GetBlendShapeWeight(0);
                var elapsedTime = 0f;
                while (isAsc ? newWeight < maxKey : newWeight > minKey)
                {
                    newWeight = Mathf.Lerp(blendShapeWeight, isAsc ? maxKey : minKey,
                        elapsedTime / soldierAnimDuration);
                    meshRenderer.SetBlendShapeWeight(0, newWeight);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}