using TMPro;
using UnityEngine;

public class ChainMenuPlaylistItem : ChainMenuItem
{
    private ParsedTrackData _trackData;

    [SerializeField]
    private TMP_Text _title;

    public void Initialize(ParsedTrackData parsedTrackData)
    {
        _trackData = parsedTrackData;
        _title.text = parsedTrackData.Title;
    }
}
