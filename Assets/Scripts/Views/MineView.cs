using UnityEngine;
using  UnityEngine.UI;

public class MineView : MonoBehaviour, IMineView, IPoolable<MineView>

{
    [SerializeField] private float _scale = 0.7f;
    private Transform _parent;
    private void Awake()
    {
        transform.localScale = new Vector3(_scale, _scale,_scale);
        transform.gameObject.SetActive(false);
        _parent = transform.parent;
    }

    public float GetWidth()
    {
        return GetComponent<Image>().sprite.rect.width;
    }

    public void SetActive(bool value)
    {
        transform.gameObject.SetActive( value );
    }

    public float GetHeight()
    {
        return GetComponent<Image>().sprite.rect.height;
    }
    
    public void ActivateMine( bool isExposion = false )
    {
        gameObject.SetActive(true);
      //  if( isExposion == true ) _parent.GetComponent<Image>().color = Color.red;  // исправить это!!
        transform.localScale = Vector3.one * _scale;
    }

    public Transform GetTransform() => transform;
    
    public void SpawnFrom( IPool<MineView> pool )
    {
        //transform.gameObject.SetActive(true);
    }

        
    public void Despawn()
    {
        transform.gameObject.SetActive(false);
    }

}
