using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {
    private float createTime;
    // Start is called before the first frame update
    void Start() {
        createTime = 0;
    }

    // Update is called once per frame
    void Update() {
        createTime += Time.deltaTime;
        if (createTime>5) {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.tag=="Untagged") {
            Destroy(this.gameObject);
        }
    }
}
