using UnityEngine;

public class CarController : MonoBehaviour
{
    // �Ԃ̑��x
    public float velocity { get; private set; } // public��getter��ǉ����Avelocity���O������ǂݎ���悤�ɂ���

    // �P�t���[���O�̑��x
    float prevVelocity = 0f;

    // �Ԃ̑��x�ω�
    float acc = 0f;

    // �Ԃ̉��������x
    float velocityX = 0f;

    // �Ԃ̐i�s�������x
    float velocityZ = 0f;

    // �n���h���̑Ǌp
    float theta = 0f;

    // ���C��
    public float friction = 2f;

    // �p�����x
    public float angularAcceleration = 5f;

    // �ő�V�[�^�p�x
    public float maxTheta = 180f; // ���

    // �ő呬�x
    public float maxVelocity = 60f;

    public Rigidbody rb;

    // ��[�̈ʒu
    public Vector3 frontPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 120; // �ڕW�̃t���[�����[�g��ݒ�
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // �����x�̃��Z�b�g
        acc = 0f;
        // �����x�̌v�Z
        if (Input.GetKey(KeyCode.W))
        {
            acc += 6f; // W�L�[�ŉ���
        }

        // S�L�[�Ō���
        if (Input.GetKey(KeyCode.S))
        {
            acc -= 4.5f; // ����
        }

        // Shift�L�[�������Ă���ԁA���x��0�ɋ߂Â���
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
        else // Shift�L�[�������Ă��Ȃ��ꍇ�A�ʏ�̑��x�̍X�V���s��
        {
            // ���x�̍X�V
            velocity = prevVelocity + acc * Time.deltaTime;

            // �ő呬�x�𒴂��Ȃ��悤�ɐ���
            velocity = Mathf.Clamp(velocity, 0f, maxVelocity);

            // ���C�ɂ�鑬�x�̌���
            if (velocity != 0) // ���x��0�łȂ��ꍇ�̂ݖ��C��K�p
            {
                if (velocity > 0)
                {
                    velocity -= friction * Time.deltaTime;
                }
                if (velocity < 0) // ���C�ɂ���đ��x��0�������Ȃ��悤�ɒ���
                {
                    velocity += friction * Time.deltaTime;
                }
            }
        }

        // �n���h���̑Ǌp�̌v�Z
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
        // �V�[�^�̏���Ɖ�����ݒ�
        theta = Mathf.Clamp(theta, -maxTheta, maxTheta);

        // �Ԃ̐i�s�������x�Ɖ��������x�̌v�Z
        velocityZ = velocity * Mathf.Cos(theta * Mathf.Deg2Rad);
        velocityX = velocity * Mathf.Sin(theta * Mathf.Deg2Rad);

        // ��[�̈ʒu���X�V
        frontPosition = transform.position + transform.forward * 1.6f; // �O��1.6m����[�Ƃ���

        // �Ԃ̈ړ�
        transform.Translate(velocityX * Time.deltaTime, 0f, velocityZ * Time.deltaTime);

        // ��[�̈ʒu�Ɍ����ĎԂ���]
        transform.LookAt(frontPosition);

        // �P�t���[���O�̑��x���X�V
        prevVelocity = velocity;

        // �p�����x�̑���
        if (Input.GetKey(KeyCode.Q))
        {
            angularAcceleration = Mathf.Max(angularAcceleration - 5f, 5f); // �p�����x��������
        }

        if (Input.GetKey(KeyCode.E))
        {
            angularAcceleration = Mathf.Min(angularAcceleration + 5f, 25f); // �p�����x���グ��
        }
    }

}
