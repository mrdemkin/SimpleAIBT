using UnityEngine;
using System.Timers;

namespace Character
{
    public class PlayerModel : CharacterModel
    {
		Timer timerUnderAttack;
        private int revertUnderAttackTime = 1500;

		private bool _underAttack;
		private bool underAttack {
			get {
				return _underAttack;
			}
			set {
                timerUnderAttack.Stop();
				_underAttack = value;
				if (_underAttack == true)
					timerUnderAttack.Start();
			}
		}
				
        private int lowHealthGandigap = 20;

		public PlayerModel(){
			Init ();
		}

		public override void Init()
        {
            if (isInited) return;
			SetHp (100);
            canRespawn = false;
            canMove = true;
            canDistantAttack = true;
            isInited = true;
			timerUnderAttack = new Timer (revertUnderAttackTime);
			timerUnderAttack.AutoReset = false;
			timerUnderAttack.Elapsed += EndUnderAttack;
        }

        public override void Attack()
        {

        }

        public override void OpenShield()
        {

        }

        public override void SetHp(float newValue)
        {
			if (this.hp > newValue) {
				this.underAttack = true;
			}
			this.hp = newValue;
        }

        public override void SetSpeed(float newValue)
        {
            this.speed = newValue;
        }

        public override void SetDefence(float newValue)
        {

        }

        public override bool isLowHealth()
        {
            return (hp <= lowHealthGandigap) ? true : false;
        }

		//TODO: get-set
        public override bool isUnderAttack()
        {
            //TODO: How check under attack?!
			//health changed to low last 2 seconds
			return this.underAttack;
        }

		public void EndUnderAttack(object sender, ElapsedEventArgs e) {
			timerUnderAttack.Stop();
			this.underAttack = false;
			Debug.Log ("END UNDER ATTTACK");
		}
    }
}
