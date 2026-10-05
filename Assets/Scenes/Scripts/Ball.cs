using UnityEngine;
using UnityEngine.UI;
public class Ball : MonoBehaviour
{
    public Transform aimTarget;
    public Slider powerBar;
    public float maxPower = 25f;
    public float resetDelay = 3f;

    Rigidbody rb;
    float power;
    bool shot;
    Vector3 startPos;
    Quaternion startRot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ScoreManager.Instance.GameOver) ScoreManager.Instance.Restart();
            ResetBall();
        }

        if (shot || ScoreManager.Instance.GameOver) return;

        if (Input.GetKey(KeyCode.Space))
        {
            power = Mathf.Min(power + 20f * Time.deltaTime, maxPower);
            powerBar.value = power / maxPower;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Vector3 dir = (aimTarget.position - transform.position).normalized;
            dir += -Physics.gravity.normalized * 0.15f;
            rb.AddForce(dir * power, ForceMode.Impulse);
            power = 0f;
            powerBar.value = 0f;
            shot = true;
            ScoreManager.Instance.KickTaken();
            Invoke(nameof(ResetBall), resetDelay);
        }
    }

    public void ResetBall()
    {
        CancelInvoke();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPos;
        transform.rotation = startRot;
        power = 0f;
        powerBar.value = 0f;
        if (shot) ScoreManager.Instance.KickFinished();
        shot = false;
    }
}