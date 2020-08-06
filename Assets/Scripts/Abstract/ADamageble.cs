using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADamageble : MonoBehaviour, IDamageble
{
    [SerializeField] private DamagebleParamDatas datas;

    [SerializeField] private GameObject popup;

    [SerializeField] private float popupDestroyTime = 2f;

    [SerializeField] private float popupRangeTime=1f;

    public void ApplyDamage(DamageByType weapon)
    {
        var allWeak = datas.FindAllByWeak(weapon.DamageType);

        Debug.Log("Дамаг вызывался");
        if (allWeak != null)
        {
            //for (int i = 0; i < allWeak.Count; i++)
            //{
            //    var tmpPopup = Instantiate(popup, transform.position, Quaternion.identity);
            //    tmpPopup.GetComponentInChildren<TextMesh>().text = $"Damage {allWeak[i].Type}: {weapon.Value}";
            //    Destroy(tmpPopup, popupDestroyTime);
            //    Debug.Log("Слабость у " + allWeak[i].Type + " " + weapon.DamageType);
            //    yield return new WaitForSeconds(popupRangeTime);
            //}

            foreach (var weak in allWeak)
            {
                Debug.Log("Слабость у " + weak.Type + " " + weapon.DamageType);
                PopupCreate(weak, weapon);
            }

        }
    }



    private /*IEnumerator*/ void PopupCreate(DamagebleParam param, DamageByType weapon)
    {

        var tmpPopup = Instantiate(popup, transform.position, Quaternion.identity);
        tmpPopup.GetComponentInChildren<TextMesh>().text = $"Damage {param.Type}: {weapon.Value}";
        Destroy(tmpPopup, popupDestroyTime);
        Debug.Log("Слабость у " + param.Type + " " + weapon.DamageType);
        //yield return new WaitForSeconds(popupRangeTime);

    }
}
