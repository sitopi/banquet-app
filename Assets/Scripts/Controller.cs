
using System;
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

        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");
                //4.結果確認
                Debug.Log(request.downloadHandler.text);
                break;

            case UnityWebRequest.Result.ConnectionError:
                Debug.Log
                (
                    @"サーバとの通信に失敗。リクエストが接続できなかった、セキュリティで保護されたチャネルを確立できなかったなど。"
                );
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log
                (
                    @"サーバがエラー応答を返した。サーバとの通信には成功したが、接続プロトコルで定義されているエラーを受け取った。"
                );
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log
                (
                    @"データの処理中にエラーが発生。リクエストはサーバとの通信に成功したが、受信したデータの処理中にエラーが発生。データが破損しているか、正しい形式ではないなど。"
                );
                break;

            default: throw new ArgumentOutOfRangeException();
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
