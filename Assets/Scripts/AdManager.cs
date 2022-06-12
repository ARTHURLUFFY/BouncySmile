using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    private void Start()
    {
        Advertisement.Initialize("3976647", true);
    }
    public void  Pressbutton()
    {
        ShowRewardingAd();
    }

    public void ShowAd()
    {
        Advertisement.Show();
    }

    public void ShowRewardingAd()
    {
        Advertisement.Show("rewardedVideo");
    }
}
