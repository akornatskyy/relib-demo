using System;
using Moq;
using ReusableLibrary.Abstractions.Caching;
using ReusableLibrary.Abstractions.Models;
using Xunit;

namespace Tickets.Tests.Infrastructure
{
    public static class MockCacheExtensions
    {
        public static Mock<ICache> SetupCacheGet<TResult>(this Mock<ICache> cacheMock, string key, bool miss = false)
        {
            cacheMock
                .Setup(c => c.Get(It.IsAny<DataKey<TResult>>()))
                .Callback<DataKey<TResult>>(datakey =>
                {
                    Assert.Equal(key, datakey.Key);
                    if (!miss)
                    {
                        datakey.Value = default(TResult);
                    }
                })
                .Returns(true)
                .Verifiable();

            return cacheMock;
        }

        public static Mock<ICache> SetupCacheRemove(this Mock<ICache> cacheMock, string key)
        {
            cacheMock.Setup(c => c.Remove(key)).Verifiable();
            return cacheMock;
        }

        public static Mock<ICache> SetupCacheStore<TResult>(this Mock<ICache> cacheMock, string key)
        {
            cacheMock
                .Setup(c => c.Store(It.IsAny<DataKey<TResult>>()))
                .Callback<DataKey<TResult>>(datakey =>
                {
                    Assert.Equal(key, datakey.Key);
                })
                .Returns(true)
                .Verifiable();

            return cacheMock;
        }

        public static Mock<ICache> SetupCacheStoreExpiresAt<TResult>(this Mock<ICache> cacheMock, string key)
        {
            cacheMock
                .Setup(c => c.Store(It.IsAny<DataKey<TResult>>(), It.IsAny<DateTime>()))
                .Callback<DataKey<TResult>, DateTime>((datakey, expiresAt) =>
                {
                    Assert.Equal(key, datakey.Key);
                    Assert.True(expiresAt > DateTime.Now);
                })
                .Returns(true)
                .Verifiable();

            return cacheMock;
        }

        public static Mock<ICache> SetupCacheStoreValidFor<TResult>(this Mock<ICache> cacheMock, string key)
        {
            cacheMock
                .Setup(c => c.Store(It.IsAny<DataKey<TResult>>(), It.IsAny<TimeSpan>()))
                .Callback<DataKey<TResult>, TimeSpan>((datakey, validFor) =>
                {
                    Assert.Equal(key, datakey.Key);
                    Assert.True(validFor > TimeSpan.Zero);
                })
                .Returns(true)
                .Verifiable();

            return cacheMock;
        }

        public static Mock<ICache> SetupCacheDependencyAdd(this Mock<ICache> cacheMock, string key)
        {
            cacheMock
                .Setup(c => c.Get<Pair<string>>(key))
                .Returns(new Pair<string>(null, null))
                .Verifiable();

            cacheMock
                .Setup(c => c.Store(key, It.IsAny<Pair<string>>(), It.IsAny<DateTime>()))
                .Returns(true)
                .Verifiable();

            return cacheMock;
        }

        public static Mock<ICache> SetupCacheDependencyRemove(this Mock<ICache> cacheMock, string key)
        {
            cacheMock
                .Setup(c => c.Get<Pair<string>>(key))
                .Returns(new Pair<string>("x", null))
                .Verifiable();

            cacheMock
                .Setup(c => c.Remove("x"))
                .Returns(true)
                .Verifiable();

            cacheMock
                .Setup(c => c.Remove(key))
                .Returns(true)
                .Verifiable();

            return cacheMock;
        }
    }
}
