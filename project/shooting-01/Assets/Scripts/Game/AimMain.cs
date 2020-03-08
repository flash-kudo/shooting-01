using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMain : MonoBehaviour {

    private bool pressAimPanelFlg = false;
    private GameObject aimPanelObject;
    private Vector3 aimPanelInitialPosition; // エイムパネル初期位置(パネル左下の位置)
    private Vector3 aimPanelCenterPosition; // エイムパネル中心点
    private float distanceLimitWorldSpace = 0.6f; // エイムパネル中心点からの距離限界。この距離をこえた点にエイムパネルを移動しようとした場合は、限界距離の位置になる
    private int pressAimPanelFingerId = -1; // 取得出来る値の採番がわからないのでとりあえず -1
    private PlayerMain playerMain;

    // Use this for initialization
    void Start () {
        this.Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        // androidであれば無視する(暫定対応)
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetMouseButton(0))
            {
                if (this.pressAimPanelFlg == false && IsAimPositionInFrame(Input.mousePosition) == true)
                {
                    //複数playerタグがあると意図した動作にならないので、そうするならFindGameObjects～にしてforeachで回す
                    //また実際は、切り替えた段階でオブジェクト作成した方が効率的かも(変更フラグ立てるだけでもよいかも)
                    //途中で変わるパターンもあって、作ってみないとわからないため保留
                    this.pressAimPanelFlg = true;
                    //PCの場合はタッチID -1 固定
                    playerMain.StartFire();
                    playerMain.SetPlayerAngle(this.GetAimAngle(Input.mousePosition));
                }
                else if(this.pressAimPanelFlg == true)
                {
                    playerMain.SetPlayerAngle(this.GetAimAngle(Input.mousePosition));
                    this.MoveAimPanel(Input.mousePosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (this.pressAimPanelFlg == true)
                {
                    this.pressAimPanelFlg = false;
                    playerMain.EndFire();
                    aimPanelObject.transform.position = this.aimPanelInitialPosition;
                }
            }
        }

        if(Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (this.pressAimPanelFingerId == -1 && IsAimPositionInFrame(touch.position) == true)
                        {
                            this.pressAimPanelFingerId = touch.fingerId;
                            //複数playerタグがあると意図した動作にならないので、そうするならFindGameObjects～にしてforeachで回す
                            //また実際は、切り替えた段階でオブジェクト作成した方が効率的かも(変更フラグ立てるだけでもよいかも)
                            //途中で変わるパターンもあって、作ってみないとわからないため保留
                            this.pressAimPanelFlg = true;
                            playerMain.StartFire();
                            playerMain.SetPlayerAngle(this.GetAimAngle(touch.position));
                        }
                        break;
                    case TouchPhase.Moved:
                        if (this.pressAimPanelFingerId == touch.fingerId)
                        {
                            playerMain.SetPlayerAngle(this.GetAimAngle(touch.position));
                            this.MoveAimPanel(touch.position);
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if(this.pressAimPanelFingerId == touch.fingerId)
                        {
                            this.pressAimPanelFlg = false;
                            playerMain.EndFire();
                            aimPanelObject.transform.position = this.aimPanelInitialPosition;
                            // -1 で FingerId を初期化する
                            this.pressAimPanelFingerId = -1;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Initialize()
    {
        // エイムパネル(ボタン)のオブジェクトを取得
        aimPanelObject = GameObject.Find("shot");
        // エイムパネル初期位置(パネル左下の位置)
        this.aimPanelInitialPosition = aimPanelObject.transform.position;
        // エイムパネルの中心点
        this.aimPanelCenterPosition = this.aimPanelInitialPosition;
        this.aimPanelCenterPosition.x += aimPanelObject.GetComponent<RectTransform>().sizeDelta.x / 2;
        this.aimPanelCenterPosition.y += aimPanelObject.GetComponent<RectTransform>().sizeDelta.y / 2;

        // TODO プレイヤーが共通でなくなった場合は変える必要がある
        playerMain = GameObject.Find("PlayerMain").GetComponent<PlayerMain>();
    }

    // エイムパネルのフレーム内かどうかを判定
    // 引数はスクリーン座標(ピクセル)
    public bool IsAimPositionInFrame(Vector3 checkPosition)
    {
        float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(checkPosition), Camera.main.ScreenToWorldPoint(this.aimPanelCenterPosition));
        if (distance > this.distanceLimitWorldSpace)
        {
            return false;
        }
        return true;
    }

    // エイムパネル(ボタン)を動かす
    // 引数はスクリーン座標(ピクセル)
    private void MoveAimPanel(Vector3 targetPosition)
    {
        Vector3 newAimPanelPosition = targetPosition;

        // フレーム外に出る場合はフレーム位置で止める
        if (!this.IsAimPositionInFrame(targetPosition))
        {
            float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(targetPosition), Camera.main.ScreenToWorldPoint(this.aimPanelCenterPosition));
            newAimPanelPosition.x = this.aimPanelCenterPosition.x + (targetPosition.x - this.aimPanelCenterPosition.x) / distance * distanceLimitWorldSpace;
            newAimPanelPosition.y = this.aimPanelCenterPosition.y + (targetPosition.y - this.aimPanelCenterPosition.y) / distance * distanceLimitWorldSpace;
        }
        newAimPanelPosition.x -= aimPanelObject.GetComponent<RectTransform>().sizeDelta.x / 2;
        newAimPanelPosition.y -= aimPanelObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        aimPanelObject.transform.position = newAimPanelPosition;
    }

    private float GetAimAngle(Vector3 pressPosition)
    {
        Vector2 sub = Camera.main.ScreenToWorldPoint(this.aimPanelCenterPosition) - Camera.main.ScreenToWorldPoint(pressPosition);
        float angle = Mathf.Atan2(sub.y, sub.x) * Mathf.Rad2Deg + 90f;
        return angle;
    }
}
