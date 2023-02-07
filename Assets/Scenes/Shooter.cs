using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject candyPrefab;
    public float shotForce;
    public float shotTorque;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("File1")) Shot();
    }

    public void Shot(){
        //プレハブからCandyオブジェクトを生成
        GameObject candy = Instantiate(
            candyPrefab,
            transform.position,
            Quaternion.identity
        );

        //CandyオブジェクトのRigidbodyを取得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
    }
}
