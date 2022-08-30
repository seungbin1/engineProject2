using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�̱���
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    //����
    private int first = 0;
    public class Data
    {
        public double mainSound;
        public double gameSound;
        public double effectSound;
        public int bestscore;
        public Data(int score, double mainSound, double gameSound,double effectSound)
        {
            this.bestscore = score;
            this.mainSound = mainSound;
            this.gameSound = gameSound;
            this.effectSound = effectSound;
        }
        
        public void SaveScore(int score)
        {
            this.bestscore = score;
        }

        public void SaveSound(double mainSound, double gameSound, double effectSound)
        {
            this.mainSound = mainSound;
            this.gameSound = gameSound;
            this.effectSound = effectSound;
        }
    }

    public Data data;

    //����
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int bestScore;

    //�Ҹ�
    [HideInInspector]
    public float mainSound;
    [HideInInspector]
    public float gameSound;    
    [HideInInspector]
    public float effectSound;

    //����
    public enum State
    {
        Main,
        PLAYING,
        PLAY,
        SELECT,
        READY,
        HIT,
        BROKEN,
        STOP
    }
    [HideInInspector]
    public State state;


    Player[] players;
    //��ũ�� ��ġ ��ġ
    [HideInInspector]
    public Vector3 mousePostion;

    //��ֹ� ����
    [HideInInspector]
    public float initInterval = 6;
    private float interval=6;
    public float Interval
    {
        get
        {
            return interval;
        }
        set
        {
            if (value < 4.5f)
            {
                interval = 4.5f;
            }
            else
            {
                interval = value;
            }
        }
    }
    [HideInInspector]
    public float intervalSpeed;
    [HideInInspector]
    public float initMouseLimit = 0;
    [HideInInspector]
    public float mouseLimit=0;
    private float mouseLimitSpeed=0.05f;

    //Į ����
    public GameObject[] prefabs;
    [HideInInspector]
    public GameObject sword;

    //Į ȸ��
    private bool swordLeft;
    public bool SwordLeft
    {
        get { return swordLeft; }
        set
        {
            swordLeft = value;
            swordRight = !value;
        }
    }
    private bool swordRight;
    public bool SwordRight
    {
        get { return swordRight; }
        set
        {
            swordRight = value;
            swordLeft = !value;
        }
    }

    //����
    [HideInInspector]
    public int life = 100;
    [HideInInspector]
    public GameObject lifeObj;
    [HideInInspector]
    public int prefabNum;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //���� �Ҹ�����, �ְ� ����, �ҷ�����

        first = PlayerPrefs.GetInt("First");
        if (first==0)
        {
            data = new Data(0, 0.5f, 0.5f, 0.5f);
            Save(data);
            first++;
            PlayerPrefs.SetInt("First", first);
        }

        JsonData json = Load();
        bestScore = int.Parse(json["bestscore"].ToString());
        mainSound = float.Parse(json["mainSound"].ToString());
        gameSound = float.Parse(json["gameSound"].ToString());
        effectSound = float.Parse(json["effectSound"].ToString());
        data = new Data(bestScore, mainSound, gameSound, effectSound);

        //���� ����(���� �޴�)
        state = State.Main;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Update()
    {
        if (state == State.PLAYING || state == State.HIT)
        {
            if (Input.touchCount > 0)
            {
                mousePostion = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                mousePostion = new Vector3(Mathf.Clamp(mousePostion.x, mouseLimit, 8.5f), Mathf.Clamp(mousePostion.y, -5, 5), 0);
            }

            if (mouseLimit < 5)
            {
                mouseLimit = mouseLimit + mouseLimitSpeed * Time.deltaTime;
            }
            Interval = Interval - intervalSpeed * Time.deltaTime;
        }

        else if(state == State.BROKEN) { }

        else
        {
            Init();
        }
    }

    public void Init()
    {
        mouseLimit = initMouseLimit;
        life = 3;
        score = 0;
    }
    //���° Į�� ���õǾ����� ���� �޾ƿ´�.
    public void OnSelect(int num)
    {
        sword=prefabs[num];
        prefabNum = num;
    }

    public void LifeDecrease()
    {
        //����
        life--;
        //����(�ְ� ����)
        BestScore(score);

        //����
        if (life <= 0)
        {
            state = State.BROKEN;
        }

        else
        {
            state = State.HIT;
        }

        //���� Ȯ���� ���� ������Ʈ ���̱�
        for (int i = 0; i < lifeObj.transform.childCount; i++)
        {
            if (lifeObj.transform.GetChild(i).gameObject.activeSelf)
            {
                lifeObj.transform.GetChild(i).gameObject.SetActive(false);
                return;
            }
        }
    }
    //����
    public void Save(Data data)
    {
        JsonData jsondata = JsonMapper.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + @"\data.json", jsondata.ToString());
    }

    JsonData Load()
    {
        string jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + @"\data.json");
        JsonData jsondata = JsonMapper.ToObject(jsonString);
        return jsondata;
    }

    //�ְ� ����
    public void BestScore(int score)
    {
        JsonData json = Load();
        if (score> bestScore)
        {
            bestScore = score;
            data.SaveScore(bestScore);
            Save(data);
        }
    }
}