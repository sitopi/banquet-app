
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
        drunkardsInstance = JsonUtility.FromJson<MstDrunkards>(WebRequestManager.responseJson);

        for (int i = 0; i < drunkardsInstance.mst_drunkards.Length; ++i)
        {
            MstDrunkard prefab = Instantiate(mDrunkardPrefab).GetComponent<Drunkard>().m_data;
            MstDrunkard data = drunkardsInstance.mst_drunkards[i];

            prefab.name = data.name;
            prefab.level = data.level;
            prefab.hp = data.hp;
            prefab.attack = data.attack;
            prefab.move_speed = data.move_speed;
        }
    }
}
