using Common.ServiceLocator;
using DataStorage.DataProviders;
using Info;
using System;
using System.Collections.Generic;
using Xunit;

namespace DataStorage.Test.ChatDataProvider
{
    public sealed class CChatDataProviderTest
    {
        private readonly ICChatInfoDataProvider _chatDataProvider;

        public CChatDataProviderTest()
        {
            var container = SServiceLocator.CreateContainer();
            container.Register<ICChatInfoDataProvider, CChatInfoDataProvider>(ELifeCycle.Transient);
            container.Register<CDataStorageSettings>(ELifeCycle.Transient);
            _chatDataProvider = container.Resolve<ICChatInfoDataProvider>();
        }

        [Fact]
        public void GetChatsByParticipantId_NormalExecution()
        {
            //Arrange
            var testUserGuid = SWholeRepositoryStub.Users[1].Id;
            var expected = new List<CChatInfo>
            {
                SWholeRepositoryStub.Chats[1],
                SWholeRepositoryStub.Chats[2],
                SWholeRepositoryStub.Chats[3],
            };

            //Act
            var result = _chatDataProvider.GetChatsByParticipantId(testUserGuid);

            //Assert
            Assert.NotNull(result);
            //for (int i = 0; i < result.Count; i++)
            //{
            //    Assert.Equal(expected[i], result[i]);
            //}
            //Xunit.Assert.Equal<CChatInfo>(expected, result);
            //Comparer<CChatInfo>.Create(new Comparison<CChatInfo>()
            var comparer = new CLambdaEqualityComparer<CChatInfo>(
                (x, y) => 
                    x.Id == y.Id && x.IsPersonal == y.IsPersonal && 
                    x.OwnerId == y.OwnerId && String.Equals(x.Title, y.Title) 
                    && x.Type == y.Type, 
                x => x.GetHashCode()
            );
            Xunit.Assert.Equal<CChatInfo>(expected, result, comparer);
        }

    }
}
