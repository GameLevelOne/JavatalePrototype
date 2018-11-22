using UnityEngine;
// using Unity.Rendering;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Javatale.Prototype
{
    public class PlayerAttackSlashBarrier : BarrierSystem {}

    public class JavataleSettings : MonoBehaviour
    {
        [HeaderAttribute("Attributes")]
        public float verticalBound = 10.0f;
        public float horizontalBound = 10.0f;
        public Vector3 worldToCameraRotation = Vector3.zero;

        [HeaderAttribute("Player Attributes")]
		public int maxPlayerAttackIndex = 0;
        public float playerMoveSpeed = 5.0f;
        // public float playerInitialHealth = 100.0f;
        // public float playerCollisionRadius = 1.0f;

        [HeaderAttribute("Enemy Attributes")]
		public int maxEnemy = 3;
        public float enemyMoveSpeed = 3.0f;
        public float enemyMinPatrolCooldown = 5.0f;
        public float enemyMaxPatrolCooldown = 10.0f;
        public float enemyMinIdleCooldown = 3.0f;
        public float enemyMaxIdleCooldown = 8.0f;
        // public float enemyInitialHealth = 100.0f;
        // public float enemyCollisionRadius = 1.0f;
        
        // public Vector3 worldToCameraRotation = new Vector3 (40f, 0f, 0f); 

        [HeaderAttribute("Rendering")]
        public Mesh playerMesh;
        public Mesh beeEnemyMesh;
        public Mesh playerAttackMesh;
        public Material playerMaterial;
        public Material enemyMaterial;

        [HeaderAttribute("Index = 0 : Attack 1, 1 : Attack 2, 2 : Attack 3")]
        public Material[] playerAttackMaterials;

        [SpaceAttribute(10f)]
        public GameObject playerChild;
        public GameObject beeEnemyChild;

        [HeaderAttribute("Index = 0 : Attack 1, 1 : Attack 2, 2 : Attack 3")]
        public GameObject[] playerAttack1Childs;
    }

	public enum PlayerAnimationState
    {
        IDLE_STAND,
        MOVE_RUN,
        ATTACK_1,
        ATTACK_2,
        ATTACK_3
    }

	public enum BeeAnimationState
    {
        IDLE_FLY,
        MOVE_FLY
    }

    public struct EntryPlayerAnim
    {
        public int DirIndex; //USELESS
        public float3 FaceDirValue;
        public PlayerAnimationState State;
        public int StartAnimationToggle;
        public int EndAnimationToggle;

        public EntryPlayerAnim (int dirIndex, float3 faceDirValue, PlayerAnimationState state, int startAnimToggle, int endAnimToggle)
        {
            DirIndex = dirIndex;
            FaceDirValue = faceDirValue;
            State = state;
            StartAnimationToggle = startAnimToggle;
            EndAnimationToggle = endAnimToggle;
        }
    }

    public struct EntryProjectileAnim
    {
        public int DirIndex; //USELESS
        public float3 FaceDirValue;
        public int StartAnimationToggle;
        public int EndAnimationToggle;

        public EntryProjectileAnim (int dirIndex, float3 faceDirValue, int startAnimToggle, int endAnimToggle)
        {
            DirIndex = dirIndex;
            FaceDirValue = faceDirValue;
            StartAnimationToggle = startAnimToggle;
            EndAnimationToggle = endAnimToggle;
        }
    }

    public struct EntryBeeAnim
    {
        public int DirIndex;
        public BeeAnimationState State;

        public EntryBeeAnim (int dirIndex, BeeAnimationState state)
        {
            DirIndex = dirIndex;
            State = state;
        }
    }

	public struct PlayerAttackSpawnData : IComponentData
	{
		public Position pos;
		public Rotation rot;
		public MoveDirection moveDir;
		public FaceDirection faceDir;
        public Parent parent;
        public Projectile projectile;
        public int attackIndex;
	}
}