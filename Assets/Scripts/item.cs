using UnityEngine;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    public GameObject myToolTip;
    public Text toolTipText;

    bool startTimer = false;
    float timer = 0;
    void Update()
    {
        if(startTimer)
        {
            if(timer >= 2)
            {
                myToolTip.SetActive(true);
                timer = 0;
                startTimer = false;
            }
            timer += Time.deltaTime;
        }
    }

    public void OnHover()
    {
        startTimer = true;
        timer = 0;
    }
    public void OnMouseExit()
    {
        myToolTip.SetActive(false);
        timer = 0;
        startTimer = false;
    }
    public void OnClick()
    {
        AddressablesManager.instance.OnSpawnBtnClicked();
        UIManager.instance.HideStorePanel();
    }
}
