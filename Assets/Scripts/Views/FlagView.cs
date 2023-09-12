using UnityEngine;
using UnityEngine.UI;

public class FlagView : MonoBehaviour, IFlagView, IPoolable<FlagView>
{
    [SerializeField] private Sprite _spriteOriginal;
    [SerializeField] private Sprite _spriteError;
    private Image _image;
    public bool Value { get; private set; }

    private void Awake()
    {
        gameObject.SetActive(false);
        _image = GetComponent<Image>();
        Value = false;
    }

    private void OnEnable()
    {
        Value = true;
    }

    private void OnDisable()
    {
        Value = false;
    }

    public void SetFlagError()
    {
        _image.sprite = _spriteError;
        transform.localScale = Vector3.one;
    }

    public void ResetSprite()
    {
        _image.sprite = _spriteOriginal;
    }


    public void Display()
    {
        transform.gameObject.SetActive(true);
        transform.SetAsFirstSibling();
    }

    public void SetActive(bool value) => transform.gameObject.SetActive(value);

    public void InitFlag(bool value)
    {
        // тут поставить сразу true если настройка вкл анимации не стоит
        ShowFlag(false);
        transform.gameObject.SetActive(value);
        transform.localScale = Vector3.one / 1.5f;
    }

    public void SpawnFrom(IPool<FlagView> pool)
    {
        transform.gameObject.SetActive(true);
    }


    public void ShowFlag(bool value)
    {
        int alphaValue = (value == false) ? (0) : (255);
        var color = _image.color;
        color.a = alphaValue;
        _image.color = color;
    }

    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }
}