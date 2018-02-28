using System;

namespace JqD.Data.ShareModels
{
    internal class OperationLogs
    {
        public int Id { get; set; }

        public string Operation { get; set; }

        public string OperatorId { get; set; }

        public string ModelType { get; set; }

        public int ModelId { get; set; }

        public string ModelJson { get; set; }

        // ReSharper disable once InconsistentNaming
        public string OperationSQL { get; set; }

        public DateTime OperationTime { get; set; }
    }
}