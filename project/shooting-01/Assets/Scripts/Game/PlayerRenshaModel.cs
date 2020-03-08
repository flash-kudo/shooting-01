using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenshaModel : MonoBehaviour {

    Vector3 before_position;

    // Use this for initialization
    void Start () {
        before_position = this.gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        bool isShot = false;
        if (Input.GetKey(KeyCode.B))
        {
            isShot = true;
        }
        GetComponent<Animator>().SetBool("ModelShotPlayerRensha", isShot);

        // 前回から今回の移動分
        Vector3 move_vector = before_position - this.gameObject.transform.position;

        bool isMoveUp = false;
        bool isMoveDown = false;
        bool isMoveRight = false;
        bool isMoveLeft = false;

        // X座標の移動が大きい場合
        if (Mathf.Abs(move_vector.x) > Mathf.Abs(move_vector.y))
        {
            if(move_vector.x > 0)
            {
                isMoveLeft = true;
            }
            else if(move_vector.x < 0)
            {
                isMoveRight = true;
            }
        }
        // Y座標の移動が大きい場合 & 同じ場合
        else
        {
            if (move_vector.y > 0)
            {
                isMoveDown = true;
            }
            else if (move_vector.y < 0)
            {
                isMoveUp = true;
            }
        }

        float now_angle = this.gameObject.transform.localEulerAngles.z;

        // 下向き
        if (now_angle > 135 && now_angle <= 225)
        {
            if (isMoveLeft)
            {
                isMoveLeft = false;
                isMoveRight = true;
            }
            else if(isMoveRight)
            {
                isMoveRight = false;
                isMoveLeft = true;
            }
            else if (isMoveUp)
            {
                isMoveUp = false;
                isMoveDown = true;
            }
            else if (isMoveDown)
            {
                isMoveDown = false;
                isMoveUp = true;
            }
        }
        // 左向き
        else if (now_angle > 45 && now_angle <= 135)
        {
            if (isMoveLeft)
            {
                isMoveLeft = false;
                isMoveUp = true;
            }
            else if (isMoveRight)
            {
                isMoveRight = false;
                isMoveDown = true;
            }
            else if (isMoveUp)
            {
                isMoveUp = false;
                isMoveRight = true;
            }
            else if (isMoveDown)
            {
                isMoveDown = false;
                isMoveLeft = true;
            }
        }
        // 右向き
        else if (now_angle > 225 && now_angle <= 315)
        {
            if (isMoveLeft)
            {
                isMoveLeft = false;
                isMoveDown = true;
            }
            else if (isMoveRight)
            {
                isMoveRight = false;
                isMoveUp = true;
            }
            else if (isMoveUp)
            {
                isMoveUp = false;
                isMoveLeft = true;
            }
            else if (isMoveDown)
            {
                isMoveDown = false;
                isMoveRight = true;
            }
        }

        GetComponent<Animator>().SetBool("ModelMoveUpPlayerRensha", isMoveUp);
        GetComponent<Animator>().SetBool("ModelMoveDownPlayerRensha", isMoveDown);
        GetComponent<Animator>().SetBool("ModelMoveRightPlayerRensha", isMoveRight);
        GetComponent<Animator>().SetBool("ModelMoveLeftPlayerRensha", isMoveLeft);

        before_position = this.gameObject.transform.position;
    }
}
