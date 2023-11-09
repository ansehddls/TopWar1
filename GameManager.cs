using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;                                                                                                              //게임메니저 싱글톤 만들기 위한 변수

    void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(GM);
        }
    }                                                                                                                                     //게임메니저 싱글톤
    public bool isEnd =  false;                                                                                                                      //게임 종료 변수 불값
    public int playerHp = 500;                                                                                                                      //플레이어의 체력을 넣어줄 변수
    public int killCount = 0;                                                                                                                         //플레이어의 킬 카운트
    public int coinCount = 0;                                                                                                                       //코인 카운트는 0
    public int coinNum;                                                                                                                              //코인 생성될 숫자
    public Text mission_Text;                                                                                                                        //플레이어의 미션을 확인할 변수
    public Text stage_Text;                                                                                                                          //스테이지 텍스트
    public Text playerHp_Text;                                                                                                                      //플레이어의 hp를 표시할 텍스트
    public Text timeText;                                                                                                                             //시간을 표시할 텍스트

    public Slider playerHp_MainUI;                                                                                                                //플레이어의 hp를 나타내는 슬라이더
    public Slider playerHp_SubUI;                                                                                                                  //플레이어의 hp를 나타내는 슬라이더
    public float limitTime = 90f;                                                                                                                    //제한시간
    public GameObject settinngUI;                                                                                                                //설정창을 담을 변수
    public GameObject portalParticle;                                                                                                            //포탈 파티클
    public GameObject playerObj;                                                                                                                //플레이어 오브젝트를 담을 변수 생성
    public GameObject gameOverUI;                                                                                                             //게임 오버 오브젝트를 담을 변수
    public Slider bossHpUI;                                                                                                                         //보스 Hp슬라이더
    public Text bossHp_Text;                                                                                                                        //보스 Hp텍스트
    public GameObject endGameUI;                                                                                                              //게임 끝 UI

    void Start()
    {
        MissionDenote();                                                                                                                               //미션을 표시하는 메서드
        stage_Text.text = SceneManager.GetActiveScene().name;                                                                            //현재 씬의 이름을 표시한다
        playerHp_MainUI.value = playerHp;                                                                                                       //플레이어의 체력을 value와 같게 한다
        playerHp_SubUI.value = playerHp;                                                                                                         //플레이어의 체력을 value와 같게 한다
        playerHp_Text.text = playerHp + "/" + "500";                                                                                          //플레이어의 체력을 텍스트로 표시한다
        timeText.text = string.Format("{0:00}:{1:00}", limitTime / 60, limitTime % 60);                                                   //게임 시작시 시간을 표시할 메서드
        settinngUI.SetActive(false);                                                                                                                   //설정 창 비활성화
        portalParticle.SetActive(false);                                                                                                               //포탈 파티클을 끈다
        gameOverUI.SetActive(false);                                                                                                                //게임 오버 오브젝트를 비활성화 한다
        endGameUI.SetActive(false);                                                                                                                 //엔드 게임 UI를 비활성화 한다
        if (SceneManager.GetActiveScene().name == "Stage05")                                                                             //만약 스테이지 5라면 
        {
            bossHpUI.gameObject.SetActive(true);                                                                                                //보스 체력 슬라이더 UI활성화
            bossHp_Text.gameObject.SetActive(true);                                                                                            //보스 체력 텍스트 활성화
            bossHpUI.value = EnemyManager.EM.boss.GetComponent<EnemyController>().enemyDHp;                          //에너미 D의 HP를 가져온다
            bossHp_Text.text = EnemyManager.EM.boss.GetComponent<EnemyController>().enemyDHp + "/" + 1000;
        }
        else
        {
            bossHpUI.gameObject.SetActive(false);                                                                                                //보스 체력 슬라이더 비활성화
            bossHp_Text.gameObject.SetActive(false);                                                                                            //보스 체력 텍스트 비활성화
           
        }
    }

    
    void Update()
    {
        MissionDenote();                                                                                                                                    //미션을 표시하는 메서드
        TimeFlow();                                                                                                                                           //시간이 흐르는 메서드
        StageClear();                                                                                                                                         //스테이지 클리어 메서드
        GameOver();                                                                                                                                         //게임 오버 메서드
        if(SceneManager.GetActiveScene().name == "Stage05")
        {
            bossHpUI.value = EnemyManager.EM.boss.GetComponent<EnemyController>().enemyDHp;                               //에너미 D의 HP를 업데이트 한다
            bossHp_Text.text = EnemyManager.EM.boss.GetComponent<EnemyController>().enemyDHp + "/" + 1000;
        }
    }

    void TimeFlow()
    {
        if (killCount < 25 && SceneManager.GetActiveScene().name == "Stage01")                                                        //킬 카운트가 25보다 작고, 씬의 이름이 스테이지 01일때
        {
            limitTime -= Time.deltaTime;                                                                                                                 //시간에서 타임을 뺀다
            float sec = limitTime % 60;                                                                                                                   //초는 리미트 타임을 60으로 나눈 나머지 이다
            float min = limitTime / 60;                                                                                                                   //분은 리미트 타임을 60으로 나눈 몫이다
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면 
            {
                sec = limitTime;                                                                                                                             //초는 리미트 타임과 같다
            }
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면
            {
                min = 0;                                                                                                                                      //분은 0이 된다
            }
            timeText.text = string.Format("{0:00}:{1:00}", min, sec);                                                                               //시간의 경과를 나타낸다
            if (limitTime <= 0)                                                                                                                            //만약 리미트 타임이 0보다 작다면
            {
                isEnd = true;                                                                                                                               //게임은 끝이다
            }
        }
         if (SceneManager.GetActiveScene().name == "Stage02")                                                                          //씬의 이름이 스테이지 02일때
        {
            limitTime -= Time.deltaTime;                                                                                                                 //시간에서 타임을 뺀다
            float sec = limitTime % 60;                                                                                                                   //초는 리미트 타임을 60으로 나눈 나머지 이다
            float min = limitTime / 60;                                                                                                                   //분은 리미트 타임을 60으로 나눈 몫이다
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면 
            {
                sec = limitTime;                                                                                                                             //초는 리미트 타임과 같다
            }
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면
            {
                min = 0;                                                                                                                                      //분은 0이 된다
            }
            timeText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        if (coinCount < 25 && SceneManager.GetActiveScene().name == "Stage03")                                                        //코인 카운트가 25보다 작고, 씬의 이름이 스테이지 03일때
        {
            limitTime -= Time.deltaTime;                                                                                                                 //시간에서 타임을 뺀다
            float sec = limitTime % 60;                                                                                                                   //초는 리미트 타임을 60으로 나눈 나머지 이다
            float min = limitTime / 60;                                                                                                                   //분은 리미트 타임을 60으로 나눈 몫이다
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면 
            {
                sec = limitTime;                                                                                                                             //초는 리미트 타임과 같다
            }
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면
            {
                min = 0;                                                                                                                                      //분은 0이 된다
            }
            timeText.text = string.Format("{0:00}:{1:00}", min, sec);                                                                               //시간의 경과를 나타낸다
            if (limitTime <= 0)                                                                                                                            //만약 리미트 타임이 0보다 작다면
            {
                isEnd = true;                                                                                                                               //게임은 끝이다
            }
        }
        if (killCount < 50 && SceneManager.GetActiveScene().name == "Stage04")                                                        //킬 카운트가 50보다 작고, 씬의 이름이 스테이지 04일때
        {
            limitTime -= Time.deltaTime;                                                                                                                 //시간에서 타임을 뺀다
            float sec = limitTime % 60;                                                                                                                   //초는 리미트 타임을 60으로 나눈 나머지 이다
            float min = limitTime / 60;                                                                                                                   //분은 리미트 타임을 60으로 나눈 몫이다
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면 
            {
                sec = limitTime;                                                                                                                             //초는 리미트 타임과 같다
            }
            if (limitTime < 60)                                                                                                                              //만약 리미트 타임이 60보다 작다면
            {
                min = 0;                                                                                                                                      //분은 0이 된다
            }
            timeText.text = string.Format("{0:00}:{1:00}", min, sec);                                                                               //시간의 경과를 나타낸다
            if (limitTime <= 0)                                                                                                                            //만약 리미트 타임이 0보다 작다면
            {
                isEnd = true;                                                                                                                               //게임은 끝이다
            }
        }
        if (killCount < 1 && SceneManager.GetActiveScene().name == "Stage05")                                                        //킬 카운트가 1보다 작고, 씬의 이름이 스테이지 05일때
        {
            timeText.gameObject.SetActive(false);                                                                                                   //시간 경과를 제거
        }
    }                                                                                                                                        //시간이 흐르는걸 표현할 메서드

    void StageClear()                                                                                                                                       //스테이지 클리어를 실행할 메서드
    { 
        if (killCount == 25 && SceneManager.GetActiveScene().name == "Stage01")                                                      //킬 카운트가 25이고, 씬의 이름이 스테이지 01일때
        {
       
            timeText.text = "StageClear";                                                                                                                //스테이지 클리어 말이 뜬다
            portalParticle.SetActive(true);                                                                                                                //포탈 파티클을 활성화 한다
            float nextDist = portalParticle.transform.position.z - playerObj.transform.position.z;                                          //포탈과 플레이어 사이의 거리값을 계산한다
            if (nextDist < 2f)                                                                                                                               //포탈과 플레이어 사이의 거리가 2보다 작다면
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);                                                 //다음 씬을 로드한다
            }
        }
        if (limitTime <= 0 && SceneManager.GetActiveScene().name == "Stage02")                                                      //코인카운트가 25이고, 씬의 이름이 스테이지 02일때
        {
            timeText.text = "StageClear";                                                                                                                //스테이지 클리어 말이 뜬다
            portalParticle.SetActive(true);                                                                                                                //포탈 파티클을 활성화 한다
            float nextDist = portalParticle.transform.position.z - playerObj.transform.position.z;                                          //포탈과 플레이어 사이의 거리값을 계산한다
            if (nextDist < 2f)                                                                                                                               //포탈과 플레이어 사이의 거리가 2보다 작다면
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);                                                 //다음 씬을 로드한다
            }
        }
        if (coinCount == coinNum && SceneManager.GetActiveScene().name == "Stage03")                                           //코인카운트가 25이고, 씬의 이름이 스테이지 03일때
        {
            timeText.text = "StageClear";                                                                                                                //스테이지 클리어 말이 뜬다
            portalParticle.SetActive(true);                                                                                                                //포탈 파티클을 활성화 한다
            float nextDist = portalParticle.transform.position.z - playerObj.transform.position.z;                                          //포탈과 플레이어 사이의 거리값을 계산한다
            if (nextDist < 2f)                                                                                                                               //포탈과 플레이어 사이의 거리가 2보다 작다면
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);                                                 //다음 씬을 로드한다
            }
        }
        if (killCount == 50 && SceneManager.GetActiveScene().name == "Stage04")                                                      //킬 카운트가 50이고, 씬의 이름이 스테이지 04일때
        {

            timeText.text = "StageClear";                                                                                                                //스테이지 클리어 말이 뜬다
            portalParticle.SetActive(true);                                                                                                                //포탈 파티클을 활성화 한다
            float nextDist = portalParticle.transform.position.z - playerObj.transform.position.z;                                          //포탈과 플레이어 사이의 거리값을 계산한다
            if (nextDist < 2f)                                                                                                                               //포탈과 플레이어 사이의 거리가 2보다 작다면
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);                                                 //다음 씬을 로드한다
            }
        }
/*        if (killCount == 1 && SceneManager.GetActiveScene().name == "Stage05")                                                      //킬 카운트가 1이고, 씬의 이름이 스테이지 05일때
        {

        }*/
    }

    public void SettingButton()                                                                                                                          //설정 버튼을 누르면 발생할 것
    {
        settinngUI.SetActive(true);                                                                                                                    //설정 창 활성화
    }

    public void BackBtn()
    {
        settinngUI.SetActive(false);                                                                                                                    //설정 창 비활성화
    }                                                                                                                                //계속하기 버튼

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);                                                                   //현재 열려 있는 씬을 재시작한다
    }                                                                                                                            //다시 시작 버튼

    public void GameQuitBtn()                                                                                                                          //게임을 종료하는 버튼
    {
        Application.Quit();                                                                                                                                  //게임 종료
    }

    public void GameOver()
    {
        if (isEnd)
        {
            timeText.text = "";                                                                                                                           //게임 over텍스트
            gameOverUI.SetActive(true);                                                                                                              //게임 오버시 데임 오버게임오브젝트를 활성화 한다
        }
        
    }                                                                                                                           //게임 오버 메서드

    public void MissionDenote()                                                                                                                       //미션을 표시하는 메서드
    {
        if (SceneManager.GetActiveScene().name == "Stage01")                                                                               //만약 스테이지 1이라면
        {
            mission_Text.text = "몬스터 처치 " + "(" + killCount + "/" + EnemyManager.EM.createNum + ")";                      //플레이어의 미션를 표시한다
        }
        else if (SceneManager.GetActiveScene().name == "Stage02")                                                                         //만약 스테이지 2라면
        {
            mission_Text.text = "생존 ";                                                                                                                //플레이어의 미션를 표시한다
        }
        else if (SceneManager.GetActiveScene().name == "Stage03")                                                                         //만약 스테이지 3이라면
        {
            mission_Text.text = "보물 상자를 처치하세요";                                                                                        //플레이어의 미션를 표시한다
            if (StageThreeManager.TBM.isDie)                                                                                                      //만약 보물 상자가 죽은 상태라면
            {
                mission_Text.text = "코인 먹기"+"("+coinCount+"/"+coinNum+")";                                                         //코인먹기 미션을 표시한다
            }
        }
        if (SceneManager.GetActiveScene().name == "Stage04")                                                                               //만약 스테이지 1이라면
        {
            mission_Text.text = "몬스터 처치 " + "(" + killCount + "/" + EnemyManager.EM.createNum + ")";                      //플레이어의 미션를 표시한다
        }
        if (SceneManager.GetActiveScene().name == "Stage05")                                                                               //만약 스테이지 1이라면
        {
            mission_Text.text = "보스 처치 " + "(" + killCount + "/" + 1 + ")";                                                              //플레이어의 미션를 표시한다
        }
    }

    public void PlayerDamaged(int Damage)                                                                                                       //플레이어가 데미지를 받는 메서드
    {
        playerHp -= Damage;                                                                                                                            //게임 메니저에서 플레이어의 체력에서 장애물의 힘만큼 뺀다
        playerHp_MainUI.value = playerHp;                                                                                                           //UI에서 플레이어의 체력을 닳게 한다
        playerHp_SubUI.value =  playerHp;                                                                                                           //UI에서 플레이어의 체력을 닳게 한다
        playerHp_Text.text = playerHp + "/" + "500";                                                                                              //플레이어의 체력을 텍스트로 표시한다
    }

    public void ResetButton()
    {
        SceneManager.LoadScene("Stage01");                                                                                                      //스테이지 1을 로드한다
    }

    public void BossDead()
    {
        if (SceneManager.GetActiveScene().name == "Stage05")
        {
            if (EnemyManager.EM.boss.GetComponent<EnemyController>().enemyDHp <= 0)
            {
                bossHpUI.gameObject.SetActive(false);
                bossHp_Text.gameObject.SetActive(false);
                endGameUI.SetActive(true);
            }
        }
    }
}
