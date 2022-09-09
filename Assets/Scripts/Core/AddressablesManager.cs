using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressablesManager : MonoBehaviour
{
    public static AddressablesManager instance;
    [SerializeField]
    private AssetReference chairObject;

    [SerializeField]
    public GameObject chairPrefab;

    public Text logger;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        logger.text += "initialing...\n";
        Addressables.InitializeAsync().Completed += Addressables_InitCompleted;
    }

    private void Addressables_InitCompleted(AsyncOperationHandle<IResourceLocator> obj)
    {
        logger.text += obj.DebugName+ "completed...\n";
        chairObject.LoadAssetAsync<GameObject>().Completed += ObjectLoadingDone;
    }

    private void ObjectLoadingDone(AsyncOperationHandle<GameObject> obj)
    {
        chairPrefab = obj.Result as GameObject;
        logger.text += obj.Result.name+ "...\n";

        
    }
    // Spawn object from cloud
    public void OnSpawnBtnClicked()
    {
        if(chairPrefab!=null)
        {
            GameObject clone = Instantiate(chairPrefab);
            Manager.instance.instantiatedObject = clone;
        }
    }
}
