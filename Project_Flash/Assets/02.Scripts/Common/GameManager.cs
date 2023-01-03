using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public static GameManager instance;
    public GameObject playerPrefab;
    public GameObject magneticPrefab;
    public Transform startPoint;
    
    private GameObject player;
    private GameObject magnetic;
    private Magnetic_FieldMove magnetic_FieldMove;

    public float reviveTime;
    void Start() // √÷√  Ω««‡ Ω√ ΩÃ±€≈Ê Ω««‡ « ø‰ Ω√ »∞º∫»≠«“ ∞Õ
    {
        if (DataManager.instance.GetSavePoint() == null)
        {
            DataManager.instance.SetSavePoint(startPoint.position);
        }
        // ΩÃ±€≈Ê
        //if (instance == null) instance = this;
        //else if (instance != this) Destroy(this.gameObject);
        //DontDestroyOnLoad(gameObject);
        StageStart();
    }

    public void StageStart()
    {
        player = Instantiate(playerPrefab, DataManager.instance.GetSavePoint(), Quaternion.identity);
        magnetic = Instantiate(magneticPrefab, DataManager.instance.GetSavePoint()+ Vector3.down * 10.0f, Quaternion.identity);
        magnetic_FieldMove = this.magnetic.GetComponent<Magnetic_FieldMove>();
        magnetic_FieldMove.ReSetPosition(player.transform);
    }

    IEnumerator StageRestart()
    {
        yield return new WaitForSeconds(reviveTime);
        player = Instantiate(playerPrefab, DataManager.instance.GetSavePoint(), Quaternion.identity);
        magnetic_FieldMove.ReSetPosition(player.transform);
    }
    public void CoroutineStageRestart()
    {
        StartCoroutine(nameof(StageRestart));
    }
    
    public void SetMagnetic(GameObject magnetic)
    {
        this.magnetic = magnetic;
    }
}
