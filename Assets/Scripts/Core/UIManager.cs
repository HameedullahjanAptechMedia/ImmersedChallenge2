using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject storePanel;
    public GameObject categoriesListParent;
    public GameObject itemParent;

    [Header("Prefabs")]
    public GameObject categoryItem;
    public GameObject item;
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        float _width = itemParent.GetComponent<RectTransform>().rect.width;
        itemParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(_width / 6, _width / 6);
    }
    public void LoadAllCategories()
    {
        List<JsonDataManager.Root> _loadedData = JsonDataManager.instance.loadedData;
        for (int i = 0; i < _loadedData.Count; i++)
        {
            GameObject _categoryItem = Instantiate(categoryItem);
            _categoryItem.transform.SetParent( categoriesListParent.transform);
            _categoryItem.transform.localScale = Vector3.one;
            _categoryItem.GetComponentInChildren<TextMeshProUGUI>().text = _loadedData[i].name;
            _categoryItem.GetComponent<Toggle>().group = categoriesListParent.GetComponent<ToggleGroup>();
            _categoryItem.GetComponent<CategoryItem>().myItems = _loadedData[i].items;
        }
    }
    public void HideStorePanel()
    {
        storePanel.SetActive(false);
    }
    public void ShowStorePanel()
    {
        storePanel.SetActive(true);
    }
}
