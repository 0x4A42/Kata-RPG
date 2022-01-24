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
        private readonly CombatEngine _combatEngine;

        public CharacterTests()
        {
            _characterToTest = new Character();
            _characterToTestCustomValues = new Character(500, 40, false, "Jordan");
            _characterToTestBoundaryValidation = new Character(-1, 61, true, "Jordan");
            _characterToReceiveDamageTest = new Character(980, 50, true, "Callum");
            _characterToReceiveDamageWithinLevel = new Character(500, 40, true, "Victor");
            _combatEngine = new CombatEngine();
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
            Assert.False(_characterToTestCustomValues.IsAlive);
        }

        [Fact]
        public void CharacterIsAliveTestCustomOutsideBoundary()
        {
            Assert.Equal(true, _characterToTestBoundaryValidation.IsAlive);
        }

        [Fact]
        public void TestDamageDealingNormal()
        {
            
            Assert.Equal(_combatEngine.CalculateMitigation(_characterToTestCustomValues, _characterToReceiveDamageWithinLevel, 100), 100);
        }
        
        [Fact]
        public void TestDamageDealingOverFiveLevels()
        {
            _characterToTestCustomValues.DealDamage(_characterToReceiveDamageTest);
        }
        
        [Fact]
        public void TestDamageDealingBelowFiveLevels()
        {
            _characterToTestCustomValues.DealDamage(_characterToReceiveDamageTest);
        }
        
        [Fact]
        public void TestDamageDealingToSelf()
        {
            _characterToTestCustomValues.DealDamage(_characterToTestCustomValues);
            Assert.Equal(_characterToTestCustomValues.Health, 500);
        }
    }
}