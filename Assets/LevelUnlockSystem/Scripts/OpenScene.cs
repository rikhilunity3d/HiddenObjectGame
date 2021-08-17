using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelUnlockSystem
{
    public class OpenScene : MonoBehaviour
    {
        public void OpenThisScene(int index)
        {
            if (SFXManager.instance.soundToogle == true)
            {
                SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Click);
            }

            //AdmobAds.instance.ShowInterstitialAd(index);

            SceneManager.LoadScene(index);           //load the level

        }
    }
}
