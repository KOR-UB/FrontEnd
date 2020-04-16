using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OneCommand : MonoBehaviour
{
    #region 태그에 따른 함수호출
    void Awake()
    {
        Application.targetFrameRate = 52;
        if (CompareTag("GameManager")) Awake_GM();
    }
    void Update()
    {
        if (CompareTag("GameManager")) Update_GM();
    }
    void FixedUpdate()
    {
        if (CompareTag("GameManager")) FixedUpdate_GM();
    }
    void Start()
    {
        if (CompareTag("Ball")) start_Ball();

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (CompareTag("Ball")) StartCoroutine(OnCollisionEnter2D_BALL(col));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (CompareTag("Ball")) StartCoroutine(OnTriggerEnter2D_BALL(col));
    }
    #endregion

    #region GameManager.Cs
    [Header("GameManagerValue")]
    public GameObject[] P_test = new GameObject[2];
    //public float groundY = -55.489f;
    public float groundY = -68.289f;
    public GameObject P_Ball, P_GreenOrb, P_ParticleBlue, P_ParticleGreen, P_ParticleRed, P_ParticleBrown;
    public GameObject BallPrivew, Arrow, GameOverPanel, BallCountTextObj, BallPlusTextObj, ExpTextObj;
    public GameObject BossInObj;
    public GameObject SkillInfoUI;
    public Transform GreenBallGroup, BlockGroup, BallGroup, WallGroup;
    public LineRenderer MouseLR, BallLR;
    public Text BestScoreText, ScoreText, BallCountText, BallPlusText, FinalScoreText, NewRecoredText, FinalLevelText;
    public Color[] blockColor;
    public Color greenColor;
    public AudioSource S_GameOver, S_GreenOrb, S_Plus;
    public AudioSource[] S_Block;
    public Quaternion QI = Quaternion.identity;
    public bool shotTrigger, shotable;
    public Vector3 veryFirstPos;
    public Camera cam;
    public bool isDie;
    public GameObject SkillBook;
    Vector3 firstPos, secondPos, gap;
    int timerCount, launchIndex;
    public int score;
    bool timerStart, isNewRecord, isBlockMoving;
    float timeDelay;
    public Data data;
    public DisplayData displaydata;
    public int[] bossHp;
    public bool camCheck = true;
    public int BossKill = 0;
    public int BossCnt = 0;
    public float ExpNum = 25.0f;
    public int BallCount = 1;
    public bool BOSSCHECK = false;
    public GameObject Panel;
    public GameObject NewYear;
    public UnityAdsManager AM;
    public GameObject AdsPop, AdsPop2 ,AdsPop3, AdsPop4;
    public GoogleManager googlemanager;
    public AdmobScreenAd Admob;
    public Wall WM;

    #region 시작
    void Awake_GM()
    {
            ////9:16 고정해상도 카메라
            //Camera camera = Camera.main;
            //Rect rect = camera.rect;
            //float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // (가로 / 세로)
            //float scalewidth = 1f / scaleheight;
            //if(scaleheight < 1)
            //{
            //    rect.height = scaleheight;
            //    rect.y = (-1f - scaleheight) / 2f;
            //}
            //else
            //{
            //    rect.width = scalewidth;
            //    rect.x = (1f - scalewidth) / 2f;
            //}
            //camera.rect = rect;
        BallCount = (int)GameObject.FindGameObjectsWithTag("Ball").Length;
        BlockGenerator();
        if(displaydata.LM.KoreanObj.activeSelf==true)
        {
            BestScoreText.text = "최고기록 : " + PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            BestScoreText.text = "Record : " + PlayerPrefs.GetInt("BestScore").ToString();
        }
    }
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void VeryFirstPosSet(Vector3 pos)
    {
        if (veryFirstPos == Vector3.zero)
            veryFirstPos = pos;
    }
    #endregion

    #region 블럭
    void BlockGenerator()
    {
        if(BossCnt == 1)
        {
            if(BossKill >= 2)
            {
                BossKill= 1;
            }
        }
        if (displaydata.ItemList[3] == true)
        {
            displaydata.randSkill();
        }
        //점수
        if (displaydata.LM.KoreanObj.activeSelf == true)
        {
            ScoreText.text = "현재점수 : " + (++score).ToString();
        }
        else
        {
            ScoreText.text = "Score : " + (++score).ToString();
        }
        if (PlayerPrefs.GetInt("BestScore", 0) < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
            if (displaydata.LM.KoreanObj.activeSelf == true)
            {
                BestScoreText.text = "최고기록 : " + PlayerPrefs.GetInt("BestScore").ToString();
            }
            else
            {
                BestScoreText.text = "Record : " + PlayerPrefs.GetInt("BestScore").ToString();
            }
            BestScoreText.color = greenColor;
            isNewRecord = true;
            googlemanager.AddLeaderboard();
        }
        int HPscore = score * (BossCnt + 1);
        if(BossCnt > 0)
        {
            HPscore = score * (BossCnt + 1) -displaydata.Level;
        }
        int count;
        int countB;
        int countC;
        int randBlock = Random.Range(0, 24);
        int randBlock2 = Random.Range(0, 24);
        int randBlock3 = Random.Range(0, 24);
        if (score < 20)
        {
            count = randBlock < 12 ? 1 : (randBlock < 20 ? 2 : 3);
            countB = 0;
            countC = 0;
        }
        else if (score == 20 || score == 40 || score == 60 || score == 80 || score == 100 || score == 120 || score == 140 || score == 160 || score == 180 || score == 200 || score == 220 || score == 240 || score == 260 || score == 280 || score == 300)
        {
            BOSSCHECK = true;
            count = 0;
            countB = 0;
            countC = 0;
            BossIn();
        }
        else if (score < 40)
        {
            count = randBlock < 14 ? 1 : (randBlock < 21 ? 2 : 3);
            countB = 0;
            countC = randBlock2 < 14 ? 1 : (randBlock2 < 21 ? 2 : 3);
        }
        else if (score < 60)
        {
            count = randBlock < 7 ? 1 : (randBlock < 14 ? 2 : (randBlock < 18 ? 3 : 4));
            countB = randBlock3 < 16 ? 1 : (randBlock3 < 22 ? 2 : 3);
            countC = randBlock2 < 7 ? 1 : (randBlock2 < 14 ? 2 : (randBlock2 < 18 ? 3 : 4));
        }
        else if (score < 80)
        {
            count = randBlock < 4 ? 1 : (randBlock < 10 ? 2 : (randBlock < 16? 3 : (randBlock < 22 ? 4 : 5)));
            countB = randBlock3 < 10 ? 1 : (randBlock3 < 19 ? 2 : 3);
            countC = randBlock2 < 4 ? 1 : (randBlock2 < 10 ? 2 : (randBlock2 < 16 ? 3 : (randBlock2 < 22 ? 4 : 5)));
        }
        else if (score < 100)
        {
            count = randBlock < 2 ? 1 : (randBlock < 8 ? 2 : (randBlock < 16 ? 3 : (randBlock < 22 ? 4 : 5)));
            countB = randBlock3 < 7 ? 1 : (randBlock3 < 16 ? 2 : 3);
            countC = randBlock2 < 2 ? 1 : (randBlock2 < 8 ? 2 : (randBlock2 < 16 ? 3 : (randBlock2 < 22 ? 4 : 5)));
        }
        else 
        {
            count = randBlock < 8 ? 2 : (randBlock < 16 ? 3 : (randBlock < 20 ? 4 : 5));
            countB = randBlock3 < 4 ? 1 : (randBlock3 < 12 ? 2 : 3);
            countC = randBlock2 < 8 ? 2 : (randBlock2 < 16 ? 3 : (randBlock2 < 20 ? 4 : 5));
        }
        List<Vector3> SpawnList = new List<Vector3>();
        List<Vector3> SpawnList2 = new List<Vector3>();
        List<Vector3> SpawnList3 = new List<Vector3>();
        for (int i = 0; i < 6; i++)
        {
            SpawnList.Add(new Vector3(-46.3f + i * 18.58f, 51.2f, 0));
            SpawnList2.Add(new Vector3(126.5f + i * 18.58f, 51.2f, 0));
        }
        for (int i = 0; i < 3; i++)
        {
            SpawnList3.Add(new Vector3(65.8f + i * 20.38f, 51.2f, 0));
        }
        if (score == 20)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[0].ToString();
        }
        if (score == 40)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[1].ToString();
        }
        if (score == 60)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[2].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[2].ToString();
        }
        if (score == 80)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[3].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[3].ToString();
        }
        if (score == 100)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[4].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[4].ToString();
        }
        if (score == 120)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[5].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[5].ToString();
        }
        if (score == 140)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[6].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[6].ToString();
        }
        if (score == 160)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[7].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[7].ToString();
        }
        if (score == 180)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[8].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[8].ToString();
        }
        if (score == 200)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[9].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[9].ToString();
        }
        if (score == 220)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[10].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[10].ToString();
        }
        if (score == 240)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[11].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[11].ToString();
        }
        if (score == 260)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[12].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[12].ToString();
        }
        if (score == 280)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[13].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[13].ToString();
        }
        if (score == 300)
        {
            Transform TR = Instantiate(P_test[1], new Vector3(0, 51, 0), QI).transform;
            Transform TR2 = Instantiate(P_test[1], new Vector3(173.3f, 51, 0), QI).transform;
            TR.SetParent(BlockGroup);
            TR2.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = bossHp[14].ToString();
            TR2.GetChild(0).GetComponentInChildren<Text>().text = bossHp[14].ToString();
        }
        for (int i = 0; i < countB; i++)
        {
            int rand3 = Random.Range(0, SpawnList3.Count);
            if (BossKill >= 2)
            {
                Transform TR3 = Instantiate(P_test[2], SpawnList3[rand3], QI).transform; //? error
                TR3.SetParent(BlockGroup);
                HPscore += score;
                TR3.GetChild(0).GetComponentInChildren<Text>().text = HPscore.ToString();
                HPscore -= score;
                if (displaydata.BuffCheck == 1) //축복 빙결
                {
                    TR3.gameObject.GetComponent<SpriteOutline>().color = blockColor[8];
                }
                if (displaydata.BuffCheck == 2) //저주 최대체력 증가
                {
                    int Num = score*2;
                    int BonusScore = HPscore + Num;
                    TR3.GetChild(0).GetComponentInChildren<Text>().text = BonusScore.ToString();
                }
                SpawnList3.RemoveAt(rand3);
            }
            System.GC.Collect();
        }
        for (int i = 0; i < countC; i++)
        {
            int rand2 = Random.Range(0, SpawnList2.Count);
            if (BossKill >= 1)
            {
                Transform TR2 = Instantiate(P_test[0], SpawnList2[rand2], QI).transform;
                TR2.SetParent(BlockGroup);
                TR2.GetChild(0).GetComponentInChildren<Text>().text = HPscore.ToString();
                if (displaydata.BuffCheck == 1) //축복 빙결
                {
                    TR2.gameObject.GetComponent<SpriteOutline>().color = blockColor[8];
                }
                if (displaydata.BuffCheck == 2) //저주 최대체력 증가
                {
                    int Num = score;
                    int BonusScore = HPscore + Num;
                    TR2.GetChild(0).GetComponentInChildren<Text>().text = BonusScore.ToString();
                }
                SpawnList2.RemoveAt(rand2);
            }
            System.GC.Collect();
        }
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, SpawnList.Count);
            //if (BossKill >= 2)
            //{
            //    Transform TR3 = Instantiate(P_test[0], SpawnList3[rand3], QI).transform; //? error
            //    TR3.SetParent(BlockGroup);
            //    TR3.GetChild(0).GetComponentInChildren<Text>().text = HPscore.ToString();
            //    if (displaydata.BuffCheck == 1) //축복 빙결
            //    {
            //        TR3.gameObject.GetComponent<SpriteOutline>().color = blockColor[8];
            //    }
            //    if (displaydata.BuffCheck == 2) //저주 최대체력 증가
            //    {
            //        int Num = score;
            //        int BonusScore = HPscore + Num;
            //        TR3.GetChild(0).GetComponentInChildren<Text>().text = BonusScore.ToString();
            //    }
            //    SpawnList3.RemoveAt(rand3);
            //}
            Transform TR = Instantiate(P_test[0], SpawnList[rand], QI).transform;
            TR.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = HPscore.ToString();
            if (displaydata.BuffCheck == 1) //축복 빙결
            {
                TR.gameObject.GetComponent<SpriteOutline>().color = blockColor[8];
            }
            if (displaydata.BuffCheck == 2) //저주 최대체력 증가
            {
                int Num = score;
                int BonusScore = HPscore + Num;
                TR.GetChild(0).GetComponentInChildren<Text>().text = BonusScore.ToString();
            }
            SpawnList.RemoveAt(rand);
            System.GC.Collect();
            //SpawnList2.RemoveAt(rand2);
        }

        if (BOSSCHECK == false)
        {
            Instantiate(P_GreenOrb, SpawnList[Random.Range(0, SpawnList.Count)], QI).transform.SetParent(BlockGroup);
            if (BossKill >= 1)
            {
                Instantiate(P_GreenOrb, SpawnList2[Random.Range(0, SpawnList2.Count)], QI).transform.SetParent(BlockGroup);
            }
        }
        //Instantiate(P_GreenOrb, SpawnList2[Random.Range(0, SpawnList2.Count)], QI).transform.SetParent(BlockGroup);

        isBlockMoving = true; //블럭 내리기
        for (int i = 0; i < BlockGroup.childCount; i++)
        {
            StartCoroutine(BlockMoveDown(BlockGroup.GetChild(i)));
        }
    }
    IEnumerator BlockMoveDown(Transform TR)
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 targetPos = TR.position + new Vector3(0, -12.8f, 0);

        //BlockColorChange();
        if (targetPos.y < -60) //막줄이면 게임오버 트리거, 콜라이더 비활성화
        {
            if (TR.CompareTag("Block") || TR.CompareTag("Boss")) isDie = true;
            for (int i = 0; i < BallGroup.childCount; i++)
                BallGroup.GetChild(i).GetComponent<CircleCollider2D>().enabled = false;
        }

        float TT = 2.5f;
        while (true)
        {
            yield return null;
            TT -= Time.deltaTime * 1.5f;
            TR.position = Vector3.MoveTowards(TR.position, targetPos + new Vector3(0, -6, 0), TT);
            if (TR.position == targetPos + new Vector3(0, -6, 0)) break;
        }
        TT = 1.9f;
        while (true)
        {
            yield return null; TT -= Time.deltaTime;
            TR.position = Vector3.MoveTowards(TR.position, targetPos, TT);
            if (TR.position == targetPos) break;
        }
        isBlockMoving = false;

        if (targetPos.y < -60) //이동된 후 마지막 줄이면 블럭일 때 게임오버 녹색공일때 파괴
        {
            if (TR.CompareTag("Block") || TR.CompareTag("Boss"))
            {
                cam.GetComponent<Animator>().enabled = true;
                if (displaydata.SoundGroup.activeSelf == true)
                {
                    S_GameOver.Play();
                }
                for (int i = 0; i < BallGroup.childCount; i++)
                {
                    Destroy(BallGroup.GetChild(i).gameObject);
                }
                Destroy(Instantiate(P_ParticleGreen, veryFirstPos, QI), 1);
                displaydata.TimeScaleCheck = false;
                Time.timeScale = 1;
                BallCountTextObj.SetActive(false);
                BallPlusTextObj.SetActive(false);
                BestScoreText.gameObject.SetActive(false);
                ScoreText.gameObject.SetActive(false);
                WM.Die();
                Admob.Invoke("Show", 0.5f);
                GameOverPanel.SetActive(true);

                ExpTextObj.SetActive(false);
                FinalScoreText.text = "최종점수 : " + score.ToString();
                FinalLevelText.text = "최종레벨 : " + displaydata.Level.ToString();

                if (isNewRecord) NewRecoredText.gameObject.SetActive(true);


                if (BossKill >= 1)
                {
                    Camera.main.GetComponent<Animator>().SetTrigger("shake");
                }
                else if (BossKill < 1)
                {
                    Camera.main.GetComponent<Animator>().SetTrigger("shake2");
                }
                //AM.ShowRewarded();
            }
            else
            {
                Destroy(TR.gameObject);
                Destroy(Instantiate(P_ParticleGreen, TR.position, QI), 1);
                if (displaydata.SoundGroup.activeSelf == true)
                {
                    S_GreenOrb.Play();
                }
                for (int i = 0; i < BallGroup.childCount; i++)
                {
                    BallGroup.GetChild(i).GetComponent<CircleCollider2D>().enabled = true;
                }
            }
        }
        if (TR.CompareTag("Block") || TR.CompareTag("Boss"))
        {
            TR.gameObject.GetComponent<SpriteOutline>().PoisonDps();
        }
    }


    //public void BlockColorChange()
    //{
    //    for (int i = 0; i < BlockGroup.childCount; i++)
    //    {
    //        if(BlockGroup.GetChild(i).CompareTag("Block"))
    //        {
    //            float per = int.Parse(BlockGroup.GetChild(i).GetChild(0).GetComponentInChildren<Text>().text) / (float)score;
    //            Color curColor;
    //            if (per <= 0.1428f) curColor = blockColor[6];
    //            else if (per <= 0.2856f) curColor = blockColor[5];
    //            else if (per <= 0.4284f) curColor = blockColor[4];
    //            else if (per <= 0.5172f) curColor = blockColor[3];
    //            else if (per <= 0.714f) curColor = blockColor[2];
    //            else if (per <= 0.8568f) curColor = blockColor[1];
    //            else curColor = blockColor[0];
    //            BlockGroup.GetChild(i).GetComponent<SpriteRenderer>().color = curColor;
    //        }
    //    }
    //}

    #endregion
    void Update_GM()
    {
        //Debug.Log(Input.mousePosition);
        if (Panel.activeSelf == true) return;
        //if (SkillInfoUI.activeSelf == true) return;
        //if (displaydata.OptionImg.activeSelf == true) return;
        //if (BossKill == 0)
        //{
        //    if (Input.mousePosition.y <= 70)
        //    {
        //        MouseLR.SetPosition(0, Vector3.zero);
        //        MouseLR.SetPosition(1, Vector3.zero);
        //        BallLR.SetPosition(0, Vector3.zero);
        //        BallLR.SetPosition(1, Vector3.zero);
        //        BallPrivew.SetActive(false);
        //        Arrow.SetActive(false);
        //        return;
        //    }
        //}
        //else if (BossKill > 0)
        //{
        //    if (Input.mousePosition.y <= 70)
        //    {
        //        MouseLR.SetPosition(0, Vector3.zero);
        //        MouseLR.SetPosition(1, Vector3.zero);
        //        BallLR.SetPosition(0, Vector3.zero);
        //        BallLR.SetPosition(1, Vector3.zero);
        //        BallPrivew.SetActive(false);
        //        Arrow.SetActive(false);
        //        return;
        //    }
        //}
        //if (SkillBook.activeSelf == true) return;
        if (isDie) return;
        if(displaydata.ItemList[5])
        {
            if (BallCount >= 70)
            {
                GameObject[] Temp = GameObject.FindGameObjectsWithTag("Ball");
                foreach (GameObject ob in Temp)
                {
                    if (ob.name == "Ball")
                    {
                        BallGroup.GetChild(0).GetChild(0).transform.localScale += new Vector3(0.7f, 0.7f, 0.7f);
                        ob.GetComponent<OneCommand>().Dmg += 99;
                        if (ob.GetComponent<OneCommand>().Dmg > 100)
                        {
                            ob.GetComponent<OneCommand>().Dmg++;
                        }
                    }
                    else if (ob.name == "Ball(Clone)")
                    {
                        Destroy(ob);
                        BallCount--;
                    }
                }
            }
        }
        else if(!displaydata.ItemList[5])
        {
            if (BallCount >= 100)
            {
                GameObject[] Temp = GameObject.FindGameObjectsWithTag("Ball");
                foreach (GameObject ob in Temp)
                {
                    if (ob.name == "Ball")
                    {
                        BallGroup.GetChild(0).GetChild(0).transform.localScale += new Vector3(0.7f, 0.7f, 0.7f);
                        ob.GetComponent<OneCommand>().Dmg += 99;
                        if (ob.GetComponent<OneCommand>().Dmg > 100)
                        {
                            ob.GetComponent<OneCommand>().Dmg++;
                        }
                    }
                    else if (ob.name == "Ball(Clone)")
                    {
                        Destroy(ob);
                        BallCount--;
                    }
                }
            }
        }
        //마우스 첫번째 좌표
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition * 0.85f) + new Vector3(0, 0, 10);
        }
        shotable = true;
        for (int i = 0; i < BallGroup.childCount; i++)
        {
            if (BallGroup.GetChild(i).GetComponent<OneCommand>().isMoving) shotable = false;
            if (isBlockMoving) shotable = false;

            if (!shotable) return;
        }

        if (shotTrigger && shotable) //모든 공이 바닥에 부딪히면 한번만 실행
        {
            shotTrigger = false;
            displaydata.Withdraw.SetActive(false);
            displaydata.WithdrawBtn.SetActive(false);
            if (AM.BallEffectAds == true)
            {
                AM.ES.RandomEffect();
            }
            BlockGenerator(); //?error
            timeDelay = 0;

            StartCoroutine(BallCountTextShow(GreenBallGroup.childCount));
            for (int i = 0; i < GreenBallGroup.childCount; i++) StartCoroutine(GreenBallMove(GreenBallGroup.GetChild(i)));
        }

        timeDelay += Time.deltaTime;
        if (timeDelay < 0.1f) return; //0.1초 딜레이로 너무 빠르게 손을 떼면 라인이 남는 버그 제거

        bool isMouse = Input.GetMouseButton(0);
        if (isMouse)
        {
            //차이값
            if (Input.mousePosition.y <= 110)
            {
                MouseLR.SetPosition(0, Vector3.zero);
                MouseLR.SetPosition(1, Vector3.zero);
                BallLR.SetPosition(0, Vector3.zero);
                BallLR.SetPosition(1, Vector3.zero);
                BallPrivew.SetActive(false);
                Arrow.SetActive(false);
                return;
            }
            if(Panel.activeSelf == true)
            {
                MouseLR.SetPosition(0, Vector3.zero);
                MouseLR.SetPosition(1, Vector3.zero);
                BallLR.SetPosition(0, Vector3.zero);
                BallLR.SetPosition(1, Vector3.zero);
                BallPrivew.SetActive(false);
                Arrow.SetActive(false);
                return;
            }
            secondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition * 0.85f) + new Vector3(0, 0, 10);
            if ((secondPos - firstPos).magnitude < 1) return;
            gap = (secondPos - firstPos).normalized;
            gap = new Vector3(gap.y >= 0 ? gap.x : gap.x >= 0 ? 1 : -1, Mathf.Clamp(gap.y, 0.16f, 1), 0);


            //화살표, 공 미리보기
            Arrow.transform.position = veryFirstPos;
            Arrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(gap.y, gap.x) * Mathf.Rad2Deg);
            BallPrivew.transform.position = Physics2D.CircleCast(new Vector2(Mathf.Clamp(veryFirstPos.x, -300, 300), groundY), 1.5f, gap, 10000, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Block")).centroid; //1.8

            RaycastHit2D hit = Physics2D.Raycast(veryFirstPos, gap, 10000, 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Block"));

            //라인
            MouseLR.SetPosition(0, firstPos);
            MouseLR.SetPosition(1, secondPos);
            BallLR.SetPosition(0, veryFirstPos);
            BallLR.SetPosition(1, (Vector3)hit.point - gap * 1.5f);
        }
        BallPrivew.SetActive(isMouse);
        Arrow.SetActive(isMouse);

        if (Input.GetMouseButtonUp(0))
        {
            if (Input.mousePosition.y <= 110)
            {
                MouseLR.SetPosition(0, Vector3.zero);
                MouseLR.SetPosition(1, Vector3.zero);
                BallLR.SetPosition(0, Vector3.zero);
                BallLR.SetPosition(1, Vector3.zero);
                BallPrivew.SetActive(false);
                Arrow.SetActive(false);
                return;
            }
            if ((secondPos - firstPos).magnitude < 1) return;
            camCheck = false;
            MouseLR.SetPosition(0, Vector3.zero);
            MouseLR.SetPosition(1, Vector3.zero);
            BallLR.SetPosition(0, Vector3.zero);
            BallLR.SetPosition(1, Vector3.zero);
            displaydata.WithdrawBtn.SetActive(true);
            timerStart = true;
            veryFirstPos = Vector3.zero;
            firstPos = Vector3.zero;
        }
    }
    IEnumerator BallCountTextShow(int greenBallCount)
    {
        BallCountTextObj.transform.position = new Vector3(Mathf.Clamp(veryFirstPos.x, -262.7f, 262.7f), -77.8f, 0);
        BallCountText.text = "x" + (int.Parse(BallGroup.childCount.ToString()) + BallGroup.GetChild(0).GetComponent<OneCommand>().Dmg-1);
        yield return new WaitForSeconds(0.17f);

        //if (BallGroup.childCount > score) Destroy(BallGroup.GetChild(BallGroup.childCount -1).gameObject);
        BallCountText.text = "x" + (int.Parse(BallGroup.childCount.ToString()) + BallGroup.GetChild(0).GetComponent<OneCommand>().Dmg-1);

        if (greenBallCount != 0) //바닥에 떨어진 녹색공 +로 표시하기
        {
            BallPlusTextObj.SetActive(true);
            BallPlusTextObj.transform.position = new Vector3(Mathf.Clamp(veryFirstPos.x, -249.9f, 249.9f), -67, 0);
            BallPlusText.text = "+" + greenBallCount.ToString();
            if (displaydata.SoundGroup.activeSelf == true)
            {
                S_Plus.Play();
            }
            yield return new WaitForSeconds(0.5f);

            BallPlusTextObj.SetActive(false);
        }
    }
    IEnumerator GreenBallMove(Transform TR)
    {
        //바닥에 떨어진 초록공 최초자표로 0.34초간 이동
        Instantiate(P_Ball, veryFirstPos, QI).transform.SetParent(BallGroup);
        BallCount++;
        System.GC.Collect();
        //Debug.Log("지금까지 추가한 공 카운트: " + BallCount);
        float speed = (TR.position - veryFirstPos).magnitude * 0.1f; //22
        while (true)
        {
            yield return null;
            TR.position = Vector3.MoveTowards(TR.position, veryFirstPos, speed);
            if (TR.position == veryFirstPos) { Destroy(TR.gameObject); yield break; }
        }
    }

    void FixedUpdate_GM()
    {
        //0.06초 간격으로 공 발사
        if (timerStart && ++timerCount == 3)
        {
            timerCount = 0;
            if (launchIndex >= 101)
            {
                launchIndex = 0;
            }
            BallGroup.GetChild(launchIndex++).GetComponent<OneCommand>().Launch(gap);
            BallCountText.text = "x" + (BallGroup.childCount - launchIndex).ToString();
            if (launchIndex == BallGroup.childCount)
            {
                timerStart = false;
                launchIndex = 0;
                BallCountText.text = "";
            }
        }
    }
    void BossIn()
    {
        BossInObj.SetActive(true);
        Invoke("BossOut", 3.0f);
    }
    void BossOut()
    {
        BossInObj.SetActive(false);
        BossCnt++;
        BOSSCHECK = false;
    }
    public void AdsPopObj()
    {
        if (!googlemanager.LoginEnable) return;
        if (shotTrigger == true) return;
        if (displaydata.TimeScaleCheck == true)
        {
            displaydata.TimeScaleBtn.GetComponent<Animator>().SetBool("Speed", false);
            Time.timeScale = 1;
            displaydata.TimeScaleCheck = false;
        }
        else if (displaydata.TimeScaleCheck == false)
        {
            if (displaydata.LM.KoreanObj.activeSelf == true)
            {
                AdsPop.SetActive(true);
                Panel.SetActive(true);
            }
            else
            {
                AdsPop3.SetActive(true);
                Panel.SetActive(true);
            }
        }
    }
    public void AdsPopObj2()
    {
        if (!googlemanager.LoginEnable) return;
        if (shotTrigger == true) return;
        if (AM.BallEffectAds == true)
        {
            BallGroup.GetChild(0).GetChild(0).gameObject.SetActive(false);
            AM.BallEffectAds = false;
            return;
        }
        if (displaydata.LM.KoreanObj.activeSelf == true)
        {
            AdsPop2.SetActive(true);
            Panel.SetActive(true);
        }
        else
        {
            AdsPop4.SetActive(true);
            Panel.SetActive(true);
        }
    }
    public void AdsPopYes()
    {
        if (displaydata.LM.KoreanObj.activeSelf == true)
        {
            AdsPop.SetActive(false);
        }
        else
        {
            AdsPop3.SetActive(false);
        }
        AM.ShowRewarded();
    }
    public void AdsPopYes2()
    {
        if (displaydata.LM.KoreanObj.activeSelf == true)
        {
            AdsPop2.SetActive(false);
        }
        else
        {
            AdsPop4.SetActive(false);
        }
        AM.ShowRewarded2();
    }
    public void AdsPopNo()
    {
        if (displaydata.LM.KoreanObj.activeSelf == true)
        {
            AdsPop.SetActive(false);
            Panel.SetActive(false);
        }
        else
        {
            AdsPop3.SetActive(false);
            Panel.SetActive(false);
        }
    }
    public void AdsPopNo2()
    {
        if (displaydata.LM.KoreanObj.activeSelf == true)
        {
            AdsPop2.SetActive(false);
            Panel.SetActive(false);
        }
        else
        {
            AdsPop4.SetActive(false) ;
            Panel.SetActive(false);
        }
    }
    #endregion

    #region BallScript.Cs
    [Header("BallScriptValue")]
    public GameObject P_GreenBall;
    public Rigidbody2D RB;
    public bool isMoving;
    public int Dmg = 1;

    OneCommand GM;

    void start_Ball() => GM = GameObject.FindWithTag("GameManager").GetComponent<OneCommand>();
    
    public void Launch(Vector3 pos)
    {
        GM.shotTrigger = true;
        isMoving = true;
        if(!GM.displaydata.TimeScaleCheck)
        {
            RB.AddForce(pos * 7000);
        }
        else if(GM.displaydata.TimeScaleCheck)
        {
            RB.AddForce(pos * 9000);
        }
    }
    IEnumerator OnCollisionEnter2D_BALL(Collision2D col)
    {
        GameObject Col = col.gameObject;
        Physics2D.IgnoreLayerCollision(2, 2);

        Vector2 pos = RB.velocity.normalized; //가로로 움직일 경우 아래로 내림
        if (pos.magnitude != 0 && pos.y < 0.1f && pos.y > -0.1f)
        {
            RB.velocity = Vector2.zero;
            if (!GM.displaydata.TimeScaleCheck)
            {
                RB.AddForce(new Vector2(pos.x > 0 ? 1 : -1, -0.2f).normalized * 7000);
            }
            else if (GM.displaydata.TimeScaleCheck)
            {
                RB.AddForce(new Vector2(pos.x > 0 ? 1 : -1, -0.2f).normalized * 9000);
            }
        }
        if(Col.CompareTag("Error"))
        {
            this.transform.position = Vector3.zero;
        }
        if (Col.CompareTag("Ground")) //바닥 충돌시 최초좌표로 이동
        {
            GM.camCheck = true;
            //GM.cam.GetComponent<Animator>().enabled = true;
            RB.velocity = Vector2.zero;
            transform.position = new Vector2(col.contacts[0].point.x, GM.groundY);
            GM.VeryFirstPosSet(transform.position);

            while (true)
            {
                yield return null;
                transform.position = Vector3.MoveTowards(transform.position, GM.veryFirstPos, 4f);
                if (transform.position == GM.veryFirstPos)
                {
                    isMoving = false;
                    yield break;
                }
            }
        }
        if (Col.CompareTag("Block") || Col.CompareTag("Boss")) //블럭 충돌시 블럭 숫자 1씩 감소 및 0이 되면 파괴
        {
            int rand = Random.Range(1, 101);
            //Debug.Log(rand);
            Text BlockText = col.transform.GetChild(0).GetComponentInChildren<Text>();
            int blockValue = int.Parse(BlockText.text) - Dmg;
            if (Col.GetComponent<SpriteOutline>().color == GM.blockColor[8])
            {
                int Brand = 0;
                blockValue -= Dmg;

                if (GM.displaydata.L_SkillInfo[0].Count < 14 && GM.displaydata.L_SkillInfo[0].Count > 6)
                {
                    Brand = 5;
                }
                else if (GM.displaydata.L_SkillInfo[0].Count < 20 && GM.displaydata.L_SkillInfo[0].Count > 13)
                {
                    Brand = 10;
                }
                else if (GM.displaydata.L_SkillInfo[0].Count == 20)
                {
                    Brand = 20;
                }
                if (rand - Brand <= 4 * GM.displaydata.L_SkillInfo[0].Count)
                {
                    blockValue -= Dmg;
                    if (GM.displaydata.ItemList[4] == true)
                    {
                        blockValue -= Dmg * 2;
                    }
                }
                else if (GM.displaydata.ItemList[4] == true)
                {
                    blockValue -= Dmg * 2;
                }
            }
            if (GM.displaydata.L_SkillInfo[0].Count <= 20)
            {
                int Brand = 0;
                if (GM.displaydata.L_SkillInfo[0].Count < 14 && GM.displaydata.L_SkillInfo[0].Count > 6)
                {
                    Brand = 5;
                }
                else if (GM.displaydata.L_SkillInfo[0].Count < 20 && GM.displaydata.L_SkillInfo[0].Count > 13)
                {
                    Brand = 10;
                }
                else if (GM.displaydata.L_SkillInfo[0].Count == 20)
                {
                    Brand = 20;
                }
                if (rand - Brand <= 4 * GM.displaydata.L_SkillInfo[0].Count)
                {
                    blockValue -= Dmg;
                    if (Col.GetComponent<SpriteOutline>().color == GM.blockColor[8])
                    {
                        if (GM.displaydata.ItemList[4] == true)
                        {
                            blockValue -= Dmg * 2;
                        }
                    }
                }
            }
            if (blockValue <= 0)
            {
                Destroy(Col);
                Destroy(Instantiate(GM.P_ParticleRed, col.transform.position, GM.QI), 1);
                if (GM.displaydata.M_SkillInfo[0].Count <= 20)
                {
                    int Brand = 0;
                    if (GM.displaydata.M_SkillInfo[0].Count < 14 && GM.displaydata.M_SkillInfo[0].Count > 6)
                    {
                        Brand = 3;
                    }
                    else if (GM.displaydata.M_SkillInfo[0].Count < 20 && GM.displaydata.M_SkillInfo[0].Count > 13)
                    {
                        Brand = 6;
                    }
                    else if (GM.displaydata.M_SkillInfo[0].Count == 20)
                    {
                        Brand = 10;
                    }
                    if (rand - Brand <= 2 * GM.displaydata.M_SkillInfo[0].Count) //2% 확률로 추가 공 생성
                    {
                        Instantiate(GM.P_GreenOrb, Col.transform.position, GM.QI).transform.SetParent(GM.BlockGroup);
                    }
                }
                if (Col.CompareTag("Boss"))
                {
                    if (GM.displaydata.M_SkillInfo[1].Count <= 20 && GM.displaydata.M_SkillInfo[1].Count <= 20)
                    {
                        if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
                        {
                            GM.displaydata.Exp += GM.ExpNum;
                        }
                    }
                    if (GM.BossCnt == 1)
                    {
                        GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
                        GM.NewYear.SetActive(false);
                    }
                    else if (GM.BossCnt == 2)
                    {
                        GM.WallGroup.GetComponent<Animator>().SetTrigger("Test");
                    }
                    //else if (GM.BossCnt == 5)
                    //{
                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
                    //}
                    //else if (GM.BossCnt == 6)
                    //{
                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
                    //}
                    //else if (GM.BossCnt == 7)
                    //{
                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
                    //}
                    //else if (GM.BossCnt == 8)
                    //{
                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
                    //}
                    //GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
                    GM.displaydata.Exp += GM.ExpNum * (GM.BossCnt + 3);
                    if (GM.displaydata.M_SkillInfo[1].Count <= 20)
                    {
                        if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
                        {
                            GM.displaydata.Exp += GM.ExpNum;
                        }
                    }
                    GM.BossKill++;
                    GM.displaydata.UiUpdate();
                    //보스 잡음
                }
                if (Col.CompareTag("Block"))
                {
                    //Destroy(Col);
                    //Destroy(Instantiate(GM.P_ParticleRed, col.transform.position, GM.QI), 1);
                    GM.displaydata.Exp += GM.ExpNum;
                    if (GM.displaydata.M_SkillInfo[1].Count <= 20)
                    {
                        if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
                        {
                            GM.displaydata.Exp += GM.ExpNum;
                        }
                    }
                    GM.displaydata.UiUpdate();
                }
                System.GC.Collect();
            }
            if (blockValue > 0 || blockValue <= 0 || blockValue > 0 && blockValue <= 0)
            {
                BlockText.text = blockValue.ToString();
                Col.GetComponent<Animator>().SetTrigger("shock");
                if (GM.displaydata.R_SkillInfo[0].Count <= 20)
                {
                    int Brand = 0;
                    if (GM.displaydata.R_SkillInfo[0].Count < 14 && GM.displaydata.R_SkillInfo[0].Count > 6)
                    {
                        Brand = 1;
                        //Debug.Log("대지 1");
                    }
                    else if (GM.displaydata.R_SkillInfo[0].Count < 20 && GM.displaydata.R_SkillInfo[0].Count > 13)
                    {
                        Brand = 3;
                        //Debug.Log("대지 3");
                    }
                    else if (GM.displaydata.R_SkillInfo[0].Count == 20)
                    {
                        Brand = 5;
                        //Debug.Log("대지 5");
                    }
                    if (rand - Brand <= 1 * GM.displaydata.R_SkillInfo[0].Count)
                    {
                        GameObject[] tempobj = GameObject.FindGameObjectsWithTag("BlockText");

                        foreach (GameObject ob in tempobj)
                        {
                            rand = Random.Range(1, 101);
                            Text AllBlockText = ob.GetComponent<Text>();
                            //Debug.Log(AllBlockText.text);
                            int AllblockValue = int.Parse(AllBlockText.text) - Dmg;
                            if (AllblockValue > 0)
                            {
                                AllBlockText.text = AllblockValue.ToString();
                                ob.transform.parent.parent.GetComponent<Animator>().SetTrigger("shock");
                            }
                            //if (AllblockValue <= 0) //치명적
                            if (AllblockValue <= 0)
                            {
                                if (ob.transform.parent.parent.CompareTag("Block"))
                                {
                                    Destroy(ob.transform.parent.parent.gameObject);
                                    Destroy(Instantiate(GM.P_ParticleRed, ob.transform.position, GM.QI), 1);
                                    GM.displaydata.Exp += GM.ExpNum;
                                    GM.displaydata.UiUpdate();
                                    if (GM.displaydata.M_SkillInfo[1].Count <= 20)
                                    {
                                        if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
                                        {
                                            GM.displaydata.Exp += GM.ExpNum;
                                        }
                                        if (GM.displaydata.M_SkillInfo[0].Count <= 20 && GM.displaydata.M_SkillInfo[0].Count > 0)
                                        {
                                            int Borand = 0;
                                            if (GM.displaydata.M_SkillInfo[0].Count < 14 && GM.displaydata.M_SkillInfo[0].Count > 6)
                                            {
                                                Borand = 3;
                                            }
                                            else if (GM.displaydata.M_SkillInfo[0].Count < 20 && GM.displaydata.M_SkillInfo[0].Count > 13)
                                            {
                                                Borand = 6;
                                            }
                                            else if (GM.displaydata.M_SkillInfo[0].Count == 20)
                                            {
                                                Borand = 10;
                                            }
                                            if (rand - Borand <= 2 * GM.displaydata.M_SkillInfo[0].Count) //2% 확률로 추가 공 생성
                                            {
                                                Instantiate(GM.P_GreenOrb, ob.transform.parent.parent.transform.position, GM.QI).transform.SetParent(GM.BlockGroup);
                                            }
                                        }
                                    }
                                }
                                if (ob.transform.parent.parent.CompareTag("Boss"))
                                {
                                    Destroy(ob.transform.parent.parent.gameObject);
                                    Destroy(Instantiate(GM.P_ParticleRed, ob.transform.position, GM.QI), 1);
                                    if (GM.displaydata.M_SkillInfo[1].Count <= 20)
                                    {
                                        if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
                                        {
                                            GM.displaydata.Exp += GM.ExpNum;
                                        }
                                        if (GM.displaydata.M_SkillInfo[0].Count <= 20 && GM.displaydata.M_SkillInfo[0].Count > 0)
                                        {
                                            int Borand = 0;
                                            if (GM.displaydata.M_SkillInfo[0].Count < 14 && GM.displaydata.M_SkillInfo[0].Count > 6)
                                            {
                                                Borand = 3;
                                            }
                                            else if (GM.displaydata.M_SkillInfo[0].Count < 20 && GM.displaydata.M_SkillInfo[0].Count > 13)
                                            {
                                                Borand = 6;
                                            }
                                            else if (GM.displaydata.M_SkillInfo[0].Count == 20)
                                            {
                                                Borand = 10;
                                            }
                                            if (rand - Borand <= 2 * GM.displaydata.M_SkillInfo[0].Count) //2% 확률로 추가 공 생성
                                            {
                                                Instantiate(GM.P_GreenOrb, ob.transform.parent.parent.transform.position, GM.QI).transform.SetParent(GM.BlockGroup);
                                            }
                                        }
                                    }
                                    if (GM.BossCnt == 1)
                                    {
                                        GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
                                        GM.NewYear.SetActive(false);
                                    }
                                    else if (GM.BossCnt == 2)
                                    {
                                        GM.WallGroup.GetComponent<Animator>().SetTrigger("Test");
                                    }
                                    //else if (GM.BossCnt == 5)
                                    //{
                                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
                                    //}
                                    //else if (GM.BossCnt == 6)
                                    //{
                                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
                                    //}
                                    //else if (GM.BossCnt == 7)
                                    //{
                                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
                                    //}
                                    //else if (GM.BossCnt == 8)
                                    //{
                                    //    GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
                                    //}
                                    GM.displaydata.Exp += GM.ExpNum * (GM.BossCnt + 3);
                                    GM.BossKill++;
                                    GM.displaydata.UiUpdate();
                                    System.GC.Collect();
                                    //GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
                                }
                                //blockValue = 1;
                                //Destroy(ob.transform.parent.parent.gameObject);
                                //Destroy(Instantiate(GM.P_ParticleRed, ob.transform.position, GM.QI), 1);
                            }
                        }
                    }
                }
                //if (blockValue <= 0)
                //{

                //    if (GM.displaydata.M_SkillInfo[0].Count <= 20)
                //    {
                //        int Brand = 0;
                //        if (GM.displaydata.M_SkillInfo[0].Count < 14 && GM.displaydata.M_SkillInfo[0].Count > 6)
                //        {
                //            Brand = 3;
                //        }
                //        else if (GM.displaydata.M_SkillInfo[0].Count < 20 && GM.displaydata.M_SkillInfo[0].Count > 13)
                //        {
                //            Brand = 6;
                //        }
                //        else if (GM.displaydata.M_SkillInfo[0].Count == 20)
                //        {
                //            Brand = 10;
                //        }
                //        if (rand - Brand <= 2 * GM.displaydata.M_SkillInfo[0].Count) //2% 확률로 추가 공 생성
                //        {
                //            Instantiate(GM.P_GreenOrb, Col.transform.position, GM.QI).transform.SetParent(GM.BlockGroup);
                //        }
                //    }
                //    if (Col.CompareTag("Boss"))
                //    {
                //        GM.displaydata.Exp += GM.ExpNum * (GM.BossCnt + 3);
                //        if (GM.displaydata.M_SkillInfo[1].Count <= 20 && GM.displaydata.M_SkillInfo[1].Count <= 20)
                //        {
                //            if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
                //            {
                //                GM.displaydata.Exp += GM.ExpNum;
                //            }
                //        }
                //        if (GM.BossCnt == 1)
                //        {
                //            GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
                //            GM.NewYear.SetActive(false);
                //        }
                //        else if (GM.BossCnt == 2)
                //        {
                //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Test");
                //        }
                //        else if (GM.BossCnt == 5)
                //        {
                //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
                //        }
                //        else if (GM.BossCnt == 6)
                //        {
                //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
                //        }
                //        else if (GM.BossCnt == 7)
                //        {
                //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
                //        }
                //        else if (GM.BossCnt == 8)
                //        {
                //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
                //        }
                //        //GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
                //        GM.BossKill++;
                //        //보스 잡음
                //    }
                //    Destroy(Col);
                //    Destroy(Instantiate(GM.P_ParticleRed, col.transform.position, GM.QI), 1);
                //    GM.displaydata.Exp += GM.ExpNum;
                //    if (GM.displaydata.M_SkillInfo[1].Count <= 20)
                //    {
                //        if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
                //        {
                //            GM.displaydata.Exp += GM.ExpNum;
                //        }
                //    }
                //    GM.displaydata.UiUpdate();
                //    System.GC.Collect();
                //}
            }
            //GM.BlockColorChange();

            //if (GM.displaydata.L_SkillInfo[1].Count <= 20)
            //{
            //    if (rand <= 1 * GM.displaydata.L_SkillInfo[1].Count && Col.GetComponent<SpriteOutline>().color != GM.blockColor[8])
            //    {
            //        Col.GetComponent<SpriteOutline>().color = GM.blockColor[8]; //얼음
            //    }
            //}
            if (GM.displaydata.L_SkillInfo[1].Count <= 20)
            {
                int Brand = 0;
                if (GM.displaydata.L_SkillInfo[1].Count < 14 && GM.displaydata.L_SkillInfo[1].Count > 6)
                {
                    Brand = 5;
                }
                else if (GM.displaydata.L_SkillInfo[1].Count < 20 && GM.displaydata.L_SkillInfo[1].Count > 13)
                {
                    Brand = 10;

                }
                else if (GM.displaydata.L_SkillInfo[1].Count == 20)
                {
                    Brand = 20;
                }
                if (rand - Brand <= 1 * GM.displaydata.L_SkillInfo[1].Count)
                {
                    if(GM.displaydata.ItemList[4] == true)
                    {
                        if(Col.GetComponent<SpriteOutline>().color == GM.blockColor[8])
                        {
                            blockValue -= Dmg * 2;
                        }
                    }
                    Col.GetComponent<SpriteOutline>().color = GM.blockColor[8]; //얼음
                }
            }
            if (GM.displaydata.R_SkillInfo[1].Count <= 20)
            {
                if (GM.displaydata.R_SkillInfo[1].Count < 7 && GM.displaydata.R_SkillInfo[1].Count > 0)
                {
                    if (rand <= 2 * GM.displaydata.R_SkillInfo[1].Count)
                    {
                        Col.GetComponent<SpriteOutline>().poisonCount += Dmg;
                    }
                    if (Col.GetComponent<SpriteOutline>().poisonCount > 0)
                    {
                        Col.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = Col.GetComponent<SpriteOutline>().poisonCount.ToString();
                        //Debug.Log("독");
                    }
                }
                else if (GM.displaydata.R_SkillInfo[1].Count < 14 && GM.displaydata.R_SkillInfo[1].Count > 6)
                {
                    if (rand <= 2 * GM.displaydata.R_SkillInfo[1].Count)
                    {
                        Col.GetComponent<SpriteOutline>().poisonCount += Dmg + 1;
                    }
                    if (Col.GetComponent<SpriteOutline>().poisonCount > 0)
                    {
                        Col.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = Col.GetComponent<SpriteOutline>().poisonCount.ToString();
                        //Debug.Log("독 +1");
                    }
                }
                else if (GM.displaydata.R_SkillInfo[1].Count < 20 && GM.displaydata.R_SkillInfo[1].Count > 13)
                {
                    if (rand <= 2 * GM.displaydata.R_SkillInfo[1].Count)
                    {
                        Col.GetComponent<SpriteOutline>().poisonCount += Dmg + 3;
                    }
                    if (Col.GetComponent<SpriteOutline>().poisonCount > 0)
                    {
                        Col.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = Col.GetComponent<SpriteOutline>().poisonCount.ToString();
                        //Debug.Log("독 +3");
                    }
                }
                else if (GM.displaydata.R_SkillInfo[1].Count == 20)
                {
                    if (rand <= 2 * GM.displaydata.R_SkillInfo[1].Count)
                    {
                        Col.GetComponent<SpriteOutline>().poisonCount += Dmg + 6;
                    }
                    if (Col.GetComponent<SpriteOutline>().poisonCount > 0)
                    {
                        Col.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = Col.GetComponent<SpriteOutline>().poisonCount.ToString();
                        //Debug.Log("독 +6");
                    }
                }
            }
            for (int i = 0; i < GM.S_Block.Length; i++)
            {
                if (GM.displaydata.SoundGroup.activeSelf == true)
                {
                    if (GM.S_Block[i].isPlaying) continue;
                    else { GM.S_Block[i].Play(); break; }
                }
            }
            //else if (blockValue > 0 || blockValue <= 0 || blockValue > 0 && blockValue <= 0) 
            //{
            //    BlockText.text = blockValue.ToString();
            //    Col.GetComponent<Animator>().SetTrigger("shock");
            //    if (GM.displaydata.R_SkillInfo[0].Count <= 20)
            //    {
            //        int Brand = 0;
            //        if (GM.displaydata.R_SkillInfo[0].Count < 14 && GM.displaydata.R_SkillInfo[0].Count > 6)
            //        {
            //            Brand = 1;
            //            //Debug.Log("대지 1");
            //        }
            //        else if (GM.displaydata.R_SkillInfo[0].Count < 20 && GM.displaydata.R_SkillInfo[0].Count > 13)
            //        {
            //            Brand = 3;
            //            //Debug.Log("대지 3");
            //        }
            //        else if (GM.displaydata.R_SkillInfo[0].Count == 20)
            //        {
            //            Brand = 5;
            //            //Debug.Log("대지 5");
            //        }
            //        if (rand - Brand <= 1 * GM.displaydata.R_SkillInfo[0].Count)
            //        {
            //            GameObject[] tempobj = GameObject.FindGameObjectsWithTag("BlockText");

            //            foreach (GameObject ob in tempobj)
            //            {
            //                rand = Random.Range(1, 101);
            //                Text AllBlockText = ob.GetComponent<Text>();
            //                //Debug.Log(AllBlockText.text);
            //                int AllblockValue = int.Parse(AllBlockText.text) - Dmg;
            //                if (AllblockValue > 0)
            //                {
            //                    AllBlockText.text = AllblockValue.ToString();
            //                    ob.transform.parent.parent.GetComponent<Animator>().SetTrigger("shock");
            //                }
            //                //if (AllblockValue <= 0) //치명적
            //                if (AllblockValue <= 0)
            //                {
            //                    if (ob.transform.parent.parent.CompareTag("Block"))
            //                    {
            //                        GM.displaydata.Exp += GM.ExpNum;
            //                        if (GM.displaydata.M_SkillInfo[1].Count <= 20)
            //                        {
            //                            if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
            //                            {
            //                                GM.displaydata.Exp += GM.ExpNum;
            //                            }
            //                            if (GM.displaydata.M_SkillInfo[0].Count <= 20 && GM.displaydata.M_SkillInfo[0].Count > 0)
            //                            {
            //                                int Borand = 0;
            //                                if (GM.displaydata.M_SkillInfo[0].Count < 14 && GM.displaydata.M_SkillInfo[0].Count > 6)
            //                                {
            //                                    Borand = 3;
            //                                }
            //                                else if (GM.displaydata.M_SkillInfo[0].Count < 20 && GM.displaydata.M_SkillInfo[0].Count > 13)
            //                                {
            //                                    Borand = 6;
            //                                }
            //                                else if (GM.displaydata.M_SkillInfo[0].Count == 20)
            //                                {
            //                                    Borand = 10;
            //                                }
            //                                if (rand - Borand <= 2 * GM.displaydata.M_SkillInfo[0].Count) //2% 확률로 추가 공 생성
            //                                {
            //                                    Instantiate(GM.P_GreenOrb, ob.transform.parent.parent.transform.position, GM.QI).transform.SetParent(GM.BlockGroup);
            //                                }
            //                            }
            //                        }
            //                    }
            //                    if (ob.transform.parent.parent.CompareTag("Boss"))
            //                    {
            //                        GM.displaydata.Exp += GM.ExpNum * (GM.BossCnt + 3);
            //                        if (GM.displaydata.M_SkillInfo[1].Count <= 20)
            //                        {
            //                            if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
            //                            {
            //                                GM.displaydata.Exp += GM.ExpNum;
            //                            }
            //                            if (GM.displaydata.M_SkillInfo[0].Count <= 20 && GM.displaydata.M_SkillInfo[0].Count > 0)
            //                            {
            //                                int Borand = 0;
            //                                if (GM.displaydata.M_SkillInfo[0].Count < 14 && GM.displaydata.M_SkillInfo[0].Count > 6)
            //                                {
            //                                    Borand = 3;
            //                                }
            //                                else if (GM.displaydata.M_SkillInfo[0].Count < 20 && GM.displaydata.M_SkillInfo[0].Count > 13)
            //                                {
            //                                    Borand = 6;
            //                                }
            //                                else if (GM.displaydata.M_SkillInfo[0].Count == 20)
            //                                {
            //                                    Borand = 10;
            //                                }
            //                                if (rand - Borand <= 2 * GM.displaydata.M_SkillInfo[0].Count) //2% 확률로 추가 공 생성
            //                                {
            //                                    Instantiate(GM.P_GreenOrb, ob.transform.parent.parent.transform.position, GM.QI).transform.SetParent(GM.BlockGroup);
            //                                }
            //                            }
            //                        }
            //                        if (GM.BossCnt == 1)
            //                        {
            //                            GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
            //                            GM.NewYear.SetActive(false);
            //                        }
            //                        else if (GM.BossCnt == 2)
            //                        {
            //                            GM.WallGroup.GetComponent<Animator>().SetTrigger("Test");
            //                        }
            //                        else if (GM.BossCnt == 5)
            //                        {
            //                            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
            //                        }
            //                        else if (GM.BossCnt == 6)
            //                        {
            //                            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
            //                        }
            //                        else if (GM.BossCnt == 7)
            //                        {
            //                            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
            //                        }
            //                        else if (GM.BossCnt == 8)
            //                        {
            //                            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
            //                        }
            //                        GM.BossKill++;
            //                        //GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
            //                    }
            //                    Destroy(ob.transform.parent.parent.gameObject);
            //                    Destroy(Instantiate(GM.P_ParticleRed, ob.transform.position, GM.QI), 1);
            //                    blockValue = 1;
            //                    GM.displaydata.UiUpdate();
            //                    System.GC.Collect();
            //                }
            //            }
            //        }
            //    }
            //    //if (blockValue <= 0)
            //    //{

            //    //    if (GM.displaydata.M_SkillInfo[0].Count <= 20)
            //    //    {
            //    //        int Brand = 0;
            //    //        if (GM.displaydata.M_SkillInfo[0].Count < 14 && GM.displaydata.M_SkillInfo[0].Count > 6)
            //    //        {
            //    //            Brand = 3;
            //    //        }
            //    //        else if (GM.displaydata.M_SkillInfo[0].Count < 20 && GM.displaydata.M_SkillInfo[0].Count > 13)
            //    //        {
            //    //            Brand = 6;
            //    //        }
            //    //        else if (GM.displaydata.M_SkillInfo[0].Count == 20)
            //    //        {
            //    //            Brand = 10;
            //    //        }
            //    //        if (rand - Brand <= 2 * GM.displaydata.M_SkillInfo[0].Count) //2% 확률로 추가 공 생성
            //    //        {
            //    //            Instantiate(GM.P_GreenOrb, Col.transform.position, GM.QI).transform.SetParent(GM.BlockGroup);
            //    //        }
            //    //    }
            //    //    if (Col.CompareTag("Boss"))
            //    //    {
            //    //        GM.displaydata.Exp += GM.ExpNum * (GM.BossCnt + 3);
            //    //        if (GM.displaydata.M_SkillInfo[1].Count <= 20 && GM.displaydata.M_SkillInfo[1].Count <= 20)
            //    //        {
            //    //            if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
            //    //            {
            //    //                GM.displaydata.Exp += GM.ExpNum;
            //    //            }
            //    //        }
            //    //        if (GM.BossCnt == 1)
            //    //        {
            //    //            GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
            //    //            GM.NewYear.SetActive(false);
            //    //        }
            //    //        else if (GM.BossCnt == 2)
            //    //        {
            //    //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Test");
            //    //        }
            //    //        else if (GM.BossCnt == 5)
            //    //        {
            //    //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
            //    //        }
            //    //        else if (GM.BossCnt == 6)
            //    //        {
            //    //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
            //    //        }
            //    //        else if (GM.BossCnt == 7)
            //    //        {
            //    //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall3");
            //    //        }
            //    //        else if (GM.BossCnt == 8)
            //    //        {
            //    //            GM.WallGroup.GetComponent<Animator>().SetTrigger("Wall4");
            //    //        }
            //    //        //GM.WallGroup.GetComponent<Animator>().SetTrigger("NewWall");
            //    //        GM.BossKill++;
            //    //        //보스 잡음
            //    //    }
            //    //    Destroy(Col);
            //    //    Destroy(Instantiate(GM.P_ParticleRed, col.transform.position, GM.QI), 1);
            //    //    GM.displaydata.Exp += GM.ExpNum;
            //    //    if (GM.displaydata.M_SkillInfo[1].Count <= 20)
            //    //    {
            //    //        if (rand <= 5 * GM.displaydata.M_SkillInfo[1].Count) //5% 확률로 추가 경험치 획득
            //    //        {
            //    //            GM.displaydata.Exp += GM.ExpNum;
            //    //        }
            //    //    }
            //    //    GM.displaydata.UiUpdate();
            //    //    System.GC.Collect();
            //    //}
            //}        
        }
    }
    IEnumerator OnTriggerEnter2D_BALL(Collider2D col)
    {
        //초록 구 충돌시 아래로 떨어짐
        if (col.gameObject.CompareTag("GreenOrb"))
        {
            Destroy(col.gameObject);
            Destroy(Instantiate(GM.P_ParticleGreen, col.transform.position, GM.QI), 1);
            if (GM.displaydata.SoundGroup.activeSelf == true)
            {
                GM.S_GreenOrb.Play();
            }

            Transform TR = Instantiate(P_GreenBall, col.transform.position, GM.QI).transform;
            TR.SetParent(GameObject.Find("GreenBallGroup").transform);
            Vector3 targetPos = new Vector3(TR.position.x, GM.groundY, 0);
            while (true)
            {
                yield return null;
                TR.position = Vector3.MoveTowards(TR.position, targetPos, 2.5f);
                if (TR.position == targetPos) yield break;
            }
        }
    }
    #endregion
}
