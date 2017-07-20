using System.Collections;
using System.Threading;

/*
 * Notes: 
 * I did not write this class, I took it from a forum discussing an issue I had
 * I had this problem in the project I am working on right now in my internship,
 * I noticed fps drops after sending multiple HTTP requests and processing all the returned data in the StartCoroutine function,
 * The solution I found is to process the data in a different thread and use the results of this processing in the Unity main Thread
 * As a beginner in Unity, I am not sure this is the right way to tackle this problem but as far as I know, it considerably improved the performance
*/



public class ThreadedJob {


    private bool m_IsDone = false;
    private object m_Handle = new object();
    private Thread m_Thread = null;
    public bool IsDone
    {
        get
        {
            bool tmp;
            lock (m_Handle)
            {
                tmp = m_IsDone;
            }
            return tmp;
        }
        set
        {
            lock (m_Handle)
            {
                m_IsDone = value;
            }
        }
    }

    public virtual void Start()
    {
        m_Thread = new Thread(Run);
        m_Thread.Start();
    }
    public virtual void Abort()
    {
        m_Thread.Abort();
    }

    protected virtual void ThreadFunction() { }

    protected virtual void OnFinished() { }

    public virtual bool Update()
    {
        if (IsDone)
        {
            OnFinished();
            return true;
        }
        return false;
    }
    public IEnumerator WaitFor()
    {
        while (!Update())
        {
            yield return null;
        }
    }
    private void Run()
    {
        ThreadFunction();
        IsDone = true;
    }
}
