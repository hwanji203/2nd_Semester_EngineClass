using System;
using UnityEngine;

namespace _Scripts.Riggings
{
    public class FootIK : MonoBehaviour
    {
        [Serializable]
        public class Leg
        {
            public Transform footBone;
            public Transform ikTarget;

            [NonSerialized] public Quaternion restLocal;
            [NonSerialized] public Vector3 dbgOrigin, dbgHit;
            [NonSerialized] public bool dbgHitFlag;
        }

        [SerializeField] private Leg leftLeg = new Leg();
        [SerializeField] private Leg rightLeg = new Leg();

        [SerializeField] private Transform characterRoot; //애니메이터가 존재하는 루트 트랜스폼
        
        [Header("Raycast info")]
        [SerializeField] private LayerMask groundMask = ~0;
        [SerializeField] private float rayUpOffset = 0.5f; //발 위치에서 얼마나 위에서 쓸 건지
        [SerializeField] private float rayLength = 1.5f;
        [SerializeField] private float footHeight = 0.12f;
        
        //리깅할 때 움직임을 부드럽게 러프하기 위한 수치.
        [Range(0, 1f)] [SerializeField] private float positionBlend = 1f;
        [Range(0, 1f)] [SerializeField] private float rotationBlend = 1f;
        
        //디버그용
        [SerializeField] private bool drawGizmos = true;

        private bool _captured; //초기 회전치를 캡쳐해두었는지를 기록하는 변수.

        private void LateUpdate()
        {
            if (!_captured)
            {
                //왼쪽 다리와 오른쪽 다리의 초기 값을 켭쳐해둔다.
                CaptureRest(leftLeg);
                CaptureRest(rightLeg);
                _captured = true;
            }

            //왼쪽과 오른쪽 다리의 위치와 회전을 Solving한다.
            Solve(leftLeg);
            Solve(rightLeg);
        }

        private void CaptureRest(Leg leg)
        {
            if (leg != null && leg.footBone != null)
            {
                //부모가 360도 돌아있고 내가 200도 돌아있다면
                //부모를 역산하면 -360이 되고 그거를 200에 곱해주면 leg의 로컬 회전이 -160이라는걸 알 수 있다.
                leg.restLocal = Quaternion.Inverse(characterRoot.rotation) * leg.footBone.rotation;
            }
        }

        private void Solve(Leg leg)
        {
            if (leg == null || leg.footBone == null || leg.ikTarget == null) return;

            Vector3 footPos = leg.footBone.position;
            Vector3 origin = footPos + Vector3.up * rayUpOffset;
            leg.dbgOrigin = origin;

            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit,
                    rayUpOffset + rayLength, groundMask, QueryTriggerInteraction.Ignore))
            {
                leg.dbgHit = hit.point;
                leg.dbgHitFlag = true;

                Vector3 target = footPos;
                target.y = hit.point.y + footHeight;
                leg.ikTarget.position = Vector3.Lerp(footPos, target, positionBlend);

                //캐릭터 기준 발 기본 회전을 현재 캐릭터 방향으로 복원시킨다.
                Quaternion flatWorld = characterRoot.rotation * leg.restLocal;
                //월드 Up을 지면 법선 만큼만 기울인다. (누적 아님)
                Quaternion tilt = Quaternion.FromToRotation(characterRoot.up, hit.normal);
                Quaternion aligned = tilt * flatWorld; //플랫 월드의 값에 tilt만큼을 곱하면 그게 목표 회전치이다.
                leg.ikTarget.rotation = Quaternion.Slerp(flatWorld, aligned, rotationBlend);
            }
            else
            {
                leg.dbgHitFlag = false;
                leg.ikTarget.position = footPos;
                leg.ikTarget.rotation = characterRoot.rotation * leg.restLocal;
            }
        }
    }
}