using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Rocket : MonoBehaviour
{
    private float fuel = 100f;

    private int SPEED;
    private readonly float FUELPERSHOOT = 10f;
    public float rocketPosition;
    private static System.Random random = new System.Random();


    [SerializeField]Rigidbody2D rb;

    [SerializeField] private TextMeshProUGUI currentScoreTxt;
    [SerializeField] private TextMeshProUGUI HighScoreTxt;

    private const string HighScoreKey = "HighScore";
    //HighScore라는 문자열을 HighScoreKey변수에 저장
    // = PlayerPrefs에서 매번 문자열을 치지 않아도 됨
    // 만약 HighScore를 수정해야 할 경우 해당 코드만 수정하면 되서
    // 유지보수가 쉬움.(이렇게 안할 경우 각각 playerprefs 문자열을 다 수정)
   
    
    void Awake()
    {
        // TODO : Rigidbody2D 컴포넌트를 가져옴(캐싱) 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentScoreTxt.text = $"{(float)rocketPosition:F1} M";
        HighScoreTxt.text = $"HIGH : {PlayerPrefs.GetInt(HighScoreKey)} M";
        
    }

    public void Shoot()
    {
        SPEED = random.Next(1, 51);

        // TODO : fuel이 넉넉하면 윗 방향으로 SPEED만큼의 힘으로 점프, 모자라면 무시
        if (fuel >= 0)
        {
            
            rb.AddForce(Vector2.up * SPEED, ForceMode2D.Impulse);
            //force = 힘을 천천히 가함 - 5만큼의 힘을 천천히 
            //impulse = 힘을 한번에 가함 - 5만큼의 힘을 빡!
            fuel -= FUELPERSHOOT;

            // 점수 증가 (여기서 점수 증가 로직 추가)

            rocketPosition = transform.position.y;
            HighScore((int)rocketPosition); // 현재 점수를 최고 점수 확인 메서드에 전달
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("RocketLauncher");
        //재시작 하면 RocketLauncher scene이 로드
    }


    public void HighScore(int currentScore)
                           // 현재 점수
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        // 현재 최고점수 = HighScoreKey에 저장

        if(currentScore > currentHighScore )
            // 만약 현재 점수가 기존 최고점수보다 높다면
        {
            PlayerPrefs.SetInt(HighScoreKey, currentScore);
            //최고점수 키에 현재 점수를 저장해라
            PlayerPrefs.Save();
            //Prefs 저장
        }

        else
        //아니라면
        {
            PlayerPrefs.SetInt(HighScoreKey, currentHighScore);
            //최고점수 키에 기존의 최고점수를 저장해라
        }
    }
}
