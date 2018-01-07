using System;
using System.Collections.Generic;
using System.Text;

namespace Kurbaga.Impl
{


    public class InternalStreamSession
        : IStreamSession
    {
        public IAdvancedStreamOperations Advanced => throw new NotImplementedException();

        public void AppendToStream(StreamId id, object[] data)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
