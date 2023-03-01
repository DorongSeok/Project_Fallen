using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instace;
    public static Managers Instance { get { Init(); return s_instace; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SoundManager Sound { get { return Instance._sound; } }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instace == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if (obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instace = obj.GetComponent<Managers>();
            s_instace._sound.Init();
        }
    }

    public void Clear()
    {
        Input.Clear();
        Sound.Clear();
    }
}
