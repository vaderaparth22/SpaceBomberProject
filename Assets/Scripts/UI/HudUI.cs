using Level;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Managers;

public class HudUI : UI
{
    private Text _bombText;
    private Text _waveText;
    private Text _scoreText;
    private GameLevel _level;

    [SerializeField] private GameObject[] bombIcons;
    private static Dictionary<BombType, GameObject> bombIconDictionary;

    public void Initialize(GameLevel level)
    {
        _level = level;
        _waveText = GameObject.Find("WaveText").GetComponent<Text>();
        _bombText = GameObject.Find("BombText").GetComponent<Text>();
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        bombIconDictionary = new Dictionary<BombType, GameObject>();
        bombIconDictionary.Add(BombType.FuseBomb, bombIcons[0]);
        bombIconDictionary.Add(BombType.ImpactBomb, bombIcons[1]);
        bombIconDictionary.Add(BombType.LandMine, bombIcons[2]);
    }
    public void Refresh()
    {
       
            UpdateBombUI();
            UpdateWaveUI();
            UpdateScoreUI();

    }

    private void UpdateWaveUI()
    {
        _waveText.text = $"Wave { _level.CurrentWave}";
    }
    
    private void UpdateScoreUI()
    {
        _scoreText.text = $"Score { EnemyManager.Instance.Score}";
    }
    
    private void UpdateBombUI()
    {
        
            if (_bombText)
            {

                if (AmmoManager.Instance.IsBombEquiped)
                {
                    _bombText.text = "1";
                }
                else
                {
                    _bombText.text = "0";
                }

            }
            else
            {

                throw new System.Exception("BombIndication text is Null");
            }
    }

    public static void SetBombIcon(BombType typeToActivate)
    {
        foreach (var item in bombIconDictionary)
        {
            if(item.Value.activeSelf)
                item.Value.SetActive(false);
        }

        bombIconDictionary[typeToActivate].SetActive(true);
    }
}
