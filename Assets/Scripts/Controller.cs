
using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject mDrunkardPrefab;
    public MstDrunkards drunkardsInstance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getDrunkards());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator getDrunkards()
    {
        yield return StartCoroutine(WebRequestManager.Instance.RunApi("/get"));
        MstDrunkards drunkards = JsonUtility.FromJson<MstDrunkards>(WebRequestManager.responseJson);

        for (int i = 0; i < drunkards.mst_drunkards.Length; ++i)
        {
            Drunkard instance = Instantiate(mDrunkardPrefab).GetComponent<Drunkard>();
            instance.m_data = drunkards.mst_drunkards[i];
        }
    }
}
