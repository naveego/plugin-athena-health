using System;
using PluginAthenaHealth.Helper;
using Xunit;

namespace PluginAthenaHealthTest.Helper
{
    public class SettingsTest
    {
        [Fact]
        public void ValidateValidTest()
        {
            // setup
            var settings = new Settings
            {
                ClientId = "Client",
                ClientSecret = "Secret",
                PracticeId = "Practice",
                ProductionPractice = true
            };

            // act
            settings.Validate();

            // assert
        }

        [Fact]
        public void ValidateClientIdTest()
        {
            // setup
            var settings = new Settings
            {
                ClientId = null,
                ClientSecret = "Secret",
                PracticeId = "Practice",
                ProductionPractice = true
            };

            // act
            Exception e = Assert.Throws<Exception>(() => settings.Validate());

            // assert
            Assert.Contains("The ClientId property must be set", e.Message);
        }
        
        [Fact]
        public void ValidateClientSecretTest()
        {
            // setup
            var settings = new Settings
            {
                ClientId = "Client",
                ClientSecret = null,
                PracticeId = "Practice",
                ProductionPractice = true
            };

            // act
            Exception e = Assert.Throws<Exception>(() => settings.Validate());

            // assert
            Assert.Contains("The ClientSecret property must be set", e.Message);
        }
        
        [Fact]
        public void ValidatePracticeIdTest()
        {
            // setup
            var settings = new Settings
            {
                ClientId = "Client",
                ClientSecret = "Secret",
                PracticeId = null,
                ProductionPractice = true
            };

            // act
            Exception e = Assert.Throws<Exception>(() => settings.Validate());

            // assert
            Assert.Contains("The PracticeId property must be set", e.Message);
        }
    }
}