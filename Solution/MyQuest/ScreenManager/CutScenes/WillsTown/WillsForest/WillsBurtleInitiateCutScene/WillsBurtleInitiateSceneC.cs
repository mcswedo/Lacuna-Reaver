using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyQuest
{
    public class WillsBurtleInitiateSceneC : Scene
    {

        #region Achievments

        public const string achievement = "defeatBurtle";
        const string brokenScytheAchievement = "brokenScythe";
        const string playedAchievement = "WillsBurtleInitiate";

        #endregion

        public override void Complete()
        {
            Party.Singleton.AddAchievement(playedAchievement);

            Party.Singleton.AddAchievement(achievement);

            Party.Singleton.AddAchievement(brokenScytheAchievement);
            
            Party.Singleton.AddFightingCharacter(Party.will);

            int willsLevel = Party.Singleton.GetFightingCharacter(Party.cara).FighterStats.Level;
            Will.Instance.SetLevel(willsLevel);

            Equipment armor = EquipmentPool.RequestEquipment("LeatherArmor");
            Equipment scythe = EquipmentPool.RequestEquipment("PlainScythe");

            Party.Singleton.GetFightingCharacter(Party.will).EquipArmor(armor);
            Party.Singleton.GetFightingCharacter(Party.will).EquipWeapon(scythe);

            Party.Singleton.ModifyNPC(
                        Maps.blindMansForestBoss,
                        NPCPool.burtle,
                        Party.Singleton.Leader.TilePosition,
                        ModAction.Remove,
                        true);

            Party.Singleton.ModifyNPC(
                     Maps.blindMansForestBoss,
                     Party.cara,
                     Party.Singleton.Leader.TilePosition,
                     ModAction.Remove,
                     true);

            Party.Singleton.ModifyNPC(
                     Maps.blindMansForestBoss,
                     Party.will,
                     Party.Singleton.Leader.TilePosition,
                     ModAction.Remove,
                     true); 
        }

        public WillsBurtleInitiateSceneC(Screen screen)
            : base(screen)
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Initialize()
        {
            Complete(); 

            ScreenManager.Singleton.AddScreen(new CombatScreen(CombatZonePool.forestBurtleZone));

            state = SceneState.Complete;

        }

        public override void Update(GameTime gameTime)
        {          

        }

        public override void HandleInput(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
