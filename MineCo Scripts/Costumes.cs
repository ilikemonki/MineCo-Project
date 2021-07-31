using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class Costumes : MonoBehaviour
{
    public string id;
    public CostumeBuyBtn costumeBuyBtn;
    public string costumeName;
    public string description;
    public Image scrollCostumeImage;
    //Costume
    public Sprite spriteCostume;
    public RuntimeAnimatorController animatorController;
    public bool isUnlocked, filteredIn;

    public void CostumeSelectedButton()
    {
        costumeBuyBtn.SetBuyBtn(this);
    }

    public void Load(GameData.CostumesSaveData c)
    {
        isUnlocked = c.isUnlocked;
        filteredIn = c.filteredIn;
    }



}
