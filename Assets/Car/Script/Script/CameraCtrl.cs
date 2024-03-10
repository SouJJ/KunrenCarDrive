using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    public Transform target; // 車のTransform
    public float distance = 20.0f; // カメラとターゲットの距離
    public float height = 10.0f; // カメラの高さ
    public float damping = 5.0f; // 追従の速度

    private bool cursorLocked = true; // カーソルロックの状態を表すフラグ
    private bool freeCameraEnabled = false; // フリーカメラが有効かどうかのフラグ

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        // ESCキーでカーソルロックの切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
            if (cursorLocked)
                LockCursor();
            else
                UnlockCursor();
        }

        // 右クリック入力中はフリーカメラにする
        if (Input.GetMouseButtonDown(1))
        {
            freeCameraEnabled = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            freeCameraEnabled = false;
        }

        // フリーカメラが有効な場合はカメラの位置と角度をマウス入力に応じて更新する
        if (freeCameraEnabled)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.RotateAround(target.position, Vector3.up, mouseX * 3f);
            transform.RotateAround(target.position, transform.right, -mouseY * 3f);
        }
    }

    void LateUpdate()
    {
        // ターゲットのTransformがなければ何もしない
        if (!target)
            return;

        // フリーカメラが有効な場合はカメラの向きを固定する
        if (!freeCameraEnabled)
        {
            // カメラの新しい位置を計算
            Vector3 wantedPosition = target.position - target.forward * distance + Vector3.up * height;

            // カメラを新しい位置にスムーズに移動
            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

            // カメラの見る方向をターゲットに向ける
            transform.LookAt(target.position);
        }
    }

    // カーソルをロックする
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // カーソルのロックを解除する
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

