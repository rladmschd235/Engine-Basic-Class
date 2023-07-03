using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets; // 스캔 결과 배열
    public Transform nearestTarget; // 가장 가까운 타겟

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    private Transform GetNearest()
    {
        // 가장 가까운 몬스터 한 마리 찾겠다
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float currentDiff = Vector3.Distance(myPos, targetPos);

            if(diff > currentDiff)
            {
                diff = currentDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
