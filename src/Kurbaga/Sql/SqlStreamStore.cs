using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Kurbaga.Sql
{
    public class SqlStreamStoreOptions
    {
        public string ConnectionString { get; set; }
    }

    public interface IDatabaseSession
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Complete();
        IDbCommand CreateCommand();
    }

    public interface IDatabaseSessionProvider
    {
        Task<IDatabaseSession> OpenSession();
    }

    public class SqlStreamStore
        : StreamStoreBase
    {
        private IDatabaseSessionProvider _databaseSessionProvider = null;
        public SqlStreamStore()
        {

        }

        public override async Task<IStreamSession> OpenSession()
        {
            var session = new SqlStreamSession(this, _databaseSessionProvider);
            await session.Init().ConfigureAwait(false);
            return session;

        }

        protected override void InitializeCore()
        {
            throw new NotImplementedException();
        }
    }

    internal class SqlStreamSession
        : IStreamSession
    {
        private readonly IDatabaseSessionProvider _databaseSessionProvider;
        private IDatabaseSession _databaseSession;
        internal readonly SqlStreamStore SqlStreamStore;
        private SqlAdvancedStreamOperations _sqlAdvancedStreamOperations;
        public SqlStreamSession(SqlStreamStore sqlStreamStore, IDatabaseSessionProvider databaseSessionProvider)
        {
            _databaseSessionProvider = databaseSessionProvider ?? throw new ArgumentNullException(nameof(databaseSessionProvider));
            SqlStreamStore = sqlStreamStore ?? throw new ArgumentNullException(nameof(sqlStreamStore));
        }

        internal async Task Init()
        {
            _sqlAdvancedStreamOperations = new SqlAdvancedStreamOperations(this);
            _databaseSession = await _databaseSessionProvider.OpenSession().ConfigureAwait(false);
        }

        public IAdvancedStreamOperations Advanced => _sqlAdvancedStreamOperations;

        public void AppendToStream(StreamId id, object[] data)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _databaseSession.Complete();
        }

        public void Cancel()
        {
            // _databaseSession.Cancel()
        }
    }

    internal class SqlAdvancedStreamOperations
        : IAdvancedStreamOperations
    {
        private readonly SqlStreamSession _session;

        public SqlAdvancedStreamOperations(SqlStreamSession session)
        {
            _session = session;
        }

        public Task DeleteStream(StreamId id)
        {
            throw new NotImplementedException();
        }
    }
}
