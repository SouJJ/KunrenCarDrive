using UnityEngine;

public class CarCameraController : MonoBehaviour
{
    public Transform target; // �Ԃ�Transform
    public float distance = 20.0f; // �J�����ƃ^�[�Q�b�g�̋���
    public float height = 10.0f; // �J�����̍���
    public float damping = 5.0f; // �Ǐ]�̑��x

    private bool cursorLocked = true; // �J�[�\�����b�N�̏�Ԃ�\���t���O
    private bool freeCameraEnabled = false; // �t���[�J�������L�����ǂ����̃t���O

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        // ESC�L�[�ŃJ�[�\�����b�N�̐؂�ւ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
            if (cursorLocked)
                LockCursor();
            else
                UnlockCursor();
        }

        // �E�N���b�N���͒��̓t���[�J�����ɂ���
        if (Input.GetMouseButtonDown(1))
        {
            freeCameraEnabled = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            freeCameraEnabled = false;
        }

        // �t���[�J�������L���ȏꍇ�̓J�����̈ʒu�Ɗp�x���}�E�X���͂ɉ����čX�V����
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
        // �^�[�Q�b�g��Transform���Ȃ���Ή������Ȃ�
        if (!target)
            return;

        // �t���[�J�������L���ȏꍇ�̓J�����̌������Œ肷��
        if (!freeCameraEnabled)
        {
            // �J�����̐V�����ʒu���v�Z
            Vector3 wantedPosition = target.position - target.forward * distance + Vector3.up * height;

            // �J������V�����ʒu�ɃX���[�Y�Ɉړ�
            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

            // �J�����̌���������^�[�Q�b�g�Ɍ�����
            transform.LookAt(target.position);
        }
    }

    // �J�[�\�������b�N����
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // �J�[�\���̃��b�N����������
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

