﻿using UnityEngine;

namespace NVTT.InAppPurchase
{
	/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object();

	public static bool IsExist
	{
		get
		{
			if (_instance == null)
				_instance = (T) FindObjectOfType(typeof(T));
			return _instance != null;
		}
	}

	public static T Instance {
		get {
			if (applicationIsQuitting) {
				Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
				return null;
			}

			lock (_lock) {
				if (_instance != null) return _instance;
				
				_instance = (T)FindObjectOfType(typeof(T));

				if (FindObjectsOfType(typeof(T)).Length > 1) {
					Debug.LogError("[Singleton] Something went really wrong " +
					               " - there should never be more than 1 singleton!" +
					               " Reopening the scene might fix it.");
					return _instance;
				}

				if (_instance == null) {
					var singleton = new GameObject();
					_instance = singleton.AddComponent<T>();
					singleton.name = "(singleton) " + typeof(T);

					Debug.Log("[Singleton] An instance of " + typeof(T) +
					          " is needed in the scene, so '" + singleton +
					          "' was created with DontDestroyOnLoad.");
				} else {
					Debug.Log("[Singleton] Using instance already created: " +
					          _instance.gameObject.name);
				}

				return _instance;
			}
		}
	}

	private void Awake()
	{
		applicationIsQuitting = false;
		if (_instance == null)
		{
			_instance = gameObject.GetComponent<T>();
			AwakeIfExist();
		}
		else if (_instance != this)
		{
			isDestroyByDuplicate = true;
			Destroy(gameObject);
		}
	}

	protected virtual void AwakeIfExist()
	{
	}

	private static bool applicationIsQuitting;
	private bool isDestroyByDuplicate;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	public virtual void OnDestroy()
	{
		var isDDOL = gameObject.scene.buildIndex == -1;
		if (!isDestroyByDuplicate && isDDOL)
			applicationIsQuitting = true;
	}
}
}
