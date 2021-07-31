using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI oreText;

    public void Spawn()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.localPosition.x, transform.localPosition.y + 10, transform.localPosition.z),
            "islocal", true, "time", 2f,
            "oncomplete", "Inactive", "oncompletetarget", this.gameObject,
            "easeType", iTween.EaseType.easeOutBack));
    }

    public void Inactive()
    {
        gameObject.SetActive(false);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
