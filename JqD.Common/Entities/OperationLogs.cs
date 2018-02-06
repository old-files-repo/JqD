using System;

namespace JqD.Common.Entities
{
    public class OperationLogs : Entity
    {
        public string Operation { get; set; }
        public string OperatorId { get; set; }
        public string ModelType { get; set; }
        public int ModelId { get; set; }
        public string ModelJson { get; set; }
        public DateTime OperationTime { get; set; }
    }
}