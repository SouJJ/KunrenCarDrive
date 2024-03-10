using UnityEngine;

public class CarController : MonoBehaviour
{
    // 車の速度
    public float velocity { get; private set; } // publicなgetterを追加し、velocityを外部から読み取れるようにする

    // １フレーム前の速度
    float prevVelocity = 0f;

    // 車の速度変化
    float acc = 0f;

    // 車の横方向速度
    float velocityX = 0f;

    // 車の進行方向速度
    float velocityZ = 0f;

    // ハンドルの舵角
    float theta = 0f;

    // 摩擦力
    public float friction = 2f;

    // 角加速度
    public float angularAcceleration = 5f;

    // 最大シータ角度
    public float maxTheta = 180f; // 上限

    // 最大速度
    public float maxVelocity = 60f;

    public Rigidbody rb;

    // 先端の位置
    public Vector3 frontPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 120; // 目標のフレームレートを設定
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // 加速度のリセット
        acc = 0f;
        // 加速度の計算
        if (Input.GetKey(KeyCode.W))
        {
            acc += 6f; // Wキーで加速
        }

        // Sキーで減速
        if (Input.GetKey(KeyCode.S))
        {
            acc -= 4.5f; // 減速
        }

        // Shiftキーを押している間、速度を0に近づける
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (velocity > 0)
            {
                velocity -= 1f * Time.deltaTime;
                if (velocity < 0)
                {
                    velocity += 1f * Time.deltaTime;
                }
            }
        }
        else // Shiftキーを押していない場合、通常の速度の更新を行う
        {
            // 速度の更新
            velocity = prevVelocity + acc * Time.deltaTime;

            // 最大速度を超えないように制限
            velocity = Mathf.Clamp(velocity, 0f, maxVelocity);

            // 摩擦による速度の減衰
            if (velocity != 0) // 速度が0でない場合のみ摩擦を適用
            {
                if (velocity > 0)
                {
                    velocity -= friction * Time.deltaTime;
                }
                if (velocity < 0) // 摩擦によって速度が0を下回らないように調整
                {
                    velocity += friction * Time.deltaTime;
                }
            }
        }

        // ハンドルの舵角の計算
        if (Input.GetKey(KeyCode.A))
        {
            theta += angularAcceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            theta -= angularAcceleration * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            theta = 0;
        }
        // シータの上限と下限を設定
        theta = Mathf.Clamp(theta, -maxTheta, maxTheta);

        // 車の進行方向速度と横方向速度の計算
        velocityZ = velocity * Mathf.Cos(theta * Mathf.Deg2Rad);
        velocityX = velocity * Mathf.Sin(theta * Mathf.Deg2Rad);

        // 先端の位置を更新
        frontPosition = transform.position + transform.forward * 1.6f; // 前方1.6m先を先端とする

        // 車の移動
        transform.Translate(velocityX * Time.deltaTime, 0f, velocityZ * Time.deltaTime);

        // 先端の位置に向けて車を回転
        transform.LookAt(frontPosition);

        // １フレーム前の速度を更新
        prevVelocity = velocity;

        // 角加速度の操作
        if (Input.GetKey(KeyCode.Q))
        {
            angularAcceleration = Mathf.Max(angularAcceleration - 5f, 5f); // 角加速度を下げる
        }

        if (Input.GetKey(KeyCode.E))
        {
            angularAcceleration = Mathf.Min(angularAcceleration + 5f, 25f); // 角加速度を上げる
        }
    }

}
