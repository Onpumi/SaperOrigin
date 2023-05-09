using Sirenix.OdinInspector;
using UnityEngine;
    
public class Sounds : SerializedMonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private SoundDatas _soundDatas;
    private Q3DAudioSource _q3DAudioSource;
    private SoundAction _soundAction;
    public GameField GameField => _gameState.Views.GameField;


    private void Awake()
    {
       _q3DAudioSource = GetComponent<Q3DAudioSource>();
       _soundAction = new SoundAction();
       _soundAction.AddClip(TypesAudio.SoundClick, _soundDatas.ClickCellClip );
       _soundAction.AddClip(TypesAudio.SoundEmpty, _soundDatas.EmptyOpenCellsClip );
       _soundAction.AddClip(TypesAudio.SoundExplode, _soundDatas.MineExplodeClip );
       _soundAction.AddClip(TypesAudio.SoundFlag, _soundDatas.FlagSetupClip );
    }

    public void PlayAudio( TypesAudio typesAudio)
    {
       if ( GameField.DataSetting.AudioData.GetValue(typesAudio))
       {
            _q3DAudioSource.mAudioSource.clip = _soundAction.GetClip(typesAudio);
            _q3DAudioSource.mAudioSource.Play();
       }

    }

    
}

