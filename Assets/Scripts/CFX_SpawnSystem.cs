using System;
using System.Collections.Generic;
using UnityEngine;

public class CFX_SpawnSystem : MonoBehaviour
{
	private static CFX_SpawnSystem instance;

	public GameObject[] objectsToPreload = new GameObject[0];

	public int[] objectsToPreloadTimes = new int[0];

	public bool hideObjectsInHierarchy;

	private bool allObjectsLoaded;

	private Dictionary<int, List<GameObject>> instantiatedObjects = new Dictionary<int, List<GameObject>>();

	private Dictionary<int, int> poolCursors = new Dictionary<int, int>();

	public static bool AllObjectsLoaded
	{
		get
		{
			return CFX_SpawnSystem.instance.allObjectsLoaded;
		}
	}

	public static GameObject GetNextObject(GameObject sourceObj, bool activateObject = true)
	{
		int instanceID = sourceObj.GetInstanceID();
		if (!CFX_SpawnSystem.instance.poolCursors.ContainsKey(instanceID))
		{
			UnityEngine.Debug.LogError(string.Concat(new object[]
			{
				"[CFX_SpawnSystem.GetNextPoolObject()] UnityEngine.Object hasn't been preloaded: ",
				sourceObj.name,
				" (ID:",
				instanceID,
				")"
			}));
			return null;
		}
		int index = CFX_SpawnSystem.instance.poolCursors[instanceID];
		Dictionary<int, int> dictionary;
		int key;
		(dictionary = CFX_SpawnSystem.instance.poolCursors)[key = instanceID] = dictionary[key] + 1;
		if (CFX_SpawnSystem.instance.poolCursors[instanceID] >= CFX_SpawnSystem.instance.instantiatedObjects[instanceID].Count)
		{
			CFX_SpawnSystem.instance.poolCursors[instanceID] = 0;
		}
		GameObject gameObject = CFX_SpawnSystem.instance.instantiatedObjects[instanceID][index];
		if (activateObject)
		{
			gameObject.SetActive(true);
		}
		return gameObject;
	}

	public static void PreloadObject(GameObject sourceObj, int poolSize = 1)
	{
		CFX_SpawnSystem.instance.addObjectToPool(sourceObj, poolSize);
	}

	public static void UnloadObjects(GameObject sourceObj)
	{
		CFX_SpawnSystem.instance.removeObjectsFromPool(sourceObj);
	}

	private void addObjectToPool(GameObject sourceObject, int number)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			this.instantiatedObjects.Add(instanceID, new List<GameObject>());
			this.poolCursors.Add(instanceID, 0);
		}
		for (int i = 0; i < number; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(sourceObject);
			gameObject.SetActive(false);
			CFX_AutoDestructShuriken[] componentsInChildren = gameObject.GetComponentsInChildren<CFX_AutoDestructShuriken>(true);
			CFX_AutoDestructShuriken[] array = componentsInChildren;
			for (int j = 0; j < array.Length; j++)
			{
				CFX_AutoDestructShuriken cFX_AutoDestructShuriken = array[j];
				cFX_AutoDestructShuriken.OnlyDeactivate = true;
			}
			CFX_LightIntensityFade[] componentsInChildren2 = gameObject.GetComponentsInChildren<CFX_LightIntensityFade>(true);
			CFX_LightIntensityFade[] array2 = componentsInChildren2;
			for (int k = 0; k < array2.Length; k++)
			{
				CFX_LightIntensityFade cFX_LightIntensityFade = array2[k];
				cFX_LightIntensityFade.autodestruct = false;
			}
			this.instantiatedObjects[instanceID].Add(gameObject);
			if (this.hideObjectsInHierarchy)
			{
				gameObject.hideFlags = HideFlags.HideInHierarchy;
			}
		}
	}

	private void removeObjectsFromPool(GameObject sourceObject)
	{
		int instanceID = sourceObject.GetInstanceID();
		if (!this.instantiatedObjects.ContainsKey(instanceID))
		{
			UnityEngine.Debug.LogWarning(string.Concat(new object[]
			{
				"[CFX_SpawnSystem.removeObjectsFromPool()] There aren't any preloaded object for: ",
				sourceObject.name,
				" (ID:",
				instanceID,
				")"
			}));
			return;
		}
		for (int i = this.instantiatedObjects[instanceID].Count - 1; i >= 0; i--)
		{
			GameObject obj = this.instantiatedObjects[instanceID][i];
			this.instantiatedObjects[instanceID].RemoveAt(i);
			UnityEngine.Object.Destroy(obj);
		}
		this.instantiatedObjects.Remove(instanceID);
		this.poolCursors.Remove(instanceID);
	}

	private void Awake()
	{
		if (CFX_SpawnSystem.instance != null)
		{
			UnityEngine.Debug.LogWarning("CFX_SpawnSystem: There should only be one instance of CFX_SpawnSystem per Scene!");
		}
		CFX_SpawnSystem.instance = this;
	}

	private void Start()
	{
		this.allObjectsLoaded = false;
		for (int i = 0; i < this.objectsToPreload.Length; i++)
		{
			CFX_SpawnSystem.PreloadObject(this.objectsToPreload[i], this.objectsToPreloadTimes[i]);
		}
		this.allObjectsLoaded = true;
	}
}
