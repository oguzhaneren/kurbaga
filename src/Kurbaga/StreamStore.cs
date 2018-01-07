using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kurbaga
{
    public class StreamStoreOptions
    {

    }

    public abstract class StreamStoreBase
    {
        public abstract Task<IStreamSession> OpenSession();

        protected virtual void Initialize()
        {
            // some init

        }

        protected abstract void InitializeCore();
    }


    public class StreamId
    {
        private readonly string _value;

        public StreamId(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString()
        {
            return _value;
        }
    }

    public interface IAdvancedStreamOperations
    {
        Task DeleteStream(StreamId id);
    }

    public interface IStreamSession
    {
        IAdvancedStreamOperations Advanced { get; }
        void AppendToStream(StreamId id, object[] data);
        void SaveChanges();
        void Cancel();
    }
}
