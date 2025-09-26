using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // 이 스크립트를 관리할 단일 인스턴스

    private AudioSource audioSource; // 소리를 재생할 AudioSource 컴포넌트

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        audioSource = GetComponent<AudioSource>(); // 이 오브젝트에 붙어있는 AudioSource 컴포넌트를 가져옴
    }


    //버튼 클릭 효과음을 재생할 함수
    //AudioClip을 파라미터로 받아서 어떤 소리든 재생 가능하게 함
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
