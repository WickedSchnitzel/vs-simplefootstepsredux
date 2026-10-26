using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace SimpleFootStepsRedux
{
    public class AiTaskLoudWander : AiTaskWander
    {
        string trigger = "wander";

        public AssetLocation stepSound;

        bool alreadycheckedforsound = false;

        public float stepTimer = new float();

        float stepTimerStop = 0.55f;

        float volume = 1;

        float range = 32;

        bool changepitch = true;

        public AiTaskLoudWander(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
        }

        public override bool ContinueExecute(float dt)
        {
            if (stepSound == null && !alreadycheckedforsound)
            {
                alreadycheckedforsound=true;
                SoundEntry trysound = SimpleFootStepsReduxModSystem.GetSoundEntry(entity, trigger);
                if (trysound != null)
                {
                    stepSound = new AssetLocation(trysound.soundFile);
                    stepTimerStop = trysound.soundTime;
                    volume = trysound.volume;
                    changepitch=trysound.changepitch;
                }
            }
            if (stepSound == null) { return base.ContinueExecute(dt); }
            stepTimer += dt;

            if (stepTimer >= stepTimerStop)
            {
                world.PlaySoundAt(stepSound, entity.Pos.X, entity.Pos.Y, entity.Pos.Z, null, true, range, volume);
                stepTimer = 0;
            }

            return base.ContinueExecute(dt); 
        }
    }

    public class AiTaskLoudFleeEntity : AiTaskFleeEntity
    {
        string trigger="fleeentity";

        public AssetLocation stepSound;

        bool alreadycheckedforsound = false;

        public float stepTimer = new float();

        float stepTimerStop = 0.55f;

        float volume = 1;

        float range = 32;

        bool changepitch = true;

        public AiTaskLoudFleeEntity(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
        }

        public override bool ContinueExecute(float dt)
        {
            if (stepSound == null && !alreadycheckedforsound)
            {
                alreadycheckedforsound = true;
                SoundEntry trysound = SimpleFootStepsReduxModSystem.GetSoundEntry(entity, trigger);
                if (trysound != null)
                {
                    stepSound = new AssetLocation(trysound.soundFile);
                    stepTimerStop = trysound.soundTime;
                    volume = trysound.volume;
                    changepitch=trysound.changepitch;
                }
            }
            if (stepSound == null) { return base.ContinueExecute(dt); }
            stepTimer += dt;

            if (stepTimer >= stepTimerStop)
            {
                world.PlaySoundAt(stepSound, entity.Pos.X, entity.Pos.Y, entity.Pos.Z, null, true, range, volume);
                stepTimer = 0;
            }

            return base.ContinueExecute(dt);
        }
    }

    public class AiTaskLoudSeekEntity : AiTaskSeekEntity
    {
        string trigger = "seekentity";

        public AssetLocation stepSound;

        bool alreadycheckedforsound = false;

        public float stepTimer = new float();

        float stepTimerStop = 0.55f;

        float volume = 1;

        float range = 32;

        bool changepitch = true;

        public AiTaskLoudSeekEntity(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
        }

        public override bool ContinueExecute(float dt)
        {
            if (stepSound == null && !alreadycheckedforsound)
            {
                alreadycheckedforsound = true;
                SoundEntry trysound = SimpleFootStepsReduxModSystem.GetSoundEntry(entity, trigger);
                if (trysound != null)
                {
                    stepSound = new AssetLocation(trysound.soundFile);
                    stepTimerStop = trysound.soundTime;
                    volume = trysound.volume;
                    changepitch=trysound.changepitch;
                }
            }
            if (stepSound == null) { return base.ContinueExecute(dt); }
            stepTimer += dt;

            if (stepTimer >= stepTimerStop)
            {
                world.PlaySoundAt(stepSound, entity.Pos.X, entity.Pos.Y, entity.Pos.Z, null, true, range, volume);
                stepTimer = 0;
            }

            return base.ContinueExecute(dt);
        }
    }

    public class AiTaskLoudGetOutOfWater : AiTaskGetOutOfWater
    {
        string trigger = "getoutofwater";

        public AssetLocation stepSound;

        bool alreadycheckedforsound = false;

        public float stepTimer = new float();

        float stepTimerStop = 0.55f;

        float volume = 1;

        float range = 32;

        bool changepitch = true;

        public AiTaskLoudGetOutOfWater(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
        }

        public override bool ContinueExecute(float dt)
        {
            if (stepSound == null && !alreadycheckedforsound)
            {
                alreadycheckedforsound = true;
                SoundEntry trysound = SimpleFootStepsReduxModSystem.GetSoundEntry(entity, trigger);
                if (trysound != null)
                {
                    stepSound = new AssetLocation(trysound.soundFile);
                    stepTimerStop = trysound.soundTime;
                    volume = trysound.volume;
                    changepitch=trysound.changepitch;
                }
            }
            if (stepSound == null) { return base.ContinueExecute(dt); }
            stepTimer += dt;

            if (stepTimer >= stepTimerStop)
            {
                world.PlaySoundAt(stepSound, entity.Pos.X, entity.Pos.Y, entity.Pos.Z, null, true, range, volume);
                stepTimer = 0;
            }

            return base.ContinueExecute(dt);
        }
    }

    public class AiTaskLoudStayCloseToEntity : AiTaskStayCloseToEntity
    {
        string trigger = "stayclosetoentity";

        public AssetLocation stepSound;

        bool alreadycheckedforsound = false;

        public float stepTimer = new float();

        float stepTimerStop = 0.55f;

        float volume = 1;

        float stepSoundRange = 32;

        bool changepitch = true;

        public AiTaskLoudStayCloseToEntity(EntityAgent entity, JsonObject taskConfig, JsonObject aiConfig) : base(entity, taskConfig, aiConfig)
        {
        }

        public override bool ContinueExecute(float dt)
        {
            if (stepSound == null && !alreadycheckedforsound)
            {
                alreadycheckedforsound = true;

                SoundEntry trysound = SimpleFootStepsReduxModSystem.GetSoundEntry(entity, trigger);

                if (trysound != null)
                {
                    stepSound = new AssetLocation(trysound.soundFile);
                    stepTimerStop = trysound.soundTime;
                    volume = trysound.volume;
                    changepitch=trysound.changepitch;
                }
            }
            if (stepSound == null) { return base.ContinueExecute(dt); }
            stepTimer += dt;

            if (stepTimer >= stepTimerStop)
            {
                world.PlaySoundAt(stepSound, entity.Pos.X, entity.Pos.Y, entity.Pos.Z, null, true, stepSoundRange, volume);
                stepTimer = 0;
            }

            return base.ContinueExecute(dt);
        }
    }
}
