
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestManager
{
    // インスタンス  
    private static WebRequestManager instance;

    // インスタンスを取得できる唯一のプロパティ
    public static WebRequestManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new WebRequestManager();

                if (null == instance)
                {
                    Debug.Log(" WebRequestManager Instance Error ");
                }
            }

            return instance;
        }
    }

    public static string responseJson = "";
    public static readonly string rootPath = "http://localhost:8000";

    public IEnumerator RunApi(string relativePath)
    {
        responseJson = "";
        string absolutePath = rootPath + relativePath;
        Debug.Log("RUN API : " + absolutePath);

        UnityWebRequest request = UnityWebRequest.Get(absolutePath);
        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");
                //4.結果確認
                responseJson = request.downloadHandler.text;
                Debug.Log(responseJson);
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
    }
}
