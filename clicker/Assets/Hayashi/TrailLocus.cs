using UnityEngine;

public class TrailLocus : MonoBehaviour
{
    [Header("É}ÉEÉXÇÃãOê’")]
    [SerializeField] Gradient _normalLocus;
    [SerializeField] Gradient _infinityLocus;

    TrailRenderer _trailLocus;

    void Awake()
    {
        _trailLocus = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        var wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wPos.z = 0f;
        transform.position = wPos;

        if(InfinitMouseManager.Instance._isInfinityTime)
        {
            _trailLocus.colorGradient = _infinityLocus;
        }
        else
        {
            _trailLocus.colorGradient = _normalLocus;
        }
        _trailLocus.emitting = Input.GetMouseButton(0);
    }
}
