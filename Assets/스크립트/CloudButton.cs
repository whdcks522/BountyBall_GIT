using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudButton : MonoBehaviour
{
    public void LoadGold() {
        
        PlayCloudDataManager.Instance.LoadFromCloud((string dataToLoad) =>
        { StageController.Instance.goggleNum = long.Parse(dataToLoad); });

        StageController.Instance.gamemanager.rememberW = (int)(StageController.Instance.goggleNum / 100);
        StageController.Instance.gamemanager.rememberS = (int)(StageController.Instance.goggleNum % 100);
        PlayerPrefs.SetInt("world", StageController.Instance.gamemanager.rememberW);
        PlayerPrefs.SetInt("stage", StageController.Instance.gamemanager.rememberS);
        PlayerPrefs.Save();


        StageController.Instance.gamemanager.RememberText.text = 
            "Save) World " + StageController.Instance.gamemanager.rememberW + " - Stage " + StageController.Instance.gamemanager.rememberS;
        
    }

    public void SaveGold() {
        //if (playerLogic.ignite) return;
        //playerLogic.mana = 0;
        //gamemanager.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //gamemanager.player.transform.position = gamemanager.stagecontroller.WorldPos;

        PlayCloudDataManager.Instance.SaveToCloud(StageController.Instance.goggleNum.ToString());
  
    }
    
}

