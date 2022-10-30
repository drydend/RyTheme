using UnityEngine;

[CreateAssetMenu(menuName = "Chain menu config", fileName = "Chain Menu Config")]
public class ChainMenuConfig : ScriptableObject
{
    [SerializeField]
    private float _distanceBetweenItems;
    [SerializeField]
    private int _startItemIndex;
    [SerializeField]
    private Vector2 _itemsPlacementDirection;
    [SerializeField]
    [Range(0f, 1f)]
    private float _animationLerpFactor = 0.5f;
    [SerializeField]
    private float _animationDurationFactor = 1f;

    public float AnimationDurationFactor => _animationDurationFactor;
    public float AnimationLerpFactor => _animationLerpFactor;
    public float DistanceBetweenItems => _distanceBetweenItems;
    public int StartItemIndex => _startItemIndex;
    public Vector2 ItemsPlacementDirection => _itemsPlacementDirection;
}