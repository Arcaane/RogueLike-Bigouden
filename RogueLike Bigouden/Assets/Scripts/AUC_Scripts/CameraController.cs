using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Controller controls;
    public GameObject Player;

    public Transform player;
    private float shakeMag, shakeTimeEnd;
    private bool shaking;
    private readonly float smoothTime = 0.2f;
    private readonly float cameraDist = 0.2f;
    private float zStart;
    private Vector3 target, mousePos, refVel, shakeOffset, shakeVector;

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.transform;
        target = player.position;
        zStart = transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
        mousePos = CaptureMousePos();
        shakeOffset = UpdateShake();
        target = UpdateTargetPos();
        UpdateCameraPosition();
    }

    private Vector3 CaptureMousePos()
    {
        Vector2 ret = player.GetChild(1).transform.position;
        ret *= 2f;
        ret -= Vector2.one;
        var max = 1.3f;
        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
            ret = ret.normalized;
        return ret;
    }

    private Vector3 UpdateTargetPos()
    {
        var mouseOffset = mousePos * cameraDist;
        var ret = player.position + mouseOffset;
        ret += shakeOffset;
        ret.z = zStart;
        return ret;
    }

    private void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target,
            ref refVel, smoothTime);
        transform.position = tempPos;
    }

    private Vector3 UpdateShake()
    {
        if (!shaking || Time.time > shakeTimeEnd)
        {
            shaking = false;
            return Vector3.zero;
        }

        var tempOffset = shakeVector;
        tempOffset *= shakeMag;
        return tempOffset;
    }
}