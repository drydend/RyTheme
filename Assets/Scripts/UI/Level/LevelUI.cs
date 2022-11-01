using UnityEngine;

public class LevelUI : UIMenu
{
    [SerializeField]
    private LevelHealsUI _healsUi;
    [SerializeField]
    private LevelScoreUI _levelScoreUI;

    public override void Close()
    {
        base.Close();
        _healsUi.gameObject.SetActive(false);
        _levelScoreUI.gameObject.SetActive(false);
    }

    public override void Open()
    {
        base.Open();
        _healsUi.gameObject.SetActive(true);
        _levelScoreUI.gameObject.SetActive(true);
    }
}
