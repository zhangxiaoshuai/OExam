using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Tools
{
    public class OperationResult
    {
        public EOperationType ResultType { get; set; }
        public string Message { get; set; }
        //public object AppentData { get; set; }
        public int ValueInt { get; set; }
        public OperationResult()
        { }
        public OperationResult(EOperationType otype)
        {
            ResultType = otype;
        }
        public OperationResult(EOperationType otype,string message):this(otype)
        {
            Message = message;
        }
        
    }
    public class OperationResult<T>:OperationResult
    {
        public T AppentData { get; set; }

        public OperationResult() { }

        public OperationResult(EOperationType otype)
        {
            ResultType = otype;
        }
        public OperationResult(EOperationType otype, string message) : this(otype)
        {
            Message = message;
        }
    }
    public enum EOperationType
    {
        Success,
        Unsuccess,
        Error,
        None,
        Exception
    }
}
