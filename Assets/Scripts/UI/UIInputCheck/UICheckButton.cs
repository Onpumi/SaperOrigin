using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class UICheckButton : MonoBehaviour
{
    [SerializeField] private Sprite _spriteCheckOn;
    [SerializeField] private Sprite _spriteCheckOff;
    private Image _image;

    public void SetSprite(bool ChangeValue)
    {
        _image ??= GetComponent<Image>();
        _image.sprite = (ChangeValue) ? (_spriteCheckOn) : (_spriteCheckOff);
    }
}