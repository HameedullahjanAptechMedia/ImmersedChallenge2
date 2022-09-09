using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JsonDataManager : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int id;
        public string name;
        public string url;
        public string description;
    }
    [System.Serializable]

    public class Root
    {
        public int id;
        public string name;
        public List<Item> items;
    }
    public static JsonDataManager instance;
    public List<Root> loadedData ;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        var jsonTextFile = Resources.Load<TextAsset>("test");
        if (jsonTextFile != null)
        {
            loadedData = JsonConvert.DeserializeObject<List<Root>>(jsonTextFile.text);
            UIManager.instance.LoadAllCategories();
        }
    }
}
