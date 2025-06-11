using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private float _speed;
    private NavMeshAgent _agent;
    private AnimationCurve _yOffsetCurve;

    private MonoBehaviour _coroutineRunner;

    private Coroutine _jumpProcess;

    public AgentJumper(
        float speed,
        NavMeshAgent agent,
        MonoBehaviour coroutineRunner,
        AnimationCurve yOffsetCurve)
    {
        _speed = speed;
        _agent = agent;
        _coroutineRunner = coroutineRunner;
        _yOffsetCurve = yOffsetCurve;
    }

    public bool InProcess => _jumpProcess != null;

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if (InProcess)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
    }

    private IEnumerator JumpProcess(OffMeshLinkData offmeshLinkData)
    {
        Vector3 startPosition = offmeshLinkData.startPos;
        Vector3 endPosition = offmeshLinkData.endPos;

        float duration = Vector3.Distance(startPosition, endPosition) / _speed;

        float progress = 0f;

        while (progress < duration)
        {
            float yOffset = _yOffsetCurve.Evaluate(progress / duration);
            _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / duration) + Vector3.up * yOffset;
            progress += Time.deltaTime;
            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _jumpProcess = null;
    }
}
