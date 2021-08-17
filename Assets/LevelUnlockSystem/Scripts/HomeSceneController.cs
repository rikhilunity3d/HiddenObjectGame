    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    #if UNITY_IPHONE
    using UnityEngine.iOS;
    using UnityEngine.UI;
    #endif

    public class HomeSceneController : MonoBehaviour
    {
    

	    [SerializeField]
	    private string str_Android_RateUs_URL, str_iPhone_RateUs_URL;

	    [SerializeField]
	    private string str_Android_MoreApps_URL, str_iPhone_MoreApps_URL;

	    [SerializeField]
        private GameObject settingsPanel;

        public GameObject SettingsPanel { get { return settingsPanel; } }

        [SerializeField]
	    private Text toogleBackgroundMusicText;

	    [SerializeField]
	    private Text toogleSoundMusicText;


        public void ShareApp()
        {
		        new NativeShare()
		        .SetTitle("Share App with Family & Friends")
		        .SetSubject("Hey, I have found new Game to Play!!")
		        .SetText("Hey, I have found new Game to Play!! \n Download it from AppStore: \n" + str_iPhone_RateUs_URL)
                .Share();

        }



	    public void RateUS()
	    {
            if(SFXManager.instance.soundToogle == true)
		    {
			    SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Click);
		    }
		

            #if UNITY_ANDROID
			            Application.OpenURL(str_Android_RateUs_URL);
            #elif UNITY_IPHONE
		        Device.RequestStoreReview();
            #endif
	    }

	    public void MoreApps()
	    {
		    if (SFXManager.instance.soundToogle == true)
		    {
			    SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Click);
		    }

                #if UNITY_ANDROID
		                Application.OpenURL(str_Android_MoreApps_URL);
                #elif UNITY_IPHONE
		                Application.OpenURL(str_iPhone_MoreApps_URL);
                #endif

	    }

        public void Settings()
        {
		    if (SFXManager.instance.soundToogle == true)
		    {
			    SFXManager.instance.audioSource.PlayOneShot(SFXManager.instance.Click);
		    }

		    if (settingsPanel.activeSelf==false)
		    {
			    TweenManager.instance.OpenPanelAnimation(settingsPanel);
			

		    }
            else
            {
			
			    TweenManager.instance.ClosePanelAnimation(settingsPanel);
			
		    }
	    }

        public void MusicToggle()
        {
            if (BackgroundMusicManager.instance.audioSource.isPlaying)
            {
			    BackgroundMusicManager.instance.audioSource.Pause();
			    toogleBackgroundMusicText.text = "ON";

		    }
		    else
            {
			    BackgroundMusicManager.instance.audioSource.Play();
			    toogleBackgroundMusicText.text = "OFF";

		    }
	    }

        public void SoundToggle()
        {
		    if (SFXManager.instance.soundToogle == true)
		    {
			    SFXManager.instance.soundToogle = false;
			    toogleSoundMusicText.text = "ON";

		    }
		    else
		    {
			    SFXManager.instance.soundToogle = true;
			    toogleSoundMusicText.text = "OFF";

		    }
	    }

        private void Start()
        {

		    if (BackgroundMusicManager.instance.audioSource.isPlaying)
		    {
			    toogleBackgroundMusicText.text = "OFF";
		    }
		    else
		    {
			    toogleBackgroundMusicText.text = "ON";
		    }

		    if (SFXManager.instance.soundToogle == true)
		    {
			    toogleSoundMusicText.text = "OFF";
		    }
		    else
		    {
			    toogleSoundMusicText.text = "ON";
		    }

	    }
    }
