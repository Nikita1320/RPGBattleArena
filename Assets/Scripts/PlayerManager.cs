using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public static GameObject selectedCharacter;
    private static PlayerManager instance;

    private PlayerManager()
    {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerManager();
            }

            return instance;
        }
    }
}
