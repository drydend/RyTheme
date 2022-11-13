using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayListMenuHandler : MonoBehaviour
{
    [SerializeField]
    private Button _playButton;

    private PlaylistLevelLoader _levelLoader;
    private PlayerInput _playerInput;
    private ChainMenu<ChainMenuPlaylistItem> _chainMenu;

    private Coroutine _chainMenuItemsAnimation;

    [Inject]
    public void Construct(PlayerInput input, PlaylistLevelLoader levelLoader)
    {
        _playerInput = input;
        _levelLoader = levelLoader;
    }

    public void Initialize(ChainMenu<ChainMenuPlaylistItem> chainMenu)
    {
        _chainMenu = chainMenu;

        _playerInput.OnMouseSrollDeltaChanged += ScrollMenu;
        _playButton.onClick.AddListener(PlaySelectedLevel);

        _chainMenuItemsAnimation = StartCoroutine(_chainMenu.PlaySwitchAnimationCoroutine());
    }

    private void OnEnable()
    {
        if (_chainMenuItemsAnimation == null)
        {
            return;
        }

        _chainMenu.ResetItemsPosition();
        _chainMenuItemsAnimation = StartCoroutine(_chainMenu.PlaySwitchAnimationCoroutine());
    }

    private void OnDisable()
    {
        if (_chainMenuItemsAnimation == null)
        {
            return;
        }

        StopCoroutine(_chainMenuItemsAnimation);
    }

    private void ScrollMenu()
    {
        if (_playerInput.MouseScrolDelta > 0)
        {
            _chainMenu.SwitchItemsPosition(ChainMenuDirection.Right);
        }
        else
        {
            _chainMenu.SwitchItemsPosition(ChainMenuDirection.Left);
        }
    }

    private void PlaySelectedLevel()
    {
        _levelLoader.PlayPlaylistLevel(_chainMenu.SelectedItem.LevelData);
    }
}
