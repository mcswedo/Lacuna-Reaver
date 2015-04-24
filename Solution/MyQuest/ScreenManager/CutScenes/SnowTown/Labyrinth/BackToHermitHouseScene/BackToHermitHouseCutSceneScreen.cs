using System;
using Microsoft.Xna.Framework;

namespace MyQuest
{
    public class BackToHermitHouseCutSceneScreen : CutSceneScreen
    {
        const string achievement = "playedBackToHermitHouse";

        protected override void Complete()
        {
            Party.Singleton.AddAchievement(achievement);
        }
        public BackToHermitHouseCutSceneScreen()
            : base()
        {
            scenes.Add(new BackToHermitHouseSceneA(this));

            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToHermitHouse(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new BackToHermitHouseSceneB(this));
            
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToThroneRoomRememberdScene(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new RoyalFamilyRememberedScene(this));
           
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToArlansStudyScene(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new StudyRememberedScene(this));
          
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToKeepf5Scene(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new Keepf5CutSceneB(this));    //Sid and Max come out and speak
            scenes.Add(new Keepf5CutSceneC(this));    //Dialog
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneD(this));    //First friend dies
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, 1.5));
            scenes.Add(new Keepf5CutSceneE(this));    //Nathan turns and speaks
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new Keepf5CutSceneF(this));    //Second friend is disintegrated
            scenes.Add(new Keepf5CutSceneG(this));    //Nathan speaks
            scenes.Add(new Keepf5CutSceneH(this));    //Arlan speaks
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneI(this));    //Arlan appears
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, .5));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneJ(this));    //Arlan disappears
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new Keepf5CutSceneK(this));    //Nathan speaks
            scenes.Add(new Keepf5CutSceneL(this));    //Arlan speaks
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneM(this));    //Arlan appears
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, .2));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneN(this));    //Arlan disappears
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new Keepf5CutSceneO(this));    //Nathan asks Arlan to reveal himself
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, .3));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, .15));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, .1));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, .05));
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new FadeInWhiteScene(this));
            scenes.Add(new DelayScene(this, .01));
            scenes.Add(new Keepf5CutSceneP(this));    //Arlan appears and speaks, Nathan runs
            scenes.Add(new Keepf5CutSceneQ(this));    //Arlan Disappears
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneR(this));    //Arlan appears
            scenes.Add(new DelayScene(this, .3));
            scenes.Add(new Keepf5CutSceneS(this));    //Nathan runs and arlan disappears
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneT(this));    //Arlan appears
            scenes.Add(new DelayScene(this, .3));
            scenes.Add(new Keepf5CutSceneU(this));    //Nathan runs and arlan disappears
            scenes.Add(new FadeOutWhiteScene(this));
            scenes.Add(new Keepf5CutSceneV(this));    //Arlan appears
            scenes.Add(new Keepf5RememberedSceneW(this));    //Nathan and Arlan move

            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToHermitHouseAgain(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new BackToHermitHouseSceneC(this));
            scenes.Add(new BackToHermitHouseSceneD(this));
        }

        public override bool CanPlay()
        {
            if (!Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
