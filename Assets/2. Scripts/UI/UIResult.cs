using TMPro;
using UnityEngine;

public class UIResult : MonoBehaviour
{
    [SerializeField] private TMP_Text _uiFinalWaveText;

    public void SetWaveText(int finalWave)
    {
        _uiFinalWaveText.text = finalWave.ToString();
    }
}
