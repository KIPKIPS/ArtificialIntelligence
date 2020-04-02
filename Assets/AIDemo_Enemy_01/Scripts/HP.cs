using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour {
    public GameObject hp;
    public Transform canvas;
    // Start is called before the first frame update
    void Start() {
        hp=Instantiate(Resources.Load<GameObject>("AIDemo_01/HP"));
        hp.transform.parent = canvas;
    }

    // Update is called once per frame
    void Update() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //根据世界坐标深度改变偏移量
        hp.transform.position = new Vector3(screenPos.x, screenPos.y +Mathf.Clamp( 200 * (1f / transform.position.z),30,40), screenPos.z);
        hp.GetComponent<Slider>().value = this.gameObject.GetComponent<Enemy>().hp/10f+0.1f;
    }
}
