using UnityEngine;

[CreateAssetMenu(menuName = "Chain menu config", fileName = "Chain Menu Config")]
public class ChainMenuConfig : ScriptableObject
{
    [SerializeField]
    private float _distanceBetweenItems;
    [SerializeField]
    private int _startItemIndex;

    public float DistanceBetweenItems => _distanceBetweenItems;
    public int StartItemIndex => _startItemIndex;
}