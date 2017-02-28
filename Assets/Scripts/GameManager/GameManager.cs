using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameManager = GameObject.Find("GameManager");
                if (gameManager != null)
                {
                    _instance = gameManager.GetComponent<GameManager>();
                }
                else
                {
                    GameObject go = new GameObject();
                    go.name = "GameManager";
                    _instance=go.AddComponent<GameManager>();


                }
            }
            return _instance;
        }
    }
    private static GameManager _instance;
    public List<BaseManager> managerList = new List<BaseManager>();
    public SoundManager soundManager = new SoundManager();
    public SaveManager saveManager = new SaveManager();
    public PoolManager poolManager = new PoolManager();
    public WorldManager worldManager = new WorldManager();
    public BulletEngineManager bulletEngine = new BulletEngineManager();
    // Use this for initialization
    void Awake () {
        if (_instance == null) { _instance = this; }
        if (_instance != this) { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        Init();
    }
	void Init()
    {
        managerList.Add(soundManager);
        managerList.Add(saveManager);
        managerList.Add(poolManager);
        managerList.Add(worldManager);
        managerList.Add(bulletEngine);
        for (int i = 0; i < managerList.Count; i++)
        {
            managerList[i].Init();
        }
    }
	// tick all manager
	void Update () {
        for (int i = 0; i < managerList.Count; i++)
        {
            managerList[i].Tick();
        }
    }



    /// <summary>
    /// application
    /// save
    /// pause 
    /// and Quit 
    /// </summary>
    #region application
    public void SaveGame()
    {

    }
    public void PauseGame(bool p)
    {
        for (int i = 0; i < managerList.Count; i++)
        {
            managerList[i].Pause(p);
        }
    }
    public void QuitGame()
    {

    }
    #endregion
}
