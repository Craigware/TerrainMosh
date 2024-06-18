using Godot;

namespace Controllers 
{
    public partial class Player : CharacterBody2D
    {
        public enum PlayerStates {
            Neutral,
            Sweeping,
            ChargingRoll,
            Rolling,
            Backstepping,
            Throwing,
            Healing
        }

        [Export] public int Health = 100;
        [Export] public int Stamina = 100;
        public bool BurnedOut = false;
 
        public float WalkSpeed = 100;
        [Export] public float MoveSpeed = 200;
        

        float MaxMoveSpeedTime = 0.5f;
        float MaxMoveSpeedProgress = 0;

        public PlayerStates State = PlayerStates.Neutral;
        Vector2 facing = Vector2.Down;

        Vector2 movementVelocity;


        [Export] public float RollSpeed = 800;
        float RollStrength = 1f;
        float RollTime = 0.25f;
        float RollProgress = 0;
        public int CostToRoll = 20;

        [Export] public float BackSteepStrength = 400;
        float BackstepTime = 0.1f;
        float BackstepProgress = 0;
        public int CostToBackstep = 20;

        public override void _PhysicsProcess(double delta)
        {
            if (State != PlayerStates.Rolling && State != PlayerStates.Backstepping) {
                Vector2 movementDirection = Input.GetVector("Left", "Right", "Up", "Down");
 
                if (movementDirection != Vector2.Zero) {
                    movementVelocity = movementDirection * (MoveSpeed * (1 + MaxMoveSpeedProgress)) * (float) delta;
                    MoveAndCollide(movementVelocity);
                    MaxMoveSpeedProgress = Mathf.Clamp(MaxMoveSpeedProgress + 1 * (float) delta, 0, 0.5f);
                } else {
                    MaxMoveSpeedProgress = 0;
                }

                facing = Position.DirectionTo(GetViewport().GetMousePosition());
            }
            
            if (State == PlayerStates.Rolling) {
                MoveAndCollide(movementVelocity * (float) delta);
                
                if (RollProgress >= RollTime) {
                    State = PlayerStates.Neutral;
                    RollProgress = 0;
                } else 
                    RollProgress += 1 * (float) delta;
            }

            if (State == PlayerStates.Backstepping) {
                MoveAndCollide(movementVelocity * (float) delta);
                if (BackstepProgress >= BackstepTime) {
                    State = PlayerStates.Neutral;
                    BackstepProgress = 0;
                } else 
                    BackstepProgress += 1 * (float) delta;
            }

            if (State == PlayerStates.ChargingRoll) {
                RollStrength = Mathf.Clamp(RollStrength + 1 * (float) delta, 1, 1.5f);
            }
        }

        public override void _Input(InputEvent @event) {
            if (BurnedOut) return;
            if (Input.IsActionJustPressed("Roll")) BeginRoll();
            if (Input.IsActionJustReleased("Roll")) Roll();
            if (Input.IsActionJustPressed("Backstep")) Backstep();
        }

        public void BeginRoll() {
            if (State == PlayerStates.Rolling) return;
            State = PlayerStates.ChargingRoll;
        }

        public void Roll() {
            if (State != PlayerStates.ChargingRoll) return;
            State = PlayerStates.Rolling;
            movementVelocity = facing * RollSpeed * RollStrength;
            RollStrength = 1;
        }

        public void Backstep() {
            if (State == PlayerStates.Backstepping) return;
            State = PlayerStates.Backstepping;
            movementVelocity = facing * -1 * RollSpeed;
        }
 
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
            if (Stamina == 20) {
                if (BurnedOut) BurnedOut = false;
            }
        }
    }
}