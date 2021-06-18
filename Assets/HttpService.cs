using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
 
public class HttpService : MonoBehaviour
{
    /// <summary>
    /// サーバへGETリクエストを送信
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="requestParams"></param>
    /// <returns></returns>
    public static IEnumerator Get<T>(string url, IDictionary<string, string> requestParams = null)
    {
        string requestUrl = url;
 
        // リクエストパラメータがある場合はURLに結合
        if (requestParams != null) {
            requestUrl += "?";
 
            // パラメータ文keyとvalueを結合
            foreach (var requestParam in requestParams) {
                requestUrl += requestParam.Key + "=" + requestParam.Value + "&";
            }
 
            // 後ろの&を削除
            requestUrl = requestUrl.Substring(0, requestUrl.Length - 1);
        }
 
        var request = UnityWebRequest.Get(requestUrl);
        Debug.Log("request : " + request);
        // リクエスト送信
        yield return request.SendWebRequest();
 
        // エラー判定
        if (request.isHttpError || request.isNetworkError) {
            throw new Exception("通信に失敗しました。(" + request.error + ")");
        }
 
        yield return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }
 
    /// <summary>
    /// サーバへPOSTリクエストを送信
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="requestParams"></param>
    /// <returns></returns>
    public static IEnumerator Post<T>(string url, IDictionary<string, string> requestParams)
    {
        var request = UnityWebRequest.Post(url, (Dictionary<string, string>)requestParams);
 
        // リクエスト送信
        yield return request.SendWebRequest();
 
        // エラー判定
        if (request.isHttpError || request.isNetworkError) {
            throw new Exception("通信に失敗しました。(" + request.error + ")");
        }
 
        yield return JsonUtility.FromJson<T>(request.downloadHandler.text);
    }
}