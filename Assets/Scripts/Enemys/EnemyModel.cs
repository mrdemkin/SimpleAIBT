using System;

namespace Character
{
	public class EnemyModel : CharacterModel
	{
		private int lowHealthGandigap = 20;
		public EnemyModel(){
			Init ();
		}

			public override void Init()
		{
				if (isInited) return;
				SetHp (100);
				canRespawn = true;
				canMove = true;
				canDistantAttack = false;
				isInited = true;
		}


		public override void Attack() {

		}

		public override void OpenShield() {

		}

		public override void SetHp(float newValue) {

		}

		public override void SetSpeed(float newValue) {

		}

		public override void SetDefence(float newValue) {

		}

		public override bool isLowHealth () {
			return (hp <= lowHealthGandigap) ? true : false;
		}

		//TODO: get-set
		public override bool isUnderAttack() {
			
			return false;
		}
	}
}

