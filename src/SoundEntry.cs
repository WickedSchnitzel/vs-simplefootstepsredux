namespace SimpleFootStepsRedux
{
    /// <summary>
    /// Data for mob sounds
    /// </summary>
    public class SoundEntry
    {      
        //the mobs code or wildcard pattern this applies to, eg: drifter
        public string mobMatchCode;

        //the name of the sound, eg: wander
        public string soundTrigger;

        //the full filename of the relevant sound eg: simplefootstepsredux:sounds/creature/steps/npc
        public string soundFile; 

        //the delay between sounds eg: 0.55
        public float soundTime = 0.5f;

        public float volume = 1;

        public bool changepitch = false;
    }
    
}
