﻿namespace Javatale.Prototype
{

	/// <summary>
	/// Contains all constant strings like tags, animator parameters, sorting layer names, and playerpref keys
	/// </summary>
	public static class Constants 
	{
		public static class AnimatorParameter
		{
			public static class Int
			{
				// 
			}

			public static class Float
			{
				public const string FACE_X = "FaceX";
				public const string FACE_Y = "FaceY";
			}

			public static class Trigger
			{
				public const string EXPLODE = "Explode";
				public const string ANIMATE = "Animate";
				public const string GRASS_WAVElEFT = "WaveLeft";
				public const string GRASS_WAVERIGHT = "WaveRight";
				public const string FIREFLY_FLY = "Fly";
				public const string HIT = "Hit";
			}

			public static class Bool
			{
				//
			}

		}

		public static class SortingLayerName
		{

		}

		public static class Tag
		{
			public const string PLAYER = "Player"; 
			public const string ENEMY = "Enemy"; 
			public const string BOSS = "Boss"; 
			public const string PLAYER_SLASH = "Player Slash";
			public const string PLAYER_DASH_ATTACK = "Player Dash Attack"; 
			public const string PLAYER_COUNTER = "Player Counter"; 
			public const string ENEMY_ATTACK = "Enemy Attack"; 
			public const string STONE = "Stone"; 
			public const string DIG_AREA = "Dig Area"; 
			public const string DIG_RESULT = "Dig Result"; 
			public const string SWIM_AREA = "Swim Area"; 
			public const string FISHING_AREA = "Fishing Area"; 

			public const string HAMMER = "Hammer"; 
			public const string NET = "Net"; 
			public const string ARROW = "Arrow"; 
			public const string FIRE_ARROW = "Fire Arrow"; 
			public const string MAGIC_MEDALLION = "Magic Medallion";
			public const string FISH = "Fish"; 
			public const string LIFTABLE = "Liftable"; 
			public const string LOOTABLE = "Lootable"; 
			public const string BOUNDARIES = "Boundaries";
			public const string CHEST = "Chest";
			public const string EXPLOSION = "Explosion";
			public const string GROUND = "Ground";
			public const string VINES = "Vines";
			public const string GATE = "Gate";
			// public const string CRACKED_WALL = "Cracked Wall";

			public const string NPC = "NPC";
			public const string PLAYER_INTERACT = "Player Interact";

			//JATAYU ATTACK
			public const string JATAYU_ATTACK_1 = "Jatayu Attack 1";
			public const string JATAYU_ATTACK_2 = "Jatayu Attack 2";
			public const string JATAYU_ATTACK_3 = "Jatayu Attack 3";

			//TERRAIN
			// public const string TERRAIN_GRASS = "Terrain Grass";
			// public const string TERRAIN_WATER = "Terrain Water";
			// public const string TERRAIN_DIRT = "Terrain Dirt";
			
		}

		public static class PlayerPrefKey
		{
			public const string FINISHED_TIMELINE = "Player/Saved/FinishedTimeline/";
			
			public const string PLAYER_SAVED_HP = "Player/Saved/HP";
			public const string PLAYER_SAVED_MP = "Player/Saved/MP";
			public const string PLAYER_SAVED_COIN = "Player/Saved/Coin";
			public const string PLAYER_SAVED_KEY = "Player/Saved/Key";
			public const string PLAYER_SAVED_TOOL = "Player/Saved/Tool";

			public const string PLAYER_STATS_MAXHP = "Player/Stats/MaxHP";
			public const string PLAYER_STATS_HP = "Player/Stats/HP";
			public const string PLAYER_STATS_MAXMP = "Player/Stats/MaxMP";
			public const string PLAYER_STATS_MP = "Player/Stats/MP";
			public const string PLAYER_STATS_COIN = "Player/Stats/Coin";

			public const string PLAYER_TOOL_BOW = "Player/Tool/Bow";
			public const string PLAYER_TOOL_HOOK = "Player/Tool/Hook";
			public const string PLAYER_TOOL_BOMB = "Player/Tool/Bomb";
			public const string PLAYER_TOOL_HAMMER = "Player/Tool/Hammer";
			public const string PLAYER_TOOL_NET = "Player/Tool/Net";
			public const string PLAYER_TOOL_FISHINGROD = "Player/Tool/FishingRod";
			public const string PLAYER_TOOL_CONTAINER1 = "Player/Tool/Container1";
			public const string PLAYER_TOOL_CONTAINER2 = "Player/Tool/Container2";
			public const string PLAYER_TOOL_CONTAINER3 = "Player/Tool/Container3";
			public const string PLAYER_TOOL_CONTAINER4 = "Player/Tool/Container4";
			public const string PLAYER_TOOL_SHOVEL = "Player/Tool/Shovel";
			public const string PLAYER_TOOL_LANTERN = "Player/Tool/Lantern";
			public const string PLAYER_TOOL_INVISIBILITYCLOAK = "Player/Tool/InvisibilityCloak";
			public const string PLAYER_TOOL_MAGICMEDALLION = "Player/Tool/MagicMedallion";
			public const string PLAYER_TOOL_FASTTRAVEL = "Player/Tool/FastTravel";
			public const string PLAYER_TOOL_POWERBRACELET = "Player/Tool/PowerBracelet";
			public const string PLAYER_TOOL_FLIPPERS = "Player/Tool/Flippers";
			public const string PLAYER_TOOL_BOOTS = "Player/Tool/Boots";

			public const string LEVEL_PLAYER_START_POS = "Level/PlayerStartPos";
			public const string LEVEL_CURRENT = "Level/CurrentLevel";
			public const string PLAYER_SAVED_CONTAINER = "Player/Itam/Container/";
			
			public const string PLAYER_DEBUG_ATTACK_AREA = "Player/Debug/AttackArea";
		}
		
		public static class NPCPrefKey {
			public const string NPC_INTERACT_STATE = "NPC/InteractState/";
		}
		
		public static class EnvirontmentPrefKey
		{
			public const string VINES_STATE = "Environtment/State/Vines";
			public const string GATES_STATE = "Environtment/State/Gates";
			public const string CHEST_STATE = "Environtment/State/Chest";
			public const string CHEST_SPAWNER_STATE = "Environtment/State/ChestSpawner";
			public const string CRACKED_WALL_STATE = "Environtment/State/CrackedWall";
		}

		public static class BlendTreeName
		{
			#region Player
			public const string IDLE_STAND = "IDLE_STAND";
			public const string MOVE_RUN = "MOVE_RUN";

			//========== ========== ========== ========== ==========

			public const string MOVE_DODGE = "MoveDodge";
			public const string IDLE_DASH = "IdleDash";
			public const string MOVE_DASH = "MoveDash";
			public const string IDLE_DIE = "IdleDie";
			public const string IDLE_BRAKE = "IdleBrake";
			public const string IDLE_CHARGE = "IdleCharge";
			public const string MOVE_CHARGE = "MoveCharge";
			public const string IDLE_GUARD = "IdleGuard";
			public const string MOVE_GUARD = "MoveGuard";
			public const string IDLE_BULLET_TIME = "IdleBulletTime";
			public const string RAPID_SLASH_BULLET_TIME = "RapidSlashBulletTime";
			public const string IDLE_SWIM = "IdleSwim";
			public const string MOVE_SWIM = "MoveSwim";
			public const string GET_HURT = "GetHurt";
			public const string BLOCK_ATTACK = "BlockAttack";
			// public const string COUNTER_ATTACK = "CounterAttack";
			public const string PARRY = "Parry";
			public const string CHARGE_ATTACK = "ChargeAttack";
			public const string NORMAL_ATTACK_1 = "NormalAttack1";
			public const string NORMAL_ATTACK_2 = "NormalAttack2";
			public const string NORMAL_ATTACK_3 = "NormalAttack3";
			public const string GRABBING = "Grabbing";
			public const string SWEATING_GRAB = "SweatingGrab";
			public const string UNGRABBING = "UnGrabbing";
			public const string IDLE_PUSH = "IdlePush";
			public const string MOVE_PUSH = "MovePush";
			public const string LIFTING = "Lifting";
			public const string IDLE_LIFT = "IdleLift";
			public const string MOVE_LIFT = "MoveLift";
			public const string THROWING_LIFT = "ThrowingLift";
			public const string THROW_FISH_BAIT = "ThrowFishBait";
			public const string IDLE_FISHING = "IdleFishing";
			public const string RETURN_FISH_BAIT = "ReturnFishBait";
			public const string FISHING_FAIL = "FishingFail";
			public const string FISHING_CAUGHT = "FishingCaught";
			public const string LIFTING_TREASURE = "LiftingTreasure";
			public const string IDLE_LIFT_TREASURE = "IdleLiftTreasure";
			public const string END_LIFT_TREASURE = "EndLiftTreasure";
			public const string TAKE_AIM_BOW = "TakeAimBow";
			public const string AIMING_BOW = "AimingBow";
			public const string OPENING_CHEST = "OpeningChest";
			public const string AFTER_OPEN_CHEST = "AfterOpenChest";
			public const string SHOT_BOW = "ShotBow";
			public const string USE_BOMB = "UseBomb";
			public const string USE_HAMMER = "UseHammer";
			public const string USE_SHOVEL = "UseShovel";
			public const string USE_MAGIC_MEDALLION = "UseMagicMedallion";
			public const string USE_CONTAINER = "UseContainer";
			#endregion

			#region Stand
			public const string STAND_TAKE_AIM_BOW = "StandTakeAimBow";
			public const string STAND_AIMING_BOW = "StandAimingBow";
			public const string STAND_SHOT_BOW = "StandShotBow";
			public const string STAND_GRABBING = "StandGrabbing";
			public const string STAND_UNGRABBING = "StandUnGrabbing";
			public const string STAND_IDLE_PUSH = "StandIdlePush";
			public const string STAND_MOVE_PUSH = "StandMovePush";
			public const string STAND_IDLE_LIFT = "StandIdleLift";
			public const string STAND_LIFTING = "StandLifting";
			public const string STAND_MOVE_LIFT = "StandMoveLift";
			public const string STAND_THROWING_LIFT = "StandThrowingLift";
			public const string STAND_DASH = "StandDash";
			public const string STAND_BOMB = "StandBomb";
			public const string STAND_MAGIC_MEDALLION = "StandMagicMedallion";
			public const string STAND_INACTIVE = "StandInactive";
			#endregion

			#region Enemy
			public const string ENEMY_IDLE = "Idle";
			public const string ENEMY_ATTACK = "Attack";
			public const string ENEMY_AGGRO = "Aggro";
			public const string ENEMY_CHASE = "Chase";
			public const string ENEMY_DAMAGED = "Damaged";
			public const string ENEMY_PATROL = "Patrol";
			public const string ENEMY_DIE = "Die";
			public const string ENEMY_IDLE_BARE = "IdleBare";
			public const string ENEMY_PATROL_BARE = "PatrolBare";
			#endregion
		}

		public static class AnimationName
		{
			#region Another Object
			public const string CHEST_OPEN = "ChestOpen"; //CHEST
			public const string CHEST_LOCKED = "ChestLocked"; //CHEST
			public const string GATE_IDLE = "GateIdle"; //GATE
			public const string GATE_LOCKED = "GateLocked"; //GATE
			public const string DESTROY = "Destroy"; //BEEHIVE
			public const string HIT = "Hit"; //BEEHIVE
			public const string Idle = "Idle"; //
			#endregion

			#region UI
			public const string CANVAS_VISIBLE = "IdleOn";
			public const string CANVAS_INVISIBLE = "IdleOff";
			public const string FADE_IN = "FadeIn";
			public const string FADE_OUT = "FadeOut";
			public const string SLIDE_IDLE = "SlideIdle";
			public const string SLIDE_RIGHT = "SlideRight";
			public const string SLIDE_LEFT = "SlideLeft";
			#endregion
		}

		public static class SceneName
		{
			public const string MAIN_MENU = "SceneMenu";
			public const string GAME_MAP_01 = "SceneMap_01";
			public const string SCENE_LEVEL_1 = "SceneLevel_1-1";
			public const string SCENE_LEVEL_2_1 = "SceneLevel_2-1";
			public const string SCENE_LEVEL_2_2 = "SceneLevel_2-2";
			public const string SCENE_LEVEL_2_3 = "SceneLevel_2-3";
			public const string SCENE_LEVEL_3_1 = "SceneLevel_3-1";
			public const string SCENE_LEVEL_3_2 = "SceneLevel_3-2";
			public const string SCENE_LEVEL_3_3 = "SceneLevel_3-3";
			public const string SCENE_JATAYU = "SceneLevel_Jatayu";
		}

		public static class DissolvedLevelPrefKey
		{
			public const string LEVEL_INDEX = "Level/Dissolved/Level_Index";
			// public const string LEVEL_1 = "Level/Saved/IsCompleted_Level_1";
			// public const string LEVEL_2_1 = "Level/Saved/IsCompleted_Level_2_1";
			// public const string LEVEL_2_2 = "Level/Saved/IsCompleted_Level_2_2";
			// public const string LEVEL_2_3 = "Level/Saved/IsCompleted_Level_2_3";
			// public const string LEVEL_3_1 = "Level/Saved/IsCompleted_Level_3_1";
			// public const string LEVEL_3_2 = "Level/Saved/IsCompleted_Level_3_2";
			// public const string LEVEL_3_3 = "Level/Saved/IsCompleted_Level_3_3";
		}

		public static class QuestPrefKey
		{
			public const string QUEST_INDEX = "Quest/Completed/Quest_Index";
			// public const string QUEST_LEVEL_1 = "Level/Quest/Kill_Count_Level_1";
			// public const string QUEST_LEVEL_2_1 = "Level/Quest/Kill_Count_Level_2_1";
			// public const string QUEST_LEVEL_2_2 = "Level/Quest/Kill_Count_Level_2_2";
			// public const string QUEST_LEVEL_2_3 = "Level/Quest/Kill_Count_Level_2_3";
			// public const string QUEST_LEVEL_3_1 = "Level/Quest/Kill_Count_Level_3_1";
			// public const string QUEST_LEVEL_3_2 = "Level/Quest/Kill_Count_Level_3_2";
			// public const string QUEST_LEVEL_3_3 = "Level/Quest/Kill_Count_Level_3_3";
		}
	}
}
