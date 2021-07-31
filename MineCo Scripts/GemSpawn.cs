using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSpawn : MonoBehaviour
{
    public Button gemSprite;
    public IdleGame idleGame;
    public bool showGem;
    public float spawnTimer;
    public float gemCD, gemRangeCD, tempGemCD;
    public float posX, posY;
    int gemAmount;
    public PopupText popupRewardText;
    public List<GameObject> spawnAreaList;

    public void Update()
    {
        if (!showGem)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= gemCD)
            {
                showGem = true;
                Spawn();
            }
        }
    }

    public void SetPosition()
    {
        posX = Random.Range(-79, 80);
        int i = Random.Range(0, 7);
        posY = Random.Range(spawnAreaList[i].transform.position.y + 5, spawnAreaList[i].transform.position.y - 5);
        gemSprite.transform.position = new Vector3(posX, posY, spawnAreaList[i].transform.position.z);
    }

    public void CollectGemButton()
    {
        gemCD = UnityEngine.Random.Range(tempGemCD, tempGemCD + gemRangeCD);
        idleGame.gems += gemAmount;
        Reset();
        idleGame.UpdateCurrencyText();
        popupRewardText.SetGemReward("+" + gemAmount.ToString() + " <sprite=5>");
        popupRewardText.ShowPopup();
    }

    public void Spawn()
    {
        gemAmount = Random.Range(1, 6);
        SetPosition();
        gemSprite.gameObject.SetActive(true);
    }

    public void Reset()
    {
        spawnTimer = 0;
        showGem = false;
        gemSprite.gameObject.SetActive(false);
    }
}
