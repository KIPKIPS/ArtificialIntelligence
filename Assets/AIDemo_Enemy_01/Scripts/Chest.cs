using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    public GameObject flag;
    public Transform canvas;
    // Start is called before the first frame update
    void Start() {
        flag = Instantiate(Resources.Load<GameObject>("AIDemo_01/HP"));
        flag.transform.parent = canvas;
    }

    // Update is called once per frame
    void Update() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //根据世界坐标深度改变偏移量
        flag.transform.position = new Vector3(screenPos.x, screenPos.y + Mathf.Clamp(200 * (1f / transform.position.z), 30, 40), screenPos.z);
    }
}
