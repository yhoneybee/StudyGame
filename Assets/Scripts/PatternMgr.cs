using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    public void Execute();
}
class PatternA : IPattern
{
    MonoBehaviour Mono;
    public PatternA(MonoBehaviour mono) => Mono = mono;
    public void Execute()
    {
        Mono.StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < PatternMgr.Instance.YPos.Count; i++)
        {
            ObstacleMgr.Instance.WarningObstacle((i % 2 == 0 ? Direction.RIGHT : Direction.LEFT), PatternMgr.Instance.YPos[i]);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

        switch (Random.Range(0, 100))
        {
            case int i when i < 33:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.B;
                break;
            case int i when 33 <= i && i < 66:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.C;
                break;
        }

        PatternMgr.Instance.Pattern.Execute();

        yield return null;
    }
}
class PatternB : IPattern
{
    MonoBehaviour Mono;
    public PatternB(MonoBehaviour mono) => Mono = mono;
    public void Execute()
    {
        Mono.StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < PatternMgr.Instance.YPos.Count; i++)
        {
            if (i % 2 == 0)
            {
                ObstacleMgr.Instance.WarningObstacle(Direction.RIGHT, PatternMgr.Instance.YPos[i]);
                ObstacleMgr.Instance.WarningObstacle(Direction.LEFT, PatternMgr.Instance.YPos[Random.Range(0, PatternMgr.Instance.YPos.Count)]);
                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

        switch (Random.Range(0, 100))
        {
            case int i when i < 33:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.C;
                break;
            case int i when 33 <= i && i < 66:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.D;
                break;
        }

        PatternMgr.Instance.Pattern.Execute();

        yield return null;
    }
}
class PatternC : IPattern
{
    MonoBehaviour Mono;
    public PatternC(MonoBehaviour mono) => Mono = mono;
    public void Execute()
    {
        Mono.StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < 7; i++)
        {
            ObstacleMgr.Instance.WarningObstacle((i % 2 == 1 ? Direction.RIGHT : Direction.LEFT), Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

        switch (Random.Range(0, 100))
        {
            case int i when i < 33:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.D;
                break;
            case int i when 33 <= i && i < 66:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.E;
                break;
        }

        PatternMgr.Instance.Pattern.Execute();

        yield return null;
    }
}
class PatternD : IPattern
{
    MonoBehaviour Mono;
    public PatternD(MonoBehaviour mono) => Mono = mono;
    public void Execute()
    {
        Mono.StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < 7; i++)
        {
            ObstacleMgr.Instance.WarningObstacle((i % 2 == 1 ? Direction.RIGHT : Direction.LEFT), GameMgr.Instance.player.transform.position.y);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

        switch (Random.Range(0, 100))
        {
            case int i when i < 33:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.E;
                break;
            case int i when 33 <= i && i < 66:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.A;
                break;
        }

        PatternMgr.Instance.Pattern.Execute();

        yield return null;
    }
}
class PatternE : IPattern
{
    MonoBehaviour Mono;
    public PatternE(MonoBehaviour mono) => Mono = mono;
    public void Execute()
    {
        Mono.StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < PatternMgr.Instance.YPos.Count / 2; i++)
        {
            ObstacleMgr.Instance.WarningObstacle((i % 2 == 0 ? Direction.RIGHT : Direction.LEFT), PatternMgr.Instance.YPos[i]);
            ObstacleMgr.Instance.WarningObstacle((i % 2 == 1 ? Direction.RIGHT : Direction.LEFT), PatternMgr.Instance.YPos[Mathf.Abs(i - (PatternMgr.Instance.YPos.Count - 1))]);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));

        switch (Random.Range(0, 100))
        {
            case int i when i < 33:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.A;
                break;
            case int i when 33 <= i && i < 66:
                PatternMgr.Instance.Pattern = PatternMgr.Instance.B;
                break;
        }

        PatternMgr.Instance.Pattern.Execute();

        yield return null;
    }
}

public class PatternMgr : MonoBehaviour
{
    public static PatternMgr Instance { get; private set; } = null;

    public IPattern Pattern { get; set; }
    public IPattern A { get; private set; }
    public IPattern B { get; private set; }
    public IPattern C { get; private set; }
    public IPattern D { get; private set; }
    public IPattern E { get; private set; }

    public List<float> YPos = new List<float>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        A = new PatternA(this);
        B = new PatternB(this);
        C = new PatternC(this);
        D = new PatternD(this);
        E = new PatternE(this);

        switch (Random.Range(0, 100))
        {
            case int i when i < 20:             Pattern = A; break;
            case int i when 20 <= i && i < 40:  Pattern = B; break;
            case int i when 40 <= i && i < 60:  Pattern = C; break;
            case int i when 60 <= i && i < 80:  Pattern = D; break;
            case int i when 80 <= i && i < 100: Pattern = E; break;
        }

        Execute();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Execute();
    }

    public void Execute()
    {
        Pattern.Execute();
    }
}
