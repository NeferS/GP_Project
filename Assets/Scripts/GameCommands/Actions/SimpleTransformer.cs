using UnityEngine;


public abstract class SimpleTransformer : GameCommandHandler
{
    public enum LoopType
    {
        Once,
        PingPong,
        Repeat
    }

    public LoopType loopType;

    public float duration = 1;
    public AnimationCurve accelCurve;

    public bool activate = false;
    public SendGameCommand OnStartCommand, OnStopCommand;

    public AudioSource onStartAudio, onEndAudio;

    float time = 0f;
    float position = 0f;
    float direction = 1f;

    protected GameCommandType lastReceived;
    protected Platform m_Platform;

    [ContextMenu("Test Start Audio")]
    void TestPlayAudio()
    {
        if (onStartAudio != null) onStartAudio.Play();
    }

    protected override void Awake()
    {
        base.Awake();

        m_Platform = GetComponentInChildren<Platform>();
    }

    public override void PerformInteraction(GameCommandType type)
    {
        activate = true;
        lastReceived = type;
        if (OnStartCommand != null) OnStartCommand.Send();
        if (onStartAudio != null) onStartAudio.Play();
    }

    public void FixedUpdate()
    {
        if (activate)
        {
            time = time + (direction * Time.deltaTime / duration);
            switch (loopType)
            {
                case LoopType.Once:
                    LoopOnce();
                    break;
                case LoopType.PingPong:
                    LoopPingPong();
                    break;
                case LoopType.Repeat:
                    LoopRepeat();
                    break;
            }
            PerformTransform(lastReceived, position);
        }
    }

    public virtual void PerformTransform(GameCommandType type, float position)
    {

    }

    void LoopPingPong()
    {
        position = Mathf.PingPong(time, 1f);
    }

    void LoopRepeat()
    {
        position = Mathf.Repeat(time, 1f);
    }

    void LoopOnce()
    {
        position = Mathf.Clamp01(time);
        if (position >= 1)
        {
            activate = false;
            if (OnStopCommand != null) OnStopCommand.Send();
            time = 0f;
        }
    }
}