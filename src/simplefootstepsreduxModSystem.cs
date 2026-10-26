
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace SimpleFootStepsRedux
{
    public class SimpleFootStepsReduxModSystem : ModSystem
    {
        public static List<SoundEntry> soundEntries;

        public static List<SoundEntry> SoundEntries
        {
            get {
                
                return soundEntries;
            }
        }

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            
            AiTaskRegistry.Register<AiTaskLoudWander>("wander");
            AiTaskRegistry.Register<AiTaskLoudFleeEntity>("fleeentity");
            AiTaskRegistry.Register<AiTaskLoudSeekEntity>("seekentity");
            AiTaskRegistry.Register<AiTaskLoudGetOutOfWater>("getoutofwater");
            AiTaskRegistry.Register<AiTaskLoudStayCloseToEntity>("stayclosetoentity");
        }

        public override void AssetsFinalize(ICoreAPI api)
        {
            try
            {
                soundEntries = new List<SoundEntry>();
                List<IAsset> many = api.Assets.GetMany("config/soundentries.json");
                foreach (IAsset asset in many)
                {
                    try
                    {
                        List<SoundEntry> se = asset.ToObject<List<SoundEntry>>();
                        if (se != null && se.Count > 0) 
                        { 
                            soundEntries.AddRange(se); 
                        }
                    }
                    catch (System.Exception ex)
                    {
                        api.Logger.Warning("SimpleFootstepsRedux: Failed to load sound entries from {0}: {1}", asset.Location, ex.Message);
                    }
                }
            }
            catch (System.Exception ex)
            {
                api.Logger.Error("SimpleFootstepsRedux: Critical error during AssetsFinalize: {0}", ex.Message);
                soundEntries = new List<SoundEntry>(); // Ensure we have a valid list even on error
            }
            base.AssetsFinalize(api);
        }

        public static SoundEntry GetSoundEntry(Entity forentity, string soundTrigger)
        {
            try
            {
                if (forentity == null) { return null; }
                if (SoundEntries == null) { return null; }
                if (string.IsNullOrEmpty(soundTrigger)) { return null; }
                
                string entityCode = forentity.Code?.ToString();
                if (string.IsNullOrEmpty(entityCode)) { return null; }
                
                SoundEntry find = SoundEntries.FirstOrDefault(x => 
                    !string.IsNullOrEmpty(x.mobMatchCode) && 
                    !string.IsNullOrEmpty(x.soundTrigger) &&
                    entityCode.Contains(x.mobMatchCode) && 
                    x.soundTrigger == soundTrigger, null);
                    
                return find;
            }
            catch
            {
                return null;
            }
        }
    }
}
