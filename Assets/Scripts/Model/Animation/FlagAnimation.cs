using DG.Tweening;
using UnityEngine;

public class FlagAnimation
{
    private Sequence _sequence;

    public void MoveFlag(Transform transform, Vector3 target)
    {
        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOMove(target, 0.5f))
            .SetEase(Ease.Flash)
            .OnComplete(() => { Object.Destroy(transform.gameObject); });
    }
}