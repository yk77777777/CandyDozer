using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform;
    public CandyManager candyManager;
    public GameObject candyPrefab;
    public float shotForce;
    public float shotTorque;
    public float baseWidth;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    //キャンディのプレハブからランダムに1つ選ぶ
    GameObject SampleCandy(){
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    Vector3 GetInstantiatePosition(){
        //画面サイズとInputの割合からキャンディ生成のポジションを計算
        float x = baseWidth * (Input.mousePosition.x / Screen.width)
                -(baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }
    public void Shot()
    {
        //キャンディを生成できる条件外ならばShotしない
        if (candyManager.GetCandyAmount() <= 0) return;

        //プレハブからCandyオブジェクトを生成
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity
        );
        
        //生成したCandyオブジェクトの親をcandyParentsFormに設定する
        candy.transform.parent = candyParentTransform;

        //CandyオブジェクトのRigidbodyを取得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        //transform.forward 物体が持っているz軸方向
        candyRigidBody.AddForce(transform.forward * shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        //Candyのストックを消費
        candyManager.ConsumeCandy();
    }
}
