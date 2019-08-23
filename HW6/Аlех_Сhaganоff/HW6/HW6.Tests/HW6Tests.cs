using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using HW6.Classes;
using HW6.DataModel;
using HW6.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HW6.Tests
{
    [TestClass]
    public class HW6Tests
    {

        [TestMethod]
        public void ShouldChangeSellerBallance()
        {
            IOutputProvider testoutput = new TestOutputProvider();
            ILogger testLogger = new TestLogger();
            IContextProvider testContext = new TestContext();
            IDataInteraction testDataInteraction = new DataInteraction();
            testDataInteraction.Context = testContext;

            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 1, FirstName = "William", LastName = "Shakespeare", PhoneNumber = "5550001", Balance = 100M });
            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 11, FirstName = "Leo", LastName = "Tolstoy", PhoneNumber = "5550011", Balance = 200M });
            testDataInteraction.Context.Shares.Add(new Share { ShareId = 1, Name = "AAA", Price = 48.06M });
            testDataInteraction.Context.Portfolios.Add(new Portfolio { TraderID = 1, ShareId = 1, Quantity = 10 });

            var testSimulation = new Simulation();

            int sellerId = 1;
            int buyerId = 11;
            int shareId = 1;
            decimal sharePrice = 48.06M;
            int purchaseQuantity = 10;

            decimal oldSellerBallance;
            decimal newSellerBallance;

            oldSellerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == sellerId).Balance;

            testSimulation.UpdateDatabase(testDataInteraction, testoutput, testLogger,
            sellerId, buyerId, shareId, sharePrice, purchaseQuantity);

            newSellerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == sellerId).Balance;

            Assert.IsTrue(newSellerBallance == oldSellerBallance + sharePrice * purchaseQuantity);
        }

        [TestMethod]
        public void ShouldChangebuyerBallance()
        {
            IOutputProvider testoutput = new TestOutputProvider();
            ILogger testLogger = new TestLogger();
            IContextProvider testContext = new TestContext();
            IDataInteraction testDataInteraction = new DataInteraction();
            testDataInteraction.Context = testContext;

            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 1, FirstName = "William", LastName = "Shakespeare", PhoneNumber = "5550001", Balance = 100M });
            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 11, FirstName = "Leo", LastName = "Tolstoy", PhoneNumber = "5550011", Balance = 200M });
            testDataInteraction.Context.Shares.Add(new Share { ShareId = 1, Name = "AAA", Price = 48.06M });
            testDataInteraction.Context.Portfolios.Add(new Portfolio { TraderID = 1, ShareId = 1, Quantity = 10 });

            var testSimulation = new Simulation();

            int sellerId = 1;
            int buyerId = 11;
            int shareId = 1;
            decimal sharePrice = 48.06M;
            int purchaseQuantity = 10;

            decimal oldBuyerBallance;
            decimal newBuyerBallance;

            oldBuyerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == buyerId).Balance;

            testSimulation.UpdateDatabase(testDataInteraction, testoutput, testLogger,
            sellerId, buyerId, shareId, sharePrice, purchaseQuantity);

            newBuyerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == buyerId).Balance;

            Assert.IsTrue(newBuyerBallance == oldBuyerBallance - sharePrice * purchaseQuantity);
        }

        [TestMethod]
        public void ShouldChangeSellerPortfolio()
        {
            IOutputProvider testoutput = new TestOutputProvider();
            ILogger testLogger = new TestLogger();
            IContextProvider testContext = new TestContext();
            IDataInteraction testDataInteraction = new DataInteraction();
            testDataInteraction.Context = testContext;

            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 1, FirstName = "William", LastName = "Shakespeare", PhoneNumber = "5550001", Balance = 100M });
            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 11, FirstName = "Leo", LastName = "Tolstoy", PhoneNumber = "5550011", Balance = 200M });
            testDataInteraction.Context.Shares.Add(new Share { ShareId = 1, Name = "AAA", Price = 48.06M });
            testDataInteraction.Context.Portfolios.Add(new Portfolio { TraderID = 1, ShareId = 1, Quantity = 10 });

            var testSimulation = new Simulation();

            int sellerId = 1;
            int buyerId = 11;
            int shareId = 1;
            decimal sharePrice = 48.06M;
            int purchaseQuantity = 9;

            decimal oldSellerSharesCount;
            decimal newSellerSharesCount;

            oldSellerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == sellerId && p.ShareId == shareId).Quantity;

            testSimulation.UpdateDatabase(testDataInteraction, testoutput, testLogger,
            sellerId, buyerId, shareId, sharePrice, purchaseQuantity);

            newSellerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == sellerId && p.ShareId == shareId).Quantity;

            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == sellerId && p.ShareId == shareId).ToList().Count == 1);

            Assert.IsTrue(newSellerSharesCount == oldSellerSharesCount - purchaseQuantity);
        }

        [TestMethod]
        public void ShouldRemoveShareRecordFromSellerPortfolioIfQuantityBecomesZero()
        {
            IOutputProvider testoutput = new TestOutputProvider();
            ILogger testLogger = new TestLogger();
            IContextProvider testContext = new TestContext();
            IDataInteraction testDataInteraction = new DataInteraction();
            testDataInteraction.Context = testContext;

            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 1, FirstName = "William", LastName = "Shakespeare", PhoneNumber = "5550001", Balance = 100M });
            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 11, FirstName = "Leo", LastName = "Tolstoy", PhoneNumber = "5550011", Balance = 200M });
            testDataInteraction.Context.Shares.Add(new Share { ShareId = 1, Name = "AAA", Price = 48.06M });
            testDataInteraction.Context.Portfolios.Add(new Portfolio { TraderID = 1, ShareId = 1, Quantity = 10 });

            var testSimulation = new Simulation();

            int sellerId = 1;
            int buyerId = 11;
            int shareId = 1;
            decimal sharePrice = 48.06M;
            int purchaseQuantity = 10;

            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == sellerId && p.ShareId == shareId).ToList().Count == 1);

            testSimulation.UpdateDatabase(testDataInteraction, testoutput, testLogger,
            sellerId, buyerId, shareId, sharePrice, purchaseQuantity);

            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == sellerId && p.ShareId == shareId).ToList().Count == 0);
        }

        [TestMethod]
        public void ShouldChangeBuyerPortfolio()
        {
            IOutputProvider testoutput = new TestOutputProvider();
            ILogger testLogger = new TestLogger();
            IContextProvider testContext = new TestContext();
            IDataInteraction testDataInteraction = new DataInteraction();
            testDataInteraction.Context = testContext;

            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 1, FirstName = "William", LastName = "Shakespeare", PhoneNumber = "5550001", Balance = 100M });
            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 11, FirstName = "Leo", LastName = "Tolstoy", PhoneNumber = "5550011", Balance = 200M });
            testDataInteraction.Context.Shares.Add(new Share { ShareId = 1, Name = "AAA", Price = 48.06M });
            testDataInteraction.Context.Portfolios.Add(new Portfolio { TraderID = 1, ShareId = 1, Quantity = 10 });
            testDataInteraction.Context.Portfolios.Add(new Portfolio { TraderID = 11, ShareId = 1, Quantity = 10 });

            var testSimulation = new Simulation();

            int sellerId = 1;
            int buyerId = 11;
            int shareId = 1;
            decimal sharePrice = 48.06M;
            int purchaseQuantity = 9;

            decimal oldBuyerSharesCount;
            decimal newBuyerSharesCount;

            oldBuyerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == buyerId && p.ShareId == shareId).Quantity;

            testSimulation.UpdateDatabase(testDataInteraction, testoutput, testLogger,
            sellerId, buyerId, shareId, sharePrice, purchaseQuantity);

            newBuyerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == buyerId && p.ShareId == shareId).Quantity;

            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count == 1);

            Assert.IsTrue(newBuyerSharesCount == oldBuyerSharesCount + purchaseQuantity);
        }

        [TestMethod]
        public void ShouldAddShareRecordToBuyerPortfolioIfNewShareTypeIsAdded()
        {
            IOutputProvider testoutput = new TestOutputProvider();
            ILogger testLogger = new TestLogger();
            IContextProvider testContext = new TestContext();
            IDataInteraction testDataInteraction = new DataInteraction();
            testDataInteraction.Context = testContext;

            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 1, FirstName = "William", LastName = "Shakespeare", PhoneNumber = "5550001", Balance = 100M });
            testDataInteraction.Context.Traders.Add(new Trader { TraderId = 11, FirstName = "Leo", LastName = "Tolstoy", PhoneNumber = "5550011", Balance = 200M });
            testDataInteraction.Context.Shares.Add(new Share { ShareId = 1, Name = "AAA", Price = 48.06M });
            testDataInteraction.Context.Portfolios.Add(new Portfolio { TraderID = 1, ShareId = 1, Quantity = 10 });

            var testSimulation = new Simulation();

            int sellerId = 1;
            int buyerId = 11;
            int shareId = 1;
            decimal sharePrice = 48.06M;
            int purchaseQuantity = 10;

            int newBuyerSharesCount;

            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count == 0);

            testSimulation.UpdateDatabase(testDataInteraction, testoutput, testLogger,
            sellerId, buyerId, shareId, sharePrice, purchaseQuantity);

            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count == 1);

            newBuyerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == buyerId && p.ShareId == shareId).Quantity;

            Assert.IsTrue(newBuyerSharesCount == purchaseQuantity);
        }

        internal class TestContext : IContextProvider
        {
            public DbSet<Trader> Traders { get; set; }
            public DbSet<Share> Shares { get; set; }
            public DbSet<Transaction> Transactions { get; set; }
            public DbSet<Portfolio> Portfolios { get; set; }

            public TestContext()
            {
                this.Traders = new TestDbSet<Trader>();
                this.Shares = new TestDbSet<Share>();
                this.Transactions = new TestDbSet<Transaction>();
                this.Portfolios = new TestDbSet<Portfolio>();
            }
                      
            public void Dispose()
            {
                
            }

            public int SaveChanges()
            {
                return 1;
            }
        }

        internal class TestOutputProvider : IOutputProvider
        {
            public void WriteLine(string text)
            {
                System.Console.WriteLine(text);
            }
        }

        internal class TestLogger : ILogger
        {
            public void Write(string text)
            {
                
            }
        }

        public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
       where TEntity : class
        {
            ObservableCollection<TEntity> _data;
            IQueryable _query;

            public TestDbSet()
            {
                _data = new ObservableCollection<TEntity>();
                _query = _data.AsQueryable();
            }

            public override TEntity Add(TEntity item)
            {
                _data.Add(item);
                return item;
            }

            public override TEntity Remove(TEntity item)
            {
                _data.Remove(item);
                return item;
            }

            public override TEntity Attach(TEntity item)
            {
                _data.Add(item);
                return item;
            }

            public override TEntity Create()
            {
                return Activator.CreateInstance<TEntity>();
            }

            public override TDerivedEntity Create<TDerivedEntity>()
            {
                return Activator.CreateInstance<TDerivedEntity>();
            }

            public override ObservableCollection<TEntity> Local
            {
                get { return _data; }
            }

            Type IQueryable.ElementType
            {
                get { return _query.ElementType; }
            }

            Expression IQueryable.Expression
            {
                get { return _query.Expression; }
            }

            IQueryProvider IQueryable.Provider
            {
                get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
            {
                return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
            }
        }

        internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestDbAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestDbAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestDbAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute(expression));
            }

            public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute<TResult>(expression));
            }
        }

        internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
        {
            public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestDbAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IDbAsyncEnumerator<T> GetAsyncEnumerator()
            {
                return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }

            IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            {
                return GetAsyncEnumerator();
            }

            IQueryProvider IQueryable.Provider
            {
                get { return new TestDbAsyncQueryProvider<T>(this); }
            }
        }

        internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestDbAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public void Dispose()
            {
                _inner.Dispose();
            }

            public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_inner.MoveNext());
            }

            public T Current
            {
                get { return _inner.Current; }
            }

            object IDbAsyncEnumerator.Current
            {
                get { return Current; }
            }
        }

        //[TestMethod]
        //public void SimulationTest()
        //{
        //    IOutputProvider testoutput = new TestOutputProvider();
        //    ILogger testLogger = new TestLogger();
        //    IContextProvider testContext = new TestContext();
        //    IDataInteraction testDataInteraction = new DataInteraction();
        //    testDataInteraction.Context = testContext;

        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 1, FirstName = "William", LastName = "Shakespeare", PhoneNumber = "5550001" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 2, FirstName = "Agatha", LastName = "Christie", PhoneNumber = "5550002" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 3, FirstName = "Barbara", LastName = "Cartland", PhoneNumber = "5550003" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 4, FirstName = "Danielle", LastName = "Steel", PhoneNumber = "5550004" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 5, FirstName = "Harold", LastName = "Robbins", PhoneNumber = "5550005" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 6, FirstName = "Georges", LastName = "Simenon", PhoneNumber = "5550006" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 7, FirstName = "Enid", LastName = "Blyton", PhoneNumber = "5550007" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 8, FirstName = "Sidney", LastName = "Sheldon", PhoneNumber = "5550008" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 9, FirstName = "Gilbert", LastName = "Patten", PhoneNumber = "5550009" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 10, FirstName = "Eiichiro", LastName = "Oda", PhoneNumber = "5550010" });

        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 11, FirstName = "Leo", LastName = "Tolstoy", PhoneNumber = "5550011" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 12, FirstName = "Corin", LastName = "Tellado", PhoneNumber = "5550012" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 13, FirstName = "Jackie", LastName = "Collins", PhoneNumber = "5550013" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 14, FirstName = "Horatio", LastName = "Alger", PhoneNumber = "5550014" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 15, FirstName = "Dean", LastName = "Koontz", PhoneNumber = "5550015" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 16, FirstName = "Nora", LastName = "Roberts", PhoneNumber = "5550016" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 17, FirstName = "Akira", LastName = "Toriyama", PhoneNumber = "5550017" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 18, FirstName = "Stephen", LastName = "King", PhoneNumber = "5550018" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 19, FirstName = "Paulo", LastName = "Coelho", PhoneNumber = "5550019" });
        //    testDataInteraction.Context.Traders.Add(new Trader { TraderId = 20, FirstName = "Jeffrey", LastName = "Archer", PhoneNumber = "5550020" });

        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 1, Name = "AAA", Price = 48.06M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 2, Name = "BBB", Price = 294.83M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 3, Name = "CCC", Price = 34.19M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 4, Name = "DDD", Price = 183.33M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 5, Name = "EEE", Price = 109.98M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 6, Name = "FFF", Price = 1807.58M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 7, Name = "GGG", Price = 196.25M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 8, Name = "HHH", Price = 28.78M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 9, Name = "III", Price = 110.66M });
        //    testDataInteraction.Context.Shares.Add(new Share { ShareId = 10, Name = "JJJ", Price = 200.99M });

        //    var testRandomizer = new Randomizer();
        //    testRandomizer.Randomize(testDataInteraction.Context, 20, 10);

        //    for(int i = 0; i < 100; i++)
        //    {
        //        var testSimulation = new Simulation();

        //        var result = testSimulation.PerformRandomOperation(testDataInteraction, testoutput, testLogger);

        //        int sellerId = result.sellerId;
        //        int buyerId = result.buyerId;
        //        int shareId = result.shareId;
        //        decimal sharePrice = result.sharePrice;
        //        int purchaseQuantity = result.purchaseQuantity;

        //        decimal oldSellerBallance;
        //        decimal oldBuyerBallance;
        //        int oldSellerSharesCount;
        //        int oldBuyerSharesCount;

        //        decimal newSellerBallance;
        //        decimal newBuyerBallance;
        //        int newSellerSharesCount;
        //        int newBuyerSharesCount;

        //        Assert.IsTrue(testDataInteraction.Context.Traders.Where(t => t.TraderId == sellerId).ToList().Count == 1);
        //        oldSellerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == sellerId).Balance;

        //        Assert.IsTrue(testDataInteraction.Context.Traders.Where(t => t.TraderId == buyerId).ToList().Count == 1);
        //        oldBuyerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == buyerId).Balance;

        //        Assert.IsTrue(testDataInteraction.Context.Shares.Where(s => s.ShareId == shareId).ToList().Count == 1);

        //        Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == sellerId && p.ShareId == shareId).ToList().Count == 1);
        //        oldSellerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == sellerId && p.ShareId == shareId).Quantity;

        //        Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count == 1 ||
        //        testDataInteraction.Context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count == 0);

        //        if (testDataInteraction.Context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count == 1)
        //        {
        //            oldBuyerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == buyerId && p.ShareId == shareId).Quantity;
        //        }
        //        else
        //        {
        //            oldBuyerSharesCount = 0;
        //        }

        //        testSimulation.UpdateDatabase(testDataInteraction, testoutput, testLogger,
        //        result.sellerId, result.buyerId, result.shareId, result.sharePrice, result.purchaseQuantity);

        //        newSellerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == sellerId).Balance;
        //        Assert.IsTrue(newSellerBallance == oldSellerBallance + sharePrice * purchaseQuantity);

        //        newBuyerBallance = testDataInteraction.Context.Traders.Single(t => t.TraderId == buyerId).Balance;
        //        Assert.IsTrue(newBuyerBallance == oldBuyerBallance - sharePrice * purchaseQuantity);

        //        if (oldSellerSharesCount == purchaseQuantity)
        //        {
        //            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == sellerId && p.ShareId == shareId).ToList().Count == 0);
        //        }
        //        else
        //        {
        //            Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == sellerId && p.ShareId == shareId).ToList().Count == 1);

        //            newSellerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == sellerId && p.ShareId == shareId).Quantity;

        //            Assert.IsTrue(newSellerSharesCount == oldSellerSharesCount - purchaseQuantity);
        //        }

        //        Assert.IsTrue(testDataInteraction.Context.Portfolios.Where(p => p.TraderID == buyerId && p.ShareId == shareId).ToList().Count == 1);
        //        newBuyerSharesCount = testDataInteraction.Context.Portfolios.Single(p => p.TraderID == buyerId && p.ShareId == shareId).Quantity;

        //        if (oldBuyerSharesCount == 0)
        //        {
        //            Assert.IsTrue(newBuyerSharesCount == purchaseQuantity);
        //        }
        //        else
        //        {
        //            Assert.IsTrue(newBuyerSharesCount == oldBuyerSharesCount + purchaseQuantity);
        //        }
        //    }
        //}
    }
}

