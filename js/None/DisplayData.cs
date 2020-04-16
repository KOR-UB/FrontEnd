using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DisplayData : MonoBehaviour
{
    [System.Serializable]
    public struct LeftSkillData
    {
        public int Count;
    }
    [SerializeField]
    [System.Serializable]
    public struct RightSkillData
    {
        public int Count;
    }
    [SerializeField]
    [System.Serializable]
    public struct MidSkillData
    {
        public int Count;
    }
    [SerializeField]
    public LeftSkillData[] L_SkillInfo;
    public MidSkillData[] M_SkillInfo;
    public RightSkillData[] R_SkillInfo;

    public OneCommand OC;
    public float Exp;
    public int Level = 1;
    public float MaxExp = 100.0f;
    public Image img;
    public Text exp;
    public Text level;
    public Data data;
    public Button TimeScaleBtn;
    public List<string> LSkillList = new List<string>() { "화염", "냉기" };
    public List<string> MSkillList = new List<string>() { "신비", "바람" };
    public List<string> RSkillList = new List<string>() { "대지", "맹독" };
    public List<string> BuffList = new List<string>() { "축복_동결", "저주_추가체력", "저주_회복", "축복_맹독" };
    public List<string> IList = new List<string>() { "저주_제거", "축복_강화", "중독_강화", "랜덤_기술","얼음_강화","추가공_강화" };
    public GameObject BuffObj;
    public int BuffCheck = 0;
    public bool TimeScaleCheck;
    //public List<string> RSkillList = new List<string>() {""};

    public GameObject L_SkillBtn;
    public GameObject M_SkillBtn;
    public GameObject R_SkillBtn;
    public Sprite[] LS_img;
    public Sprite[] MS_img;
    public Sprite[] RS_img;
    public GameObject SkillCountImg;
    public GameObject ItemCountImg;
    public GameObject Skill_left, Skill_right, Skill_mid;
    public Text leftText, rightText, buffText, midText, buffText_2;
    public GameObject Book;
    public GameObject SoundGroup;
    public GameObject SoundBtn;
    public GameObject OptionImg;
    public Sprite[] OptionSprite;
    public GameObject BackgrondEffect;
    public Text[] EffectOnOff;
    public AudioSource[] S_UI;
    public RectTransform ScrollRT;
    public bool[] ItemList;
    public int SP;
    public GameObject Withdraw;
    public GameObject WithdrawBtn;
    public LanguageManager LM;
    private void Awake()
    {
        //if(ItemList[5] == true)
        //{
        //    OC.RestartPlus();
        //}
        if (PlayerPrefs.GetInt("SOUNDCHECK") == 1)
        {
            SoundGroup.SetActive(false);
            SoundBtn.GetComponent<Image>().sprite = OptionSprite[1];
        }
        if (PlayerPrefs.GetInt("EFFECTCHECK") == 1)
        {
            BackgrondEffect.SetActive(false);
            EffectOnOff[0].color = Color.white;
            EffectOnOff[1].color = OC.blockColor[9];
        }

    }
    private void Start()
    {
        img.fillAmount = Exp % MaxExp / MaxExp;
        var Textexp = img.fillAmount * 100;
        exp.text = "Exp : " + Textexp.ToString("N2") + "%";
        level.text = "Level " + Level.ToString();
    }
    public void RemoveDebuff()
    {
        ItemList[0] = true; //저주 제거
        if (ItemList[0] == true)
        {
            ItemCountImg.transform.GetChild(0).gameObject.SetActive(true);
            BuffList.RemoveAt(2);
            BuffList.RemoveAt(1);
            BuffList.Add("");
            BuffList.Add("");
        }
    }
    public void AddBuff()
    {
        ItemList[1] = true; //축복 증가
        if (ItemList[1] == true)
        {
            ItemCountImg.transform.GetChild(1).gameObject.SetActive(true);
            BuffList.RemoveAt(5);
            BuffList.RemoveAt(4);
            BuffList.Add("축복_동결");
            BuffList.Add("축복_맹독");
        }
    }
    public void PoisonItem()
    {
        ItemList[2] = true; //광역 중독
        if (ItemList[2] == true)
        {
            ItemCountImg.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    public void RandItem()
    {
        ItemList[3] = true; //랜덤 스킬
        if (ItemList[3] == true)
        {
            ItemCountImg.transform.GetChild(3).gameObject.SetActive(true);
        }
    }
    public void IceItem()
    {
        ItemList[4] = true; //얼음 강화
        if (ItemList[4] == true)
        {
            ItemCountImg.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
    public void PlusBallItem()
    {
        ItemList[5] = true; //공 추가
        if (ItemList[5] == true)
        {
            ItemCountImg.transform.GetChild(5).gameObject.SetActive(true);
        }
    }
    public void randSkill()
    {
        if (ItemList[3] == true)
        {
            Exp += OC.ExpNum + (Level*2);
            UiUpdate();
            ItemExp();
            //int L1, L2, M1, M2, R1, R2;
            //int L1rand = Random.Range(0, (Level * 2));
            //int L2rand = Random.Range(0, (Level * 2));
            //int M1rand = Random.Range(0, (Level * 2));
            //int M2rand = Random.Range(0, (Level * 2));
            //int R1rand = Random.Range(0, (Level * 2));
            //int R2rand = Random.Range(0, (Level * 2));
            //L1rand += Level;
            //L2rand += Level;
            //M1rand += Level;
            //M2rand += Level;
            //R1rand += Level;
            //R2rand += Level;
            //L1 = L1rand < 40 ? Random.Range(0, 8) : L1rand < 60 ? Random.Range(2, 15) : L1rand < 70 ? Random.Range(3, 16) : L1rand < 80 ? Random.Range(3, 18) : L1rand < 90 ? Random.Range(4, 19) : L1rand < 95 ? Random.Range(5, 20) : L1rand < 100 ? Random.Range(6, 21) : L1rand < 110 ? Random.Range(7, 21) : Random.Range(9, 21);
            //L2 = L2rand < 40 ? Random.Range(0, 8) : L2rand < 60 ? Random.Range(2, 15) : L2rand < 70 ? Random.Range(3, 16) : L2rand < 80 ? Random.Range(3, 18) : L2rand < 90 ? Random.Range(4, 19) : L2rand < 95 ? Random.Range(5, 20) : L2rand < 100 ? Random.Range(6, 21) : L2rand < 110 ? Random.Range(7, 21) : Random.Range(9, 21);
            //M1 = M1rand < 40 ? Random.Range(0, 8) : M1rand < 60 ? Random.Range(2, 15) : M1rand < 70 ? Random.Range(3, 16) : M1rand < 80 ? Random.Range(3, 18) : M1rand < 90 ? Random.Range(4, 19) : M1rand < 95 ? Random.Range(5, 20) : M1rand < 100 ? Random.Range(6, 21) : M1rand < 110 ? Random.Range(7, 21) : Random.Range(9, 21);
            //M2 = M2rand < 40 ? Random.Range(0, 8) : M2rand < 60 ? Random.Range(2, 15) : M2rand < 70 ? Random.Range(3, 16) : M2rand < 80 ? Random.Range(3, 18) : M2rand < 90 ? Random.Range(4, 19) : M2rand < 95 ? Random.Range(5, 20) : M2rand < 100 ? Random.Range(6, 21) : M2rand < 110 ? Random.Range(7, 21) : Random.Range(9, 21);
            //R1 = R1rand < 40 ? Random.Range(0, 8) : R1rand < 60 ? Random.Range(2, 15) : R1rand < 70 ? Random.Range(3, 16) : R1rand < 80 ? Random.Range(3, 18) : R1rand < 90 ? Random.Range(4, 19) : R1rand < 95 ? Random.Range(5, 20) : R1rand < 100 ? Random.Range(6, 21) : R1rand < 110 ? Random.Range(7, 21) : Random.Range(9, 21);
            //R2 = R2rand < 40 ? Random.Range(0, 8) : R2rand < 60 ? Random.Range(2, 15) : R2rand < 70 ? Random.Range(3, 16) : R2rand < 80 ? Random.Range(3, 18) : R2rand < 90 ? Random.Range(4, 19) : R2rand < 95 ? Random.Range(5, 20) : R1rand < 100 ? Random.Range(6, 21) : R2rand < 110 ? Random.Range(7, 21) : Random.Range(9, 21);
            //L_SkillInfo[0].Count = L1;
            //SkillCountImg.transform.GetChild(0).GetComponentInChildren<Text>().text = "x" + L_SkillInfo[0].Count.ToString();
            //if(L1 == 0)
            //{
            //    SkillCountImg.transform.GetChild(0).gameObject.SetActive(false);
            //}
            //else
            //{
            //    SkillCountImg.transform.GetChild(0).gameObject.SetActive(true);
            //}
            //L_SkillInfo[1].Count = L2;
            //SkillCountImg.transform.GetChild(1).GetComponentInChildren<Text>().text = "x" + L_SkillInfo[1].Count.ToString();
            //if (L2 == 0)
            //{
            //    SkillCountImg.transform.GetChild(1).gameObject.SetActive(false);
            //}
            //else
            //{
            //    SkillCountImg.transform.GetChild(1).gameObject.SetActive(true);
            //}
            //M_SkillInfo[0].Count = M1;
            //SkillCountImg.transform.GetChild(2).GetComponentInChildren<Text>().text = "x" + M_SkillInfo[0].Count.ToString();
            //if (M1 == 0)
            //{
            //    SkillCountImg.transform.GetChild(2).gameObject.SetActive(false);
            //}
            //else
            //{
            //    SkillCountImg.transform.GetChild(2).gameObject.SetActive(true);
            //}
            //M_SkillInfo[1].Count = M2;
            //SkillCountImg.transform.GetChild(3).GetComponentInChildren<Text>().text = "x" + M_SkillInfo[1].Count.ToString();
            //if (M2 == 0)
            //{
            //    SkillCountImg.transform.GetChild(3).gameObject.SetActive(false);
            //}
            //else
            //{
            //    SkillCountImg.transform.GetChild(3).gameObject.SetActive(true);
            //}
            //R_SkillInfo[0].Count = R1;
            //SkillCountImg.transform.GetChild(4).GetComponentInChildren<Text>().text = "x" + R_SkillInfo[0].Count.ToString();
            //if (R1 == 0)
            //{
            //    SkillCountImg.transform.GetChild(4).gameObject.SetActive(false);
            //}
            //else
            //{
            //    SkillCountImg.transform.GetChild(4).gameObject.SetActive(true);
            //}
            //R_SkillInfo[1].Count = R2;
            //SkillCountImg.transform.GetChild(5).GetComponentInChildren<Text>().text = "x" + R_SkillInfo[1].Count.ToString();
            //if (R2 == 0)
            //{
            //    SkillCountImg.transform.GetChild(5).gameObject.SetActive(false);
            //}
            //else
            //{
            //    SkillCountImg.transform.GetChild(5).gameObject.SetActive(true);
            //}
            //ItemExp();
        }
    }
    private void Update()
    {
        if (Book.activeSelf == true)
        {
            if (LSkillList.Count == 0 && RSkillList.Count == 0 && MSkillList.Count == 0/* || ItemList[3] == true*/)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Book.SetActive(false);
                    Time.timeScale = 1;
                    //if (!TimeScaleCheck)
                    //{
                    //    Time.timeScale = 1;
                    //}
                    //else if (TimeScaleCheck)
                    //{
                    //    Time.timeScale = 1.5f;
                    //}
                }
            }
        }
        if (OC.displaydata.M_SkillInfo[1].Count < 14 && OC.displaydata.M_SkillInfo[1].Count > 6)
        {
            if (OC.ExpNum == 30)
            {
                OC.ExpNum = 33f;
            }
        }
        else if (OC.displaydata.M_SkillInfo[1].Count < 20 && OC.displaydata.M_SkillInfo[1].Count > 13)
        {
            if (OC.ExpNum == 33)
            {
                OC.ExpNum = 36;
            }
        }
        else if (OC.displaydata.M_SkillInfo[1].Count == 20)
        {
            if (OC.ExpNum == 36)
            {
                OC.ExpNum = 42;
            }
        }
    }
    public void ItemExp()
    {
        if (OC.displaydata.M_SkillInfo[1].Count < 14 && OC.displaydata.M_SkillInfo[1].Count > 6)
        {
            OC.ExpNum = 33;
        }
        else if (OC.displaydata.M_SkillInfo[1].Count < 20 && OC.displaydata.M_SkillInfo[1].Count > 13)
        {

            OC.ExpNum = 36;

        }
        else if (OC.displaydata.M_SkillInfo[1].Count == 20)
        {

            OC.ExpNum = 42;
        }
        else
        {
            OC.ExpNum = 30;
        }
    }
    public void WithdrawBall()
    {
        Withdraw.SetActive(true);
        WithdrawBtn.SetActive(false);
    }
    public void POP_Option()
    {
        if (OC.AdsPop.activeSelf == true) return;
        if (OC.AdsPop2.activeSelf == true) return;
        if (SoundGroup.activeSelf == true)
        {
            S_UI[0].Play();
        }
        if (OptionImg.activeSelf == false)
        {
            OC.Panel.SetActive(true);
            OptionImg.SetActive(true);
        }
    }
    public void Exit_Option()
    {
        if (SoundGroup.activeSelf == true)
        {
            S_UI[0].Play();
        }
        if (OptionImg.activeSelf == true)
        {
            OC.Panel.SetActive(false);
            OptionImg.SetActive(false);
        }
    }
    public void EffectOn()
    {
        if (SoundGroup.activeSelf == true)
        {
            S_UI[0].Play();
        }
        if (BackgrondEffect.activeSelf == false)
        {
            BackgrondEffect.SetActive(true);
            EffectOnOff[0].color = OC.blockColor[9];
            EffectOnOff[1].color = Color.white;
            data.EffectCheck = 0;
            PlayerPrefs.SetInt("EFFECTCHECK", 0);
        }
    }
    public void EffectOff()
    {
        if (SoundGroup.activeSelf == true)
        {
            S_UI[0].Play();
        }
        if (BackgrondEffect.activeSelf == true)
        {
            BackgrondEffect.SetActive(false);
            EffectOnOff[1].color = OC.blockColor[9];
            EffectOnOff[0].color = Color.white;
            data.EffectCheck = 1;
            PlayerPrefs.SetInt("EFFECTCHECK", 1);
        }
    }
    //public void BallEffectOn()
    //{
    //    if (SoundGroup.activeSelf == true)
    //    {
    //        S_UI[0].Play();
    //    }
    //    if (OC.data.BallEffectCheck == false)
    //    {
    //        //OC.BallGroup.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    //        EffectOnOff[2].color = OC.blockColor[9];
    //        EffectOnOff[3].color = Color.white;
    //        OC.data.BallEffectCheck = true;
    //    }
    //}
    //public void BallEffectOff()
    //{
    //    if (SoundGroup.activeSelf == true)
    //    {
    //        S_UI[0].Play();
    //    }
    //    if (OC.data.BallEffectCheck == true)
    //    {
    //        //OC.BallGroup.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    //        EffectOnOff[3].color = OC.blockColor[9];
    //        EffectOnOff[2].color = Color.white;
    //        OC.data.BallEffectCheck = false;
    //    }
    //}
    public void SoundMute()
    {
        if (SoundGroup.activeSelf == true)
        {
            SoundGroup.SetActive(false);
            SoundBtn.GetComponent<Image>().sprite = OptionSprite[1];
            data.SoundCheck = 1;
            PlayerPrefs.SetInt("SOUNDCHECK", 1);
        }
        else if (SoundGroup.activeSelf == false)
        {
            SoundGroup.SetActive(true);
            S_UI[0].Play();
            SoundBtn.GetComponent<Image>().sprite = OptionSprite[0];
            data.SoundCheck = 0;
            PlayerPrefs.SetInt("SOUNDCHECK", 0);
        }
    }
    public void POP_SkillInfoUI()
    {
        if (OC.AdsPop.activeSelf == true) return;
        if (OC.AdsPop2.activeSelf == true) return;
        if (SoundGroup.activeSelf == true)
        {
            S_UI[0].Play();
        }
        OC.Panel.SetActive(true);
        OC.SkillInfoUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void Exit_SkillInfoUI()
    {
        if (SoundGroup.activeSelf == true)
        {
            S_UI[0].Play();
        }
        ScrollRT.anchoredPosition = new Vector3(0, 0, 0);
        OC.Panel.SetActive(false);
        OC.SkillInfoUI.SetActive(false);
        Time.timeScale = 1;
        //if (!TimeScaleCheck)
        //{
        //    Time.timeScale = 1;
        //}
        //else if (TimeScaleCheck)
        //{
        //    Time.timeScale = 1.5f;
        //}
    }

    public void UiUpdate()
    {
        if (Exp >= MaxExp)
        {
            int rand = Random.Range(0, BuffList.Count);
            Time.timeScale = 0;
            ++Level;
            ++SP;
            Exp -= MaxExp;
            MaxExp = MaxExp * 1.06f;
            //if (ItemList[3] == true)
            //{
            //    if(LM.KoreanObj.activeSelf == true)
            //    {
            //        leftText.text = "제한".ToString();
            //        midText.text = "제한".ToString();
            //        rightText.text = "제한".ToString();
            //    }
            //    else
            //    {
            //        leftText.text = "Limit".ToString();
            //        midText.text = "Limit".ToString();
            //        rightText.text = "Limit".ToString();
            //    }
            //    L_SkillBtn.GetComponent<Image>().sprite = LS_img[3];
            //    L_SkillBtn.GetComponent<Button>().enabled = false;
            //    M_SkillBtn.GetComponent<Image>().sprite = LS_img[3];
            //    M_SkillBtn.GetComponent<Button>().enabled = false;
            //    R_SkillBtn.GetComponent<Image>().sprite = LS_img[3];
            //    R_SkillBtn.GetComponent<Button>().enabled = false;
            //}
            if (LSkillList.Count == 0)
            {
                leftText.text = "MAX".ToString();
                L_SkillBtn.GetComponent<Image>().sprite = LS_img[2];
                L_SkillBtn.GetComponent<Button>().enabled = false;
            }
            if (MSkillList.Count == 0)
            {
                midText.text = "MAX".ToString();
                M_SkillBtn.GetComponent<Image>().sprite = LS_img[2];
                M_SkillBtn.GetComponent<Button>().enabled = false;
            }
            if (RSkillList.Count == 0)
            {
                rightText.text = "MAX".ToString();
                R_SkillBtn.GetComponent<Image>().sprite = LS_img[2];
                R_SkillBtn.GetComponent<Button>().enabled = false;
            }
            if (LSkillList.Count > 0)
            {
                LeftBook();
                //if (!ItemList[3])
                //{
                //    LeftBook();
                //}
            }
            if (RSkillList.Count > 0)
            {
                RightBook();
                //if (!ItemList[3])
                //{
                //    RightBook();
                //}
            }
            if (MSkillList.Count > 0)
            {
                MidBook();
                //if (!ItemList[3])
                //{
                //    MidBook();
                //}
            }
            if (BuffList[rand].ToString() == "축복_동결")
            {
                BuffObj.SetActive(true);
                if (LM.KoreanObj.activeSelf == true)
                {
                    buffText.text = "축복\n일반 블록이 동결 상태로 등장합니다.";
                    buffText_2.text = "축복\n일반 블록이 동결 상태로 등장합니다.";
                }
                else
                {
                    buffText.text = "Blessing\nNormal block appears frozen.";
                    buffText_2.text = "Blessing\nNormal block appears frozen.";
                }
                BuffCheck = 1;
            }
            else if (BuffList[rand].ToString() == "저주_추가체력")
            {
                BuffObj.SetActive(true);
                if (LM.KoreanObj.activeSelf == true)
                {
                    buffText.text = "저주\n일반 블록의 체력이 증가합니다.";
                    buffText_2.text = "저주\n일반 블록의 체력이 증가합니다.";
                }
                else
                {
                    buffText.text = "Curse\nGeneric blocks gain strength.";
                    buffText_2.text = "Curse\nGeneric blocks gain strength.";
                }
                BuffCheck = 2;
            }
            else if (BuffList[rand].ToString() == "저주_회복")
            {
                BuffObj.SetActive(true);
                if (LM.KoreanObj.activeSelf == true)
                {
                    buffText.text = "저주\n일반 블록이 차례마다 회복합니다.";
                    buffText_2.text = "저주\n일반 블록이 차례마다 회복합니다.";
                }
                else
                {
                    buffText.text = "Curse\nNormal block recovers Every turn.";
                    buffText_2.text = "Curse\nNormal block recovers\nEvery turn.";
                }
                BuffCheck = 3;
            }
            else if (BuffList[rand].ToString() == "축복_맹독")
            {
                BuffObj.SetActive(true);
                if (LM.KoreanObj.activeSelf == true)
                {
                    buffText.text = "축복\n일반 블록이 차례마다 중독이 중첩됩니다.";
                    buffText_2.text = "축복\n일반 블록이 차례마다 중독이 중첩됩니다.";
                }
                else
                {
                    buffText.text = "Blessing\nNormal blocks overlap Poison in Every turn.";
                    buffText_2.text = "Blessing\nNormal blocks overlap\nPoison in Every turn.";
                }
                BuffCheck = 4;
            }
            else if (BuffList[rand].ToString() == "")
            {
                BuffObj.SetActive(false);
                buffText_2.text = "";
                BuffCheck = 0;
            }
            Book.SetActive(true);
            if (OC.AdsPop.activeSelf == true || OC.AdsPop2.activeSelf == true)
            {
                OC.AdsPop.SetActive(false);
                OC.AdsPop2.SetActive(false);
                OC.Panel.SetActive(false);
            }
            if (Level == 15 || Level == 30 || Level == 45 || Level == 60 || Level ==75 || Level == 90)
            {
                int Irand = Random.Range(0, IList.Count);
                if (IList[Irand].ToString() == "저주_제거") // "저주_제거", "축복_강화", "중독_강화", "랜덤_기술","얼음_강화","추가공_강화"
                {
                    RemoveDebuff();
                    IList.Remove("저주_제거");
                }
                else if (IList[Irand].ToString() == "축복_강화")
                {
                    AddBuff();
                    IList.Remove("축복_강화");
                }
                else if (IList[Irand].ToString() == "중독_강화")
                {
                    PoisonItem();
                    IList.Remove("중독_강화");
                }
                else if (IList[Irand].ToString() == "랜덤_기술")
                {
                    RandItem();
                    IList.Remove("랜덤_기술");
                }
                else if (IList[Irand].ToString() == "얼음_강화")
                {
                    IceItem();
                    IList.Remove("얼음_강화");
                }
                else if (IList[Irand].ToString() == "추가공_강화")
                {
                    PlusBallItem();
                    IList.Remove("추가공_강화");
                }
            }
        }
        img.fillAmount = Exp % MaxExp / MaxExp;
        var Textexp = img.fillAmount * 100;
        exp.text = "Exp : " + Textexp.ToString("N2") + "%";
        level.text = "Level " + Level.ToString();
    }
    //public void timeStop()
    //{
    //    optionBtn.enabled = false;
    //    Time.timeScale = 0;
    //    option.gameObject.SetActive(true);
    //}

    //public void ExitOption()
    //{
    //    optionBtn.enabled = true;
    //    option.gameObject.SetActive(false);
    //    Time.timeScale = 1;
    //}
    public void LeftBook()
    {
        int rand = Random.Range(0, LSkillList.Count);
        //print(LSkillList[rand]);
        if (LSkillList[rand].ToString() == "화염")
        {
            //leftText.text = LSkillList[rand].ToString();
            if(LM.KoreanObj.activeSelf == true) leftText.text = "블록과 충돌시 +4% 확률로\n2배의 피해";
            else leftText.text = "In the case of a collision, 4%\nchance to cause\ntwice as damage";
            L_SkillBtn.GetComponent<Image>().sprite = LS_img[0];
        }
        else if (LSkillList[rand].ToString() == "냉기")
        {
            //leftText.text = LSkillList[rand].ToString();
            if (LM.KoreanObj.activeSelf == true) leftText.text = "블록과 충돌시 +1% 확률로\n동결 상태이상 부여\n동결: 충돌시 받는피해\n2배 증가";
            else leftText.text = "In the case of a collision, 1%\n chance to freezing\nFreeze:100% increase in\ncollision damage received";
            L_SkillBtn.GetComponent<Image>().sprite = LS_img[1];
        }
        //else if (LSkillList[rand].ToString() == "신비")
        //{
        //    //leftText.text = LSkillList[rand].ToString();
        //    leftText.text = "블록파괴시 +3%\n추가 공 생성";
        //    L_SkillBtn.GetComponent<Image>().sprite = LS_img[2];
        //}
        //else if (LSkillList[rand].ToString() == "맹독")
        //{
        //    //leftText.text = LSkillList[rand].ToString();
        //    leftText.text = "블록과 충돌시 +2%\n매 차례마다 중독피해\n(중첩가능)";
        //    L_SkillBtn.GetComponent<Image>().sprite = LS_img[3];
        //}
    }
    public void MidBook()
    {
        int rand3 = Random.Range(0, MSkillList.Count);
        //print(LSkillList[rand]);
        if (MSkillList[rand3].ToString() == "신비")
        {
            //leftText.text = LSkillList[rand].ToString();
            if (LM.KoreanObj.activeSelf == true) midText.text = "블록파괴시 +2% 확률로\n추가 공 생성";
            else midText.text = "2% chance to create\nadditional balls\nat block breakage";
            M_SkillBtn.GetComponent<Image>().sprite = MS_img[0];
        }
        else if (MSkillList[rand3].ToString() == "바람")
        {
            //leftText.text = LSkillList[rand].ToString();
            if (LM.KoreanObj.activeSelf == true) midText.text = "블록파괴시 +5% 확률로\n추가 경험치 획득";
            else midText.text = "2% chance to gain additional\nexperience in the event\nof a block breakage";
            M_SkillBtn.GetComponent<Image>().sprite = MS_img[1];
        }

    }
    public void RightBook()
    {

        int rand2 = Random.Range(0, RSkillList.Count);
        //print(RSkillList[rand2]);
        if (RSkillList[rand2].ToString() == "대지")
        {
            //rightText.text = RSkillList[rand2].ToString();
            if (LM.KoreanObj.activeSelf == true) rightText.text = "블록과 충돌시 +1% 확률로\n모든 블록에게 피해";
            else rightText.text = "In the case of a collision, 1 %\nchance to Damage\nto all the blocks";
            R_SkillBtn.GetComponent<Image>().sprite = RS_img[0];
        }
        else if (RSkillList[rand2].ToString() == "맹독")
        {
            //rightText.text = RSkillList[rand2].ToString();
            //rightText.text = "블록과 충돌시 +2% 확률로 중독 중첩\n매 차례마다 중독피해\n(중첩가능)";
            if (LM.KoreanObj.activeSelf == true) rightText.text = "블록과 충돌시 +2% 확률로\n중독 상태이상 부여 (중첩가능)\n중독: 중독 중첩 수 만큼\n매 차례마다 피해";
            else rightText.text = "In the case of a collision, 1%\nchance to poisoning\n(poisoning overlap)\npoison: damage as much as\nthe value of the poison at every turn";
            R_SkillBtn.GetComponent<Image>().sprite = RS_img[1];
        }
        //else if(Level == 15)
        //{
        //    int rand = Random.Range(0, 7);
        //}
        //else if (RSkillList[rand2].ToString() == "바람")
        //{
        //    //rightText.text = RSkillList[rand2].ToString();
        //    rightText.text = "블록파괴시 +5%\n추가 경험치 획득";
        //    R_SkillBtn.GetComponent<Image>().sprite = RS_img[1];
        //}
        //else if (RSkillList[rand2].ToString() == "강화")
        //{
        //    rightText.text = RSkillList[rand2].ToString();
        //    R_SkillBtn.GetComponent<Image>().sprite = RS_img[3];
        //}
    }
    public void TimeScaleIndex()
    {
        if (SoundGroup.activeSelf == true)
        {
            S_UI[0].Play();
        }
        if (Book.activeSelf == true) return;
        if (!TimeScaleCheck)
        {
            TimeScaleBtn.GetComponent<Animator>().SetBool("Speed", true);
            //Time.timeScale = 1.5f;
            TimeScaleCheck = true;
        }
        else if (TimeScaleCheck)
        {
            TimeScaleBtn.GetComponent<Animator>().SetBool("Speed", false);
            //Time.timeScale = 1;
            TimeScaleCheck = false;
        }
    }
    public void LeftSelect()
    {
        if (L_SkillBtn.GetComponent<Image>().sprite == LS_img[0])
        {
            L_SkillInfo[0].Count++;
            if (SkillCountImg.transform.GetChild(0).gameObject.activeSelf == false)
            {
                SkillCountImg.transform.GetChild(0).gameObject.SetActive(true);
            }
            SkillCountImg.transform.GetChild(0).GetComponentInChildren<Text>().text = "x" + L_SkillInfo[0].Count.ToString();
            SkillCountImg.transform.GetChild(0).gameObject.SetActive(true);
            if (L_SkillInfo[0].Count == 20)
            {
                LSkillList.Remove("화염");
            }
            //Debug.Log(L_SkillInfo[0].Count);
        }
        else if (L_SkillBtn.GetComponent<Image>().sprite == LS_img[1])
        {
            L_SkillInfo[1].Count++;
            if (SkillCountImg.transform.GetChild(1).gameObject.activeSelf == false)
            {
                SkillCountImg.transform.GetChild(1).gameObject.SetActive(true);
            }
            SkillCountImg.transform.GetChild(1).GetComponentInChildren<Text>().text = "x" + L_SkillInfo[1].Count.ToString();
            if (L_SkillInfo[1].Count == 20)
            {
                LSkillList.Remove("냉기");
            }
            //Debug.Log(L_SkillInfo[1].Count);
        }
        //else if (L_SkillBtn.GetComponent<Image>().sprite == LS_img[2])
        //{
        //    L_SkillInfo[2].Count++;
        //    if (L_SkillInfo[2].Count == 20)
        //    {
        //        LSkillList.Remove("신비");
        //    }
        //    //Debug.Log(L_SkillInfo[2].Count);
        //}
        //else if (L_SkillBtn.GetComponent<Image>().sprite == LS_img[3])
        //{
        //    L_SkillInfo[3].Count++;
        //    //Debug.Log(L_SkillInfo[3].Count);
        //    if (L_SkillInfo[3].Count == 20)
        //    {
        //        LSkillList.Remove("맹독");
        //    }
        //}
        if (SoundGroup.activeSelf == true)
        {
            S_UI[1].Play();
        }
        SP--;
        Book.SetActive(false);
        Time.timeScale = 1;
        //if (!TimeScaleCheck)
        //{
        //    Time.timeScale = 1;
        //}
        //else if (TimeScaleCheck)
        //{
        //    Time.timeScale = 1.5f;
        //}
        if (SP > 0)
        {
            Time.timeScale = 0;
            if (LSkillList.Count > 0)
            {
                LeftBook();
                //if (!ItemList[3])
                //{
                //    LeftBook();
                //}
            }
            if (RSkillList.Count > 0)
            {
                RightBook();
                //if (!ItemList[3])
                //{
                //    RightBook();
                //}
            }
            if (MSkillList.Count > 0)
            {
                MidBook();
                //if (!ItemList[3])
                //{
                //    MidBook();
                //}
            }
            Book.SetActive(true);
        }
    }
    public void MidSelect()
    {

        if (M_SkillBtn.GetComponent<Image>().sprite == MS_img[0])
        {
            M_SkillInfo[0].Count++;
            if (SkillCountImg.transform.GetChild(2).gameObject.activeSelf == false)
            {
                SkillCountImg.transform.GetChild(2).gameObject.SetActive(true);
            }
            SkillCountImg.transform.GetChild(2).GetComponentInChildren<Text>().text = "x" + M_SkillInfo[0].Count.ToString();
            if (M_SkillInfo[0].Count == 20)
            {
                MSkillList.Remove("신비");
            }
            //Debug.Log(L_SkillInfo[2].Count);
        }
        else if (M_SkillBtn.GetComponent<Image>().sprite == MS_img[1])
        {
            M_SkillInfo[1].Count++;
            if (SkillCountImg.transform.GetChild(3).gameObject.activeSelf == false)
            {
                SkillCountImg.transform.GetChild(3).gameObject.SetActive(true);
            }
            SkillCountImg.transform.GetChild(3).GetComponentInChildren<Text>().text = "x" + M_SkillInfo[1].Count.ToString();
            if (M_SkillInfo[1].Count == 20)
            {
                //경험치 획득
                MSkillList.Remove("바람");
            }
            //Debug.Log(R_SkillInfo[1].Count);
        }
        if (SoundGroup.activeSelf == true)
        {
            S_UI[1].Play();
        }
        SP--;
        Book.SetActive(false);
        Time.timeScale = 1;
        //if (!TimeScaleCheck)
        //{
        //    Time.timeScale = 1;
        //}
        //else if (TimeScaleCheck)
        //{
        //    Time.timeScale = 1.5f;
        //}
        if (SP > 0)
        {
            Time.timeScale = 0;
            if (LSkillList.Count > 0)
            {
                LeftBook();
                //if (!ItemList[3])
                //{
                //    LeftBook();
                //}
            }
            if (RSkillList.Count > 0)
            {
                RightBook();
                //if (!ItemList[3])
                //{
                //    RightBook();
                //}
            }
            if (MSkillList.Count > 0)
            {
                MidBook();
                //if (!ItemList[3])
                //{
                //    MidBook();
                //}
            }
            Book.SetActive(true);
        }
    }

    public void RightSelect()
    {
        if (R_SkillBtn.GetComponent<Image>().sprite == RS_img[0])
        {
            R_SkillInfo[0].Count++;
            if (SkillCountImg.transform.GetChild(4).gameObject.activeSelf == false)
            {
                SkillCountImg.transform.GetChild(4).gameObject.SetActive(true);
            }
            SkillCountImg.transform.GetChild(4).GetComponentInChildren<Text>().text = "x" + R_SkillInfo[0].Count.ToString();
            if (R_SkillInfo[0].Count == 20)
            {
                //모든 블럭에게 1데미지 
                RSkillList.Remove("대지");
            }
            //Debug.Log(R_SkillInfo[0].Count);
        }
        //else if (R_SkillBtn.GetComponent<Image>().sprite == RS_img[1])
        //{
        //    R_SkillInfo[1].Count++;
        //    if (R_SkillInfo[1].Count == 20)
        //    {
        //        //경험치 획득
        //        RSkillList.Remove("바람");
        //    }
        //    //Debug.Log(R_SkillInfo[1].Count);
        //}
        else if (R_SkillBtn.GetComponent<Image>().sprite == RS_img[1])
        {
            R_SkillInfo[1].Count++;
            if (SkillCountImg.transform.GetChild(5).gameObject.activeSelf == false)
            {
                SkillCountImg.transform.GetChild(5).gameObject.SetActive(true);
            }
            SkillCountImg.transform.GetChild(5).GetComponentInChildren<Text>().text = "x" + R_SkillInfo[1].Count.ToString();
            if (R_SkillInfo[1].Count == 20)
            {
                //경험치 획득
                RSkillList.Remove("맹독");
            }
            //Debug.Log(R_SkillInfo[2].Count);
        }
        //else if (R_SkillBtn.GetComponent<Image>().sprite == RS_img[3])
        //{
        //    R_SkillInfo[3].Count++;
        //    //Debug.Log(R_SkillInfo[3].Count);
        //}
        if (SoundGroup.activeSelf == true)
        {
            S_UI[1].Play();
        }
        SP--;
        Book.SetActive(false);
        Time.timeScale = 1;
        //if (!TimeScaleCheck)
        //{
        //    Time.timeScale = 1;
        //}
        //else if (TimeScaleCheck)
        //{
        //    Time.timeScale = 1.5f;
        //}
        if (SP > 0)
        {
            Time.timeScale = 0;
            if (LSkillList.Count > 0)
            {
                LeftBook();
                //if (!ItemList[3])
                //{
                //    LeftBook();
                //}
            }
            if (RSkillList.Count > 0)
            {
                RightBook();
                //if (!ItemList[3])
                //{
                //    RightBook();
                //}
            }
            if (MSkillList.Count > 0)
            {
                MidBook();
                //if (!ItemList[3])
                //{
                //    MidBook();
                //}
            }
            Book.SetActive(true);
        }
    }
}