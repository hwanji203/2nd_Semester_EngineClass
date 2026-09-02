using System;
using UnityEngine;

namespace _Scripts.Riggings
{
    public class AnimatorFootIK : MonoBehaviour
    {
        [Header("Ground info")] [SerializeField]
        private LayerMask groundMask = ~0;

        [SerializeField] private float rayUpOffset = 0.5f;
        [SerializeField] private float rayLength = 1.0f;
        [SerializeField] private float footHeight = 0.12f;

        [Header("Weight")] [Range(0, 1f), SerializeField]
        private float masterWeight = 1f;

        [SerializeField] private string leftCurveParam = "IKLeftFoot";
        [SerializeField] private string rightCurveParam = "IKRightFoot";

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (_animator == null) return;
            SolveFoot(AvatarIKGoal.LeftFoot, leftCurveParam);
            SolveFoot(AvatarIKGoal.RightFoot, rightCurveParam);
        }

        private void SolveFoot(AvatarIKGoal goal, string curveParam)
        {
            Vector3 animPos = _animator.GetIKPosition(goal);
            Quaternion animRot = _animator.GetIKRotation(goal);

            float w = masterWeight * GetParam(curveParam, 1f);
            if (w <= 0f)
            {
                _animator.SetIKPositionWeight(goal, 0f);
                _animator.SetIKRotationWeight(goal, 0f);
                return;
            }

            Vector3 origin = animPos + Vector3.up * rayUpOffset;
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit,   
                    rayLength + rayUpOffset, groundMask, QueryTriggerInteraction.Ignore))
            {
                Vector3 target = new Vector3(animPos.x, hit.point.y + footHeight, animPos.z);
                //애초에 로컬 회전으로 넘어오기 때문에 법선 벡터에 대한 회전만 주면 된다.
                Quaternion aligned = Quaternion.FromToRotation(Vector3.up, hit.normal) * animRot;

                _animator.SetIKPosition(goal, target);
                _animator.SetIKPositionWeight(goal, w);

                _animator.SetIKRotation(goal, aligned);
                _animator.SetIKRotationWeight(goal, w);
            }
            else
            {
                _animator.SetIKPositionWeight(goal, 0f);
                _animator.SetIKRotationWeight(goal, 0f);
            }
        }

        private float GetParam(string curveParam, float fallback)
        {
            foreach (AnimatorControllerParameter param in _animator.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Float && param.name == curveParam)
                    return _animator.GetFloat(param.name);
            }

            return fallback;
        }
    }
}