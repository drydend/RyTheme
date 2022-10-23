using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class PlayListMenuHandler: MonoBehaviour
    {
        private PlayerInput _playerInput;
        ChainMenu<ChainMenuPlaylistItem> _chainMenu;

        [Inject]
        public void Construct(PlayerInput input)
        {
            _playerInput = input;
        }

        public void Initialize(ChainMenu<ChainMenuPlaylistItem> chainMenu)
        {
            _chainMenu = chainMenu;

            _playerInput.OnMouseSrollDeltaChanged += ScrollMenu;
        }

        private void ScrollMenu()
        {
            if(_playerInput.MouseScrolDelta > 0)
            {
                _chainMenu.SwitchItemsPosition(ChainMenuDirection.Right);
            }
            else
            {
                _chainMenu.SwitchItemsPosition(ChainMenuDirection.Left);
            }
        }
    }
}