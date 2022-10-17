using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChainMenuButton : MonoBehaviour
{
    [SerializeField]
    private ChainMenuDirection _direction;

    public event Action<ChainMenuDirection> OnPressed; 

    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => OnPressed?.Invoke(_direction));
    }
}
