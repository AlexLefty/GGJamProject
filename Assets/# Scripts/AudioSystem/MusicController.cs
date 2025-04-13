using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

/// <summary>
/// Управляет фоновой музыкой
/// </summary>
public class MusicController : MonoBehaviour
{
    // Internal class state
    [SerializeField] private string[] m_clipAddresses;
    private int m_current;
    private bool m_isLoading;
    private Coroutine m_cicleRoutine;
    // References
    private AudioSource musicSource;

    public float Volume => musicSource.volume;
    public bool IsPlaying => musicSource.isPlaying;


    private void Start()
    {
        GameManager.Instance.GamePaused.AddListener(() =>
        {
            if (m_cicleRoutine is not null)
                StopCoroutine(m_cicleRoutine);
        });

        GameManager.Instance.GameResumed.AddListener(() =>
        {
            m_current--;
            Pause();
            m_cicleRoutine = StartCoroutine(Loop());
        });
    }



    public void Play() => musicSource.Play();
    public void Pause() => musicSource.Pause();
    public void Stop() => musicSource.Stop();

    public void Next()
    {
        m_current = (m_current + 1) % m_clipAddresses.Length;

        LoadAudioClipAsync();
    }

    private async void LoadAudioClipAsync()
    {
        Stop();
        m_isLoading = true;

        var handle = Addressables.LoadAssetAsync<AudioClip>(m_clipAddresses[m_current]);
        await handle.ToUniTask();
        musicSource.clip = handle.Result;

        m_isLoading = false;
        Play();
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            while (musicSource.isPlaying || m_isLoading)
            {
                yield return null;
            }
            Next();
        }
    }
}
