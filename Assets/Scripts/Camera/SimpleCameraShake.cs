using UnityEngine;
using Cinemachine;
using Managers;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class SimpleCameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float ShakeAmplitude = 1.2f;
    [SerializeField] private float ShakeFrequency = 2.0f;

    private float shakeElapsedTime = 0f;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    private CinemachineVirtualCamera VirtualCamera;

    void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera()
    {
        shakeElapsedTime = shakeDuration;
    }

    void Update()
    {
        if (shakeElapsedTime > 0)
        {
            virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
            virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            virtualCameraNoise.m_AmplitudeGain = 0f;
            shakeElapsedTime = 0f;
        }
    }
}
