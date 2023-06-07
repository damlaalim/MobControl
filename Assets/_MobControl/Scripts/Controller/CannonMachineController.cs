using System.Collections;
using _MobControl.Scripts.Data;
using UnityEngine;

namespace _MobControl.Scripts.Controller
{
    public class CannonMachineController : BuildController
    {
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [SerializeField] private float soldierAnimDuration, soldierSlideIncCount, moveSpeed;
        [SerializeField] private int minKey, maxKey;
        [SerializeField] private Vector2Int border;

        private bool _isMove, _isCreateBigSoldier;
        private float _bigSoldierSlideCount;
        
        public void Move(bool moveIsLeft)
        {
            var posX = transform.position.x;
            if ((posX <= border.x && moveIsLeft) || (posX >= border.y && !moveIsLeft))
                return;
            
            var direction = moveIsLeft ? -1 : 1;
            var movement = new Vector3(direction, 0, 0);
            var newPos = movement * moveSpeed * Time.deltaTime;
            transform.position += newPos;
            
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