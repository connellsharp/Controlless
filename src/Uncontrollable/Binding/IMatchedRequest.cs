using System;

// TODO remove? Are these not used any more?

namespace Uncontrollable
{
    public interface IMatchedRequest
    {
        bool IsMatched { get; }

        Type RequestType { get; }
        
        object RequestObject { get; }
    }

    public class NoMatchedRequest : IMatchedRequest
    {
        public bool IsMatched => false;

        public Type RequestType => throw new InvalidOperationException("Cannot get RequestType from NoMatchedRequest");

        public object RequestObject => throw new InvalidOperationException("Cannot get RequestObject from NoMatchedRequest");
    }

    public class MatchedRequest : IMatchedRequest
    {
        public MatchedRequest(object requestObject)
        {
            RequestObject = requestObject;
        }

        public bool IsMatched => true;

        public Type RequestType => RequestObject.GetType();

        public object RequestObject { get; }
    }
}