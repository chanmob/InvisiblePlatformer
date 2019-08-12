using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using CodeStage.AntiCheat.ObscuredTypes;

public class AdmobManager : Singleton<AdmobManager>
{
    public static AdmobManager _instance;

    private readonly string bannerID = "ca-app-pub-9954381112163314/5187211523";
    //ca-app-pub-9954381112163314/9471144920
    private readonly string screenID = "ca-app-pub-9954381112163314/7430231481";
    //ca-app-pub-9954381112163314/8926728759
    private readonly string rewardID = "ca-app-pub-9954381112163314/5925578121";
    //ca-app-pub-9954381112163314/1293272005

    private readonly string testBanner = "ca-app-pub-3940256099942544/6300978111";
    private readonly string testScreen = "ca-app-pub-3940256099942544/1033173712";
    private readonly string testRewarded = "ca-app-pub-3940256099942544/5224354917";

    private BannerView banner;
    private InterstitialAd screenAD;
    private RewardBasedVideoAd rewardedAD;

    public ObscuredBool rewarded = false;

    public GameObject noAd;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (banner == null)
            InitBannerAD();
        if (screenAD == null)
            InitScreenAD();
        if (rewardedAD == null)
            InitRewardedAD();

        banner.Show();
        Debug.Log("배너 광고 On");
    }

    public void ShowScreenAD()
    {
        //if(!screenAD.IsLoaded() || screenAD == null)
        //    InitScreenAD();
        ShowScreenADCoroutine();

        Debug.Log("스크린 광고 On");
    }

    private void ShowScreenADCoroutine()
    {
        if(!screenAD.IsLoaded())
        {
            return;
        }

        screenAD.Show();
    }

    private void InitBannerAD()
    {
        banner = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Top);

        AdRequest request = new AdRequest.Builder().Build();

        banner.LoadAd(request);
    }

    private void InitScreenAD()
    {
        screenAD = new InterstitialAd(screenID);

        AdRequest request = new AdRequest.Builder().Build();

        screenAD.LoadAd(request);
        screenAD.OnAdClosed += ScreenADClose;
    }

    public void ShowRewardAD()
    {
        //if(!rewardedAD.IsLoaded() || rewardedAD == null)
        //    InitRewardedAD();
        RewardCoroutine();

        Debug.Log("보상형 광고 On");
    }

    private void InitRewardedAD()
    {
        rewardedAD = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAD.LoadAd(request, rewardID);
        rewardedAD.OnAdLoaded += RewardedADLoad;
        rewardedAD.OnAdClosed += RewardedADClose;
        rewardedAD.OnAdRewarded += RewardToUesr;

        Debug.Log("리워드 광고 생성");
    }

    private void RewardCoroutine()
    {
        if (!rewardedAD.IsLoaded())
        {
            noAd.SetActive(true);
            InitRewardedAD();
            Debug.Log("보상형 광고 로드 안됨");
            return;
        }
        else
        {
            Debug.Log("보상형 광고 로드 완료");
        }

        rewardedAD.Show();
    }

    private void ScreenADClose(object sender, EventArgs arg)
    {
        Debug.Log("스크린 광고 닫힘");
        InitScreenAD();
    }

    private void RewardedADLoad(object sender, EventArgs arg)
    {
        Debug.Log("보상형 광고 로드");
    }

    private void RewardedADClose(object sender, EventArgs arg)
    {
        Debug.Log("보상형 광고 닫힘");
        InitRewardedAD();
    }

    private void RewardToUesr(object sender, EventArgs arg)
    {
        rewarded = true;

        string[] sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Split(' ');
        int level = int.Parse(sceneName[1]);

        if (SaveAndLoad.instance.LoadIntData("CurLevel") < level)
        {
            SaveAndLoad.instance.SaveIntData("CurLevel", level);
        }

        rewardedAD.OnAdLoaded -= RewardedADLoad;
        rewardedAD.OnAdClosed -= RewardedADClose;
        rewardedAD.OnAdRewarded -= RewardToUesr;
    }
}
