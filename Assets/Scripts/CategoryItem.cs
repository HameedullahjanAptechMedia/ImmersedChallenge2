using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CategoryItem : MonoBehaviour
{
    public List<JsonDataManager.Item> myItems = new List<JsonDataManager.Item>();
    UIManager uiManger;
    public void OnClickCategoryItemBtn(bool val)
    {
        if (val)
        {
            if (UIManager.instance != null)
            {
                uiManger = UIManager.instance;
            }
            // clear the childern objects
            foreach (Transform _t in uiManger.itemParent.transform)
            {
                Destroy(_t.gameObject);
            }
            for (int i = 0; i < myItems.Count; i++)
            {
                GameObject _item = Instantiate(uiManger.item);
                _item.transform.SetParent (uiManger.itemParent.transform);
                _item.transform.localScale = Vector3.one;
                _item.GetComponentInChildren<TextMeshProUGUI>().text = myItems[i].name;
                _item.GetComponent<item>().toolTipText.text = myItems[i].description;
                StartCoroutine(DownloadImage(myItems[i].url, _item.transform.GetChild(0).GetComponentInChildren<Image>()));
            }
        }
    }

    IEnumerator DownloadImage(string MediaUrl, Image _image)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
       
        if (request.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log(request.error);
        else 
        {
            while(!request.downloadHandler.isDone)
            {
                Debug.Log("request not done yet");
                yield return null;
            }    
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            if (_image != null)
            {
                _image.overrideSprite = sprite;
            }
        }
    }
}
