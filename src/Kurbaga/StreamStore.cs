using System;
using System.Collections.Generic;
using System.Text;

namespace Kurbaga
{
    public class StreamStoreOptions
    {

    }

    public class StreamStore
    {
        public IStreamSession OpenSession()
        {
            return null;
        }


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
        void DeleteStream(StreamId id);
    }

    public interface IStreamSession
    {
        void AppendToStream(StreamId id, object[] data);
        void SaveChanges();
        void Cancel();
    }
}
