using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Controller : MonoBehaviour
{
    public GameObject mDrunkardPrefab;
    public MstDrunkards drunkardsInstance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunApi());
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected IEnumerator RunApi()
    {
        Debug.Log("start api"!);

        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8888/get");
        yield return request.SendWebRequest();

        //3.isNetworkErrorとisHttpErrorでエラー判定
        if (request.isHttpError || request.isNetworkError)
        {
            //4.エラー確認
            Debug.Log(request.error);
        }
        else
        {
            //4.結果確認
            Debug.Log(request.downloadHandler.text);
        }

        string json = "{\"drunkards\":" + request.downloadHandler.text + "}";
        drunkardsInstance = JsonUtility.FromJson<MstDrunkards>(json);

        for (int i = 0; i < drunkardsInstance.drunkards.Length; ++i)
        {
            MstDrunkard prefab = Instantiate(mDrunkardPrefab).GetComponent<Drunkard>().m_data;
            MstDrunkard data = drunkardsInstance.drunkards[i];

            prefab.name = data.name;
            prefab.level = data.level;
            prefab.hp = data.hp;
            prefab.attack = data.attack;
            prefab.move_speed = data.move_speed;
        }
    }
}
