using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyQuest
{
    public class Keepf5CutSceneScreen : CutSceneScreen
    {
        const string achievement = "keepf5CutScenePlayed";

        public Keepf5CutSceneScreen()
            : base()
        {
        }

        public override void Initialize()
        {
            scenes.Add(new Keepf5CutSceneA(this));    //Nathan moves and speaks
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
            scenes.Add(new Keepf5CutSceneW(this));    //Nathan and Arlan move
            scenes.Add(new Keepf5CutSceneY(this));    //Arlan's beam
            scenes.Add(new Keepf5CutSceneZ(this));   //Arlan's beam
            scenes.Add(new PortalToMushroomForest(this));    //Portal to Mushroom Forest
            scenes.Add(new SlowFadeInWhiteScene(this));
            scenes.Add(new NathanFoundCutSceneA(this));
            scenes.Add(new FadeOutScene(this));
            scenes.Add(new PortalToCarasHouse(this));
            scenes.Add(new FadeInScene(this));
            scenes.Add(new MayorsHouseSceneA(this)); //Removes all fighting characters, resets Nathan, and saves game
            scenes.Add(new MayorsHouseSceneB(this));
            

            base.Initialize();
        }

        public override bool CanPlay()
        {
            if (Party.Singleton.PartyAchievements.Contains(achievement))
            {
                return false;
            }

            Party.Singleton.AddAchievement(achievement);
            return true;
        }
    }
}
