using System;
using UnityEngine;

[Serializable]
public class SoundDatas
{
    [SerializeField] private AudioClip _clickCellClip;
    [SerializeField] private AudioClip _mineExplodeClip;
    [SerializeField] private AudioClip _flagSetClip;
    [SerializeField] private AudioClip _emptyOpenCellsClip;

    public AudioClip ClickCellClip => _clickCellClip;
    public AudioClip MineExplodeClip => _mineExplodeClip;
    public AudioClip FlagSetupClip => _flagSetClip;
    public AudioClip EmptyOpenCellsClip => _emptyOpenCellsClip;

}
