using Godot;
using System;

namespace Controllers 
{
    public partial class Player : CharacterBody2D
    {
        public enum PlayerStates {
            Neutral,
            Sweeping,
            Rolling,
            Backstepping,
            Throwing,
            Healing
        }

        public int Health = 100;
        public int Stamina = 100;
        public float MoveSpeed = 200;

        public PlayerStates State = PlayerStates.Neutral;
        Vector2 facing = Vector2.Down;

        public int CostToRoll = 20;
        public int CostToBackstep = 20;

        public bool BurnedOut = false;

        public override void _PhysicsProcess(double delta)
        {
            Vector2 movementDirection = Input.GetVector("Left", "Right", "Up", "Down");
            if (movementDirection != Vector2.Zero) {
                var movementVelocity = movementDirection * MoveSpeed * (float) delta;
                MoveAndCollide(movementVelocity);
            }
        }

        public override void _Input(InputEvent @event) {
            if (BurnedOut) return;

            if (Input.IsActionJustPressed("Roll")) {
                if (State == PlayerStates.Neutral || State == PlayerStates.Healing) {
                    Roll();
                }
            }
        }




        public void Roll() {
            if (State == PlayerStates.Rolling) return;
        }

        public void Backstep() {}
        public void Attack() {}
        public void Throw() {}
        public void Heal() {}

        public void ConsumeStamina(int amount) {
            Stamina = Mathf.Clamp(Stamina - amount, 0, 100);
            if (Stamina == 0){
                BurnedOut = true;
            }
        }

        public void RecoverStamina(int amount) {
            Stamina = Mathf.Clamp(Stamina + amount, 0, 100);
            if (Stamina == 100) {
                if (BurnedOut) BurnedOut = false;
            }
        }

    }
}