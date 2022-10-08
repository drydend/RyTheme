using System;
using UnityEngine;

public class Cross : MonoBehaviour
{
    [SerializeField]
    private KeyCode _keyCode;

    public event Action OnPressed;

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            OnPressed?.Invoke();
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }

        if (Input.GetKeyUp(_keyCode))
        {
            transform.localScale = new Vector3(1f, 1f, 1);
        }
    }


}
