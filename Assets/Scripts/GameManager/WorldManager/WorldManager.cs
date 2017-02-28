using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorldManager : BaseManager {

    WorldManagerData data;
    public static LayerMask levelCollisionLayer { get {return GameManager.instance.worldManager.data.levelCollisionLayer; } }
    public override void Init()
    {
        base.Init();

        data = Resources.Load<WorldManagerData>("Data/Manager/WorldManager");
        
        
    }
}
