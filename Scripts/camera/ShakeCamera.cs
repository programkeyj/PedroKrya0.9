using Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance {get; private set;}

    [SerializeField] private float ShakeTimer;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;
    [SerializeField] private bool TimedShakingRun = false;

    private void Awake() {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        Instance = this;
    }
    public void ShakeCameraRun(float intensity, float frequency){
        CinemachineBasicMultiChannelPerlin Perlin =
            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Perlin.m_AmplitudeGain = intensity;
        Perlin.m_FrequencyGain = frequency;
    }
    public void ShakeCameraWithTimer(float intensity, float frequency, float time){
        TimedShakingRun = true;
        CinemachineBasicMultiChannelPerlin Perlin =
            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Perlin.m_AmplitudeGain = intensity;
        Perlin.m_FrequencyGain = frequency;
        ShakeTimer = time;
    }
    public void StopShaking(){
        TimedShakingRun = false;
        CinemachineBasicMultiChannelPerlin Perlin =
                            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Perlin.m_AmplitudeGain = 0f;
    }
    private void Update() {
        if (TimedShakingRun){
            if (ShakeTimer > 0){
                ShakeTimer -= Time.deltaTime;
                if (ShakeTimer <= 0f)
                {
                    if (ShakeTimer <= 0f){
                        CinemachineBasicMultiChannelPerlin Perlin =
                            VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                        Perlin.m_AmplitudeGain = 0f;
                        TimedShakingRun = false;
                    }
                }
            }
        }    
    }
}