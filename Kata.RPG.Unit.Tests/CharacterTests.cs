using System;
using Kata_RPG;
using Xunit;


namespace Kata_RPG_Tests
{
    
    public class CharacterTests
    {
        private readonly Character _characterToTest;
        private readonly Character _characterToTestCustomValues;
        private readonly Character _characterToTestBoundaryValidation;
        private readonly Character _characterToReceiveDamageTest;
        private readonly Character _characterToReceiveDamageWithinLevel;
        private readonly Character _characterToReceiveDamage5LevelsAbove;
        private readonly Character _characterToReceiveDamage5LevelsBelow;
        private readonly CombatEngine _combatEngine;

        public CharacterTests()
        {
            _characterToTest = new Character();
            _characterToTestCustomValues = new Character(500, 40, "Jordan", "Melee");
            _characterToTestBoundaryValidation = new Character(-1, 61, "Jordan", "Meeeeeleeeee");
            _characterToReceiveDamageTest = new Character(980, 50, "Callum", "Ranged");
            _characterToReceiveDamageWithinLevel = new Character(500, 40, "Victor", "Melee");
            _combatEngine = new CombatEngine();
            _characterToReceiveDamage5LevelsAbove = new Character(980, 50, "Callum", "Ranged");
            _characterToReceiveDamage5LevelsBelow = new Character(500, 30, "Victor", "Ranged");
        }
        
        [Fact]
        public void CharacterNameTest()
        {
            Assert.Equal("Victor", _characterToReceiveDamage5LevelsBelow.Name);
        }
        [Fact]
        public void CharacterHealthTest()
        {
            Assert.Equal(1000, _characterToTest.Health);
        }
        
        [Fact]
        public void CharacterHealthTestCustom()
        {
            Assert.Equal(500, _characterToTestCustomValues.Health);
        }
        
        [Fact]
        public void CharacterHealthTestCustomOutsideBoundary()
        {
            Assert.Equal(1000, _characterToTestBoundaryValidation.Health);
        }
        
        
        [Fact]
        public void CharacterLevelTest()
        {
            Assert.Equal(1, _characterToTest.Level);
        }
        
        [Fact]
        public void CharacterLevelTestCustom()
        {
            Assert.Equal(40, _characterToTestCustomValues.Level);
        }
        
        [Fact]
        public void CharacterLevelTestCustomOutsideBoundary()
        {
            Assert.Equal(1, _characterToTestBoundaryValidation.Level);
        }
        
        [Fact]
        public void CharacterIsAliveTest()
        {
            Assert.True(_characterToTest.IsAlive);
        }
        
        [Fact]
        public void CharacterIsAliveTestCustom()
        {
            while (_characterToTestCustomValues.IsAlive)
            {
                _characterToTestBoundaryValidation.DealDamage(_characterToTestCustomValues);
            }
            Assert.False(_characterToTestCustomValues.IsAlive);
        }

        [Fact]
        public void CharacterIsAliveTestCustomOutsideBoundary()
        {
            Assert.True(_characterToTestBoundaryValidation.IsAlive);
        }

        [Fact]
        public void TestDamageDealingNormal()
        {
            
            Assert.Equal(100, _combatEngine.CalculateMitigation(_characterToTestCustomValues, _characterToReceiveDamageWithinLevel, 100));
        }
        
        [Fact]
        public void TestDamageDealingOverFiveLevels()
        {
            Assert.Equal(50, _combatEngine.CalculateMitigation(_characterToTestCustomValues, _characterToReceiveDamage5LevelsAbove, 100));
        }
        
        [Fact]
        public void TestDamageDealingBelowFiveLevels()
        {
            Assert.Equal(150, _combatEngine.CalculateMitigation(_characterToTestCustomValues, _characterToReceiveDamage5LevelsBelow, 100));
        }
        
        [Fact]
        public void TestDamageDealingToSelf()
        {
            _characterToTestCustomValues.DealDamage(_characterToTestCustomValues);
            Assert.Equal(500, _characterToTestCustomValues.Health);
        }

        [Fact]
        public void TestHealing()
        {
            _characterToReceiveDamageWithinLevel.Heal();
            Assert.True(_characterToReceiveDamageWithinLevel.Health > 500);
        }
        
        [Fact]
        public void TestCombatTypeMelee()
        {
           Assert.Equal(CombatType.Melee, _characterToTestCustomValues.CombatType);
        }
        
        [Fact]
        public void TestCombatTypeRanged()
        {
            Assert.Equal(CombatType.Ranged, _characterToReceiveDamage5LevelsAbove.CombatType);
        }
        
        [Fact]
        public void TestCombatTypeDefault()
        {
            Assert.Equal(CombatType.Melee, _characterToTestBoundaryValidation.CombatType);
        }
    }
}