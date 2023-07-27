using UnityEngine;
using UnityEngine.UI;

public class MineView : MonoBehaviour, IMineView, IPoolable<MineView>

{
    [SerializeField] private float _scale = 0.7f;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Color _explosionColor = Color.red;
    private Transform _parent;
    private Image _imageBackGround;
    private Color _colorBackGround;
    public RectTransform RectTransform => _rectTransform;


    private Color _color;

    private void Awake()
    {
        _parent = transform.parent;
        transform.localScale = new Vector3(_scale, _scale, _scale);
        transform.gameObject.SetActive(false);
        _imageBackGround = _parent.GetComponent<Image>();
        _colorBackGround = _imageBackGround.color;
    }


    public void SetActive(bool value)
    {
        transform.gameObject.SetActive(value);
    }


    public void ActivateMine(bool isExposion = false)
    {
        gameObject.SetActive(true);
        if (isExposion == true)
            _imageBackGround.color = _explosionColor;
    }

    public void Reset()
    {
        _imageBackGround.color = _colorBackGround;
    }

    public void SpawnFrom(IPool<MineView> pool)
    {
    }

    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }
}