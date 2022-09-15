namespace ET
{
    namespace EventType
    {
        public struct SceneChangeStart
        {
        }
        
        public struct SceneChangeFinish
        {
            public Scene ZoneScene;
            public Scene CurrentScene;
        }
        
        public struct AfterCreateClientScene
        {
        }
        
        public struct AfterCreateCurrentScene
        {
        }

        public struct AppStartInitFinish
        {
        }

        public struct LoginFinish
        {
        }

        public struct EnterMapFinish
        {
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }

        public struct AdventureBattleRound
        {
            public Scene ZoneScene;
            public Unit AttackUnit;
            public Unit TargetUnit;
        }
        public struct AdventureBattleRoundView
        {
            public Scene ZoneScene;
            public Unit AttackUnit;
            public Unit TargetUnit;
        }


        public struct AdventureBattleOver
        {
            public Scene ZoneScene;
            public Unit WinUnit;
        }
        
        public struct AdventureBattleReport
        {
            public Scene ZoneScene;
            public BattleRoundResult BattleRoundResult;
            public int Round;
        }
        
        public struct AdventureRoundReset
        {
            public Scene ZoneScene;
        }
        
        public struct ShowDamageValueView
        {
            public Scene ZoneScene;
            public Unit TargetUnit;
            public long DamamgeValue;
        }
        
        public class ShowAdventureHpBar : DisposeObject
        {
            [StaticField]
            public static readonly ShowAdventureHpBar Instance = new ShowAdventureHpBar();
            public Unit Unit;
            public bool isShow;
            
            public override void Dispose()
            {
                this.Unit = null;
            }
        }
    }
}