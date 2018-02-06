using System;
using JqD.Common.Entities;
using JqD.Common.IRepository;
using JqD.Data.CodeSection;
using log4net;
using Newtonsoft.Json;

namespace JqD.Common.Helper
{
    public class OperationLogsHelper
    {
        public static void SaveOperationLogs(IOperationLogsRepository repository, string operation, dynamic model)
        {
            repository.Add(new OperationLogs
            {
                Operation = operation,
                OperatorId = LoginUserSection.CurrentUser.SystemUserId.ToString(),
                ModelId = model.Id,
                ModelJson = JsonConvert.SerializeObject(model),
                ModelType = model.GetType()
            });
        }
    }
    public class LogHelper
    {
        private LogHelper()
        {
        }
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteLog(string info)
        {
            if (Logger.IsInfoEnabled)
            {
                Logger.Info(info);
            }
        }

        public static void WriteLog(string info, Exception e)
        {
            if (Logger.IsErrorEnabled)
            {
                Logger.Error(info, e);
            }
        }
    }
}
