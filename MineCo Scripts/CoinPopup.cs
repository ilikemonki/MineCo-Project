using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPopup : MonoBehaviour
{
    public List<CoinPrefab> coinsPool;
    public CoinPrefab coinPrefab;
    public GameObject spawnPoint;
    public GameObject coinPoolParent;

    public void Start()
    {
        //spawn Coins
        SpawnMoreCoins(10);
    }

    public void SpawnInCoin()
    {
        for (int i = 0; i < coinsPool.Count; i++)
        {
            if (!coinsPool[i].gameObject.activeSelf)
            {
                coinsPool[i].gameObject.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                coinsPool[i].gameObject.SetActive(true);
                coinsPool[i].Pop();
                return;
            }
            else if (i == coinsPool.Count - 2)
            {
                SpawnMoreCoins(5);
                i = 0;
            }
        }
    }

    public void SpawnMoreCoins(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var c = Instantiate(coinPrefab, coinPoolParent.transform);
            c.gameObject.SetActive(false);
            coinsPool.Add(c);
        }
    }
}
