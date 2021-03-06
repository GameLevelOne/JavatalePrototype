using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
// using Unity.Transforms;
using UnityEngine;
using Unity.Burst;

using UnityRandom = UnityEngine.Random; //AMBIGUOUS ISSUE

namespace Javatale.Prototype 
{
	public class EnemyAIDirectionSystem : JobComponentSystem 
	{
		[BurstCompileAttribute]
		struct EnemyAIDirectionJob : IJobProcessComponentData <EnemyAI, MoveDirection, FaceDirection>
		{
            public float deltaTime;
            public float minPatrolCooldown;
            public float maxPatrolCooldown;
            public float minIdleCooldown;
            public float maxIdleCooldown;
            public Vector3 vector3Zero;

			public void Execute (
				ref EnemyAI enemyAI,
				ref MoveDirection moveDir,
				ref FaceDirection faceDir)
			{
                Vector3 moveDirValue = moveDir.Value;
                
                if (moveDirValue != vector3Zero)
                {
                    float patrolTimer = enemyAI.PatrolTimer;

                    if (patrolTimer <= 0.0f) 
                    {
                        moveDir.Value = new float3(0f, 0f, 0f);

                        enemyAI.IdleTimer = minIdleCooldown;
                        enemyAI.AnimationToggle = 1;
                    }
                    else
                    {
                        enemyAI.PatrolTimer -= deltaTime;
                    }
                }
                else
                {
                    float idleTimer = enemyAI.IdleTimer;

                    if (idleTimer <= 0.0f) 
                    {
                        Vector3 currentDir = faceDir.Value;
                        float randomX = currentDir.x * -1; //
                        float randomZ = 0; //
				        Vector3 direction = new Vector3 (randomX, 0f, randomZ);

                        if (currentDir == vector3Zero) direction = new float3(1, 0, 0); //
                        else direction = new float3(randomX, 0, 0);

                        if (randomX!=0f && randomZ!=0f) {//DIAGONAL FACING
                            if (currentDir.x==0f) {//PREVIOUS MOVEMENT IS VERTICAL
                                if (randomZ == -1f)
                                    faceDir.dirIndex = 1;//FACE DOWN
                                else 
                                    faceDir.dirIndex = 3;//FACE UP
                            } else {//PREVIOUS MOVEMENT IS HORIZONTAL
                                if (randomX == -1f)
                                    faceDir.dirIndex = 2;//FACE LEFT
                                else 
                                    faceDir.dirIndex = 4;//FACE RIGHT
                            }
                        } else if (randomZ == -1f) {//FACE DOWN
                            faceDir.dirIndex = 1;
                        } else if (randomZ == 1f) {//FACE UP
                            faceDir.dirIndex = 3;
                        } else if (randomX == -1f) {//FACE LEFT
                            faceDir.dirIndex = 2;
                        } else if (randomX == 1f) {//FACE RIGHT
                            faceDir.dirIndex = 4;
                        }

                        if (direction != vector3Zero) faceDir.Value = direction;

                        enemyAI.PatrolTimer = minPatrolCooldown;
					    moveDir.Value = direction;

                        enemyAI.AnimationToggle = 1;
                    }
                    else
                    {
                        enemyAI.IdleTimer -= deltaTime;
                    }
                }
			}
		}

		protected override JobHandle OnUpdate (JobHandle inputDeps)
		{
			EnemyAIDirectionJob enemyAIDirJob = new EnemyAIDirectionJob
			{
                deltaTime = Time.deltaTime,
                minPatrolCooldown = GameManager.settings.enemyMinPatrolCooldown,
                maxPatrolCooldown = GameManager.settings.enemyMaxPatrolCooldown,
                minIdleCooldown = GameManager.settings.enemyMinIdleCooldown,
                maxIdleCooldown = GameManager.settings.enemyMaxIdleCooldown,
                vector3Zero = Vector3.zero
			};

			JobHandle enemyAIDirHandle = enemyAIDirJob.Schedule(this, inputDeps);

			return enemyAIDirHandle;
		}
	}
}
