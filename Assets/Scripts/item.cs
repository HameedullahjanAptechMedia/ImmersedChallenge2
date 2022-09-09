using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public void OnClick()
    {
        AddressablesManager.instance.OnSpawnBtnClicked();
        UIManager.instance.HideStorePanel();
    }
}
