using DG.Tweening;
using UnityEngine;

public class FlagAnimation
{
    private Sequence _sequence;
    private GameField _gameField;

    public FlagAnimation(GameField gameField)
    {
        _gameField = gameField;
    }

    public void MoveFlag(Transform transform, CellView cellView)
    {
        _sequence = DOTween.Sequence();
        var target = cellView.transform.position;

            _sequence
                .Append(transform.DOMove(target, 0.5f))
                .SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                   transform.gameObject.SetActive(false);
                    _gameField.Sounds.PlayAudio(TypesAudio.SoundFlag);
                    cellView.FlagView.ShowFlag(true);
                });
    }

    public void MoveBackFlag(Transform transform, Vector3 target)
    {

        _sequence = DOTween.Sequence();

        _sequence
            .Append(transform.DOMove(target, 0.5f))
            .SetEase(Ease.Flash)
            .OnComplete(() =>
            {
                Object.Destroy(transform.gameObject);
                _gameField.Sounds.PlayAudio(TypesAudio.SoundFlag);
            });
        
    }
}