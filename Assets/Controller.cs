using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunApi());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator RunApi(){
        Debug.Log("start api"!);

        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8888/get");
        // UnityWebRequest request = UnityWebRequest.Get("http://pricer.com");
        // request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        //3.isNetworkErrorとisHttpErrorでエラー判定
  if(request.isHttpError || request.isNetworkError) {
    //4.エラー確認
    Debug.Log(request.error);
  }
  else{
    //4.結果確認
    Debug.Log(request.downloadHandler.text);
  }

        MstDrunkards characters = JsonUtility.FromJson<MstDrunkards>("{\"mstDrunkards\":" + request.downloadHandler.text + "}");

Debug.Log(characters);
        // id = characterClass.characters[0].id;
        // name = characterClass.characters[0].name;
        // hp = characterClass.characters[0].hp;  
    //     // ランクインしているか判定
    // var httpGet = HttpService.Get<MstDrunkards>(
    //     SystemConstants.RANKING_SERVER_BASE_URL + "/api/get",
    //     new Dictionary<string, string>()
    //     {
    //         // { "game_id", SystemConstants.GAME_ID.ToString() },
    //         // { "score", score.ToString() }
    //     }
    // );
    // yield return StartCoroutine(httpGet);
    // var responseCheckRankin = httpGet.Current as MstDrunkards;
    
    // Debug.Log(responseCheckRankin);
    }
}
