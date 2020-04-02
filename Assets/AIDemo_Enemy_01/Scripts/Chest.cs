using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    public GameObject flag;
    public Transform canvas;

    private float y;
    public Transform player;
    private float scale;
    // Start is called before the first frame update
    void Start() {
        flag = Instantiate(Resources.Load<GameObject>("AIDemo_01/FlagPos"));
        flag.transform.parent = canvas;
        flag.transform.localScale=new Vector3(0.2f,0.2f,0.2f);
    }

    // Update is called once per frame
    void Update() {
        if (player!=null) {
            y = 10+Mathf.Sin(Time.realtimeSinceStartup*2)*10;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //计算缩放比例
            scale = Mathf.Clamp01(1f / (transform.position.z - player.position.z) * 10);
            //Debug.Log(scale);
            //根据世界坐标深度改变Y方向上的偏移量
            flag.transform.position = new Vector3(screenPos.x, y+screenPos.y + scale*100, screenPos.z);
            flag.transform.Rotate(Vector3.up,Time.deltaTime*180);
            //大小缩放
            flag.transform.localScale=new Vector3(scale,scale,scale);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.tag=="Player") {
            Debug.Log("Victory");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//用于退出运行
            #else
                Application.Quit();
            #endif
        }
    }
}
