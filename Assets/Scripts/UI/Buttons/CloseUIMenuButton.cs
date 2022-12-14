using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseUIMenuButton : MonoBehaviour
{
    [SerializeField]
    private UIMenu _menu;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => _menu.Close());
    }
}
